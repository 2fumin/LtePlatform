using System;

namespace ZipLib.Comppression
{
    public class DeflaterHuffman
    {
        private static readonly byte[] bit4Reverse = { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 };

        private static readonly int[] BL_ORDER =
        { 
            0x10, 0x11, 0x12, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 
            14, 1, 15
        };
        private Tree blTree;
        private short[] d_buf;
        private Tree distTree;
        private int extra_bits;
        private byte[] l_buf;
        private int last_lit;
        private Tree literalTree;
        public DeflaterPending pending;
        private static short[] staticDCodes;
        private static byte[] staticDLength;
        private static short[] staticLCodes = new short[0x11e];
        private static byte[] staticLLength = new byte[0x11e];

        static DeflaterHuffman()
        {
            int index = 0;
            while (index < 0x90)
            {
                staticLCodes[index] = BitReverse((0x30 + index) << 8);
                staticLLength[index++] = 8;
            }
            while (index < 0x100)
            {
                staticLCodes[index] = BitReverse((0x100 + index) << 7);
                staticLLength[index++] = 9;
            }
            while (index < 280)
            {
                staticLCodes[index] = BitReverse((-256 + index) << 9);
                staticLLength[index++] = 7;
            }
            while (index < 0x11e)
            {
                staticLCodes[index] = BitReverse((-88 + index) << 8);
                staticLLength[index++] = 8;
            }
            staticDCodes = new short[30];
            staticDLength = new byte[30];
            for (index = 0; index < 30; index++)
            {
                staticDCodes[index] = BitReverse(index << 11);
                staticDLength[index] = 5;
            }
        }

        public DeflaterHuffman(DeflaterPending pending)
        {
            this.pending = pending;
            literalTree = new Tree(this, 0x11e, 0x101, 15);
            distTree = new Tree(this, 30, 1, 15);
            blTree = new Tree(this, 0x13, 4, 7);
            d_buf = new short[0x4000];
            l_buf = new byte[0x4000];
        }

        public static short BitReverse(int toReverse)
        {
            return (short)((((bit4Reverse[toReverse & 15] << 12) | (bit4Reverse[(toReverse >> 4) & 15] << 8)) | (bit4Reverse[(toReverse >> 8) & 15] << 4)) | bit4Reverse[toReverse >> 12]);
        }

        public void CompressBlock()
        {
            for (int i = 0; i < last_lit; i++)
            {
                int length = l_buf[i] & 0xff;
                int distance = d_buf[i];
                if (distance-- != 0)
                {
                    int code = Lcode(length);
                    literalTree.WriteSymbol(code);
                    int count = (code - 0x105) / 4;
                    if ((count > 0) && (count <= 5))
                    {
                        pending.WriteBits(length & ((1 << count) - 1), count);
                    }
                    int num6 = Dcode(distance);
                    distTree.WriteSymbol(num6);
                    count = (num6 / 2) - 1;
                    if (count > 0)
                    {
                        pending.WriteBits(distance & ((1 << count) - 1), count);
                    }
                }
                else
                {
                    literalTree.WriteSymbol(length);
                }
            }
            literalTree.WriteSymbol(0x100);
        }

        private static int Dcode(int distance)
        {
            int num = 0;
            while (distance >= 4)
            {
                num += 2;
                distance = distance >> 1;
            }
            return (num + distance);
        }

        public void FlushBlock(byte[] stored, int storedOffset, int storedLength, bool lastBlock)
        {
            literalTree.freqs[0x100] = (short)(literalTree.freqs[0x100] + 1);
            literalTree.BuildTree();
            distTree.BuildTree();
            literalTree.CalcBLFreq(blTree);
            distTree.CalcBLFreq(blTree);
            blTree.BuildTree();
            int blTreeCodes = 4;
            for (int i = 0x12; i > blTreeCodes; i--)
            {
                if (blTree.length[BL_ORDER[i]] > 0)
                {
                    blTreeCodes = i + 1;
                }
            }
            int num3 = ((((14 + (blTreeCodes * 3)) + blTree.GetEncodedLength()) + literalTree.GetEncodedLength()) + distTree.GetEncodedLength()) + extra_bits;
            int num4 = extra_bits;
            for (int j = 0; j < 0x11e; j++)
            {
                num4 += literalTree.freqs[j] * staticLLength[j];
            }
            for (int k = 0; k < 30; k++)
            {
                num4 += distTree.freqs[k] * staticDLength[k];
            }
            if (num3 >= num4)
            {
                num3 = num4;
            }
            if ((storedOffset >= 0) && ((storedLength + 4) < (num3 >> 3)))
            {
                FlushStoredBlock(stored, storedOffset, storedLength, lastBlock);
            }
            else if (num3 == num4)
            {
                pending.WriteBits(2 + (lastBlock ? 1 : 0), 3);
                literalTree.SetStaticCodes(staticLCodes, staticLLength);
                distTree.SetStaticCodes(staticDCodes, staticDLength);
                CompressBlock();
                Reset();
            }
            else
            {
                pending.WriteBits(4 + (lastBlock ? 1 : 0), 3);
                SendAllTrees(blTreeCodes);
                CompressBlock();
                Reset();
            }
        }

