using System;
using System.IO;

namespace ZipLib.Tar
{
    public class TarBuffer
    {
        private int blockFactor = 20;
        public const int BlockSize = 0x200;
        private int currentBlockIndex;
        private int currentRecordIndex;
        public const int DefaultBlockFactor = 20;
        public const int DefaultRecordSize = 0x2800;
        private Stream inputStream;
        private bool isStreamOwner_ = true;
        private Stream outputStream;
        private byte[] recordBuffer;
        private int recordSize = 0x2800;

        protected TarBuffer()
        {
        }

        public void Close()
        {
            if (outputStream != null)
            {
                WriteFinalRecord();
                if (isStreamOwner_)
                {
                    outputStream.Close();
                }
                outputStream = null;
            }
            else if (inputStream != null)
            {
                if (isStreamOwner_)
                {
                    inputStream.Close();
                }
                inputStream = null;
            }
        }

        public static TarBuffer CreateInputTarBuffer(Stream inputStream)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream");
            }
            return CreateInputTarBuffer(inputStream, 20);
        }

        public static TarBuffer CreateInputTarBuffer(Stream inputStream, int blockFactor)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream");
            }
            if (blockFactor <= 0)
            {
                throw new ArgumentOutOfRangeException("blockFactor", "Factor cannot be negative");
            }
            TarBuffer buffer = new TarBuffer
            {
                inputStream = inputStream,
                outputStream = null
            };
            buffer.Initialize(blockFactor);
            return buffer;
        }

        public static TarBuffer CreateOutputTarBuffer(Stream outputStream)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException("outputStream");
            }
            return CreateOutputTarBuffer(outputStream, 20);
        }

        public static TarBuffer CreateOutputTarBuffer(Stream outputStream, int blockFactor)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException("outputStream");
            }
            if (blockFactor <= 0)
            {
                throw new ArgumentOutOfRangeException("blockFactor", "Factor cannot be negative");
            }
            TarBuffer buffer = new TarBuffer
            {
                inputStream = null,
                outputStream = outputStream
            };
            buffer.Initialize(blockFactor);
            return buffer;
        }

        [Obsolete("Use BlockFactor property instead")]
        public int GetBlockFactor()
        {
            return blockFactor;
        }

        [Obsolete("Use CurrentBlock property instead")]
        public int GetCurrentBlockNum()
        {
            return currentBlockIndex;
        }

        [Obsolete("Use CurrentRecord property instead")]
        public int GetCurrentRecordNum()
        {
            return currentRecordIndex;
        }

        [Obsolete("Use RecordSize property instead")]
        public int GetRecordSize()
        {
            return recordSize;
        }

        private void Initialize(int archiveBlockFactor)
        {
            blockFactor = archiveBlockFactor;
            recordSize = archiveBlockFactor * 0x200;
            recordBuffer = new byte[RecordSize];
            if (inputStream != null)
            {
                currentRecordIndex = -1;
                currentBlockIndex = BlockFactor;
            }
            else
            {
                currentRecordIndex = 0;
                currentBlockIndex = 0;
            }
        }

        public static bool IsEndOfArchiveBlock(byte[] block)
        {
            if (block == null)
            {
                throw new ArgumentNullException("block");
            }
            if (block.Length != 0x200)
            {
                throw new ArgumentException("block length is invalid");
            }
            for (int i = 0; i < 0x200; i++)
            {
                if (block[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        [Obsolete("Use IsEndOfArchiveBlock instead")]
        public bool IsEOFBlock(byte[] block)
        {
            if (block == null)
            {
                throw new ArgumentNullException("block");
            }
            if (block.Length != 0x200)
            {
                throw new ArgumentException("block length is invalid");
            }
            for (int i = 0; i < 0x200; i++)
            {
                if (block[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public byte[] ReadBlock()
        {
            if (inputStream == null)
            {
                throw new TarException("TarBuffer.ReadBlock - no input stream defined");
            }
            if ((currentBlockIndex >= BlockFactor) && !ReadRecord())
            {
                throw new TarException("Failed to read a record");
            }
            byte[] destinationArray = new byte[0x200];
            Array.Copy(recordBuffer, currentBlockIndex * 0x200, destinationArray, 0, 0x200);
            currentBlockIndex++;
            return destinationArray;
        }

        private bool ReadRecord()
        {
            long num3;
            if (inputStream == null)
            {
                throw new TarException("no input stream stream defined");
            }
            currentBlockIndex = 0;
            int offset = 0;
            for (int i = RecordSize; i > 0; i -= (int)num3)
            {
                num3 = inputStream.Read(recordBuffer, offset, i);
                if (num3 <= 0L)
                {
                    break;
                }
                offset += (int)num3;
            }
            currentRecordIndex++;
            return true;
        }

        public void SkipBlock()
        {
            if (inputStream == null)
            {
                throw new TarException("no input stream defined");
            }
            if ((currentBlockIndex >= BlockFactor) && !ReadRecord())
            {
                throw new TarException("Failed to read a record");
            }
            currentBlockIndex++;
        }

        public void WriteBlock(byte[] block)
        {
            if (block == null)
            {
                throw new ArgumentNullException("block");
            }
            if (outputStream == null)
            {
                throw new TarException("TarBuffer.WriteBlock - no output stream defined");
            }
            if (block.Length != 0x200)
            {
                throw new TarException(string.Format("TarBuffer.WriteBlock - block to write has length '{0}' which is not the block size of '{1}'", block.Length, 0x200));
            }
            if (currentBlockIndex >= BlockFactor)
            {
                WriteRecord();
            }
            Array.Copy(block, 0, recordBuffer, currentBlockIndex * 0x200, 0x200);
            currentBlockIndex++;
        }

        public void WriteBlock(byte[] buffer, int offset)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (outputStream == null)
            {
                throw new TarException("TarBuffer.WriteBlock - no output stream stream defined");
            }
            if ((offset < 0) || (offset >= buffer.Length))
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if ((offset + 0x200) > buffer.Length)
            {
                throw new TarException(string.Format("TarBuffer.WriteBlock - record has length '{0}' with offset '{1}' which is less than the record size of '{2}'", buffer.Length, offset, recordSize));
            }
            if (currentBlockIndex >= BlockFactor)
            {
                WriteRecord();
            }
            Array.Copy(buffer, offset, recordBuffer, currentBlockIndex * 0x200, 0x200);
            currentBlockIndex++;
        }

        private void WriteFinalRecord()
        {
            if (outputStream == null)
            {
                throw new TarException("TarBuffer.WriteFinalRecord no output stream defined");
            }
            if (currentBlockIndex > 0)
            {
                int index = currentBlockIndex * 0x200;
                Array.Clear(recordBuffer, index, RecordSize - index);
                WriteRecord();
            }
            outputStream.Flush();
        }

        private void WriteRecord()
        {
            if (outputStream == null)
            {
                throw new TarException("TarBuffer.WriteRecord no output stream defined");
            }
            outputStream.Write(recordBuffer, 0, RecordSize);
            outputStream.Flush();
            currentBlockIndex = 0;
            currentRecordIndex++;
        }

        public int BlockFactor
        {
            get
            {
                return blockFactor;
            }
        }

        public int CurrentBlock
        {
            get
            {
                return currentBlockIndex;
            }
        }

        public int CurrentRecord
        {
            get
            {
                return currentRecordIndex;
            }
        }

        public bool IsStreamOwner
        {
            get
            {
                return isStreamOwner_;
            }
            set
            {
                isStreamOwner_ = value;
            }
        }

        public int RecordSize
        {
            get
            {
                return recordSize;
            }
        }
    }
}
