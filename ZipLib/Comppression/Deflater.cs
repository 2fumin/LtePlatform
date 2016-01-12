using System;

namespace ZipLib.Comppression
{
    public class Deflater
    {
        public const int BEST_COMPRESSION = 9;
        public const int BEST_SPEED = 1;
        private const int BUSY_STATE = 0x10;
        private const int CLOSED_STATE = 0x7f;
        public const int DEFAULT_COMPRESSION = -1;
        public const int DEFLATED = 8;
        private DeflaterEngine engine;
        private const int FINISHED_STATE = 30;
        private const int FINISHING_STATE = 0x1c;
        private const int FLUSHING_STATE = 20;
        private const int IS_FINISHING = 8;
        private const int IS_FLUSHING = 4;
        private int level;
        public const int NO_COMPRESSION = 0;
        private bool noZlibHeaderOrFooter;
        private DeflaterPending pending;
        private int state;
        private long totalOut;

        public Deflater()
            : this(-1)
        {
        }

        public Deflater(int level, bool noZlibHeaderOrFooter = false)
        {
            if (level == -1)
            {
                level = 6;
            }
            else if ((level < 0) || (level > 9))
            {
                throw new ArgumentOutOfRangeException("level");
            }
            pending = new DeflaterPending();
            engine = new DeflaterEngine(pending);
            this.noZlibHeaderOrFooter = noZlibHeaderOrFooter;
            SetStrategy(DeflateStrategy.Default);
            SetLevel(level);
            Reset();
        }

        public int Deflate(byte[] output)
        {
            return Deflate(output, 0, output.Length);
        }

        public int Deflate(byte[] output, int offset, int length)
        {
            int num = length;
            if (state == CLOSED_STATE)
            {
                throw new InvalidOperationException("Deflater closed");
            }
            if (state < BUSY_STATE)
            {
                int s = 0x7800;
                int num3 = (level - 1) >> 1;
                if ((num3 < 0) || (num3 > 3))
                {
                    num3 = 3;
                }
                s |= num3 << 6;
                if ((state & 1) != 0)
                {
                    s |= 0x20;
                }
                s += 0x1f - (s % 0x1f);
                pending.WriteShortMsb(s);
                if ((state & 1) != 0)
                {
                    int adler = engine.Adler;
                    engine.ResetAdler();
                    pending.WriteShortMsb(adler >> BUSY_STATE);
                    pending.WriteShortMsb(adler & 0xffff);
                }
                state = BUSY_STATE | (state & 12);
            }
            while (true)
            {
                int num5 = pending.Flush(output, offset, length);
                offset += num5;
                totalOut += num5;
                length -= num5;
                if ((length == 0) || (state == FINISHED_STATE))
                {
                    return (num - length);
                }
                if (!engine.Deflate((state & IS_FLUSHING) != 0, (state & IS_FINISHING) != 0))
                {
                    if (state == 0x10)
                    {
                        return (num - length);
                    }
                    if (state == FLUSHING_STATE)
                    {
                        if (level != 0)
                        {
                            for (int i = IS_FINISHING + (-pending.BitCount & 7); i > 0; i -= 10)
                            {
                                pending.WriteBits(2, 10);
                            }
                        }
                        state = BUSY_STATE;
                    }
                    else if (state == FINISHING_STATE)
                    {
                        pending.AlignToByte();
                        if (!noZlibHeaderOrFooter)
                        {
                            int num7 = engine.Adler;
                            pending.WriteShortMsb(num7 >> BUSY_STATE);
                            pending.WriteShortMsb(num7 & 0xffff);
                        }
                        state = FINISHED_STATE;
                    }
                }
            }
        }

        public void Finish()
        {
            state |= 12;
        }

        public void Flush()
        {
            state |= IS_FLUSHING;
        }

        public int GetLevel()
        {
            return level;
        }

        public void Reset()
        {
            state = noZlibHeaderOrFooter ? BUSY_STATE : 0;
            totalOut = 0L;
            pending.Reset();
            engine.Reset();
        }

        public void SetDictionary(byte[] dictionary)
        {
            SetDictionary(dictionary, 0, dictionary.Length);
        }

        public void SetDictionary(byte[] dictionary, int index, int count)
        {
            if (state != 0)
            {
                throw new InvalidOperationException();
            }
            state = 1;
            engine.SetDictionary(dictionary, index, count);
        }

        public void SetInput(byte[] input)
        {
            SetInput(input, 0, input.Length);
        }

        public void SetInput(byte[] input, int offset, int count)
        {
            if ((state & IS_FINISHING) != 0)
            {
                throw new InvalidOperationException("Finish() already called");
            }
            engine.SetInput(input, offset, count);
        }

        public void SetLevel(int level)
        {
            if (level == -1)
            {
                level = 6;
            }
            else if ((level < 0) || (level > 9))
            {
                throw new ArgumentOutOfRangeException("level");
            }
            if (this.level != level)
            {
                this.level = level;
                engine.SetLevel(level);
            }
        }

        public void SetStrategy(DeflateStrategy strategy)
        {
            engine.Strategy = strategy;
        }

        public int Adler
        {
            get
            {
                return engine.Adler;
            }
        }

        public bool IsFinished
        {
            get
            {
                return ((state == FINISHED_STATE) && pending.IsFlushed);
            }
        }

        public bool IsNeedingInput
        {
            get
            {
                return engine.NeedsInput();
            }
        }

        public long TotalIn
        {
            get
            {
                return engine.TotalIn;
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

