using System;
using System.IO;
using ZipLib.CheckSums;

namespace ZipLib.Bzip
{
    public class BZip2InputStream : Stream
    {
        private int[][] baseArray = new int[6][];
        private Stream baseStream;
        private bool blockRandomised;
        private int blockSize100k;
        private int bsBuff;
        private int bsLive;
        private int ch2;
        private int chPrev;
        private int computedBlockCRC;
        private uint computedCombinedCRC;
        private int count;
        private int currentChar = -1;
        private int currentState = 1;
        private int i2;
        private bool[] inUse = new bool[0x100];
        private bool isStreamOwner = true;
        private int j2;
        private int last;
        private int[][] limit = new int[6][];
        private byte[] ll8;
        private IChecksum mCrc = new StrangeCRC();
        private int[] minLens = new int[6];
        private int nInUse;
        private int origPtr;
        private int[][] perm = new int[6][];
        private int rNToGo;
        private int rTPos;
        private byte[] selector = new byte[0x4652];
        private byte[] selectorMtf = new byte[0x4652];
        private byte[] seqToUnseq = new byte[0x100];
        private int storedBlockCRC;
        private int storedCombinedCRC;
        private bool streamEnd;
        private int tPos;
        private int[] tt;
        private byte[] unseqToSeq = new byte[0x100];
        private int[] unzftab = new int[0x100];
        private byte z;

        public BZip2InputStream(Stream stream)
        {
            for (int i = 0; i < 6; i++)
            {
                limit[i] = new int[0x102];
                baseArray[i] = new int[0x102];
                perm[i] = new int[0x102];
            }
            BsSetStream(stream);
            Initialize();
            InitBlock();
            SetupBlock();
        }

        private static void BadBlockHeader()
        {
            throw new BZip2Exception("BZip2 input stream bad block header");
        }

        private static void BlockOverrun()
        {
            throw new BZip2Exception("BZip2 input stream block overrun");
        }

        private int BsGetInt32()
        {
            int num = (BsR(8) << 8) | BsR(8);
            num = (num << 8) | BsR(8);
            return ((num << 8) | BsR(8));
        }

        private int BsGetIntVS(int numBits)
        {
            return BsR(numBits);
        }

        private char BsGetUChar()
        {
            return (char)BsR(8);
        }

        private int BsR(int n)
        {
            while (bsLive < n)
            {
                FillBuffer();
            }
            int num = (bsBuff >> (bsLive - n)) & ((1 << n) - 1);
            bsLive -= n;
            return num;
        }

        private void BsSetStream(Stream stream)
        {
            baseStream = stream;
            bsLive = 0;
            bsBuff = 0;
        }

        public override void Close()
        {
            if (IsStreamOwner && (baseStream != null))
            {
                baseStream.Close();
            }
        }

        private void Complete()
        {
            storedCombinedCRC = BsGetInt32();
            if (storedCombinedCRC != computedCombinedCRC)
            {
                CrcError();
            }
            streamEnd = true;
        }

        private static void CompressedStreamEOF()
        {
            throw new EndOfStreamException("BZip2 input stream end of compressed stream");
        }

        private static void CrcError()
        {
            throw new BZip2Exception("BZip2 input stream crc error");
        }

        private void EndBlock()
        {
            computedBlockCRC = (int)mCrc.Value;
            if (storedBlockCRC != computedBlockCRC)
            {
                CrcError();
            }
            computedCombinedCRC = ((computedCombinedCRC << 1) & uint.MaxValue) | (computedCombinedCRC >> 0x1f);
            computedCombinedCRC ^= (uint)computedBlockCRC;
        }

        private void FillBuffer()
        {
            int num = 0;
            try
            {
                num = baseStream.ReadByte();
            }
            catch (Exception)
            {
                CompressedStreamEOF();
            }
            if (num == -1)
            {
                CompressedStreamEOF();
            }
            bsBuff = (bsBuff << 8) | (num & 0xff);
            bsLive += 8;
        }

        public override void Flush()
        {
            if (baseStream != null)
            {
                baseStream.Flush();
            }
        }

