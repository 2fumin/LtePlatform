using System;
using System.IO;

namespace ZipLib.Tar
{
    public class TarBuffer
    {
        private int _blockFactor = 20;
        public const int BlockSize = 0x200;
        private int _currentBlockIndex;
        private int _currentRecordIndex;
        public const int DefaultBlockFactor = 20;
        public const int DefaultRecordSize = 0x2800;
        private Stream _inputStream;
        private bool _isStreamOwner = true;
        private Stream _outputStream;
        private byte[] _recordBuffer;
        private int _recordSize = 0x2800;

        protected TarBuffer()
        {
        }

        public void Close()
        {
            if (_outputStream != null)
            {
                WriteFinalRecord();
                if (_isStreamOwner)
                {
                    _outputStream.Close();
                }
                _outputStream = null;
            }
            else if (_inputStream != null)
            {
                if (_isStreamOwner)
                {
                    _inputStream.Close();
                }
                _inputStream = null;
            }
        }

        public static TarBuffer CreateInputTarBuffer(Stream inputStream)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }
            return CreateInputTarBuffer(inputStream, 20);
        }

        public static TarBuffer CreateInputTarBuffer(Stream inputStream, int blockFactor)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }
            if (blockFactor <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(blockFactor), "Factor cannot be negative");
            }
            TarBuffer buffer = new TarBuffer
            {
                _inputStream = inputStream,
                _outputStream = null
            };
            buffer.Initialize(blockFactor);
            return buffer;
        }

        public static TarBuffer CreateOutputTarBuffer(Stream outputStream)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream));
            }
            return CreateOutputTarBuffer(outputStream, 20);
        }

        public static TarBuffer CreateOutputTarBuffer(Stream outputStream, int blockFactor)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream));
            }
            if (blockFactor <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(blockFactor), "Factor cannot be negative");
            }
            TarBuffer buffer = new TarBuffer
            {
                _inputStream = null,
                _outputStream = outputStream
            };
            buffer.Initialize(blockFactor);
            return buffer;
        }

        [Obsolete("Use BlockFactor property instead")]
        public int GetBlockFactor()
        {
            return _blockFactor;
        }

        [Obsolete("Use CurrentBlock property instead")]
        public int GetCurrentBlockNum()
        {
            return _currentBlockIndex;
        }

        [Obsolete("Use CurrentRecord property instead")]
        public int GetCurrentRecordNum()
        {
            return _currentRecordIndex;
        }

        [Obsolete("Use RecordSize property instead")]
        public int GetRecordSize()
        {
            return _recordSize;
        }

        private void Initialize(int archiveBlockFactor)
        {
            _blockFactor = archiveBlockFactor;
            _recordSize = archiveBlockFactor * 0x200;
            _recordBuffer = new byte[RecordSize];
            if (_inputStream != null)
            {
                _currentRecordIndex = -1;
                _currentBlockIndex = BlockFactor;
            }
            else
            {
                _currentRecordIndex = 0;
                _currentBlockIndex = 0;
            }
        }

        public static bool IsEndOfArchiveBlock(byte[] block)
        {
            if (block == null)
            {
                throw new ArgumentNullException(nameof(block));
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
        public bool IsEofBlock(byte[] block)
        {
            if (block == null)
            {
                throw new ArgumentNullException(nameof(block));
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
            if (_inputStream == null)
            {
                throw new TarException("TarBuffer.ReadBlock - no input stream defined");
            }
            if ((_currentBlockIndex >= BlockFactor) && !ReadRecord())
            {
                throw new TarException("Failed to read a record");
            }
            byte[] destinationArray = new byte[0x200];
            Array.Copy(_recordBuffer, _currentBlockIndex * 0x200, destinationArray, 0, 0x200);
            _currentBlockIndex++;
            return destinationArray;
        }

        private bool ReadRecord()
        {
            long num3;
            if (_inputStream == null)
            {
                throw new TarException("no input stream stream defined");
            }
            _currentBlockIndex = 0;
            int offset = 0;
            for (int i = RecordSize; i > 0; i -= (int)num3)
            {
                num3 = _inputStream.Read(_recordBuffer, offset, i);
                if (num3 <= 0L)
                {
                    break;
                }
                offset += (int)num3;
            }
            _currentRecordIndex++;
            return true;
        }

        public void SkipBlock()
        {
            if (_inputStream == null)
            {
                throw new TarException("no input stream defined");
            }
            if ((_currentBlockIndex >= BlockFactor) && !ReadRecord())
            {
                throw new TarException("Failed to read a record");
            }
            _currentBlockIndex++;
        }

        public void WriteBlock(byte[] block)
        {
            if (block == null)
            {
                throw new ArgumentNullException(nameof(block));
            }
            if (_outputStream == null)
            {
                throw new TarException("TarBuffer.WriteBlock - no output stream defined");
            }
            if (block.Length != 0x200)
            {
                throw new TarException(
                    $"TarBuffer.WriteBlock - block to write has length '{block.Length}' which is not the block size of '{0x200}'");
            }
            if (_currentBlockIndex >= BlockFactor)
            {
                WriteRecord();
            }
            Array.Copy(block, 0, _recordBuffer, _currentBlockIndex * 0x200, 0x200);
            _currentBlockIndex++;
        }

        public void WriteBlock(byte[] buffer, int offset)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (_outputStream == null)
            {
                throw new TarException("TarBuffer.WriteBlock - no output stream stream defined");
            }
            if ((offset < 0) || (offset >= buffer.Length))
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }
            if ((offset + 0x200) > buffer.Length)
            {
                throw new TarException(
                    $"TarBuffer.WriteBlock - record has length '{buffer.Length}' with offset '{offset}' which is less than the record size of '{_recordSize}'");
            }
            if (_currentBlockIndex >= BlockFactor)
            {
                WriteRecord();
            }
            Array.Copy(buffer, offset, _recordBuffer, _currentBlockIndex * 0x200, 0x200);
            _currentBlockIndex++;
        }

        private void WriteFinalRecord()
        {
            if (_outputStream == null)
            {
                throw new TarException("TarBuffer.WriteFinalRecord no output stream defined");
            }
            if (_currentBlockIndex > 0)
            {
                int index = _currentBlockIndex * 0x200;
                Array.Clear(_recordBuffer, index, RecordSize - index);
                WriteRecord();
            }
            _outputStream.Flush();
        }

        private void WriteRecord()
        {
            if (_outputStream == null)
            {
                throw new TarException("TarBuffer.WriteRecord no output stream defined");
            }
            _outputStream.Write(_recordBuffer, 0, RecordSize);
            _outputStream.Flush();
            _currentBlockIndex = 0;
            _currentRecordIndex++;
        }

        public int BlockFactor
        {
            get
            {
                return _blockFactor;
            }
        }

        public int CurrentBlock
        {
            get
            {
                return _currentBlockIndex;
            }
        }

        public int CurrentRecord
        {
            get
            {
                return _currentRecordIndex;
            }
        }

        public bool IsStreamOwner
        {
            get
            {
                return _isStreamOwner;
            }
            set
            {
                _isStreamOwner = value;
            }
        }

        public int RecordSize
        {
            get
            {
                return _recordSize;
            }
        }
    }
}
