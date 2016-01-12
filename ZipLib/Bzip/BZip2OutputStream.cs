using System;
using System.IO;
using System.Runtime.InteropServices;
using ZipLib.CheckSums;

namespace ZipLib.Bzip
{
    public class BZip2OutputStream : Stream
    {
        private int allowableBlockSize;
        private Stream baseStream;
        private byte[] block;
        private uint blockCRC;
        private bool blockRandomised;
        private int blockSize100k;
        private int bsBuff;
        private int bsLive;
        private int bytesOut;
        private uint combinedCRC;
        private int currentChar;
        private bool disposed_;
        private bool firstAttempt;
        private int[] ftab;
        private readonly int[] increments;
        private bool[] inUse;
        private bool isStreamOwner;
        private int last;
        private IChecksum mCrc;
        private int[] mtfFreq;
        private int nBlocksRandomised;
        private int nInUse;
        private int nMTF;
        private int origPtr;
        private int[] quadrant;
        private int runLength;
        private char[] selector;
        private char[] selectorMtf;
        private char[] seqToUnseq;
        private short[] szptr;
        private char[] unseqToSeq;
        private int workDone;
        private int workFactor;
        private int workLimit;
        private int[] zptr;

        public BZip2OutputStream(Stream stream)
            : this(stream, 9)
        {
        }

        public BZip2OutputStream(Stream stream, int blockSize)
        {
            increments 
                = new[] { 1, 4, 13, 40, 0x79, 0x16c, 0x445, 0xcd0, 0x2671, 0x7354, 0x159fd, 0x40df8, 0xc29e9, 0x247dbc };
            isStreamOwner = true;
            mCrc = new StrangeCRC();
            inUse = new bool[0x100];
            seqToUnseq = new char[0x100];
            unseqToSeq = new char[0x100];
            selector = new char[0x4652];
            selectorMtf = new char[0x4652];
            mtfFreq = new int[0x102];
            currentChar = -1;
            BsSetStream(stream);
            workFactor = 50;
            if (blockSize > 9)
            {
                blockSize = 9;
            }
            if (blockSize < 1)
            {
                blockSize = 1;
            }
            blockSize100k = blockSize;
            AllocateCompressStructures();
            Initialize();
            InitBlock();
        }

        private void AllocateCompressStructures()
        {
            int num = 0x186a0 * blockSize100k;
            block = new byte[(num + 1) + 20];
            quadrant = new int[num + 20];
            zptr = new int[num];
            ftab = new int[0x10001];
            szptr = new short[2 * num];
        }

        private void BsFinishedWithStream()
        {
            while (bsLive > 0)
            {
                int num = bsBuff >> 0x18;
                baseStream.WriteByte((byte)num);
                bsBuff = bsBuff << 8;
                bsLive -= 8;
                bytesOut++;
            }
        }

        private void BsPutint(int u)
        {
            BsW(8, (u >> 0x18) & 0xff);
            BsW(8, (u >> 0x10) & 0xff);
            BsW(8, (u >> 8) & 0xff);
            BsW(8, u & 0xff);
        }

        private void BsPutIntVS(int numBits, int c)
        {
            BsW(numBits, c);
        }

        private void BsPutUChar(int c)
        {
            BsW(8, c);
        }

        private void BsSetStream(Stream stream)
        {
            baseStream = stream;
            bsLive = 0;
            bsBuff = 0;
            bytesOut = 0;
        }

        private void BsW(int n, int v)
        {
            while (bsLive >= 8)
            {
                int num = bsBuff >> 0x18;
                baseStream.WriteByte((byte)num);
                bsBuff = bsBuff << 8;
                bsLive -= 8;
                bytesOut++;
            }
            bsBuff |= v << ((0x20 - bsLive) - n);
            bsLive += n;
        }