        public void FlushStoredBlock(byte[] stored, int storedOffset, int storedLength, bool lastBlock)
        {
            pending.WriteBits(lastBlock ? 1 : 0, 3);
            pending.AlignToByte();
            pending.WriteShort(storedLength);
            pending.WriteShort(~storedLength);
            pending.WriteBlock(stored, storedOffset, storedLength);
            Reset();
        }

        public bool IsFull()
        {
            return (last_lit >= 0x4000);
        }

        private static int Lcode(int length)
        {
            if (length == 0xff)
            {
                return 0x11d;
            }
            int num = 0x101;
            while (length >= 8)
            {
                num += 4;
                length = length >> 1;
            }
            return (num + length);
        }

        public void Reset()
        {
            last_lit = 0;
            extra_bits = 0;
            literalTree.Reset();
            distTree.Reset();
            blTree.Reset();
        }

        public void SendAllTrees(int blTreeCodes)
        {
            blTree.BuildCodes();
            literalTree.BuildCodes();
            distTree.BuildCodes();
            pending.WriteBits(literalTree.numCodes - 0x101, 5);
            pending.WriteBits(distTree.numCodes - 1, 5);
            pending.WriteBits(blTreeCodes - 4, 4);
            for (int i = 0; i < blTreeCodes; i++)
            {
                pending.WriteBits(blTree.length[BL_ORDER[i]], 3);
            }
            literalTree.WriteTree(blTree);
            distTree.WriteTree(blTree);
        }

        public bool TallyDist(int distance, int length)
        {
            d_buf[last_lit] = (short)distance;
            l_buf[last_lit++] = (byte)(length - 3);
            int index = Lcode(length - 3);
            literalTree.freqs[index] = (short)(literalTree.freqs[index] + 1);
            if ((index >= 0x109) && (index < 0x11d))
            {
                extra_bits += (index - 0x105) / 4;
            }
            int num2 = Dcode(distance - 1);
            distTree.freqs[num2] = (short)(distTree.freqs[num2] + 1);
            if (num2 >= 4)
            {
                extra_bits += (num2 / 2) - 1;
            }
            return IsFull();
        }

        public bool TallyLit(int literal)
        {
            d_buf[last_lit] = 0;
            l_buf[last_lit++] = (byte)literal;
            literalTree.freqs[literal] = (short)(literalTree.freqs[literal] + 1);
            return IsFull();
        }

        private class Tree
        {
            private int[] bl_counts;
            private short[] codes;
            private DeflaterHuffman dh;
            public short[] freqs;
            public byte[] length;
            private int maxLength;
            public int minNumCodes;
            public int numCodes;

            public Tree(DeflaterHuffman dh, int elems, int minCodes, int maxLength)
            {
                this.dh = dh;
                minNumCodes = minCodes;
                this.maxLength = maxLength;
                freqs = new short[elems];
                bl_counts = new int[maxLength];
            }

            public void BuildCodes()
            {
                int[] numArray = new int[maxLength];
                int num = 0;
                codes = new short[freqs.Length];
                for (int i = 0; i < maxLength; i++)
                {
                    numArray[i] = num;
                    num += bl_counts[i] << (15 - i);
                }
                for (int j = 0; j < numCodes; j++)
                {
                    int num4 = length[j];
                    if (num4 > 0)
                    {
                        codes[j] = BitReverse(numArray[num4 - 1]);
                        numArray[num4 - 1] += 1 << (0x10 - num4);
                    }
                }
            }

