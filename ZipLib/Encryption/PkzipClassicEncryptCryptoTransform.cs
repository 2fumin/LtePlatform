using System;
using System.Security.Cryptography;

namespace Lte.Domain.Lz4Net.Encryption
{
    internal class PkzipClassicEncryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
    {
        internal PkzipClassicEncryptCryptoTransform(byte[] keyBlock)
        {
            SetKeys(keyBlock);
        }

        public void Dispose()
        {
            Reset();
        }

        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            for (int i = inputOffset; i < (inputOffset + inputCount); i++)
            {
                byte ch = inputBuffer[i];
                outputBuffer[outputOffset++] = (byte)(inputBuffer[i] ^ TransformByte());
                UpdateKeys(ch);
            }
            return inputCount;
        }

        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            byte[] outputBuffer = new byte[inputCount];
            TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, 0);
            return outputBuffer;
        }

        public bool CanReuseTransform
        {
            get
            {
                return true;
            }
        }

        public bool CanTransformMultipleBlocks
        {
            get
            {
                return true;
            }
        }

        public int InputBlockSize
        {
            get
            {
                return 1;
            }
        }

        public int OutputBlockSize
        {
            get
            {
                return 1;
            }
        }
    }
}

