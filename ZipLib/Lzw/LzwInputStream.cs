using System;
using System.IO;

namespace Lte.Domain.ZipLib.Lzw
{
    public class LzwInputStream : Stream
    {
        private Stream baseInputStream;
        private int bitMask;
        private int bitPos;
        private bool blockMode;
        private readonly byte[] data = new byte[0x2000];
        private int end;
        private bool eof;
        private const int EXTRA = 0x40;
        private byte finChar;
        private int freeEnt;
        private int got;
        private bool headerParsed;
        private bool isClosed;
        private bool isStreamOwner = true;
        private int maxBits;
        private int maxCode;
        private int maxMaxCode;
        private int nBits;
        private int oldCode;
        private readonly byte[] one = new byte[1];
        private byte[] stack;
        private int stackP;
        private int[] tabPrefix;
        private byte[] tabSuffix;
        private const int TBL_CLEAR = 0x100;
        private const int TBL_FIRST = 0x101;
        private readonly int[] zeros = new int[TBL_CLEAR];

        public LzwInputStream(Stream baseInputStream)
        {
            this.baseInputStream = baseInputStream;
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            throw new NotSupportedException("InflaterInputStream BeginWrite not supported");
        }

        public override void Close()
        {
            if (!isClosed)
            {
                isClosed = true;
                if (isStreamOwner)
                {
                    baseInputStream.Close();
                }
            }
        }

        private void Fill()
        {
            got = baseInputStream.Read(data, end, (data.Length - 1) - end);
            if (got > 0)
            {
                end += got;
            }
        }

        public override void Flush()
        {
            baseInputStream.Flush();
        }