            private void BuildLength(int[] childs)
            {
                length = new byte[freqs.Length];
                int num = childs.Length / 2;
                int num2 = (num + 1) / 2;
                int num3 = 0;
                for (int i = 0; i < maxLength; i++)
                {
                    bl_counts[i] = 0;
                }
                int[] numArray = new int[num];
                numArray[num - 1] = 0;
                for (int j = num - 1; j >= 0; j--)
                {
                    if (childs[(2 * j) + 1] != -1)
                    {
                        int localLength = numArray[j] + 1;
                        if (localLength > maxLength)
                        {
                            localLength = maxLength;
                            num3++;
                        }
                        numArray[childs[2 * j]] = numArray[childs[(2 * j) + 1]] = localLength;
                    }
                    else
                    {
                        int num7 = numArray[j];
                        bl_counts[num7 - 1]++;
                        length[childs[2 * j]] = (byte)numArray[j];
                    }
                }
                if (num3 != 0)
                {
                    int index = maxLength - 1;
                    do
                    {
                        while (bl_counts[--index] == 0)
                        {
                        }
                        do
                        {
                            bl_counts[index]--;
                            bl_counts[++index]++;
                            num3 -= 1 << ((maxLength - 1) - index);
                        }
                        while ((num3 > 0) && (index < (maxLength - 1)));
                    }
                    while (num3 > 0);
                    bl_counts[maxLength - 1] += num3;
                    bl_counts[maxLength - 2] -= num3;
                    int num9 = 2 * num2;
                    for (int k = maxLength; k != 0; k--)
                    {
                        int num11 = bl_counts[k - 1];
                        while (num11 > 0)
                        {
                            int num12 = 2 * childs[num9++];
                            if (childs[num12 + 1] == -1)
                            {
                                length[childs[num12]] = (byte)k;
                                num11--;
                            }
                        }
                    }
                }
            }

            public void BuildTree()
            {
                int localLength = freqs.Length;
                int[] numArray = new int[localLength];
                int num2 = 0;
                int num3 = 0;
                for (int i = 0; i < localLength; i++)
                {
                    int num5 = freqs[i];
                    if (num5 != 0)
                    {
                        int num7;
                        int index = num2++;
                        while ((index > 0) && (freqs[numArray[num7 = (index - 1) / 2]] > num5))
                        {
                            numArray[index] = numArray[num7];
                            index = num7;
                        }
                        numArray[index] = i;
                        num3 = i;
                    }
                }
                while (num2 < 2)
                {
                    int num8 = (num3 < 2) ? ++num3 : 0;
                    numArray[num2++] = num8;
                }
                numCodes = Math.Max(num3 + 1, minNumCodes);
                int num9 = num2;
                int[] childs = new int[(4 * num2) - 2];
                int[] numArray3 = new int[(2 * num2) - 1];
                int num10 = num9;
                for (int j = 0; j < num2; j++)
                {
                    int num12 = numArray[j];
                    childs[2 * j] = num12;
                    childs[(2 * j) + 1] = -1;
                    numArray3[j] = freqs[num12] << 8;
                    numArray[j] = j;
                }
                do
                {
                    int num13 = numArray[0];
                    int num14 = numArray[--num2];
                    int num15 = 0;
                    int num16 = 1;
                    while (num16 < num2)
                    {
                        if (((num16 + 1) < num2) && (numArray3[numArray[num16]] > numArray3[numArray[num16 + 1]]))
                        {
                            num16++;
                        }
                        numArray[num15] = numArray[num16];
                        num15 = num16;
                        num16 = (num16 * 2) + 1;
                    }
                    int num17 = numArray3[num14];
                    while (((num16 = num15) > 0) && (numArray3[numArray[num15 = (num16 - 1) / 2]] > num17))
                    {
                        numArray[num16] = numArray[num15];
                    }
                    numArray[num16] = num14;
                    int num18 = numArray[0];
                    num14 = num10++;
                    childs[2 * num14] = num13;
                    childs[(2 * num14) + 1] = num18;
                    int num19 = Math.Min(numArray3[num13] & 0xff, numArray3[num18] & 0xff);
                    numArray3[num14] = num17 = ((numArray3[num13] + numArray3[num18]) - num19) + 1;
                    num15 = 0;
                    num16 = 1;
                    while (num16 < num2)
                    {
                        if (((num16 + 1) < num2) && (numArray3[numArray[num16]] > numArray3[numArray[num16 + 1]]))
                        {
                            num16++;
                        }
                        numArray[num15] = numArray[num16];
                        num15 = num16;
                        num16 = (num15 * 2) + 1;
                    }
                    while (((num16 = num15) > 0) && (numArray3[numArray[num15 = (num16 - 1) / 2]] > num17))
                    {
                        numArray[num16] = numArray[num15];
                    }
                    numArray[num16] = num14;
                }
                while (num2 > 1);
                if (numArray[0] != ((childs.Length / 2) - 1))
                {
                    throw new SharpZipBaseException("Heap invariant violated");
                }
                BuildLength(childs);
            }

