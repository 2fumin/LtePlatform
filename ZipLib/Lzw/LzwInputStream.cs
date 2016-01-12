using System;
using System.IO;

namespace ZipLib.Lzw
{
    public class LzwInputStream : Stream
    {
        private readonly Stream _baseInputStream;
        private int _bitMask;
        private int _bitPos;
        private bool _blockMode;
        private readonly byte[] _data = new byte[0x2000];
        private int _end;
        private bool _eof;
        private const int Extra = 0x40;
        private byte _finChar;
        private int _freeEnt;
        private int _got;
        private bool _headerParsed;
        private bool _isClosed;
        private bool _isStreamOwner = true;
        private int _maxBits;
        private int _maxCode;
        private int _maxMaxCode;
        private int _nBits;
        private int _oldCode;
        private readonly byte[] _one = new byte[1];
        private byte[] _stack;
        private int _stackP;
        private int[] _tabPrefix;
        private byte[] _tabSuffix;
        private const int TblClear = 0x100;
        private const int TblFirst = 0x101;
        private readonly int[] _zeros = new int[TblClear];

        public LzwInputStream(Stream baseInputStream)
        {
            this._baseInputStream = baseInputStream;
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            throw new NotSupportedException("InflaterInputStream BeginWrite not supported");
        }

        public override void Close()
        {
            if (!_isClosed)
            {
                _isClosed = true;
                if (_isStreamOwner)
                {
                    _baseInputStream.Close();
                }
            }
        }

        private void Fill()
        {
            _got = _baseInputStream.Read(_data, _end, (_data.Length - 1) - _end);
            if (_got > 0)
            {
                _end += _got;
            }
        }

        public override void Flush()
        {
            _baseInputStream.Flush();
        }

