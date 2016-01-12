using System;
using System.Security.Cryptography;

namespace Lte.Domain.Lz4Net.Encryption
{
    public class ZipAESTransform : ICryptoTransform, IDisposable
    {
        private readonly int _blockSize;
        private readonly byte[] _counterNonce;
        private int _encrPos;
        private readonly byte[] _encryptBuffer;
        private readonly ICryptoTransform _encryptor;
        private bool _finalised;
        private readonly HMACSHA1 _hmacsha1;
        private readonly bool _writeMode;
        private const int ENCRYPT_BLOCK = 0x10;
        private const int KEY_ROUNDS = 0x3e8;
        private const int PWD_VER_LENGTH = 2;

        public ZipAESTransform(string key, byte[] saltBytes, int blockSize, bool writeMode)
        {
            if ((blockSize != ENCRYPT_BLOCK) && (blockSize != 0x20))
            {
                throw new Exception("Invalid blocksize " + blockSize + ". Must be 16 or 32.");
            }
            if (saltBytes.Length != (blockSize / PWD_VER_LENGTH))
            {
                throw new Exception(string.Concat(new object[] { "Invalid salt len. Must be ", blockSize / 2, " for blocksize ", blockSize }));
            }
            _blockSize = blockSize;
            _encryptBuffer = new byte[_blockSize];
            _encrPos = ENCRYPT_BLOCK;
            Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(key, saltBytes, KEY_ROUNDS);
            RijndaelManaged managed = new RijndaelManaged
            {
                Mode = CipherMode.ECB
            };
            _counterNonce = new byte[_blockSize];
            byte[] rgbKey = bytes.GetBytes(_blockSize);
            byte[] rgbIV = bytes.GetBytes(_blockSize);
            _encryptor = managed.CreateEncryptor(rgbKey, rgbIV);
            PwdVerifier = bytes.GetBytes(2);
            _hmacsha1 = new HMACSHA1(rgbIV);
            _writeMode = writeMode;
        }

        public void Dispose()
        {
            _encryptor.Dispose();
        }

        public byte[] GetAuthCode()
        {
            if (!_finalised)
            {
                byte[] inputBuffer = new byte[0];
                _hmacsha1.TransformFinalBlock(inputBuffer, 0, 0);
                _finalised = true;
            }
            return _hmacsha1.Hash;
        }

        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            if (!_writeMode)
            {
                _hmacsha1.TransformBlock(inputBuffer, inputOffset, inputCount, inputBuffer, inputOffset);
            }
            for (int i = 0; i < inputCount; i++)
            {
                if (_encrPos == ENCRYPT_BLOCK)
                {
                    for (int j = 0; (_counterNonce[j] = (byte)(_counterNonce[j] + 1)) == 0; j++)
                    {
                    }
                    _encryptor.TransformBlock(_counterNonce, 0, _blockSize, _encryptBuffer, 0);
                    _encrPos = 0;
                }
                outputBuffer[i + outputOffset] = (byte)(inputBuffer[i + inputOffset] ^ _encryptBuffer[_encrPos++]);
            }
            if (_writeMode)
            {
                _hmacsha1.TransformBlock(outputBuffer, outputOffset, inputCount, outputBuffer, outputOffset);
            }
            return inputCount;
        }

        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            throw new NotImplementedException("ZipAESTransform.TransformFinalBlock");
        }

        public bool CanReuseTransform => true;

        public bool CanTransformMultipleBlocks => true;

        public int InputBlockSize => _blockSize;

        public int OutputBlockSize => _blockSize;

        public byte[] PwdVerifier { get; }
    }
}