            public void CalcBLFreq(Tree blTree)
            {
                int index = -1;
                int num5 = 0;
                while (num5 < numCodes)
                {
                    int num;
                    int num2;
                    int num3 = 1;
                    int num6 = length[num5];
                    if (num6 == 0)
                    {
                        num = 0x8a;
                        num2 = 3;
                    }
                    else
                    {
                        num = 6;
                        num2 = 3;
                        if (index != num6)
                        {
                            blTree.freqs[num6] = (short)(blTree.freqs[num6] + 1);
                            num3 = 0;
                        }
                    }
                    index = num6;
                    num5++;
                    while ((num5 < numCodes) && (index == length[num5]))
                    {
                        num5++;
                        if (++num3 >= num)
                        {
                            break;
                        }
                    }
                    if (num3 < num2)
                    {
                        blTree.freqs[index] = (short)(blTree.freqs[index] + ((short)num3));
                    }
                    else
                    {
                        if (index != 0)
                        {
                            blTree.freqs[0x10] = (short)(blTree.freqs[0x10] + 1);
                            continue;
                        }
                        if (num3 <= 10)
                        {
                            blTree.freqs[0x11] = (short)(blTree.freqs[0x11] + 1);
                            continue;
                        }
                        blTree.freqs[0x12] = (short)(blTree.freqs[0x12] + 1);
                    }
                }
            }

            public void CheckEmpty()
            {
                bool flag = true;
                for (int i = 0; i < freqs.Length; i++)
                {
                    if (freqs[i] != 0)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    throw new SharpZipBaseException("!Empty");
                }
            }

            public int GetEncodedLength()
            {
                int num = 0;
                for (int i = 0; i < freqs.Length; i++)
                {
                    num += freqs[i] * length[i];
                }
                return num;
            }

            public void Reset()
            {
                for (int i = 0; i < freqs.Length; i++)
                {
                    freqs[i] = 0;
                }
                codes = null;
                length = null;
            }

            public void SetStaticCodes(short[] staticCodes, byte[] staticLengths)
            {
                codes = staticCodes;
                length = staticLengths;
            }

            public void WriteSymbol(int code)
            {
                dh.pending.WriteBits(codes[code] & 0xffff, length[code]);
            }

            public void WriteTree(Tree blTree)
            {
                int code = -1;
                int index = 0;
                while (index < numCodes)
                {
                    int num;
                    int num2;
                    int num3 = 1;
                    int num6 = length[index];
                    if (num6 == 0)
                    {
                        num = 0x8a;
                        num2 = 3;
                    }
                    else
                    {
                        num = 6;
                        num2 = 3;
                        if (code != num6)
                        {
                            blTree.WriteSymbol(num6);
                            num3 = 0;
                        }
                    }
                    code = num6;
                    index++;
                    while ((index < numCodes) && (code == length[index]))
                    {
                        index++;
                        if (++num3 >= num)
                        {
                            break;
                        }
                    }
                    if (num3 < num2)
                    {
                        while (num3-- > 0)
                        {
                            blTree.WriteSymbol(code);
                        }
                    }
                    else if (code != 0)
                    {
                        blTree.WriteSymbol(0x10);
                        dh.pending.WriteBits(num3 - 3, 2);
                    }
                    else
                    {
                        if (num3 <= 10)
                        {
                            blTree.WriteSymbol(0x11);
                            dh.pending.WriteBits(num3 - 3, 3);
                            continue;
                        }
                        blTree.WriteSymbol(0x12);
                        dh.pending.WriteBits(num3 - 11, 7);
                    }
                }
            }
        }
    }
}

