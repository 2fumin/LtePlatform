using System;
using System.IO;
using System.Security.Cryptography;

namespace Lte.Domain.Lz4Net.Encryption
{
    public class ZipAESStream : CryptoStream
    {
        private readonly int _blockAndAuth;
        private readonly byte[] _slideBuffer;
        private int _slideBufFreePos;
        private int _slideBufStartPos;
        private readonly Stream _stream;
        private readonly ZipAESTransform _transform;
        private const int AUTH_CODE_LENGTH = 10;
        private const int CRYPTO_BLOCK_SIZE = 0x10;

        public ZipAESStream(Stream stream, ZipAESTransform transform, CryptoStreamMode mode)
            : base(stream, transform, mode)
        {
            _stream = stream;
            _transform = transform;
            _slideBuffer = new byte[0x400];
            _blockAndAuth = 0x1a;
            if (mode != CryptoStreamMode.Read)
            {
                throw new Exception("ZipAESStream only for read");
            }
        }

        public override int Read(byte[] outBuffer, int offset, int count)
        {
            int num = 0;
            while (num < count)
            {
                int num2 = _slideBufFreePos - _slideBufStartPos;
                int num3 = _blockAndAuth - num2;
                if ((_slideBuffer.Length - _slideBufFreePos) < num3)
                {
                    int index = 0;
                    int num5 = _slideBufStartPos;
                    while (num5 < _slideBufFreePos)
                    {
                        _slideBuffer[index] = _slideBuffer[num5];
                        num5++;
                        index++;
                    }
                    _slideBufFreePos -= _slideBufStartPos;
                    _slideBufStartPos = 0;
                }
                int num6 = _stream.Read(_slideBuffer, _slideBufFreePos, num3);
                _slideBufFreePos += num6;
                num2 = _slideBufFreePos - _slideBufStartPos;
                if (num2 >= _blockAndAuth)
                {
                    _transform.TransformBlock(_slideBuffer, _slideBufStartPos, CRYPTO_BLOCK_SIZE, 
                        outBuffer, offset);
                    num += CRYPTO_BLOCK_SIZE;
                    offset += CRYPTO_BLOCK_SIZE;
                    _slideBufStartPos += CRYPTO_BLOCK_SIZE;
                }
                else
                {
                    if (num2 > AUTH_CODE_LENGTH)
                    {
                        int inputCount = num2 - AUTH_CODE_LENGTH;
                        _transform.TransformBlock(_slideBuffer, _slideBufStartPos, inputCount, outBuffer, offset);
                        num += inputCount;
                        _slideBufStartPos += inputCount;
                    }
                    else if (num2 < AUTH_CODE_LENGTH)
                    {
                        throw new Exception("Internal error missed auth code");
                    }
                    byte[] authCode = _transform.GetAuthCode();
                    for (int i = 0; i < AUTH_CODE_LENGTH; i++)
                    {
                        if (authCode[i] != _slideBuffer[_slideBufStartPos + i])
                        {
                            throw new Exception("AES Authentication Code does not match. This is a super-CRC check on the data in the file after compression and encryption. \r\nThe file may be damaged.");
                        }
                    }
                    return num;
                }
            }
            return num;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}


