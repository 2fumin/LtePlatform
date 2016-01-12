using System;

namespace ZipLib.Comppression
{
    public class DeflaterHuffman
    {
        private static readonly byte[] Bit4Reverse = { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 };

        private static readonly int[] BlOrder =
        { 
            0x10, 0x11, 0x12, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 
            14, 1, 15
        };
        private readonly Tree _blTree;
        private readonly short[] _dBuf;
        private readonly Tree _distTree;
        private int _extraBits;
        private readonly byte[] _lBuf;
        private int _lastLit;
        private readonly Tree _literalTree;
        private readonly DeflaterPending _pending;
        private static readonly short[] StaticDCodes;
        private static readonly byte[] StaticDLength;
        private static readonly short[] StaticLCodes = new short[0x11e];
        private static readonly byte[] StaticLLength = new byte[0x11e];

        static DeflaterHuffman()
        {
            int index = 0;
            while (index < 0x90)
            {
                StaticLCodes[index] = BitReverse((0x30 + index) << 8);
                StaticLLength[index++] = 8;
            }
            while (index < 0x100)
            {
                StaticLCodes[index] = BitReverse((0x100 + index) << 7);
                StaticLLength[index++] = 9;
            }
            while (index < 280)
            {
                StaticLCodes[index] = BitReverse((-256 + index) << 9);
                StaticLLength[index++] = 7;
            }
            while (index < 0x11e)
            {
                StaticLCodes[index] = BitReverse((-88 + index) << 8);
                StaticLLength[index++] = 8;
            }
            StaticDCodes = new short[30];
            StaticDLength = new byte[30];
            for (index = 0; index < 30; index++)
            {
                StaticDCodes[index] = BitReverse(index << 11);
                StaticDLength[index] = 5;
            }
        }

        public DeflaterHuffman(DeflaterPending pending)
        {
            this._pending = pending;
            _literalTree = new Tree(this, 0x11e, 0x101, 15);
            _distTree = new Tree(this, 30, 1, 15);
            _blTree = new Tree(this, 0x13, 4, 7);
            _dBuf = new short[0x4000];
            _lBuf = new byte[0x4000];
        }

        public static short BitReverse(int toReverse)
        {
            return (short)((((Bit4Reverse[toReverse & 15] << 12) | (Bit4Reverse[(toReverse >> 4) & 15] << 8)) | (Bit4Reverse[(toReverse >> 8) & 15] << 4)) | Bit4Reverse[toReverse >> 12]);
        }

        public void CompressBlock()
        {
            for (int i = 0; i < _lastLit; i++)
            {
                int length = _lBuf[i] & 0xff;
                int distance = _dBuf[i];
                if (distance-- != 0)
                {
                    int code = Lcode(length);
                    _literalTree.WriteSymbol(code);
                    int count = (code - 0x105) / 4;
                    if ((count > 0) && (count <= 5))
                    {
                        _pending.WriteBits(length & ((1 << count) - 1), count);
                    }
                    int num6 = Dcode(distance);
                    _distTree.WriteSymbol(num6);
                    count = (num6 / 2) - 1;
                    if (count > 0)
                    {
                        _pending.WriteBits(distance & ((1 << count) - 1), count);
                    }
                }
                else
                {
                    _literalTree.WriteSymbol(length);
                }
            }
            _literalTree.WriteSymbol(0x100);
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
            _literalTree.Freqs[0x100] = (short)(_literalTree.Freqs[0x100] + 1);
            _literalTree.BuildTree();
            _distTree.BuildTree();
            _literalTree.CalcBlFreq(_blTree);
            _distTree.CalcBlFreq(_blTree);
            _blTree.BuildTree();
            int blTreeCodes = 4;
            for (int i = 0x12; i > blTreeCodes; i--)
            {
                if (_blTree.Length[BlOrder[i]] > 0)
                {
                    blTreeCodes = i + 1;
                }
            }
            int num3 = ((((14 + (blTreeCodes * 3)) + _blTree.GetEncodedLength()) + _literalTree.GetEncodedLength()) + _distTree.GetEncodedLength()) + _extraBits;
            int num4 = _extraBits;
            for (int j = 0; j < 0x11e; j++)
            {
                num4 += _literalTree.Freqs[j] * StaticLLength[j];
            }
            for (int k = 0; k < 30; k++)
            {
                num4 += _distTree.Freqs[k] * StaticDLength[k];
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
                _pending.WriteBits(2 + (lastBlock ? 1 : 0), 3);
                _literalTree.SetStaticCodes(StaticLCodes, StaticLLength);
                _distTree.SetStaticCodes(StaticDCodes, StaticDLength);
                CompressBlock();
                Reset();
            }
            else
            {
                _pending.WriteBits(4 + (lastBlock ? 1 : 0), 3);
                SendAllTrees(blTreeCodes);
                CompressBlock();
                Reset();
            }
        }

