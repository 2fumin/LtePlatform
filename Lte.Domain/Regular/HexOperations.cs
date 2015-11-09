using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common;

namespace Lte.Domain.Regular
{
    public static class HexOperations
    {
        public static BitArrayInputStream GetInputStream(this string HexWithOutSp)
        {
            HexWithOutSp = HexWithOutSp.Replace(" ", "");
            Stream byteStream = new MemoryStream();
            List<byte> list = new List<byte>();
            for (int i = -(HexWithOutSp.Length % 2); i < HexWithOutSp.Length; i += 2)
            {
                int num3 = Convert.ToInt16(HexWithOutSp.Substring(i == -1 ? 0 : i, i == -1 ? 1 : 2), 0x10);
                list.Add((byte)num3);
            }
            byte[] buffer = list.ToArray();
            byteStream.Write(buffer, 0, buffer.Length);
            byteStream.Position = 0L;
            return new BitArrayInputStream(byteStream) { Position = 0L };
        }

        private static byte[] GetBufferByString(this string HexWithOutSp)
        {
            int result;
            Math.DivRem(HexWithOutSp.Length, 2, out result);
            List<byte> list = new List<byte>();
            if (result == 0)
            {
                for (int i = 0; i < HexWithOutSp.Length; i++)
                {
                    int num3 = Convert.ToInt16(HexWithOutSp.Substring(i, 2), 0x10);
                    list.Add((byte)num3);
                    i++;
                }
            }
            return list.ToArray();
        }

        public static byte GetLastByte(this int source)
        {
            return (byte)(source & GenerateMask(8));
        }

        public static int GenerateMask(byte length)
        {
            int result = 1;
            for (byte index = 1; index < length; index++)
            {
                result |= 1 << index;
            }
            return result;
        }
    }
}
