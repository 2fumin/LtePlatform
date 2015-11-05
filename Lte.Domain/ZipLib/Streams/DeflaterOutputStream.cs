using System;
using System.IO;
using System.Security.Cryptography;
using Lte.Domain.Lz4Net.Encryption;
using Lte.Domain.ZipLib.Compression;
using Lte.Domain.ZipLib.Zip;

namespace Lte.Domain.ZipLib.Streams
{
    public class DeflaterOutputStream : Stream
    {
        private static RNGCryptoServiceProvider _aesRnd;
        protected byte[] AESAuthCode;
        protected Stream baseOutputStream_;
        private byte[] buffer_;
        private ICryptoTransform cryptoTransform_;
        protected Deflater deflater_;
        private bool isClosed_;
        private bool isStreamOwner_;
        private string password;

        public DeflaterOutputStream(Stream baseOutputStream)
            : this(baseOutputStream, new Deflater())
        {
        }

        public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater, int bufferSize = 0x200)
        {
            isStreamOwner_ = true;
            if (baseOutputStream == null)
            {
                throw new ArgumentNullException("baseOutputStream");
            }
            if (!baseOutputStream.CanWrite)
            {
                throw new ArgumentException("Must support writing", "baseOutputStream");
            }
            if (deflater == null)
            {
                throw new ArgumentNullException("deflater");
            }
            if (bufferSize < 0x200)
            {
                throw new ArgumentOutOfRangeException("bufferSize");
            }
            baseOutputStream_ = baseOutputStream;
            buffer_ = new byte[bufferSize];
            deflater_ = deflater;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            throw new NotSupportedException("DeflaterOutputStream BeginRead not currently supported");
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            throw new NotSupportedException("BeginWrite is not supported");
        }

        public override void Close()
        {
            if (!isClosed_)
            {
                isClosed_ = true;
                try
                {
                    Finish();
                    if (cryptoTransform_ != null)
                    {
                        GetAuthCodeIfAES();
                        cryptoTransform_.Dispose();
                        cryptoTransform_ = null;
                    }
                }
                finally
                {
                    if (isStreamOwner_)
                    {
                        baseOutputStream_.Close();
                    }
                }
            }
        }

        protected void Deflate()
        {
            while (!deflater_.IsNeedingInput)
            {
                int length = deflater_.Deflate(buffer_, 0, buffer_.Length);
                if (length <= 0)
                {
                    break;
                }
                if (cryptoTransform_ != null)
                {
                    EncryptBlock(buffer_, 0, length);
                }
                baseOutputStream_.Write(buffer_, 0, length);
            }
            if (!deflater_.IsNeedingInput)
            {
                throw new SharpZipBaseException("DeflaterOutputStream can't deflate all input?");
            }
        }

        protected void EncryptBlock(byte[] buffer, int offset, int length)
        {
            cryptoTransform_.TransformBlock(buffer, 0, length, buffer, 0);
        }

        public virtual void Finish()
        {
            deflater_.Finish();
            while (!deflater_.IsFinished)
            {
                int length = deflater_.Deflate(buffer_, 0, buffer_.Length);
                if (length <= 0)
                {
                    break;
                }
                if (cryptoTransform_ != null)
                {
                    EncryptBlock(buffer_, 0, length);
                }
                baseOutputStream_.Write(buffer_, 0, length);
            }
            if (!deflater_.IsFinished)
            {
                throw new SharpZipBaseException("Can't deflate all input?");
            }
            baseOutputStream_.Flush();
            if (cryptoTransform_ != null)
            {
                ZipAESTransform transform = cryptoTransform_ as ZipAESTransform;
                if (transform != null)
                {
                    AESAuthCode = transform.GetAuthCode();
                }
                cryptoTransform_.Dispose();
                cryptoTransform_ = null;
            }
        }

        public override void Flush()
        {
            deflater_.Flush();
            Deflate();
            baseOutputStream_.Flush();
        }

        private void GetAuthCodeIfAES()
        {
            if (cryptoTransform_ is ZipAESTransform)
            {
                AESAuthCode = ((ZipAESTransform)cryptoTransform_).GetAuthCode();
            }
        }

        protected void InitializeAESPassword(ZipEntry entry, string rawPassword, out byte[] salt, out byte[] pwdVerifier)
        {
            salt = new byte[entry.AESSaltLen];
            if (_aesRnd == null)
            {
                _aesRnd = new RNGCryptoServiceProvider();
            }
            _aesRnd.GetBytes(salt);
            int blockSize = entry.AESKeySize / 8;
            cryptoTransform_ = new ZipAESTransform(rawPassword, salt, blockSize, true);
            pwdVerifier = ((ZipAESTransform)cryptoTransform_).PwdVerifier;
        }

        protected void InitializePassword(string pwd)
        {
            PkzipClassicManaged managed = new PkzipClassicManaged();
            byte[] rgbKey = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(pwd));
            cryptoTransform_ = managed.CreateEncryptor(rgbKey, null);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("DeflaterOutputStream Read not supported");
        }

        public override int ReadByte()
        {
            throw new NotSupportedException("DeflaterOutputStream ReadByte not supported");
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("DeflaterOutputStream Seek not supported");
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("DeflaterOutputStream SetLength not supported");
        }

        public override void Write(byte[] buffer, int off, int count)
        {
            deflater_.SetInput(buffer, off, count);
            Deflate();
        }

        public override void WriteByte(byte value)
        {
            byte[] buffer = { value };
            Write(buffer, 0, 1);
        }

        public bool CanPatchEntries
        {
            get
            {
                return baseOutputStream_.CanSeek;
            }
        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return baseOutputStream_.CanWrite;
            }
        }

        public bool IsStreamOwner
        {
            get
            {
                return isStreamOwner_;
            }
            set
            {
                isStreamOwner_ = value;
            }
        }

        public override long Length
        {
            get
            {
                return baseOutputStream_.Length;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if ((value != null) && (value.Length == 0))
                {
                    password = null;
                }
                else
                {
                    password = value;
                }
            }
        }

        public override long Position
        {
            get
            {
                return baseOutputStream_.Position;
            }
            set
            {
                throw new NotSupportedException("Position property not supported");
            }
        }
    }
}


