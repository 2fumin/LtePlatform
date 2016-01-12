using System;
using System.IO;
using System.Security.Cryptography;
using Lte.Domain.Lz4Net.Encryption;
using ZipLib.Comppression;
using ZipLib.Encryption;
using ZipLib.Zip;

namespace ZipLib.Streams
{
    public class DeflaterOutputStream : Stream
    {
        private static RNGCryptoServiceProvider _aesRnd;
        protected byte[] AesAuthCode;
        protected readonly Stream BaseOutputStream;
        private readonly byte[] _buffer;
        private ICryptoTransform _cryptoTransform;
        protected readonly Deflater Deflater;
        private bool _isClosed;
        private bool _isStreamOwner;
        private string _password;

        public DeflaterOutputStream(Stream baseOutputStream)
            : this(baseOutputStream, new Deflater())
        {
        }

        public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater, int bufferSize = 0x200)
        {
            _isStreamOwner = true;
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
            BaseOutputStream = baseOutputStream;
            _buffer = new byte[bufferSize];
            Deflater = deflater;
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
            if (!_isClosed)
            {
                _isClosed = true;
                try
                {
                    Finish();
                    if (_cryptoTransform != null)
                    {
                        GetAuthCodeIfAes();
                        _cryptoTransform.Dispose();
                        _cryptoTransform = null;
                    }
                }
                finally
                {
                    if (_isStreamOwner)
                    {
                        BaseOutputStream.Close();
                    }
                }
            }
        }

        protected void Deflate()
        {
            while (!Deflater.IsNeedingInput)
            {
                int length = Deflater.Deflate(_buffer, 0, _buffer.Length);
                if (length <= 0)
                {
                    break;
                }
                if (_cryptoTransform != null)
                {
                    EncryptBlock(_buffer, 0, length);
                }
                BaseOutputStream.Write(_buffer, 0, length);
            }
            if (!Deflater.IsNeedingInput)
            {
                throw new SharpZipBaseException("DeflaterOutputStream can't deflate all input?");
            }
        }

        protected void EncryptBlock(byte[] buffer, int offset, int length)
        {
            _cryptoTransform.TransformBlock(buffer, 0, length, buffer, 0);
        }

        public virtual void Finish()
        {
            Deflater.Finish();
            while (!Deflater.IsFinished)
            {
                int length = Deflater.Deflate(_buffer, 0, _buffer.Length);
                if (length <= 0)
                {
                    break;
                }
                if (_cryptoTransform != null)
                {
                    EncryptBlock(_buffer, 0, length);
                }
                BaseOutputStream.Write(_buffer, 0, length);
            }
            if (!Deflater.IsFinished)
            {
                throw new SharpZipBaseException("Can't deflate all input?");
            }
            BaseOutputStream.Flush();
            if (_cryptoTransform != null)
            {
                ZipAESTransform transform = _cryptoTransform as ZipAESTransform;
                if (transform != null)
                {
                    AesAuthCode = transform.GetAuthCode();
                }
                _cryptoTransform.Dispose();
                _cryptoTransform = null;
            }
        }

        public override void Flush()
        {
            Deflater.Flush();
            Deflate();
            BaseOutputStream.Flush();
        }

        private void GetAuthCodeIfAes()
        {
            if (_cryptoTransform is ZipAESTransform)
            {
                AesAuthCode = ((ZipAESTransform)_cryptoTransform).GetAuthCode();
            }
        }

        protected void InitializeAesPassword(ZipEntry entry, string rawPassword, out byte[] salt, out byte[] pwdVerifier)
        {
            salt = new byte[entry.AesSaltLen];
            if (_aesRnd == null)
            {
                _aesRnd = new RNGCryptoServiceProvider();
            }
            _aesRnd.GetBytes(salt);
            int blockSize = entry.AesKeySize / 8;
            _cryptoTransform = new ZipAESTransform(rawPassword, salt, blockSize, true);
            pwdVerifier = ((ZipAESTransform)_cryptoTransform).PwdVerifier;
        }

        protected void InitializePassword(string pwd)
        {
            PkzipClassicManaged managed = new PkzipClassicManaged();
            byte[] rgbKey = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(pwd));
            _cryptoTransform = managed.CreateEncryptor(rgbKey, null);
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
            Deflater.SetInput(buffer, off, count);
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
                return BaseOutputStream.CanSeek;
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
                return BaseOutputStream.CanWrite;
            }
        }

        public bool IsStreamOwner
        {
            get
            {
                return _isStreamOwner;
            }
            set
            {
                _isStreamOwner = value;
            }
        }

        public override long Length
        {
            get
            {
                return BaseOutputStream.Length;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if ((value != null) && (value.Length == 0))
                {
                    _password = null;
                }
                else
                {
                    _password = value;
                }
            }
        }

        public override long Position
        {
            get
            {
                return BaseOutputStream.Position;
            }
            set
            {
                throw new NotSupportedException("Position property not supported");
            }
        }
    }
}