        public override void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
                if (!disposed_)
                {
                    disposed_ = true;
                    if (runLength > 0)
                    {
                        WriteRun();
                    }
                    currentChar = -1;
                    EndBlock();
                    EndCompression();
                    Flush();
                }
            }
            finally
            {
                if (disposing && IsStreamOwner)
                {
                    baseStream.Close();
                }
            }
        }

        private void DoReversibleTransformation()
        {
            workLimit = workFactor * last;
            workDone = 0;
            blockRandomised = false;
            firstAttempt = true;
            MainSort();
            if ((workDone > workLimit) && firstAttempt)
            {
                RandomiseBlock();
                workLimit = workDone = 0;
                blockRandomised = true;
                firstAttempt = false;
                MainSort();
            }
            origPtr = -1;
            for (int i = 0; i <= last; i++)
            {
                if (zptr[i] == 0)
                {
                    origPtr = i;
                    break;
                }
            }
            if (origPtr == -1)
            {
                Panic();
            }
        }

        private void EndBlock()
        {
            if (last >= 0)
            {
                blockCRC = (uint)mCrc.Value;
                combinedCRC = (combinedCRC << 1) | (combinedCRC >> 0x1f);
                combinedCRC ^= blockCRC;
                DoReversibleTransformation();
                BsPutUChar(0x31);
                BsPutUChar(0x41);
                BsPutUChar(0x59);
                BsPutUChar(0x26);
                BsPutUChar(0x53);
                BsPutUChar(0x59);
                BsPutint((int)blockCRC);
                if (blockRandomised)
                {
                    BsW(1, 1);
                    nBlocksRandomised++;
                }
                else
                {
                    BsW(1, 0);
                }
                MoveToFrontCodeAndSend();
            }
        }

        private void EndCompression()
        {
            BsPutUChar(0x17);
            BsPutUChar(0x72);
            BsPutUChar(0x45);
            BsPutUChar(0x38);
            BsPutUChar(80);
            BsPutUChar(0x90);
            BsPutint((int)combinedCRC);
            BsFinishedWithStream();
        }

        ~BZip2OutputStream()
        {
            Dispose(false);
        }

        public override void Flush()
        {
            baseStream.Flush();
        }

        private bool FullGtU(int i1, int i2)
        {
            byte num2 = block[i1 + 1];
            byte num3 = block[i2 + 1];
            if (num2 != num3)
            {
                return (num2 > num3);
            }
            i1++;
            i2++;
            num2 = block[i1 + 1];
            num3 = block[i2 + 1];
            if (num2 != num3)
            {
                return (num2 > num3);
            }
            i1++;
            i2++;
            num2 = block[i1 + 1];
            num3 = block[i2 + 1];
            if (num2 != num3)
            {
                return (num2 > num3);
            }
            i1++;
            i2++;
            num2 = block[i1 + 1];
            num3 = block[i2 + 1];
            if (num2 != num3)
            {
                return (num2 > num3);
            }
            i1++;
            i2++;
            num2 = block[i1 + 1];
            num3 = block[i2 + 1];
            if (num2 != num3)
            {
                return (num2 > num3);
            }
            i1++;
            i2++;
            num2 = block[i1 + 1];
            num3 = block[i2 + 1];
            if (num2 != num3)
            {
                return (num2 > num3);
            }
            i1++;
            i2++;
            int num = last + 1;
            do
            {
                num2 = block[i1 + 1];
                num3 = block[i2 + 1];
                if (num2 != num3)
                {
                    return (num2 > num3);
                }
                int num4 = quadrant[i1];
                int num5 = quadrant[i2];
                if (num4 != num5)
                {
                    return (num4 > num5);
                }
                i1++;
                i2++;
                num2 = block[i1 + 1];
                num3 = block[i2 + 1];
                if (num2 != num3)
                {
                    return (num2 > num3);
                }
                num4 = quadrant[i1];
                num5 = quadrant[i2];
                if (num4 != num5)
                {
                    return (num4 > num5);
                }
                i1++;
                i2++;
                num2 = block[i1 + 1];
                num3 = block[i2 + 1];
                if (num2 != num3)
                {
                    return (num2 > num3);
                }
                num4 = quadrant[i1];
                num5 = quadrant[i2];
                if (num4 != num5)
                {
                    return (num4 > num5);
                }
                i1++;
                i2++;
                num2 = block[i1 + 1];
                num3 = block[i2 + 1];
                if (num2 != num3)
                {
                    return (num2 > num3);
                }
                num4 = quadrant[i1];
                num5 = quadrant[i2];
                if (num4 != num5)
                {
                    return (num4 > num5);
                }
                i1++;
                i2++;
                if (i1 > last)
                {
                    i1 -= last;
                    i1--;
                }
                if (i2 > last)
                {
                    i2 -= last;
                    i2--;
                }
                num -= 4;
                workDone++;
            }
            while (num >= 0);
            return false;
        }

        private void GenerateMTFValues()
        {
            int num;
            char[] chArray = new char[0x100];
            MakeMaps();
            int index = nInUse + 1;
            for (num = 0; num <= index; num++)
            {
                mtfFreq[num] = 0;
            }
            int num4 = 0;
            int num3 = 0;
            for (num = 0; num < nInUse; num++)
            {
                chArray[num] = (char)num;
            }
            for (num = 0; num <= last; num++)
            {
                int num2 = 0;
                char ch = chArray[num2];
                while (unseqToSeq[block[zptr[num]]] != ch)
                {
                    num2++;
                    char ch2 = ch;
                    ch = chArray[num2];
                    chArray[num2] = ch2;
                }
                chArray[0] = ch;
                if (num2 == 0)
                {
                    num3++;
                    continue;
                }
                if (num3 <= 0)
                {
                    goto Label_0126;
                }
                num3--;
            Label_00A9:
                switch ((num3 % 2))
                {
                    case 0:
                        szptr[num4] = 0;
                        num4++;
                        mtfFreq[0]++;
                        break;

                    case 1:
                        szptr[num4] = 1;
                        num4++;
                        mtfFreq[1]++;
                        break;
                }
                if (num3 >= 2)
                {
                    num3 = (num3 - 2) / 2;
                    goto Label_00A9;
                }
                num3 = 0;
            Label_0126:
                szptr[num4] = (short)(num2 + 1);
                num4++;
                mtfFreq[num2 + 1]++;
            }
            if (num3 <= 0)
            {
                goto Label_01EC;
            }
            num3--;
        Label_0172:
            switch ((num3 % 2))
            {
                case 0:
                    szptr[num4] = 0;
                    num4++;
                    mtfFreq[0]++;
                    break;

                case 1:
                    szptr[num4] = 1;
                    num4++;
                    mtfFreq[1]++;
                    break;
            }
            if (num3 >= 2)
            {
                num3 = (num3 - 2) / 2;
                goto Label_0172;
            }
        Label_01EC:
            szptr[num4] = (short)index;
            num4++;
            mtfFreq[index]++;
            nMTF = num4;
        }

        private static void HbAssignCodes(int[] code, char[] length, int minLen, int maxLen, int alphaSize)
        {
            int num = 0;
            for (int i = minLen; i <= maxLen; i++)
            {
                for (int j = 0; j < alphaSize; j++)
                {
                    if (length[j] == i)
                    {
                        code[j] = num;
                        num++;
                    }
                }
                num = num << 1;
            }
        }

        private static void HbMakeCodeLengths(char[] len, int[] freq, int alphaSize, int maxLen)
        {
            int[] numArray = new int[260];
            int[] numArray2 = new int[0x204];
            int[] numArray3 = new int[0x204];
            for (int i = 0; i < alphaSize; i++)
            {
                numArray2[i + 1] = ((freq[i] == 0) ? 1 : freq[i]) << 8;
            }
            while (true)
            {
                int num5;
                int index = alphaSize;
                int num2 = 0;
                numArray[0] = 0;
                numArray2[0] = 0;
                numArray3[0] = -2;
                for (int j = 1; j <= alphaSize; j++)
                {
                    numArray3[j] = -1;
                    num2++;
                    numArray[num2] = j;
                    int num9 = num2;
                    int num10 = numArray[num9];
                    while (numArray2[num10] < numArray2[numArray[num9 >> 1]])
                    {
                        numArray[num9] = numArray[num9 >> 1];
                        num9 = num9 >> 1;
                    }
                    numArray[num9] = num10;
                }
                if (num2 >= 260)
                {
                    Panic();
                }
                while (num2 > 1)
                {
                    int num3 = numArray[1];
                    numArray[1] = numArray[num2];
                    num2--;
                    int num11 = 1;
                    int num13 = numArray[num11];
                Label_00E7:
                    int num12 = num11 << 1;
                    if (num12 <= num2)
                    {
                        if ((num12 < num2) && (numArray2[numArray[num12 + 1]] < numArray2[numArray[num12]]))
                        {
                            num12++;
                        }
                        if (numArray2[num13] >= numArray2[numArray[num12]])
                        {
                            numArray[num11] = numArray[num12];
                            num11 = num12;
                            goto Label_00E7;
                        }
                    }
                    numArray[num11] = num13;
                    int num4 = numArray[1];
                    numArray[1] = numArray[num2];
                    num2--;
                    num11 = 1;
                    num13 = numArray[num11];
                Label_0155:
                    num12 = num11 << 1;
                    if (num12 <= num2)
                    {
                        if ((num12 < num2) && (numArray2[numArray[num12 + 1]] < numArray2[numArray[num12]]))
                        {
                            num12++;
                        }
                        if (numArray2[num13] >= numArray2[numArray[num12]])
                        {
                            numArray[num11] = numArray[num12];
                            num11 = num12;
                            goto Label_0155;
                        }
                    }
                    numArray[num11] = num13;
                    index++;
                    numArray3[num3] = numArray3[num4] = index;
                    numArray2[index] = ((int)((numArray2[num3] & 0xffffff00L) + (numArray2[num4] & 0xffffff00L))) | (1 + (((numArray2[num3] & 0xff) > (numArray2[num4] & 0xff)) ? (numArray2[num3] & 0xff) : (numArray2[num4] & 0xff)));
                    numArray3[index] = -1;
                    num2++;
                    numArray[num2] = index;
                    num11 = num2;
                    num13 = numArray[num11];
                    while (numArray2[num13] < numArray2[numArray[num11 >> 1]])
                    {
                        numArray[num11] = numArray[num11 >> 1];
                        num11 = num11 >> 1;
                    }
                    numArray[num11] = num13;
                }
                if (index >= 0x204)
                {
                    Panic();
                }
                bool flag = false;
                for (int k = 1; k <= alphaSize; k++)
                {
                    num5 = 0;
                    int num6 = k;
                    while (numArray3[num6] >= 0)
                    {
                        num6 = numArray3[num6];
                        num5++;
                    }
                    len[k - 1] = (char)num5;
                    if (num5 > maxLen)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return;
                }
                for (int m = 1; m < alphaSize; m++)
                {
                    num5 = numArray2[m] >> 8;
                    num5 = 1 + (num5 / 2);
                    numArray2[m] = num5 << 8;
                }
            }
        }

        private void InitBlock()
        {
            mCrc.Reset();
            last = -1;
            for (int i = 0; i < 0x100; i++)
            {
                inUse[i] = false;
            }
            allowableBlockSize = (0x186a0 * blockSize100k) - 20;
        }

        private void Initialize()
        {
            bytesOut = 0;
            nBlocksRandomised = 0;
            BsPutUChar(0x42);
            BsPutUChar(90);
            BsPutUChar(0x68);
            BsPutUChar(0x30 + blockSize100k);
            combinedCRC = 0;
        }

        private void MainSort()
        {
            int num;
            int[] numArray = new int[0x100];
            int[] numArray2 = new int[0x100];
            bool[] flagArray = new bool[0x100];
            for (num = 0; num < 20; num++)
            {
                block[(last + num) + 2] = block[(num % (last + 1)) + 1];
            }
            for (num = 0; num <= (last + 20); num++)
            {
                quadrant[num] = 0;
            }
            block[0] = block[last + 1];
            if (last < 0xfa0)
            {
                for (num = 0; num <= last; num++)
                {
                    zptr[num] = num;
                }
                firstAttempt = false;
                workDone = workLimit = 0;
                SimpleSort(0, last, 0);
            }
            else
            {
                int num2;
                int num6;
                for (num = 0; num <= 0xff; num++)
                {
                    flagArray[num] = false;
                }
                for (num = 0; num <= 0x10000; num++)
                {
                    ftab[num] = 0;
                }
                int index = block[0];
                for (num = 0; num <= last; num++)
                {
                    num6 = block[num + 1];
                    ftab[(index << 8) + num6]++;
                    index = num6;
                }
                for (num = 1; num <= 0x10000; num++)
                {
                    ftab[num] += ftab[num - 1];
                }
                index = block[1];
                for (num = 0; num < last; num++)
                {
                    num6 = block[num + 2];
                    num2 = (index << 8) + num6;
                    index = num6;
                    ftab[num2]--;
                    zptr[ftab[num2]] = num;
                }
                num2 = (block[last + 1] << 8) + block[1];
                ftab[num2]--;
                zptr[ftab[num2]] = last;
                num = 0;
                while (num <= 0xff)
                {
                    numArray[num] = num;
                    num++;
                }
                int num9 = 1;
                do
                {
                    num9 = (3 * num9) + 1;
                }
                while (num9 <= 0x100);
                do
                {
                    num9 /= 3;
                    num = num9;
                    while (num <= 0xff)
                    {
                        int num8 = numArray[num];
                        num2 = num;
                        while ((ftab[(numArray[num2 - num9] + 1) << 8] - ftab[numArray[num2 - num9] << 8]) > (ftab[(num8 + 1) << 8] - ftab[num8 << 8]))
                        {
                            numArray[num2] = numArray[num2 - num9];
                            num2 -= num9;
                            if (num2 <= (num9 - 1))
                            {
                                break;
                            }
                        }
                        numArray[num2] = num8;
                        num++;
                    }
                }
                while (num9 != 1);
                for (num = 0; num <= 0xff; num++)
                {
                    int num3 = numArray[num];
                    num2 = 0;
                    while (num2 <= 0xff)
                    {
                        int num4 = (num3 << 8) + num2;
                        if ((ftab[num4] & 0x200000) != 0x200000)
                        {
                            int loSt = ftab[num4] & -2097153;
                            int hiSt = (ftab[num4 + 1] & -2097153) - 1;
                            if (hiSt > loSt)
                            {
                                QSort3(loSt, hiSt, 2);
                                if ((workDone > workLimit) && firstAttempt)
                                {
                                    return;
                                }
                            }
                            ftab[num4] |= 0x200000;
                        }
                        num2++;
                    }
                    flagArray[num3] = true;
                    if (num < 0xff)
                    {
                        int num12 = ftab[num3 << 8] & -2097153;
                        int num13 = (ftab[(num3 + 1) << 8] & -2097153) - num12;
                        int num14 = 0;
                        while ((num13 >> num14) > 0xfffe)
                        {
                            num14++;
                        }
                        num2 = 0;
                        while (num2 < num13)
                        {
                            int num15 = zptr[num12 + num2];
                            int num16 = num2 >> num14;
                            quadrant[num15] = num16;
                            if (num15 < 20)
                            {
                                quadrant[(num15 + last) + 1] = num16;
                            }
                            num2++;
                        }
                        if (((num13 - 1) >> num14) > 0xffff)
                        {
                            Panic();
                        }
                    }
                    num2 = 0;
                    while (num2 <= 0xff)
                    {
                        numArray2[num2] = ftab[(num2 << 8) + num3] & -2097153;
                        num2++;
                    }
                    num2 = ftab[num3 << 8] & -2097153;
                    while (num2 < (ftab[(num3 + 1) << 8] & -2097153))
                    {
                        index = block[zptr[num2]];
                        if (!flagArray[index])
                        {
                            zptr[numArray2[index]] = (zptr[num2] == 0) ? last : (zptr[num2] - 1);
                            numArray2[index]++;
                        }
                        num2++;
                    }
                    for (num2 = 0; num2 <= 0xff; num2++)
                    {
                        ftab[(num2 << 8) + num3] |= 0x200000;
                    }
                }
            }
        }

        private void MakeMaps()
        {
            nInUse = 0;
            for (int i = 0; i < 0x100; i++)
            {
                if (inUse[i])
                {
                    seqToUnseq[nInUse] = (char)i;
                    unseqToSeq[i] = (char)nInUse;
                    nInUse++;
                }
            }
        }

        private static byte Med3(byte a, byte b, byte c)
        {
            byte num;
            if (a > b)
            {
                num = a;
                a = b;
                b = num;
            }
            if (b > c)
            {
                b = c;
            }
            if (a > b)
            {
                b = a;
            }
            return b;
        }

        private void MoveToFrontCodeAndSend()
        {
            BsPutIntVS(0x18, origPtr);
            GenerateMTFValues();
            SendMTFValues();
        }

        private static void Panic()
        {
            throw new BZip2Exception("BZip2 output stream panic");
        }

        private void QSort3(int loSt, int hiSt, int dSt)
        {
            StackElement[] elementArray = new StackElement[0x3e8];
            int index = 0;
            elementArray[index].ll = loSt;
            elementArray[index].hh = hiSt;
            elementArray[index].dd = dSt;
            index++;
            while (index > 0)
            {
                int num3;
                int num4;
                int num6;
                if (index >= 0x3e8)
                {
                    Panic();
                }
                index--;
                int ll = elementArray[index].ll;
                int hh = elementArray[index].hh;
                int dd = elementArray[index].dd;
                if (((hh - ll) < 20) || (dd > 10))
                {
                    SimpleSort(ll, hh, dd);
                    if ((workDone > workLimit) && firstAttempt)
                    {
                        return;
                    }
                    continue;
                }
                int num5 = Med3(block[(zptr[ll] + dd) + 1], block[(zptr[hh] + dd) + 1], block[(zptr[(ll + hh) >> 1] + dd) + 1]);
                int num = num3 = ll;
                int num2 = num4 = hh;
            Label_0118:
                if (num <= num2)
                {
                    num6 = block[(zptr[num] + dd) + 1] - num5;
                    if (num6 == 0)
                    {
                        int num12 = zptr[num];
                        zptr[num] = zptr[num3];
                        zptr[num3] = num12;
                        num3++;
                        num++;
                        goto Label_0118;
                    }
                    if (num6 <= 0)
                    {
                        num++;
                        goto Label_0118;
                    }
                }
            Label_0172:
                if (num <= num2)
                {
                    num6 = block[(zptr[num2] + dd) + 1] - num5;
                    if (num6 == 0)
                    {
                        int num13 = zptr[num2];
                        zptr[num2] = zptr[num4];
                        zptr[num4] = num13;
                        num4--;
                        num2--;
                        goto Label_0172;
                    }
                    if (num6 >= 0)
                    {
                        num2--;
                        goto Label_0172;
                    }
                }
                if (num <= num2)
                {
                    int num14 = zptr[num];
                    zptr[num] = zptr[num2];
                    zptr[num2] = num14;
                    num++;
                    num2--;
                    goto Label_0118;
                }
                if (num4 < num3)
                {
                    elementArray[index].ll = ll;
                    elementArray[index].hh = hh;
                    elementArray[index].dd = dd + 1;
                    index++;
                }
                else
                {
                    num6 = ((num3 - ll) < (num - num3)) ? (num3 - ll) : (num - num3);
                    Vswap(ll, num - num6, num6);
                    int n = ((hh - num4) < (num4 - num2)) ? (hh - num4) : (num4 - num2);
                    Vswap(num, (hh - n) + 1, n);
                    num6 = ((ll + num) - num3) - 1;
                    n = (hh - (num4 - num2)) + 1;
                    elementArray[index].ll = ll;
                    elementArray[index].hh = num6;
                    elementArray[index].dd = dd;
                    index++;
                    elementArray[index].ll = num6 + 1;
                    elementArray[index].hh = n - 1;
                    elementArray[index].dd = dd + 1;
                    index++;
                    elementArray[index].ll = n;
                    elementArray[index].hh = hh;
                    elementArray[index].dd = dd;
                    index++;
                }
            }
        }

        private void RandomiseBlock()
        {
            int num;
            int num2 = 0;
            int index = 0;
            for (num = 0; num < 0x100; num++)
            {
                inUse[num] = false;
            }
            for (num = 0; num <= last; num++)
            {
                if (num2 == 0)
                {
                    num2 = BZip2Constants.RandomNumbers[index];
                    index++;
                    if (index == 0x200)
                    {
                        index = 0;
                    }
                }
                num2--;
                block[num + 1] = (byte)(block[num + 1] ^ ((num2 == 1) ? 1 : 0));
                block[num + 1] = (byte)(block[num + 1] & 0xff);
                inUse[block[num + 1]] = true;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("BZip2OutputStream Read not supported");
        }

        public override int ReadByte()
        {
            throw new NotSupportedException("BZip2OutputStream ReadByte not supported");
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("BZip2OutputStream Seek not supported");
        }

        private void SendMTFValues()
        {
            int num3;
            int num13;
            char[][] chArray = new char[6][];
            for (int i = 0; i < 6; i++)
            {
                chArray[i] = new char[0x102];
            }
            int index = 0;
            int alphaSize = nInUse + 2;
            for (int j = 0; j < 6; j++)
            {
                for (int num15 = 0; num15 < alphaSize; num15++)
                {
                    chArray[j][num15] = '\x000f';
                }
            }
            if (nMTF <= 0)
            {
                Panic();
            }
            if (nMTF < 200)
            {
                num13 = 2;
            }
            else if (nMTF < 600)
            {
                num13 = 3;
            }
            else if (nMTF < 0x4b0)
            {
                num13 = 4;
            }
            else if (nMTF < 0x960)
            {
                num13 = 5;
            }
            else
            {
                num13 = 6;
            }
            int num16 = num13;
            int num2 = 0;
            while (num16 > 0)
            {
                int num18 = nMTF / num16;
                int num19 = 0;
                num3 = num2 - 1;
                while ((num19 < num18) && (num3 < (alphaSize - 1)))
                {
                    num3++;
                    num19 += mtfFreq[num3];
                }
                if (((num3 > num2) && (num16 != num13)) && ((num16 != 1) && (((num13 - num16) % 2) == 1)))
                {
                    num19 -= mtfFreq[num3];
                    num3--;
                }
                for (int num20 = 0; num20 < alphaSize; num20++)
                {
                    if ((num20 >= num2) && (num20 <= num3))
                    {
                        chArray[num16 - 1][num20] = '\0';
                    }
                    else
                    {
                        chArray[num16 - 1][num20] = '\x000f';
                    }
                }
                num16--;
                num2 = num3 + 1;
                nMTF -= num19;
            }
            int[][] numArray = new int[6][];
            for (int k = 0; k < 6; k++)
            {
                numArray[k] = new int[0x102];
            }
            int[] numArray2 = new int[6];
            short[] numArray3 = new short[6];
            for (int m = 0; m < 4; m++)
            {
                for (int num22 = 0; num22 < num13; num22++)
                {
                    numArray2[num22] = 0;
                }
                for (int num23 = 0; num23 < num13; num23++)
                {
                    for (int num24 = 0; num24 < alphaSize; num24++)
                    {
                        numArray[num23][num24] = 0;
                    }
                }
                index = 0;
                num2 = 0;
                while (true)
                {
                    if (num2 >= nMTF)
                    {
                        break;
                    }
                    num3 = (num2 + 50) - 1;
                    if (num3 >= nMTF)
                    {
                        num3 = nMTF - 1;
                    }
                    for (int num25 = 0; num25 < num13; num25++)
                    {
                        numArray3[num25] = 0;
                    }
                    if (num13 == 6)
                    {
                        short num27;
                        short num28;
                        short num29;
                        short num30;
                        short num31;
                        short num26 = num27 = num28 = num29 = num30 = num31 = 0;
                        for (int num32 = num2; num32 <= num3; num32++)
                        {
                            short num33 = szptr[num32];
                            num26 = (short)(num26 + ((short)chArray[0][num33]));
                            num27 = (short)(num27 + ((short)chArray[1][num33]));
                            num28 = (short)(num28 + ((short)chArray[2][num33]));
                            num29 = (short)(num29 + ((short)chArray[3][num33]));
                            num30 = (short)(num30 + ((short)chArray[4][num33]));
                            num31 = (short)(num31 + ((short)chArray[5][num33]));
                        }
                        numArray3[0] = num26;
                        numArray3[1] = num27;
                        numArray3[2] = num28;
                        numArray3[3] = num29;
                        numArray3[4] = num30;
                        numArray3[5] = num31;
                    }
                    else
                    {
                        for (int num34 = num2; num34 <= num3; num34++)
                        {
                            short num35 = szptr[num34];
                            for (int num36 = 0; num36 < num13; num36++)
                            {
                                numArray3[num36] = (short)(numArray3[num36] + ((short)chArray[num36][num35]));
                            }
                        }
                    }
                    int num6 = 0x3b9ac9ff;
                    int num5 = -1;
                    for (int num37 = 0; num37 < num13; num37++)
                    {
                        if (numArray3[num37] < num6)
                        {
                            num6 = numArray3[num37];
                            num5 = num37;
                        }
                    }
                    numArray2[num5]++;
                    selector[index] = (char)num5;
                    index++;
                    for (int num38 = num2; num38 <= num3; num38++)
                    {
                        numArray[num5][szptr[num38]]++;
                    }
                    num2 = num3 + 1;
                }
                for (int num39 = 0; num39 < num13; num39++)
                {
                    HbMakeCodeLengths(chArray[num39], numArray[num39], alphaSize, 20);
                }
            }
            if (num13 >= 8)
            {
                Panic();
            }
            if ((index >= 0x8000) || (index > 0x4652))
            {
                Panic();
            }
            char[] chArray2 = new char[6];
            for (int n = 0; n < num13; n++)
            {
                chArray2[n] = (char)n;
            }
            for (int num41 = 0; num41 < index; num41++)
            {
                int num42 = 0;
                char ch3 = chArray2[num42];
                while (selector[num41] != ch3)
                {
                    num42++;
                    char ch2 = ch3;
                    ch3 = chArray2[num42];
                    chArray2[num42] = ch2;
                }
                chArray2[0] = ch3;
                selectorMtf[num41] = (char)num42;
            }
            int[][] numArray4 = new int[6][];
            for (int num43 = 0; num43 < 6; num43++)
            {
                numArray4[num43] = new int[0x102];
            }
            for (int num44 = 0; num44 < num13; num44++)
            {
                int minLen = 0x20;
                int maxLen = 0;
                for (int num45 = 0; num45 < alphaSize; num45++)
                {
                    if (chArray[num44][num45] > maxLen)
                    {
                        maxLen = chArray[num44][num45];
                    }
                    if (chArray[num44][num45] < minLen)
                    {
                        minLen = chArray[num44][num45];
                    }
                }
                if (maxLen > 20)
                {
                    Panic();
                }
                if (minLen < 1)
                {
                    Panic();
                }
                HbAssignCodes(numArray4[num44], chArray[num44], minLen, maxLen, alphaSize);
            }
            bool[] flagArray = new bool[0x10];
            for (int num46 = 0; num46 < 0x10; num46++)
            {
                flagArray[num46] = false;
                for (int num47 = 0; num47 < 0x10; num47++)
                {
                    if (inUse[(num46 * 0x10) + num47])
                    {
                        flagArray[num46] = true;
                    }
                }
            }
            for (int num48 = 0; num48 < 0x10; num48++)
            {
                if (flagArray[num48])
                {
                    BsW(1, 1);
                }
                else
                {
                    BsW(1, 0);
                }
            }
            for (int num49 = 0; num49 < 0x10; num49++)
            {
                if (flagArray[num49])
                {
                    for (int num50 = 0; num50 < 0x10; num50++)
                    {
                        if (inUse[(num49 * 0x10) + num50])
                        {
                            BsW(1, 1);
                        }
                        else
                        {
                            BsW(1, 0);
                        }
                    }
                }
            }
            BsW(3, num13);
            BsW(15, index);
            for (int num51 = 0; num51 < index; num51++)
            {
                for (int num52 = 0; num52 < selectorMtf[num51]; num52++)
                {
                    BsW(1, 1);
                }
                BsW(1, 0);
            }
            for (int num53 = 0; num53 < num13; num53++)
            {
                int v = chArray[num53][0];
                BsW(5, v);
                int num55 = 0;
                goto Label_0691;
            Label_064F:
                BsW(2, 2);
                v++;
            Label_065D:
                if (v < chArray[num53][num55])
                {
                    goto Label_064F;
                }
                while (v > chArray[num53][num55])
                {
                    BsW(2, 3);
                    v--;
                }
                BsW(1, 0);
                num55++;
            Label_0691:
                if (num55 < alphaSize)
                {
                    goto Label_065D;
                }
            }
            int num12 = 0;
            num2 = 0;
            while (true)
            {
                if (num2 >= nMTF)
                {
                    break;
                }
                num3 = (num2 + 50) - 1;
                if (num3 >= nMTF)
                {
                    num3 = nMTF - 1;
                }
                for (int num56 = num2; num56 <= num3; num56++)
                {
                    BsW(chArray[selector[num12]][szptr[num56]], numArray4[selector[num12]][szptr[num56]]);
                }
                num2 = num3 + 1;
                num12++;
            }
            if (num12 != index)
            {
                Panic();
            }
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("BZip2OutputStream SetLength not supported");
        }

        private void SimpleSort(int lo, int hi, int d)
        {
            int num4 = (hi - lo) + 1;
            if (num4 >= 2)
            {
                int index = 0;
                while (increments[index] < num4)
                {
                    index++;
                }
                index--;
                while (index >= 0)
                {
                    int num3 = increments[index];
                    int num = lo + num3;
                    do
                    {
                        if (num > hi)
                        {
                            goto Label_0160;
                        }
                        int num6 = zptr[num];
                        int num2 = num;
                        while (FullGtU(zptr[num2 - num3] + d, num6 + d))
                        {
                            zptr[num2] = zptr[num2 - num3];
                            num2 -= num3;
                            if (num2 <= ((lo + num3) - 1))
                            {
                                break;
                            }
                        }
                        zptr[num2] = num6;
                        num++;
                        if (num > hi)
                        {
                            goto Label_0160;
                        }
                        num6 = zptr[num];
                        num2 = num;
                        while (FullGtU(zptr[num2 - num3] + d, num6 + d))
                        {
                            zptr[num2] = zptr[num2 - num3];
                            num2 -= num3;
                            if (num2 <= ((lo + num3) - 1))
                            {
                                break;
                            }
                        }
                        zptr[num2] = num6;
                        num++;
                        if (num > hi)
                        {
                            goto Label_0160;
                        }
                        num6 = zptr[num];
                        num2 = num;
                        while (FullGtU(zptr[num2 - num3] + d, num6 + d))
                        {
                            zptr[num2] = zptr[num2 - num3];
                            num2 -= num3;
                            if (num2 <= ((lo + num3) - 1))
                            {
                                break;
                            }
                        }
                        zptr[num2] = num6;
                        num++;
                    }
                    while ((workDone <= workLimit) || !firstAttempt);
                    return;
                Label_0160:
                    index--;
                }
            }
        }

        private void Vswap(int p1, int p2, int n)
        {
            while (n > 0)
            {
                int num = zptr[p1];
                zptr[p1] = zptr[p2];
                zptr[p2] = num;
                p1++;
                p2++;
                n--;
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if ((buffer.Length - offset) < count)
            {
                throw new ArgumentException("Offset/count out of range");
            }
            for (int i = 0; i < count; i++)
            {
                WriteByte(buffer[offset + i]);
            }
        }

        public override void WriteByte(byte value)
        {
            int num = (0x100 + value) % 0x100;
            if (currentChar != -1)
            {
                if (currentChar != num)
                {
                    WriteRun();
                    runLength = 1;
                    currentChar = num;
                }
                else
                {
                    runLength++;
                    if (runLength > 0xfe)
                    {
                        WriteRun();
                        currentChar = -1;
                        runLength = 0;
                    }
                }
            }
            else
            {
                currentChar = num;
                runLength++;
            }
        }

        private void WriteRun()
        {
            if (last < allowableBlockSize)
            {
                inUse[currentChar] = true;
                for (int i = 0; i < runLength; i++)
                {
                    mCrc.Update(currentChar);
                }
                switch (runLength)
                {
                    case 1:
                        last++;
                        block[last + 1] = (byte)currentChar;
                        return;

                    case 2:
                        last++;
                        block[last + 1] = (byte)currentChar;
                        last++;
                        block[last + 1] = (byte)currentChar;
                        return;

                    case 3:
                        last++;
                        block[last + 1] = (byte)currentChar;
                        last++;
                        block[last + 1] = (byte)currentChar;
                        last++;
                        block[last + 1] = (byte)currentChar;
                        return;
                }
                inUse[runLength - 4] = true;
                last++;
                block[last + 1] = (byte)currentChar;
                last++;
                block[last + 1] = (byte)currentChar;
                last++;
                block[last + 1] = (byte)currentChar;
                last++;
                block[last + 1] = (byte)currentChar;
                last++;
                block[last + 1] = (byte)(runLength - 4);
            }
            else
            {
                EndBlock();
                InitBlock();
                WriteRun();
            }
        }

        public int BytesWritten
        {
            get
            {
                return bytesOut;
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
                return baseStream.CanWrite;
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
                throw new NotSupportedException("BZip2OutputStream position cannot be set");
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct StackElement
        {
            public int ll;
            public int hh;
            public int dd;
        }
    }
}