        private void GetAndMoveToFrontDecode()
        {
            int num11;
            byte[] buffer = new byte[0x100];
            int num2 = 0x186a0 * blockSize100k;
            origPtr = BsGetIntVS(0x18);
            RecvDecodingTables();
            int num3 = nInUse + 1;
            int index = -1;
            int num5 = 0;
            for (int i = 0; i <= 0xff; i++)
            {
                unzftab[i] = 0;
            }
            for (int j = 0; j <= 0xff; j++)
            {
                buffer[j] = (byte)j;
            }
            last = -1;
            if (num5 == 0)
            {
                index++;
                num5 = 50;
            }
            num5--;
            int num8 = selector[index];
            int n = minLens[num8];
            int num10 = BsR(n);
            while (num10 > limit[num8][n])
            {
                if (n > 20)
                {
                    throw new BZip2Exception("Bzip data error");
                }
                n++;
                while (bsLive < 1)
                {
                    FillBuffer();
                }
                num11 = (bsBuff >> (bsLive - 1)) & 1;
                bsLive--;
                num10 = (num10 << 1) | num11;
            }
            if (((num10 - baseArray[num8][n]) < 0) || ((num10 - baseArray[num8][n]) >= 0x102))
            {
                throw new BZip2Exception("Bzip data error");
            }
            int num = perm[num8][num10 - baseArray[num8][n]];
        Label_0163:
            if (num == num3)
            {
                return;
            }
            if ((num != 0) && (num != 1))
            {
                last++;
                if (last >= num2)
                {
                    BlockOverrun();
                }
                byte num15 = buffer[num - 1];
                unzftab[seqToUnseq[num15]]++;
                ll8[last] = seqToUnseq[num15];
                for (int k = num - 1; k > 0; k--)
                {
                    buffer[k] = buffer[k - 1];
                }
                buffer[0] = num15;
                if (num5 == 0)
                {
                    index++;
                    num5 = 50;
                }
                num5--;
                num8 = selector[index];
                n = minLens[num8];
                num10 = BsR(n);
                while (num10 > limit[num8][n])
                {
                    n++;
                    while (bsLive < 1)
                    {
                        FillBuffer();
                    }
                    num11 = (bsBuff >> (bsLive - 1)) & 1;
                    bsLive--;
                    num10 = (num10 << 1) | num11;
                }
                num = perm[num8][num10 - baseArray[num8][n]];
                goto Label_0163;
            }
            int num12 = -1;
            int num13 = 1;
        Label_0178:
            switch (num)
            {
                case 0:
                    num12 += num13;
                    break;

                case 1:
                    num12 += 2 * num13;
                    break;
            }
            num13 = num13 << 1;
            if (num5 == 0)
            {
                index++;
                num5 = 50;
            }
            num5--;
            num8 = selector[index];
            n = minLens[num8];
            num10 = BsR(n);
            while (num10 > limit[num8][n])
            {
                n++;
                while (bsLive < 1)
                {
                    FillBuffer();
                }
                num11 = (bsBuff >> (bsLive - 1)) & 1;
                bsLive--;
                num10 = (num10 << 1) | num11;
            }
            switch (perm[num8][num10 - baseArray[num8][n]])
            {
                case 0:
                case 1:
                    goto Label_0178;
            }
            num12++;
            byte num14 = seqToUnseq[buffer[0]];
            unzftab[num14] += num12;
            while (num12 > 0)
            {
                last++;
                ll8[last] = num14;
                num12--;
            }
            if (last >= num2)
            {
                BlockOverrun();
            }
            goto Label_0163;
        }

        private static void HbCreateDecodeTables(int[] limit, int[] baseArray, int[] perm, char[] length, int minLen, int maxLen, int alphaSize)
        {
            int index = 0;
            for (int i = minLen; i <= maxLen; i++)
            {
                for (int num3 = 0; num3 < alphaSize; num3++)
                {
                    if (length[num3] == i)
                    {
                        perm[index] = num3;
                        index++;
                    }
                }
            }
            for (int j = 0; j < 0x17; j++)
            {
                baseArray[j] = 0;
            }
            for (int k = 0; k < alphaSize; k++)
            {
                baseArray[length[k] + '\x0001']++;
            }
            for (int m = 1; m < 0x17; m++)
            {
                baseArray[m] += baseArray[m - 1];
            }
            for (int n = 0; n < 0x17; n++)
            {
                limit[n] = 0;
            }
            int num8 = 0;
            for (int num9 = minLen; num9 <= maxLen; num9++)
            {
                num8 += baseArray[num9 + 1] - baseArray[num9];
                limit[num9] = num8 - 1;
                num8 = num8 << 1;
            }
            for (int num10 = minLen + 1; num10 <= maxLen; num10++)
            {
                baseArray[num10] = ((limit[num10 - 1] + 1) << 1) - baseArray[num10];
            }
        }

        private void InitBlock()
        {
            char ch = BsGetUChar();
            ch2 = BsGetUChar();
            char ch3 = BsGetUChar();
            char ch4 = BsGetUChar();
            char ch5 = BsGetUChar();
            char ch6 = BsGetUChar();
            if ((((ch == '\x0017') && (ch2 == 'r')) && ((ch3 == 'E') && (ch4 == '8'))) && ((ch5 == 'P') && (ch6 == '\x0090')))
            {
                Complete();
            }
            else if ((((ch != '1') || (ch2 != 'A')) || ((ch3 != 'Y') || (ch4 != '&'))) || ((ch5 != 'S') || (ch6 != 'Y')))
            {
                BadBlockHeader();
                streamEnd = true;
            }
            else
            {
                storedBlockCRC = BsGetInt32();
                blockRandomised = BsR(1) == 1;
                GetAndMoveToFrontDecode();
                mCrc.Reset();
                currentState = 1;
            }
        }

