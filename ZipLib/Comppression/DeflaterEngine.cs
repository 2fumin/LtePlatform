using System;
using ZipLib.CheckSums;

namespace ZipLib.Comppression
{
    public class DeflaterEngine : DeflaterConstants
    {
        private Adler32 adler;
        private int blockStart;
        private int compressionFunction;
        private int goodLength;
        private short[] head;
        private DeflaterHuffman huffman;
        private byte[] inputBuf;
        private int inputEnd;
        private int inputOff;
        private int ins_h;
        private int lookahead;
        private int matchLen;
        private int matchStart;
        private int max_chain;
        private int max_lazy;
        private int niceLength;
        private DeflaterPending pending;
        private short[] prev;
        private bool prevAvailable;
        private DeflateStrategy strategy;
        private int strstart;
        private const int TooFar = 0x1000;
        private long totalIn;
        private byte[] window;

        public DeflaterEngine(DeflaterPending pending)
        {
            this.pending = pending;
            huffman = new DeflaterHuffman(pending);
            adler = new Adler32();
            window = new byte[0x10000];
            head = new short[0x8000];
            prev = new short[0x8000];
            blockStart = strstart = 1;
        }

        public bool Deflate(bool flush, bool finish)
        {
            bool flag;
            do
            {
                FillWindow();
                bool flag2 = flush && (inputOff == inputEnd);
                switch (compressionFunction)
                {
                    case 0:
                        flag = DeflateStored(flag2, finish);
                        break;

                    case 1:
                        flag = DeflateFast(flag2, finish);
                        break;

                    case 2:
                        flag = DeflateSlow(flag2, finish);
                        break;

                    default:
                        throw new InvalidOperationException("unknown compressionFunction");
                }
            }
            while (pending.IsFlushed && flag);
            return flag;
        }

        private bool DeflateFast(bool flush, bool finish)
        {
            if ((lookahead >= 0x106) || flush)
            {
                goto Label_01EC;
            }
            return false;
        Label_0199:
            if (huffman.IsFull())
            {
                bool lastBlock = finish && (lookahead == 0);
                huffman.FlushBlock(window, blockStart, strstart - blockStart, lastBlock);
                blockStart = strstart;
                return !lastBlock;
            }
        Label_01EC:
            if ((lookahead >= 0x106) || flush)
            {
                int num;
                if (lookahead == 0)
                {
                    huffman.FlushBlock(window, blockStart, strstart - blockStart, finish);
                    blockStart = strstart;
                    return false;
                }
                if (strstart > 0xfefa)
                {
                    SlideWindow();
                }
                if ((((lookahead >= 3) && ((num = InsertString()) != 0)) && ((strategy != DeflateStrategy.HuffmanOnly) && ((strstart - num) <= 0x7efa))) && FindLongestMatch(num))
                {
                    bool flag = huffman.TallyDist(strstart - matchStart, matchLen);
                    lookahead -= matchLen;
                    if ((matchLen <= max_lazy) && (lookahead >= 3))
                    {
                        while (--matchLen > 0)
                        {
                            strstart++;
                            InsertString();
                        }
                        strstart++;
                    }
                    else
                    {
                        strstart += matchLen;
                        if (lookahead >= 2)
                        {
                            UpdateHash();
                        }
                    }
                    matchLen = 2;
                    if (flag)
                    {
                        goto Label_0199;
                    }
                    goto Label_01EC;
                }
                huffman.TallyLit(window[strstart] & 0xff);
                strstart++;
                lookahead--;
                goto Label_0199;
            }
            return true;
        }