        public void FlushStoredBlock(byte[] stored, int storedOffset, int storedLength, bool lastBlock)
        {
            _pending.WriteBits(lastBlock ? 1 : 0, 3);
            _pending.AlignToByte();
            _pending.WriteShort(storedLength);
            _pending.WriteShort(~storedLength);
            _pending.WriteBlock(stored, storedOffset, storedLength);
            Reset();
        }

        public bool IsFull()
        {
            return (_lastLit >= 0x4000);
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
            _lastLit = 0;
            _extraBits = 0;
            _literalTree.Reset();
            _distTree.Reset();
            _blTree.Reset();
        }

        public void SendAllTrees(int blTreeCodes)
        {
            _blTree.BuildCodes();
            _literalTree.BuildCodes();
            _distTree.BuildCodes();
            _pending.WriteBits(_literalTree.NumCodes - 0x101, 5);
            _pending.WriteBits(_distTree.NumCodes - 1, 5);
            _pending.WriteBits(blTreeCodes - 4, 4);
            for (int i = 0; i < blTreeCodes; i++)
            {
                _pending.WriteBits(_blTree.Length[BlOrder[i]], 3);
            }
            _literalTree.WriteTree(_blTree);
            _distTree.WriteTree(_blTree);
        }

        public bool TallyDist(int distance, int length)
        {
            _dBuf[_lastLit] = (short)distance;
            _lBuf[_lastLit++] = (byte)(length - 3);
            int index = Lcode(length - 3);
            _literalTree.Freqs[index] = (short)(_literalTree.Freqs[index] + 1);
            if ((index >= 0x109) && (index < 0x11d))
            {
                _extraBits += (index - 0x105) / 4;
            }
            int num2 = Dcode(distance - 1);
            _distTree.Freqs[num2] = (short)(_distTree.Freqs[num2] + 1);
            if (num2 >= 4)
            {
                _extraBits += (num2 / 2) - 1;
            }
            return IsFull();
        }

        public bool TallyLit(int literal)
        {
            _dBuf[_lastLit] = 0;
            _lBuf[_lastLit++] = (byte)literal;
            _literalTree.Freqs[literal] = (short)(_literalTree.Freqs[literal] + 1);
            return IsFull();
        }

        private class Tree
        {
            private readonly int[] _blCounts;
            private short[] _codes;
            private readonly DeflaterHuffman _dh;
            public readonly short[] Freqs;
            public byte[] Length;
            private readonly int _maxLength;
            public readonly int MinNumCodes;
            public int NumCodes;

            public Tree(DeflaterHuffman dh, int elems, int minCodes, int maxLength)
            {
                this._dh = dh;
                MinNumCodes = minCodes;
                this._maxLength = maxLength;
                Freqs = new short[elems];
                _blCounts = new int[maxLength];
            }

            public void BuildCodes()
            {
                int[] numArray = new int[_maxLength];
                int num = 0;
                _codes = new short[Freqs.Length];
                for (int i = 0; i < _maxLength; i++)
                {
                    numArray[i] = num;
                    num += _blCounts[i] << (15 - i);
                }
                for (int j = 0; j < NumCodes; j++)
                {
                    int num4 = Length[j];
                    if (num4 > 0)
                    {
                        _codes[j] = BitReverse(numArray[num4 - 1]);
                        numArray[num4 - 1] += 1 << (0x10 - num4);
                    }
                }
            }

