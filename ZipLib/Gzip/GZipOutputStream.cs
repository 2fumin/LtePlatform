using System;
using System.IO;
using ZipLib.CheckSums;
using ZipLib.Comppression;
using ZipLib.Streams;

namespace ZipLib.Gzip
{
    public class GZipOutputStream : DeflaterOutputStream
    {
        protected Crc32 crc;
        private OutputState state_;

        public GZipOutputStream(Stream baseOutputStream, int size = 0x1000)
            : base(baseOutputStream, new Deflater(-1, true), size)
        {
            crc = new Crc32();
        }

        public override void Close()
        {
            try
            {
                Finish();
            }
            finally
            {
                if (state_ != OutputState.Closed)
                {
                    state_ = OutputState.Closed;
                    if (IsStreamOwner)
                    {
                        baseOutputStream_.Close();
                    }
                }
            }
        }

        public override void Finish()
        {
            if (state_ == OutputState.Header)
            {
                WriteHeader();
            }
            if (state_ == OutputState.Footer)
            {
                state_ = OutputState.Finished;
                base.Finish();
                uint num = (uint)(((ulong)deflater_.TotalIn) & 0xffffffffL);
                uint num2 = (uint)(((ulong)crc.Value) & 0xffffffffL);
                byte[] buffer = { (byte)num2, (byte)(num2 >> 8), (byte)(num2 >> 0x10), (byte)(num2 >> 0x18),
                    (byte)num, (byte)(num >> 8), (byte)(num >> 0x10), (byte)(num >> 0x18) };
                baseOutputStream_.Write(buffer, 0, buffer.Length);
            }
        }

        public int GetLevel()
        {
            return deflater_.GetLevel();
        }

        public void SetLevel(int level)
        {
            if (level < 1)
            {
                throw new ArgumentOutOfRangeException("level");
            }
            deflater_.SetLevel(level);
        }

        public override void Write(byte[] buffer, int off, int count)
        {
            if (state_ == OutputState.Header)
            {
                WriteHeader();
            }
            if (state_ != OutputState.Footer)
            {
                throw new InvalidOperationException("Write not permitted in current state");
            }
            crc.Update(buffer, off, count);
            base.Write(buffer, off, count);
        }

        private void WriteHeader()
        {
            if (state_ == OutputState.Header)
            {
                state_ = OutputState.Footer;
                DateTime time2 = new DateTime(0x7b2, 1, 1);
                int num = (int)((DateTime.Now.Ticks - time2.Ticks) / 0x989680L);
                byte[] buffer2 = { 0x1f, 0x8b, 8, 0, 0, 0, 0, 0, 0, 0xff };
                buffer2[4] = (byte)num;
                buffer2[5] = (byte)(num >> 8);
                buffer2[6] = (byte)(num >> 0x10);
                buffer2[7] = (byte)(num >> 0x18);
                byte[] buffer = buffer2;
                baseOutputStream_.Write(buffer, 0, buffer.Length);
            }
        }

        private enum OutputState
        {
            Header,
            Footer,
            Finished,
            Closed
        }
    }
}