        private void ParseHeader()
        {
            _headerParsed = true;
            byte[] buffer = new byte[3];
            if (_baseInputStream.Read(buffer, 0, buffer.Length) < 0)
            {
                throw new LzwException("Failed to read LZW header");
            }
            if ((buffer[0] != 0x1f) || (buffer[1] != 0x9d))
            {
                throw new LzwException(string.Format("Wrong LZW header. Magic bytes don't match. 0x{0:x2} 0x{1:x2}", buffer[0], buffer[1]));
            }
            _blockMode = (buffer[2] & 0x80) > 0;
            _maxBits = buffer[2] & 0x1f;
            if (_maxBits > 0x10)
            {
                throw new LzwException(string.Concat(new object[] { "Stream compressed with ", _maxBits, " bits, but decompression can only handle ", 0x10, " bits." }));
            }
            if ((buffer[2] & 0x60) > 0)
            {
                throw new LzwException("Unsupported bits set in the header.");
            }
            _maxMaxCode = 1 << _maxBits;
            _nBits = 9;
            _maxCode = (1 << _nBits) - 1;
            _bitMask = _maxCode;
            _oldCode = -1;
            _finChar = 0;
            _freeEnt = _blockMode ? TblFirst : TblClear;
            _tabPrefix = new int[1 << _maxBits];
            _tabSuffix = new byte[1 << _maxBits];
            _stack = new byte[1 << _maxBits];
            _stackP = _stack.Length;
            for (int i = 0xff; i >= 0; i--)
            {
                _tabSuffix[i] = (byte)i;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!_headerParsed)
            {
                ParseHeader();
            }
            if (_eof)
            {
                return -1;
            }
            int num = offset;
            int bits = _nBits;
            int code = _maxCode;
            int localMaxCode = _maxMaxCode;
            int mask = _bitMask;
            int localOldCode = _oldCode;
            byte fin = _finChar;
            int p = _stackP;
            int ent = _freeEnt;
            int pos = _bitPos;
            int num11 = _stack.Length - p;
            if (num11 > 0)
            {
                int length = (num11 >= count) ? count : num11;
                Array.Copy(_stack, p, buffer, offset, length);
                offset += length;
                count -= length;
                p += length;
            }
            if (count == 0)
            {
                _stackP = p;
                return (offset - num);
            }
        Label_00C6:
            if (_end < Extra)
            {
                Fill();
            }
            int num13 = (_got > 0) ? ((_end - (_end % bits)) << 3) : ((_end << 3) - (bits - 1));
            while (pos < num13)
            {
                if (count == 0)
                {
                    _nBits = bits;
                    _maxCode = code;
                    _maxMaxCode = localMaxCode;
                    _bitMask = mask;
                    _oldCode = localOldCode;
                    _finChar = fin;
                    _stackP = p;
                    _freeEnt = ent;
                    _bitPos = pos;
                    return (offset - num);
                }
                if (ent > code)
                {
                    int num14 = bits << 3;
                    pos = ((pos - 1) + num14) - (((pos - 1) + num14) % num14);
                    bits++;
                    code = (bits == _maxBits) ? localMaxCode : ((1 << bits) - 1);
                    mask = (1 << bits) - 1;
                    pos = ResetBuf(pos);
                    goto Label_00C6;
                }
                int index = pos >> 3;
                int num16 = ((((_data[index] & 0xff) | ((_data[index + 1] & 0xff) << 8)) | ((_data[index + 2] & 0xff) << 0x10)) >> (pos & 7)) & mask;
                pos += bits;
                if (localOldCode == -1)
                {
                    if (num16 >= TblClear)
                    {
                        throw new LzwException("corrupt input: " + num16 + " > 255");
                    }
                    fin = (byte)(localOldCode = num16);
                    buffer[offset++] = fin;
                    count--;
                }
                else
                {
                    if ((num16 == TblClear) && _blockMode)
                    {
                        Array.Copy(_zeros, 0, _tabPrefix, 0, _zeros.Length);
                        ent = TblClear;
                        int num17 = bits << 3;
                        pos = ((pos - 1) + num17) - (((pos - 1) + num17) % num17);
                        bits = 9;
                        code = (1 << bits) - 1;
                        mask = code;
                        pos = ResetBuf(pos);
                        goto Label_00C6;
                    }
                    int num18 = num16;
                    p = _stack.Length;
                    if (num16 >= ent)
                    {
                        if (num16 > ent)
                        {
                            throw new LzwException(string.Concat(new object[] { "corrupt input: code=", num16, ", freeEnt=", ent }));
                        }
                        _stack[--p] = fin;
                        num16 = localOldCode;
                    }
                    while (num16 >= TblClear)
                    {
                        _stack[--p] = _tabSuffix[num16];
                        num16 = _tabPrefix[num16];
                    }
                    fin = _tabSuffix[num16];
                    buffer[offset++] = fin;
                    count--;
                    num11 = _stack.Length - p;
                    int num19 = (num11 >= count) ? count : num11;
                    Array.Copy(_stack, p, buffer, offset, num19);
                    offset += num19;
                    count -= num19;
                    p += num19;
                    if (ent < localMaxCode)
                    {
                        _tabPrefix[ent] = localOldCode;
                        _tabSuffix[ent] = fin;
                        ent++;
                    }
                    localOldCode = num18;
                    if (count == 0)
                    {
                        _nBits = bits;
                        _maxCode = code;
                        _bitMask = mask;
                        _oldCode = localOldCode;
                        _finChar = fin;
                        _stackP = p;
                        _freeEnt = ent;
                        _bitPos = pos;
                        return (offset - num);
                    }
                }
            }
            pos = ResetBuf(pos);
            if (_got > 0)
            {
                goto Label_00C6;
            }
            _nBits = bits;
            _maxCode = code;
            _bitMask = mask;
            _oldCode = localOldCode;
            _finChar = fin;
            _stackP = p;
            _freeEnt = ent;
            _bitPos = pos;
            _eof = true;
            return (offset - num);
        }

        public override int ReadByte()
        {
            if (Read(_one, 0, 1) == 1)
            {
                return (_one[0] & 0xff);
            }
            return -1;
        }

        private int ResetBuf(int bitPosition)
        {
            int sourceIndex = bitPosition >> 3;
            Array.Copy(_data, sourceIndex, _data, 0, _end - sourceIndex);
            _end -= sourceIndex;
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
                return _baseInputStream.CanRead;
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
                return _isStreamOwner;
            }
            set
            {
                _isStreamOwner = value;
            }
        }

        public override long Length
        {
            get
            {
                return _got;
            }
        }

        public override long Position
        {
            get
            {
                return _baseInputStream.Position;
            }
            set
            {
                throw new NotSupportedException("InflaterInputStream Position not supported");
            }
        }
    }
}
