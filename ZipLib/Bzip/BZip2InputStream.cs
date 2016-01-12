using System;
using System.IO;
using ZipLib.CheckSums;

namespace ZipLib.Bzip
{
    public class BZip2InputStream : Stream
    {
        private readonly int[][] _baseArray = new int[6][];
        private Stream _baseStream;
        private bool _blockRandomised;
        private int _blockSize100K;
        private int _bsBuff;
        private int _bsLive;
        private int _ch2;
        private int _chPrev;
        private int _computedBlockCrc;
        private uint _computedCombinedCrc;
        private int _count;
        private int _currentChar = -1;
        private int _currentState = 1;
        private int _i2;
        private readonly bool[] _inUse = new bool[0x100];
        private bool _isStreamOwner = true;
        private int _j2;
        private int _last;
        private readonly int[][] _limit = new int[6][];
        private byte[] _ll8;
        private readonly IChecksum _mCrc = new StrangeCRC();
        private readonly int[] _minLens = new int[6];
        private int _nInUse;
        private int _origPtr;
        private readonly int[][] _perm = new int[6][];
        private int _rNToGo;
        private int _rTPos;
        private readonly byte[] _selector = new byte[0x4652];
        private readonly byte[] _selectorMtf = new byte[0x4652];
        private readonly byte[] _seqToUnseq = new byte[0x100];
        private int _storedBlockCrc;
        private int _storedCombinedCrc;
        private bool _streamEnd;
        private int _tPos;
        private int[] _tt;
        private readonly byte[] _unseqToSeq = new byte[0x100];
        private readonly int[] _unzftab = new int[0x100];
        private byte _z;

