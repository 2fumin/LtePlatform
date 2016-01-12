using System;
using ZipLib.CheckSums;
using ZipLib.Streams;

namespace ZipLib.Comppression
{
    public class Inflater
    {
        private Adler32 adler;
        private static readonly int[] CPDEXT =
        { 
            0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 
            7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13
        };
        private static readonly int[] CPDIST =
        { 
            1, 2, 3, 4, 5, 7, 9, 13, 0x11, 0x19, 0x21, 0x31, 0x41, 0x61, 0x81, 0xc1, 
            0x101, 0x181, 0x201, 0x301, 0x401, 0x601, 0x801, 0xc01, 0x1001, 0x1801, 0x2001, 0x3001, 0x4001, 0x6001
        };
        private static readonly int[] CPLENS =
        { 
            3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 0x11, 0x13, 0x17, 0x1b, 0x1f, 
            0x23, 0x2b, 0x33, 0x3b, 0x43, 0x53, 0x63, 0x73, 0x83, 0xa3, 0xc3, 0xe3, 0x102
        };
        private static readonly int[] CPLEXT =
        { 
            0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 
            3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0
        };
        private const int DECODE_BLOCKS = 2;
        private const int DECODE_CHKSUM = 11;
        private const int DECODE_DICT = 1;
        private const int DECODE_DYN_HEADER = 6;
        private const int DECODE_HEADER = 0;
        private const int DECODE_HUFFMAN = 7;
        private const int DECODE_HUFFMAN_DIST = 9;
        private const int DECODE_HUFFMAN_DISTBITS = 10;
        private const int DECODE_HUFFMAN_LENBITS = 8;
        private const int DECODE_STORED = 5;
        private const int DECODE_STORED_LEN1 = 3;
        private const int DECODE_STORED_LEN2 = 4;
        private InflaterHuffmanTree distTree;
        private InflaterDynHeader dynHeader;
        private const int FINISHED = 12;
        private StreamManipulator input;
        private bool isLastBlock;
        private InflaterHuffmanTree litlenTree;
        private int mode;
        private int neededBits;
        private bool noHeader;
        private OutputWindow outputWindow;
        private int readAdler;
        private int repDist;
        private int repLength;
        private long totalIn;
        private long totalOut;
        private int uncomprLen;

        public Inflater()
            : this(false)
        {
        }

        public Inflater(bool noHeader)
        {
            this.noHeader = noHeader;
            adler = new Adler32();
            input = new StreamManipulator();
            outputWindow = new OutputWindow();
            mode = noHeader ? 2 : 0;
        }

        private bool Decode()
        {
            int num2;
            int num3;
            switch (mode)
            {
                case 0:
                    return DecodeHeader();

                case 1:
                    return DecodeDict();

                case 2:
                    if (!isLastBlock)
                    {
                        int num = input.PeekBits(3);
                        if (num < 0)
                        {
                            return false;
                        }
                        input.DropBits(3);
                        if ((num & 1) != 0)
                        {
                            isLastBlock = true;
                        }
                        switch ((num >> 1))
                        {
                            case 0:
                                input.SkipToByteBoundary();
                                mode = 3;
                                goto Label_0134;

                            case 1:
                                litlenTree = InflaterHuffmanTree.DefLitLenTree;
                                distTree = InflaterHuffmanTree.DefDistTree;
                                mode = 7;
                                goto Label_0134;

                            case 2:
                                dynHeader = new InflaterDynHeader();
                                mode = 6;
                                goto Label_0134;
                        }
                        throw new SharpZipBaseException("Unknown block type " + num);
                    }
                    if (!noHeader)
                    {
                        input.SkipToByteBoundary();
                        neededBits = 0x20;
                        mode = DECODE_CHKSUM;
                        return true;
                    }
                    mode = FINISHED;
                    return false;

                case 3:
                    uncomprLen = input.PeekBits(0x10);
                    if (uncomprLen >= 0)
                    {
                        input.DropBits(0x10);
                        mode = 4;
                        goto Label_0167;
                    }
                    return false;

                case 4:
                    goto Label_0167;

                case 5:
                    goto Label_01A9;

                case 6:
                    if (dynHeader.Decode(input))
                    {
                        litlenTree = dynHeader.BuildLitLenTree();
                        distTree = dynHeader.BuildDistTree();
                        mode = 7;
                        goto Label_022D;
                    }
                    return false;

                case 7:
                case 8:
                case 9:
                case 10:
                    goto Label_022D;

                case DECODE_CHKSUM:
                    return DecodeChksum();

                case FINISHED:
                    return false;

                default:
                    throw new SharpZipBaseException("Inflater.Decode unknown mode");
            }
        Label_0134:
            return true;
        Label_0167:
            num2 = input.PeekBits(0x10);
            if (num2 < 0)
            {
                return false;
            }
            input.DropBits(0x10);
            if (num2 != (uncomprLen ^ 0xffff))
            {
                throw new SharpZipBaseException("broken uncompressed block");
            }
            mode = 5;
        Label_01A9:
            num3 = outputWindow.CopyStored(input, uncomprLen);
            uncomprLen -= num3;
            if (uncomprLen == 0)
            {
                mode = 2;
                return true;
            }
            return !input.IsNeedingInput;
        Label_022D:
            return DecodeHuffman();
        }

