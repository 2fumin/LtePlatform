using System;
using ZipLib.CheckSums;

namespace ZipLib.Encryption
{
    internal class PkzipClassicCryptoBase
    {
        private uint[] keys;

        protected void Reset()
        {
            keys[0] = 0;
            keys[1] = 0;
            keys[2] = 0;
        }

        protected void SetKeys(byte[] keyData)
        {
            if (keyData == null)
            {
                throw new ArgumentNullException(nameof(keyData));
            }
            if (keyData.Length != 12)
            {
                throw new InvalidOperationException("Key length is not valid");
            }
            keys = new[] 
            { 
                (((uint)(keyData[3] << 0x18) | (uint)(keyData[2] << 0x10)) | (uint)(keyData[1] << 8)) | keyData[0], 
                (((uint)(keyData[7] << 0x18) | (uint)(keyData[6] << 0x10)) | (uint)(keyData[5] << 8)) | keyData[4], 
                (((uint)(keyData[11] << 0x18) | (uint)(keyData[10] << 0x10)) | (uint)(keyData[9] << 8)) | keyData[8] 
            };
        }

        protected byte TransformByte()
        {
            uint num = (keys[2] & 0xffff) | 2;
            return (byte)((num * (num ^ 1)) >> 8);
        }

        protected void UpdateKeys(byte ch)
        {
            keys[0] = Crc32.ComputeCrc32(keys[0], ch);
            keys[1] += (byte)keys[0];
            keys[1] = (keys[1] * 0x8088405) + 1;
            keys[2] = Crc32.ComputeCrc32(keys[2], (byte)(keys[1] >> 0x18));
        }
    }
}