            private void BuildLength(int[] childs)
            {
                Length = new byte[Freqs.Length];
                int num = childs.Length / 2;
                int num2 = (num + 1) / 2;
                int num3 = 0;
                for (int i = 0; i < _maxLength; i++)
                {
                    _blCounts[i] = 0;
                }
                int[] numArray = new int[num];
                numArray[num - 1] = 0;
                for (int j = num - 1; j >= 0; j--)
                {
                    if (childs[(2 * j) + 1] != -1)
                    {
                        int localLength = numArray[j] + 1;
                        if (localLength > _maxLength)
                        {
                            localLength = _maxLength;
                            num3++;
                        }
                        numArray[childs[2 * j]] = numArray[childs[(2 * j) + 1]] = localLength;
                    }
                    else
                    {
                        int num7 = numArray[j];
                        _blCounts[num7 - 1]++;
                        Length[childs[2 * j]] = (byte)numArray[j];
                    }
                }
                if (num3 != 0)
                {
                    int index = _maxLength - 1;
                    do
                    {
                        while (_blCounts[--index] == 0)
                        {
                        }
                        do
                        {
                            _blCounts[index]--;
                            _blCounts[++index]++;
                            num3 -= 1 << ((_maxLength - 1) - index);
                        }
                        while ((num3 > 0) && (index < (_maxLength - 1)));
                    }
                    while (num3 > 0);
                    _blCounts[_maxLength - 1] += num3;
                    _blCounts[_maxLength - 2] -= num3;
                    int num9 = 2 * num2;
                    for (int k = _maxLength; k != 0; k--)
                    {
                        int num11 = _blCounts[k - 1];
                        while (num11 > 0)
                        {
                            int num12 = 2 * childs[num9++];
                            if (childs[num12 + 1] == -1)
                            {
                                Length[childs[num12]] = (byte)k;
                                num11--;
                            }
                        }
                    }
                }
            }

            public void BuildTree()
            {
                int localLength = Freqs.Length;
                int[] numArray = new int[localLength];
                int num2 = 0;
                int num3 = 0;
                for (int i = 0; i < localLength; i++)
                {
                    int num5 = Freqs[i];
                    if (num5 != 0)
                    {
                        int num7;
                        int index = num2++;
                        while ((index > 0) && (Freqs[numArray[num7 = (index - 1) / 2]] > num5))
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
                NumCodes = Math.Max(num3 + 1, MinNumCodes);
                int num9 = num2;
                int[] childs = new int[(4 * num2) - 2];
                int[] numArray3 = new int[(2 * num2) - 1];
                int num10 = num9;
                for (int j = 0; j < num2; j++)
                {
                    int num12 = numArray[j];
                    childs[2 * j] = num12;
                    childs[(2 * j) + 1] = -1;
                    numArray3[j] = Freqs[num12] << 8;
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

            public void CalcBlFreq(Tree blTree)
            {
                int index = -1;
                int num5 = 0;
                while (num5 < NumCodes)
                {
                    int num;
                    int num2;
                    int num3 = 1;
                    int num6 = Length[num5];
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
                            blTree.Freqs[num6] = (short)(blTree.Freqs[num6] + 1);
                            num3 = 0;
                        }
                    }
                    index = num6;
                    num5++;
                    while ((num5 < NumCodes) && (index == Length[num5]))
                    {
                        num5++;
                        if (++num3 >= num)
                        {
                            break;
                        }
                    }
                    if (num3 < num2)
                    {
                        blTree.Freqs[index] = (short)(blTree.Freqs[index] + ((short)num3));
                    }
                    else
                    {
                        if (index != 0)
                        {
                            blTree.Freqs[0x10] = (short)(blTree.Freqs[0x10] + 1);
                            continue;
                        }
                        if (num3 <= 10)
                        {
                            blTree.Freqs[0x11] = (short)(blTree.Freqs[0x11] + 1);
                            continue;
                        }
                        blTree.Freqs[0x12] = (short)(blTree.Freqs[0x12] + 1);
                    }
                }
            }

            public void CheckEmpty()
            {
                bool flag = true;
                for (int i = 0; i < Freqs.Length; i++)
                {
                    if (Freqs[i] != 0)
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
                for (int i = 0; i < Freqs.Length; i++)
                {
                    num += Freqs[i] * Length[i];
                }
                return num;
            }

            public void Reset()
            {
                for (int i = 0; i < Freqs.Length; i++)
                {
                    Freqs[i] = 0;
                }
                _codes = null;
                Length = null;
            }

            public void SetStaticCodes(short[] staticCodes, byte[] staticLengths)
            {
                _codes = staticCodes;
                Length = staticLengths;
            }

            public void WriteSymbol(int code)
            {
                _dh._pending.WriteBits(_codes[code] & 0xffff, Length[code]);
            }

            public void WriteTree(Tree blTree)
            {
                int code = -1;
                int index = 0;
                while (index < NumCodes)
                {
                    int num;
                    int num2;
                    int num3 = 1;
                    int num6 = Length[index];
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
                    while ((index < NumCodes) && (code == Length[index]))
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
                        _dh._pending.WriteBits(num3 - 3, 2);
                    }
                    else
                    {
                        if (num3 <= 10)
                        {
                            blTree.WriteSymbol(0x11);
                            _dh._pending.WriteBits(num3 - 3, 3);
                            continue;
                        }
                        blTree.WriteSymbol(0x12);
                        _dh._pending.WriteBits(num3 - 11, 7);
                    }
                }
            }
        }
    }
}