        private bool DeflateSlow(bool flush, bool finish)
        {
            if ((lookahead >= 0x106) || flush)
            {
                while ((lookahead >= 0x106) || flush)
                {
                    if (lookahead == 0)
                    {
                        if (prevAvailable)
                        {
                            huffman.TallyLit(window[strstart - 1] & 0xff);
                        }
                        prevAvailable = false;
                        huffman.FlushBlock(window, blockStart, strstart - blockStart, finish);
                        blockStart = strstart;
                        return false;
                    }
                    if (strstart >= 0xfefa)
                    {
                        SlideWindow();
                    }
                    int start = matchStart;
                    int len = matchLen;
                    if (lookahead >= 3)
                    {
                        int curMatch = InsertString();
                        if ((((strategy != DeflateStrategy.HuffmanOnly) && (curMatch != 0)) && (((strstart - curMatch) <= 0x7efa) && FindLongestMatch(curMatch))) && ((matchLen <= 5) && ((strategy == DeflateStrategy.Filtered) || ((matchLen == 3) && ((strstart - matchStart) > TooFar)))))
                        {
                            matchLen = 2;
                        }
                    }
                    if ((len >= 3) && (matchLen <= len))
                    {
                        huffman.TallyDist((strstart - 1) - start, len);
                        len -= 2;
                        do
                        {
                            strstart++;
                            lookahead--;
                            if (lookahead >= 3)
                            {
                                InsertString();
                            }
                        }
                        while (--len > 0);
                        strstart++;
                        lookahead--;
                        prevAvailable = false;
                        matchLen = 2;
                    }
                    else
                    {
                        if (prevAvailable)
                        {
                            huffman.TallyLit(window[strstart - 1] & 0xff);
                        }
                        prevAvailable = true;
                        strstart++;
                        lookahead--;
                    }
                    if (huffman.IsFull())
                    {
                        int storedLength = strstart - blockStart;
                        if (prevAvailable)
                        {
                            storedLength--;
                        }
                        bool lastBlock = (finish && (lookahead == 0)) && !prevAvailable;
                        huffman.FlushBlock(window, blockStart, storedLength, lastBlock);
                        blockStart += storedLength;
                        return !lastBlock;
                    }
                }
                return true;
            }
            return false;
        }

        private bool DeflateStored(bool flush, bool finish)
        {
            if (!flush && (lookahead == 0))
            {
                return false;
            }
            strstart += lookahead;
            lookahead = 0;
            int storedLength = strstart - blockStart;
            if (((storedLength < MAX_BLOCK_SIZE) && ((blockStart >= 0x8000) || (storedLength < 0x7efa))) && !flush)
            {
                return true;
            }
            bool lastBlock = finish;
            if (storedLength > MAX_BLOCK_SIZE)
            {
                storedLength = MAX_BLOCK_SIZE;
                lastBlock = false;
            }
            huffman.FlushStoredBlock(window, blockStart, storedLength, lastBlock);
            blockStart += storedLength;
            return !lastBlock;
        }

        public void FillWindow()
        {
            if (strstart >= 0xfefa)
            {
                SlideWindow();
            }
            while ((lookahead < 0x106) && (inputOff < inputEnd))
            {
                int length = (0x10000 - lookahead) - strstart;
                if (length > (inputEnd - inputOff))
                {
                    length = inputEnd - inputOff;
                }
                Array.Copy(inputBuf, inputOff, window, strstart + lookahead, length);
                adler.Update(inputBuf, inputOff, length);
                inputOff += length;
                totalIn += length;
                lookahead += length;
            }
            if (lookahead >= 3)
            {
                UpdateHash();
            }
        }

        private bool FindLongestMatch(int curMatch)
        {
            int num = max_chain;
            int length = niceLength;
            short[] previous = prev;
            int stringStart = strstart;
            int index = strstart + matchLen;
            int num6 = Math.Max(matchLen, 2);
            int num7 = Math.Max(strstart - 0x7efa, 0);
            int num8 = (strstart + 0x102) - 1;
            byte num9 = window[index - 1];
            byte num10 = window[index];
            if (num6 >= goodLength)
            {
                num = num >> 2;
            }
            if (length > lookahead)
            {
                length = lookahead;
            }
            do
            {
                if (((window[curMatch + num6] == num10) && (window[(curMatch + num6) - 1] == num9)) && ((window[curMatch] == window[stringStart]) && (window[curMatch + 1] == window[stringStart + 1])))
                {
                    int num4 = curMatch + 2;
                    stringStart += 2;
                    while ((((window[++stringStart] == window[++num4]) && (window[++stringStart] == window[++num4])) && ((window[++stringStart] == window[++num4]) && (window[++stringStart] == window[++num4]))) && (((window[++stringStart] == window[++num4]) && (window[++stringStart] == window[++num4])) && (((window[++stringStart] == window[++num4]) && (window[++stringStart] == window[++num4])) && (stringStart < num8))))
                    {
                    }
                    if (stringStart > index)
                    {
                        matchStart = curMatch;
                        index = stringStart;
                        num6 = stringStart - strstart;
                        if (num6 >= length)
                        {
                            break;
                        }
                        num9 = window[index - 1];
                        num10 = window[index];
                    }
                    stringStart = strstart;
                }
            }
            while (((curMatch = previous[curMatch & HASH_MASK] & 0xffff) > num7) && (--num != 0));
            matchLen = Math.Min(num6, lookahead);
            return (matchLen >= 3);
        }