        private bool DecodeChksum()
        {
            while (neededBits > 0)
            {
                int num = input.PeekBits(8);
                if (num < 0)
                {
                    return false;
                }
                input.DropBits(8);
                readAdler = (readAdler << 8) | num;
                neededBits -= 8;
            }
            if (((int)adler.Value) != readAdler)
            {
                throw new SharpZipBaseException(string.Concat(new object[] { "Adler chksum doesn't match: ", (int)adler.Value, " vs. ", readAdler }));
            }
            mode = FINISHED;
            return false;
        }

        private bool DecodeDict()
        {
            while (neededBits > 0)
            {
                int num = input.PeekBits(8);
                if (num < 0)
                {
                    return false;
                }
                input.DropBits(8);
                readAdler = (readAdler << 8) | num;
                neededBits -= 8;
            }
            return false;
        }

        private bool DecodeHeader()
        {
            int num = input.PeekBits(0x10);
            if (num < 0)
            {
                return false;
            }
            input.DropBits(0x10);
            num = ((num << 8) | (num >> 8)) & 0xffff;
            if ((num % 0x1f) != 0)
            {
                throw new SharpZipBaseException("Header checksum illegal");
            }
            if ((num & 0xf00) != 0x800)
            {
                throw new SharpZipBaseException("Compression Method unknown");
            }
            if ((num & 0x20) == 0)
            {
                mode = 2;
            }
            else
            {
                mode = 1;
                neededBits = 0x20;
            }
            return true;
        }

        private bool DecodeHuffman()
        {
            int freeSpace = outputWindow.GetFreeSpace();
            while (freeSpace >= 0x102)
            {
                int num2;
                switch (mode)
                {
                    case 7:
                        goto Label_0051;

                    case 8:
                        goto Label_00C5;

                    case 9:
                        goto Label_0114;

                    case DECODE_HUFFMAN_DISTBITS:
                        goto Label_0154;

                    default:
                        throw new SharpZipBaseException("Inflater unknown mode");
                }
            Label_0037:
                outputWindow.Write(num2);
                if (--freeSpace < 0x102)
                {
                    return true;
                }
            Label_0051:
                if (((num2 = litlenTree.GetSymbol(input)) & -256) == 0)
                {
                    goto Label_0037;
                }
                if (num2 < 0x101)
                {
                    if (num2 < 0)
                    {
                        return false;
                    }
                    distTree = null;
                    litlenTree = null;
                    mode = 2;
                    return true;
                }
                try
                {
                    repLength = CPLENS[num2 - 0x101];
                    neededBits = CPLEXT[num2 - 0x101];
                }
                catch (Exception)
                {
                    throw new SharpZipBaseException("Illegal rep length code");
                }
            Label_00C5:
                if (neededBits > 0)
                {
                    mode = 8;
                    int num3 = input.PeekBits(neededBits);
                    if (num3 < 0)
                    {
                        return false;
                    }
                    input.DropBits(neededBits);
                    repLength += num3;
                }
                mode = 9;
            Label_0114:
                num2 = distTree.GetSymbol(input);
                if (num2 < 0)
                {
                    return false;
                }
                try
                {
                    repDist = CPDIST[num2];
                    neededBits = CPDEXT[num2];
                }
                catch (Exception)
                {
                    throw new SharpZipBaseException("Illegal rep dist code");
                }
            Label_0154:
                if (neededBits > 0)
                {
                    mode = DECODE_HUFFMAN_DISTBITS;
                    int num4 = input.PeekBits(neededBits);
                    if (num4 < 0)
                    {
                        return false;
                    }
                    input.DropBits(neededBits);
                    repDist += num4;
                }
                outputWindow.Repeat(repLength, repDist);
                freeSpace -= repLength;
                mode = 7;
            }
            return true;
        }