        private void ParseHeader()
        {
            headerParsed = true;
            byte[] buffer = new byte[3];
            if (baseInputStream.Read(buffer, 0, buffer.Length) < 0)
            {
                throw new LzwException("Failed to read LZW header");
            }
            if ((buffer[0] != 0x1f) || (buffer[1] != 0x9d))
            {
                throw new LzwException(string.Format("Wrong LZW header. Magic bytes don't match. 0x{0:x2} 0x{1:x2}", buffer[0], buffer[1]));
            }
            blockMode = (buffer[2] & 0x80) > 0;
            maxBits = buffer[2] & 0x1f;
            if (maxBits > 0x10)
            {
                throw new LzwException(string.Concat(new object[] { "Stream compressed with ", maxBits, " bits, but decompression can only handle ", 0x10, " bits." }));
            }
            if ((buffer[2] & 0x60) > 0)
            {
                throw new LzwException("Unsupported bits set in the header.");
            }
            maxMaxCode = 1 << maxBits;
            nBits = 9;
            maxCode = (1 << nBits) - 1;
            bitMask = maxCode;
            oldCode = -1;
            finChar = 0;
            freeEnt = blockMode ? TBL_FIRST : TBL_CLEAR;
            tabPrefix = new int[1 << maxBits];
            tabSuffix = new byte[1 << maxBits];
            stack = new byte[1 << maxBits];
            stackP = stack.Length;
            for (int i = 0xff; i >= 0; i--)
            {
                tabSuffix[i] = (byte)i;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!headerParsed)
            {
                ParseHeader();
            }
            if (eof)
            {
                return -1;
            }
            int num = offset;
            int bits = nBits;
            int code = maxCode;
            int localMaxCode = maxMaxCode;
            int mask = bitMask;
            int localOldCode = oldCode;
            byte fin = finChar;
            int p = stackP;
            int ent = freeEnt;
            int pos = bitPos;
            int num11 = stack.Length - p;
            if (num11 > 0)
            {
                int length = (num11 >= count) ? count : num11;
                Array.Copy(stack, p, buffer, offset, length);
                offset += length;
                count -= length;
                p += length;
            }
            if (count == 0)
            {
                stackP = p;
                return (offset - num);
            }
        Label_00C6:
            if (end < EXTRA)
            {
                Fill();
            }
            int num13 = (got > 0) ? ((end - (end % bits)) << 3) : ((end << 3) - (bits - 1));
            while (pos < num13)
            {
                if (count == 0)
                {
                    nBits = bits;
                    maxCode = code;
                    maxMaxCode = localMaxCode;
                    bitMask = mask;
                    oldCode = localOldCode;
                    finChar = fin;
                    stackP = p;
                    freeEnt = ent;
                    bitPos = pos;
                    return (offset - num);
                }
                if (ent > code)
                {
                    int num14 = bits << 3;
                    pos = ((pos - 1) + num14) - (((pos - 1) + num14) % num14);
                    bits++;
                    code = (bits == maxBits) ? localMaxCode : ((1 << bits) - 1);
                    mask = (1 << bits) - 1;
                    pos = ResetBuf(pos);
                    goto Label_00C6;
                }
                int index = pos >> 3;
                int num16 = ((((data[index] & 0xff) | ((data[index + 1] & 0xff) << 8)) | ((data[index + 2] & 0xff) << 0x10)) >> (pos & 7)) & mask;
                pos += bits;
                if (localOldCode == -1)
                {
                    if (num16 >= TBL_CLEAR)
                    {
                        throw new LzwException("corrupt input: " + num16 + " > 255");
                    }
                    fin = (byte)(localOldCode = num16);
                    buffer[offset++] = fin;
                    count--;
                }
                else
                {
                    if ((num16 == TBL_CLEAR) && blockMode)
                    {
                        Array.Copy(zeros, 0, tabPrefix, 0, zeros.Length);
                        ent = TBL_CLEAR;
                        int num17 = bits << 3;
                        pos = ((pos - 1) + num17) - (((pos - 1) + num17) % num17);
                        bits = 9;
                        code = (1 << bits) - 1;
                        mask = code;
                        pos = ResetBuf(pos);
                        goto Label_00C6;
                    }
                    int num18 = num16;
                    p = stack.Length;
                    if (num16 >= ent)
                    {
                        if (num16 > ent)
                        {
                            throw new LzwException(string.Concat(new object[] { "corrupt input: code=", num16, ", freeEnt=", ent }));
                        }
                        stack[--p] = fin;
                        num16 = localOldCode;
                    }
                    while (num16 >= TBL_CLEAR)
                    {
                        stack[--p] = tabSuffix[num16];
                        num16 = tabPrefix[num16];
                    }
                    fin = tabSuffix[num16];
                    buffer[offset++] = fin;
                    count--;
                    num11 = stack.Length - p;
                    int num19 = (num11 >= count) ? count : num11;
                    Array.Copy(stack, p, buffer, offset, num19);
                    offset += num19;
                    count -= num19;
                    p += num19;
                    if (ent < localMaxCode)
                    {
                        tabPrefix[ent] = localOldCode;
                        tabSuffix[ent] = fin;
                        ent++;
                    }
                    localOldCode = num18;
                    if (count == 0)
                    {
                        nBits = bits;
                        maxCode = code;
                        bitMask = mask;
                        oldCode = localOldCode;
                        finChar = fin;
                        stackP = p;
                        freeEnt = ent;
                        bitPos = pos;
                        return (offset - num);
                    }
                }
            }
            pos = ResetBuf(pos);
            if (got > 0)
            {
                goto Label_00C6;
            }
            nBits = bits;
            maxCode = code;
            bitMask = mask;
            oldCode = localOldCode;
            finChar = fin;
            stackP = p;
            freeEnt = ent;
            bitPos = pos;
            eof = true;
            return (offset - num);
        }

        public override int ReadByte()
        {
            if (Read(one, 0, 1) == 1)
            {
                return (one[0] & 0xff);
            }
            return -1;
        }

        private int ResetBuf(int bitPosition)
        {
            int sourceIndex = bitPosition >> 3;
            Array.Copy(data, sourceIndex, data, 0, end - sourceIndex);
            end -= sourceIndex;
            return 0;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Seek not supported");
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("InflaterInputStream SetLength not supported");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("InflaterInputStream Write not supported");
        }

        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("InflaterInputStream WriteByte not supported");
        }

        public override bool CanRead
        {
            get
            {
                return baseInputStream.CanRead;
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
                return got;
            }
        }

        public override long Position
        {
            get
            {
                return baseInputStream.Position;
            }
            set
            {
                throw new NotSupportedException("InflaterInputStream Position not supported");
            }
        }
    }
}