        private void Initialize()
        {
            char ch = BsGetUChar();
            ch2 = BsGetUChar();
            char ch3 = BsGetUChar();
            char ch4 = BsGetUChar();
            if (((ch != 'B') || (ch2 != 'Z')) || (((ch3 != 'h') || (ch4 < '1')) || (ch4 > '9')))
            {
                streamEnd = true;
            }
            else
            {
                SetDecompressStructureSizes(ch4 - '0');
                computedCombinedCRC = 0;
            }
        }

        private void MakeMaps()
        {
            nInUse = 0;
            for (int i = 0; i < 0x100; i++)
            {
                if (inUse[i])
                {
                    seqToUnseq[nInUse] = (byte)i;
                    unseqToSeq[i] = (byte)nInUse;
                    nInUse++;
                }
            }
        }
        
        public override int Read(byte[] buffer, int offset, int length)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            for (int i = 0; i < length; i++)
            {
                int num2 = ReadByte();
                if (num2 == -1)
                {
                    return i;
                }
                buffer[offset + i] = (byte)num2;
            }
            return length;
        }

        public override int ReadByte()
        {
            if (streamEnd)
            {
                return -1;
            }

            switch (currentState)
            {
                case 1:
                case 2:
                case 5:
                    return currentChar;

                case 3:
                    SetupRandPartB();
                    return currentChar;

                case 4:
                    SetupRandPartC();
                    return currentChar;

                case 6:
                    SetupNoRandPartB();
                    return currentChar;

                case 7:
                    SetupNoRandPartC();
                    return currentChar;
            }
            return currentChar;
        }