        public BZip2InputStream(Stream stream)
        {
            for (int i = 0; i < 6; i++)
            {
                _limit[i] = new int[0x102];
                _baseArray[i] = new int[0x102];
                _perm[i] = new int[0x102];
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

        private int BsGetIntVs(int numBits)
        {
            return BsR(numBits);
        }

        private char BsGetUChar()
        {
            return (char)BsR(8);
        }

        private int BsR(int n)
        {
            while (_bsLive < n)
            {
                FillBuffer();
            }
            int num = (_bsBuff >> (_bsLive - n)) & ((1 << n) - 1);
            _bsLive -= n;
            return num;
        }

        private void BsSetStream(Stream stream)
        {
            _baseStream = stream;
            _bsLive = 0;
            _bsBuff = 0;
        }

        public override void Close()
        {
            if (IsStreamOwner && (_baseStream != null))
            {
                _baseStream.Close();
            }
        }

        private void Complete()
        {
            _storedCombinedCrc = BsGetInt32();
            if (_storedCombinedCrc != _computedCombinedCrc)
            {
                CrcError();
            }
            _streamEnd = true;
        }

        private static void CompressedStreamEof()
        {
            throw new EndOfStreamException("BZip2 input stream end of compressed stream");
        }

        private static void CrcError()
        {
            throw new BZip2Exception("BZip2 input stream crc error");
        }

        private void EndBlock()
        {
            _computedBlockCrc = (int)_mCrc.Value;
            if (_storedBlockCrc != _computedBlockCrc)
            {
                CrcError();
            }
            _computedCombinedCrc = ((_computedCombinedCrc << 1) & uint.MaxValue) | (_computedCombinedCrc >> 0x1f);
            _computedCombinedCrc ^= (uint)_computedBlockCrc;
        }

        private void FillBuffer()
        {
            int num = 0;
            try
            {
                num = _baseStream.ReadByte();
            }
            catch (Exception)
            {
                CompressedStreamEof();
            }
            if (num == -1)
            {
                CompressedStreamEof();
            }
            _bsBuff = (_bsBuff << 8) | (num & 0xff);
            _bsLive += 8;
        }

        public override void Flush()
        {
            if (_baseStream != null)
            {
                _baseStream.Flush();
            }
        }

        private void GetAndMoveToFrontDecode()
        {
            int num11;
            byte[] buffer = new byte[0x100];
            int num2 = 0x186a0 * _blockSize100K;
            _origPtr = BsGetIntVs(0x18);
            RecvDecodingTables();
            int num3 = _nInUse + 1;
            int index = -1;
            int num5 = 0;
            for (int i = 0; i <= 0xff; i++)
            {
                _unzftab[i] = 0;
            }
            for (int j = 0; j <= 0xff; j++)
            {
                buffer[j] = (byte)j;
            }
            _last = -1;
            if (num5 == 0)
            {
                index++;
                num5 = 50;
            }
            num5--;
            int num8 = _selector[index];
            int n = _minLens[num8];
            int num10 = BsR(n);
            while (num10 > _limit[num8][n])
            {
                if (n > 20)
                {
                    throw new BZip2Exception("Bzip data error");
                }
                n++;
                while (_bsLive < 1)
                {
                    FillBuffer();
                }
                num11 = (_bsBuff >> (_bsLive - 1)) & 1;
                _bsLive--;
                num10 = (num10 << 1) | num11;
            }
            if (((num10 - _baseArray[num8][n]) < 0) || ((num10 - _baseArray[num8][n]) >= 0x102))
            {
                throw new BZip2Exception("Bzip data error");
            }
            int num = _perm[num8][num10 - _baseArray[num8][n]];
        Label_0163:
            if (num == num3)
            {
                return;
            }
            if ((num != 0) && (num != 1))
            {
                _last++;
                if (_last >= num2)
                {
                    BlockOverrun();
                }
                byte num15 = buffer[num - 1];
                _unzftab[_seqToUnseq[num15]]++;
                _ll8[_last] = _seqToUnseq[num15];
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
                num8 = _selector[index];
                n = _minLens[num8];
                num10 = BsR(n);
                while (num10 > _limit[num8][n])
                {
                    n++;
                    while (_bsLive < 1)
                    {
                        FillBuffer();
                    }
                    num11 = (_bsBuff >> (_bsLive - 1)) & 1;
                    _bsLive--;
                    num10 = (num10 << 1) | num11;
                }
                num = _perm[num8][num10 - _baseArray[num8][n]];
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
            num8 = _selector[index];
            n = _minLens[num8];
            num10 = BsR(n);
            while (num10 > _limit[num8][n])
            {
                n++;
                while (_bsLive < 1)
                {
                    FillBuffer();
                }
                num11 = (_bsBuff >> (_bsLive - 1)) & 1;
                _bsLive--;
                num10 = (num10 << 1) | num11;
            }
            switch (_perm[num8][num10 - _baseArray[num8][n]])
            {
                case 0:
                case 1:
                    goto Label_0178;
            }
            num12++;
            byte num14 = _seqToUnseq[buffer[0]];
            _unzftab[num14] += num12;
            while (num12 > 0)
            {
                _last++;
                _ll8[_last] = num14;
                num12--;
            }
            if (_last >= num2)
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
            _ch2 = BsGetUChar();
            char ch3 = BsGetUChar();
            char ch4 = BsGetUChar();
            char ch5 = BsGetUChar();
            char ch6 = BsGetUChar();
            if ((((ch == '\x0017') && (_ch2 == 'r')) && ((ch3 == 'E') && (ch4 == '8'))) && ((ch5 == 'P') && (ch6 == '\x0090')))
            {
                Complete();
            }
            else if ((((ch != '1') || (_ch2 != 'A')) || ((ch3 != 'Y') || (ch4 != '&'))) || ((ch5 != 'S') || (ch6 != 'Y')))
            {
                BadBlockHeader();
                _streamEnd = true;
            }
            else
            {
                _storedBlockCrc = BsGetInt32();
                _blockRandomised = BsR(1) == 1;
                GetAndMoveToFrontDecode();
                _mCrc.Reset();
                _currentState = 1;
            }
        }

        private void Initialize()
        {
            char ch = BsGetUChar();
            _ch2 = BsGetUChar();
            char ch3 = BsGetUChar();
            char ch4 = BsGetUChar();
            if (((ch != 'B') || (_ch2 != 'Z')) || (((ch3 != 'h') || (ch4 < '1')) || (ch4 > '9')))
            {
                _streamEnd = true;
            }
            else
            {
                SetDecompressStructureSizes(ch4 - '0');
                _computedCombinedCrc = 0;
            }
        }

        private void MakeMaps()
        {
            _nInUse = 0;
            for (int i = 0; i < 0x100; i++)
            {
                if (_inUse[i])
                {
                    _seqToUnseq[_nInUse] = (byte)i;
                    _unseqToSeq[i] = (byte)_nInUse;
                    _nInUse++;
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
            if (_streamEnd)
            {
                return -1;
            }

            switch (_currentState)
            {
                case 1:
                case 2:
                case 5:
                    return _currentChar;

                case 3:
                    SetupRandPartB();
                    return _currentChar;

                case 4:
                    SetupRandPartC();
                    return _currentChar;

                case 6:
                    SetupNoRandPartB();
                    return _currentChar;

                case 7:
                    SetupNoRandPartC();
                    return _currentChar;
            }
            return _currentChar;
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
                        _inUse[(k * 0x10) + num4] = BsR(1) == 1;
                    }
                }
                else
                {
                    for (int num5 = 0; num5 < 0x10; num5++)
                    {
                        _inUse[(k * 0x10) + num5] = false;
                    }
                }
            }
            MakeMaps();
            int alphaSize = _nInUse + 2;
            int num7 = BsR(3);
            int num8 = BsR(15);
            for (int m = 0; m < num8; m++)
            {
                int num10 = 0;
                while (BsR(1) == 1)
                {
                    num10++;
                }
                _selectorMtf[m] = (byte)num10;
            }
            byte[] buffer = new byte[6];
            for (int n = 0; n < num7; n++)
            {
                buffer[n] = (byte)n;
            }
            for (int num12 = 0; num12 < num8; num12++)
            {
                int index = _selectorMtf[num12];
                byte num14 = buffer[index];
                while (index > 0)
                {
                    buffer[index] = buffer[index - 1];
                    index--;
                }
                buffer[0] = num14;
                _selector[num12] = num14;
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
                HbCreateDecodeTables(_limit[num18], _baseArray[num18], _perm[num18], chArray[num18], num19, num20, alphaSize);
                _minLens[num18] = num19;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("BZip2InputStream Seek not supported");
        }

        private void SetDecompressStructureSizes(int newSize100K)
        {
            if (((0 > newSize100K) || (newSize100K > 9)) || ((0 > _blockSize100K) || (_blockSize100K > 9)))
            {
                throw new BZip2Exception("Invalid block size");
            }
            _blockSize100K = newSize100K;
            if (newSize100K != 0)
            {
                int num = 0x186a0 * newSize100K;
                _ll8 = new byte[num];
                _tt = new int[num];
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
            Array.Copy(_unzftab, 0, destinationArray, 1, 0x100);
            for (int i = 1; i <= 0x100; i++)
            {
                destinationArray[i] += destinationArray[i - 1];
            }
            for (int j = 0; j <= _last; j++)
            {
                byte index = _ll8[j];
                _tt[destinationArray[index]] = j;
                destinationArray[index]++;
            }
            _tPos = _tt[_origPtr];
            _count = 0;
            _i2 = 0;
            _ch2 = 0x100;
            if (_blockRandomised)
            {
                _rNToGo = 0;
                _rTPos = 0;
                SetupRandPartA();
            }
            else
            {
                SetupNoRandPartA();
            }
        }

        private void SetupNoRandPartA()
        {
            if (_i2 <= _last)
            {
                _chPrev = _ch2;
                _ch2 = _ll8[_tPos];
                _tPos = _tt[_tPos];
                _i2++;
                _currentChar = _ch2;
                _currentState = 6;
                _mCrc.Update(_ch2);
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
            if (_ch2 != _chPrev)
            {
                _currentState = 5;
                _count = 1;
                SetupNoRandPartA();
            }
            else
            {
                _count++;
                if (_count >= 4)
                {
                    _z = _ll8[_tPos];
                    _tPos = _tt[_tPos];
                    _currentState = 7;
                    _j2 = 0;
                    SetupNoRandPartC();
                }
                else
                {
                    _currentState = 5;
                    SetupNoRandPartA();
                }
            }
        }

        private void SetupNoRandPartC()
        {
            if (_j2 < _z)
            {
                _currentChar = _ch2;
                _mCrc.Update(_ch2);
                _j2++;
            }
            else
            {
                _currentState = 5;
                _i2++;
                _count = 0;
                SetupNoRandPartA();
            }
        }

        private void SetupRandPartA()
        {
            if (_i2 <= _last)
            {
                _chPrev = _ch2;
                _ch2 = _ll8[_tPos];
                _tPos = _tt[_tPos];
                if (_rNToGo == 0)
                {
                    _rNToGo = BZip2Constants.RandomNumbers[_rTPos];
                    _rTPos++;
                    if (_rTPos == 0x200)
                    {
                        _rTPos = 0;
                    }
                }
                _rNToGo--;
                _ch2 ^= (_rNToGo == 1) ? 1 : 0;
                _i2++;
                _currentChar = _ch2;
                _currentState = 3;
                _mCrc.Update(_ch2);
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
            if (_ch2 != _chPrev)
            {
                _currentState = 2;
                _count = 1;
                SetupRandPartA();
            }
            else
            {
                _count++;
                if (_count >= 4)
                {
                    _z = _ll8[_tPos];
                    _tPos = _tt[_tPos];
                    if (_rNToGo == 0)
                    {
                        _rNToGo = BZip2Constants.RandomNumbers[_rTPos];
                        _rTPos++;
                        if (_rTPos == 0x200)
                        {
                            _rTPos = 0;
                        }
                    }
                    _rNToGo--;
                    _z = (byte)(_z ^ ((_rNToGo == 1) ? 1 : 0));
                    _j2 = 0;
                    _currentState = 4;
                    SetupRandPartC();
                }
                else
                {
                    _currentState = 2;
                    SetupRandPartA();
                }
            }
        }

        private void SetupRandPartC()
        {
            if (_j2 < _z)
            {
                _currentChar = _ch2;
                _mCrc.Update(_ch2);
                _j2++;
            }
            else
            {
                _currentState = 2;
                _i2++;
                _count = 0;
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

        public override bool CanRead => _baseStream.CanRead;

        public override bool CanSeek => _baseStream.CanSeek;

        public override bool CanWrite => false;

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

        public override long Length => _baseStream.Length;

        public override long Position
        {
            get
            {
                return _baseStream.Position;
            }
            set
            {
                throw new NotSupportedException("BZip2InputStream position cannot be set");
            }
        }
    }
}