        private int InsertString()
        {
            short num;
            int index = ((ins_h << 5) ^ window[strstart + 2]) & WMASK;
            prev[strstart & HASH_MASK] = num = head[index];
            head[index] = (short)strstart;
            ins_h = index;
            return (num & 0xffff);
        }

        public bool NeedsInput()
        {
            return (inputEnd == inputOff);
        }

        public void Reset()
        {
            huffman.Reset();
            adler.Reset();
            blockStart = strstart = 1;
            lookahead = 0;
            totalIn = 0L;
            prevAvailable = false;
            matchLen = 2;
            for (int i = 0; i < 0x8000; i++)
            {
                head[i] = 0;
            }
            for (int j = 0; j < 0x8000; j++)
            {
                prev[j] = 0;
            }
        }

        public void ResetAdler()
        {
            adler.Reset();
        }

        public void SetDictionary(byte[] buffer, int offset, int length)
        {
            adler.Update(buffer, offset, length);
            if (length >= 3)
            {
                if (length > 0x7efa)
                {
                    offset += length - 0x7efa;
                    length = 0x7efa;
                }
                Array.Copy(buffer, offset, window, strstart, length);
                UpdateHash();
                length--;
                while (--length > 0)
                {
                    InsertString();
                    strstart++;
                }
                strstart += 2;
                blockStart = strstart;
            }
        }

        public void SetInput(byte[] buffer, int offset, int count)
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
            if (inputOff < inputEnd)
            {
                throw new InvalidOperationException("Old input was not completely processed");
            }
            int num = offset + count;
            if ((offset > num) || (num > buffer.Length))
            {
                throw new ArgumentOutOfRangeException("count");
            }
            inputBuf = buffer;
            inputOff = offset;
            inputEnd = num;
        }

        public void SetLevel(int level)
        {
            if ((level < 0) || (level > 9))
            {
                throw new ArgumentOutOfRangeException("level");
            }
            goodLength = GOOD_LENGTH[level];
            max_lazy = MAX_LAZY[level];
            niceLength = NICE_LENGTH[level];
            max_chain = MAX_CHAIN[level];
            if (COMPR_FUNC[level] != compressionFunction)
            {
                switch (compressionFunction)
                {
                    case 0:
                        if (strstart > blockStart)
                        {
                            huffman.FlushStoredBlock(window, blockStart, strstart - blockStart, false);
                            blockStart = strstart;
                        }
                        UpdateHash();
                        break;

                    case 1:
                        if (strstart > blockStart)
                        {
                            huffman.FlushBlock(window, blockStart, strstart - blockStart, false);
                            blockStart = strstart;
                        }
                        break;

                    case 2:
                        if (prevAvailable)
                        {
                            huffman.TallyLit(window[strstart - 1] & 0xff);
                        }
                        if (strstart > blockStart)
                        {
                            huffman.FlushBlock(window, blockStart, strstart - blockStart, false);
                            blockStart = strstart;
                        }
                        prevAvailable = false;
                        matchLen = 2;
                        break;
                }
                compressionFunction = COMPR_FUNC[level];
            }
        }

        private void SlideWindow()
        {
            Array.Copy(window, 0x8000, window, 0, 0x8000);
            matchStart -= 0x8000;
            strstart -= 0x8000;
            blockStart -= 0x8000;
            for (int i = 0; i < 0x8000; i++)
            {
                int num2 = head[i] & 0xffff;
                head[i] = (num2 >= 0x8000) ? ((short)(num2 - 0x8000)) : ((short)0);
            }
            for (int j = 0; j < 0x8000; j++)
            {
                int num4 = prev[j] & 0xffff;
                prev[j] = (num4 >= 0x8000) ? ((short)(num4 - 0x8000)) : ((short)0);
            }
        }

        private void UpdateHash()
        {
            ins_h = (window[strstart] << 5) ^ window[strstart + 1];
        }

        public int Adler
        {
            get
            {
                return (int)adler.Value;
            }
        }

        public DeflateStrategy Strategy
        {
            get
            {
                return strategy;
            }
            set
            {
                strategy = value;
            }
        }

        public long TotalIn
        {
            get
            {
                return totalIn;
            }
        }
    }
}