        public int Inflate(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            return Inflate(buffer, 0, buffer.Length);
        }

        public int Inflate(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", "count cannot be negative");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "offset cannot be negative");
            }
            if ((offset + count) > buffer.Length)
            {
                throw new ArgumentException("count exceeds buffer bounds");
            }
            if (count == 0)
            {
                if (!IsFinished)
                {
                    Decode();
                }
                return 0;
            }
            int num = 0;
            do
            {
                if (mode != DECODE_CHKSUM)
                {
                    int num2 = outputWindow.CopyOutput(buffer, offset, count);
                    if (num2 > 0)
                    {
                        adler.Update(buffer, offset, num2);
                        offset += num2;
                        num += num2;
                        totalOut += num2;
                        count -= num2;
                        if (count == 0)
                        {
                            return num;
                        }
                    }
                }
            }
            while (Decode() || ((outputWindow.GetAvailable() > 0) && (mode != DECODE_CHKSUM)));
            return num;
        }

        public void Reset()
        {
            mode = noHeader ? 2 : 0;
            totalIn = 0L;
            totalOut = 0L;
            input.Reset();
            outputWindow.Reset();
            dynHeader = null;
            litlenTree = null;
            distTree = null;
            isLastBlock = false;
            adler.Reset();
        }

        public void SetDictionary(byte[] buffer)
        {
            SetDictionary(buffer, 0, buffer.Length);
        }

        public void SetDictionary(byte[] buffer, int index, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (!IsNeedingDictionary)
            {
                throw new InvalidOperationException("Dictionary is not needed");
            }
            adler.Update(buffer, index, count);
            if (((int)adler.Value) != readAdler)
            {
                throw new SharpZipBaseException("Wrong adler checksum");
            }
            adler.Reset();
            outputWindow.CopyDict(buffer, index, count);
            mode = 2;
        }

        public void SetInput(byte[] buffer)
        {
            SetInput(buffer, 0, buffer.Length);
        }

        public void SetInput(byte[] buffer, int index, int count)
        {
            input.SetInput(buffer, index, count);
            totalIn += count;
        }

        public int Adler
        {
            get
            {
                if (!IsNeedingDictionary)
                {
                    return (int)adler.Value;
                }
                return readAdler;
            }
        }

        public bool IsFinished
        {
            get
            {
                return ((mode == FINISHED) && (outputWindow.GetAvailable() == 0));
            }
        }

        public bool IsNeedingDictionary
        {
            get
            {
                return ((mode == 1) && (neededBits == 0));
            }
        }

        public bool IsNeedingInput
        {
            get
            {
                return input.IsNeedingInput;
            }
        }

        public int RemainingInput
        {
            get
            {
                return input.AvailableBytes;
            }
        }

        public long TotalIn
        {
            get
            {
                return (totalIn - RemainingInput);
            }
        }

        public long TotalOut
        {
            get
            {
                return totalOut;
            }
        }
    }
}