        private void RecvDecodingTables()
        {
            char[][] chArray = new char[6][];
            for (int i = 0; i < 6; i++)
            {
                chArray[i] = new char[0x102];
            }
            bool[] flagArray = new bool[0x10];
            for (int j = 0; j < 0x10; j++)
            {
                flagArray[j] = BsR(1) == 1;
            }
            for (int k = 0; k < 0x10; k++)
            {
                if (flagArray[k])
                {
                    for (int num4 = 0; num4 < 0x10; num4++)
                    {
                        inUse[(k * 0x10) + num4] = BsR(1) == 1;
                    }
                }
                else
                {
                    for (int num5 = 0; num5 < 0x10; num5++)
                    {
                        inUse[(k * 0x10) + num5] = false;
                    }
                }
            }
            MakeMaps();
            int alphaSize = nInUse + 2;
            int num7 = BsR(3);
            int num8 = BsR(15);
            for (int m = 0; m < num8; m++)
            {
                int num10 = 0;
                while (BsR(1) == 1)
                {
                    num10++;
                }
                selectorMtf[m] = (byte)num10;
            }
            byte[] buffer = new byte[6];
            for (int n = 0; n < num7; n++)
            {
                buffer[n] = (byte)n;
            }
            for (int num12 = 0; num12 < num8; num12++)
            {
                int index = selectorMtf[num12];
                byte num14 = buffer[index];
                while (index > 0)
                {
                    buffer[index] = buffer[index - 1];
                    index--;
                }
                buffer[0] = num14;
                selector[num12] = num14;
            }
            for (int num15 = 0; num15 < num7; num15++)
            {
                int num16 = BsR(5);
                int num17 = 0;
                goto Label_01AD;
            Label_017C:
                if (BsR(1) == 0)
                {
                    num16++;
                }
                else
                {
                    num16--;
                }
            Label_0193:
                if (BsR(1) == 1)
                {
                    goto Label_017C;
                }
                chArray[num15][num17] = (char)num16;
                num17++;
            Label_01AD:
                if (num17 < alphaSize)
                {
                    goto Label_0193;
                }
            }
            for (int num18 = 0; num18 < num7; num18++)
            {
                int num19 = 0x20;
                int num20 = 0;
                for (int num21 = 0; num21 < alphaSize; num21++)
                {
                    num20 = Math.Max(num20, chArray[num18][num21]);
                    num19 = Math.Min(num19, chArray[num18][num21]);
                }
                HbCreateDecodeTables(limit[num18], baseArray[num18], perm[num18], chArray[num18], num19, num20, alphaSize);
                minLens[num18] = num19;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("BZip2InputStream Seek not supported");
        }

        private void SetDecompressStructureSizes(int newSize100k)
        {
            if (((0 > newSize100k) || (newSize100k > 9)) || ((0 > blockSize100k) || (blockSize100k > 9)))
            {
                throw new BZip2Exception("Invalid block size");
            }
            blockSize100k = newSize100k;
            if (newSize100k != 0)
            {
                int num = 0x186a0 * newSize100k;
                ll8 = new byte[num];
                tt = new int[num];
            }
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("BZip2InputStream SetLength not supported");
        }

        private void SetupBlock()
        {
            int[] destinationArray = new int[0x101];
            destinationArray[0] = 0;
            Array.Copy(unzftab, 0, destinationArray, 1, 0x100);
            for (int i = 1; i <= 0x100; i++)
            {
                destinationArray[i] += destinationArray[i - 1];
            }
            for (int j = 0; j <= last; j++)
            {
                byte index = ll8[j];
                tt[destinationArray[index]] = j;
                destinationArray[index]++;
            }
            tPos = tt[origPtr];
            count = 0;
            i2 = 0;
            ch2 = 0x100;
            if (blockRandomised)
            {
                rNToGo = 0;
                rTPos = 0;
                SetupRandPartA();
            }
            else
            {
                SetupNoRandPartA();
            }
        }

        private void SetupNoRandPartA()
        {
            if (i2 <= last)
            {
                chPrev = ch2;
                ch2 = ll8[tPos];
                tPos = tt[tPos];
                i2++;
                currentChar = ch2;
                currentState = 6;
                mCrc.Update(ch2);
            }
            else
            {
                EndBlock();
                InitBlock();
                SetupBlock();
            }
        }

        private void SetupNoRandPartB()
        {
            if (ch2 != chPrev)
            {
                currentState = 5;
                count = 1;
                SetupNoRandPartA();
            }
            else
            {
                count++;
                if (count >= 4)
                {
                    z = ll8[tPos];
                    tPos = tt[tPos];
                    currentState = 7;
                    j2 = 0;
                    SetupNoRandPartC();
                }
                else
                {
                    currentState = 5;
                    SetupNoRandPartA();
                }
            }
        }

        private void SetupNoRandPartC()
        {
            if (j2 < z)
            {
                currentChar = ch2;
                mCrc.Update(ch2);
                j2++;
            }
            else
            {
                currentState = 5;
                i2++;
                count = 0;
                SetupNoRandPartA();
            }
        }

        private void SetupRandPartA()
        {
            if (i2 <= last)
            {
                chPrev = ch2;
                ch2 = ll8[tPos];
                tPos = tt[tPos];
                if (rNToGo == 0)
                {
                    rNToGo = BZip2Constants.RandomNumbers[rTPos];
                    rTPos++;
                    if (rTPos == 0x200)
                    {
                        rTPos = 0;
                    }
                }
                rNToGo--;
                ch2 ^= (rNToGo == 1) ? 1 : 0;
                i2++;
                currentChar = ch2;
                currentState = 3;
                mCrc.Update(ch2);
            }
            else
            {
                EndBlock();
                InitBlock();
                SetupBlock();
            }
        }

        private void SetupRandPartB()
        {
            if (ch2 != chPrev)
            {
                currentState = 2;
                count = 1;
                SetupRandPartA();
            }
            else
            {
                count++;
                if (count >= 4)
                {
                    z = ll8[tPos];
                    tPos = tt[tPos];
                    if (rNToGo == 0)
                    {
                        rNToGo = BZip2Constants.RandomNumbers[rTPos];
                        rTPos++;
                        if (rTPos == 0x200)
                        {
                            rTPos = 0;
                        }
                    }
                    rNToGo--;
                    z = (byte)(z ^ ((rNToGo == 1) ? 1 : 0));
                    j2 = 0;
                    currentState = 4;
                    SetupRandPartC();
                }
                else
                {
                    currentState = 2;
                    SetupRandPartA();
                }
            }
        }

        private void SetupRandPartC()
        {
            if (j2 < z)
            {
                currentChar = ch2;
                mCrc.Update(ch2);
                j2++;
            }
            else
            {
                currentState = 2;
                i2++;
                count = 0;
                SetupRandPartA();
            }
        }

        public override void Write(byte[] buffer, int offset, int length)
        {
            throw new NotSupportedException("BZip2InputStream Write not supported");
        }

        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("BZip2InputStream WriteByte not supported");
        }

        public override bool CanRead
        {
            get
            {
                return baseStream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return baseStream.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public bool IsStreamOwner
        {
            get
            {
                return isStreamOwner;
            }
            set
            {
                isStreamOwner = value;
            }
        }

        public override long Length
        {
            get
            {
                return baseStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return baseStream.Position;
            }
            set
            {
                throw new NotSupportedException("BZip2InputStream position cannot be set");
            }
        }
    }
}

