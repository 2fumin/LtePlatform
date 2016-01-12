using System;
using System.Security.Cryptography;
using ZipLib.CheckSums;

namespace ZipLib.Encryption
{
    public abstract class PkzipClassic : SymmetricAlgorithm
    {
        public static byte[] GenerateKeys(byte[] seed)
        {
            if (seed == null)
            {
                throw new ArgumentNullException(nameof(seed));
            }
            if (seed.Length == 0)
            {
                throw new ArgumentException("Length is zero", nameof(seed));
            }
            uint[] numArray = { 0x12345678, 0x23456789, 0x34567890 };
            for (int i = 0; i < seed.Length; i++)
            {
                numArray[0] = Crc32.ComputeCrc32(numArray[0], seed[i]);
                numArray[1] += (byte)numArray[0];
                numArray[1] = (numArray[1] * 0x8088405) + 1;
                numArray[2] = Crc32.ComputeCrc32(numArray[2], (byte)(numArray[1] >> 0x18));
            }
            return new[]
            {
                ((byte) (numArray[0] & 0xff)), ((byte) ((numArray[0] >> 8) & 0xff)),
                ((byte) ((numArray[0] >> 0x10) & 0xff)), ((byte) ((numArray[0] >> 0x18) & 0xff)),
                ((byte) (numArray[1] & 0xff)), ((byte) ((numArray[1] >> 8) & 0xff)),
                ((byte) ((numArray[1] >> 0x10) & 0xff)), ((byte) ((numArray[1] >> 0x18) & 0xff)),
                ((byte) (numArray[2] & 0xff)), ((byte) ((numArray[2] >> 8) & 0xff)),
                ((byte) ((numArray[2] >> 0x10) & 0xff)), ((byte) ((numArray[2] >> 0x18) & 0xff))
            };
        }
    }
}
