using System;
using ZipLib.Streams;

namespace ZipLib.Comppression
{
    internal class InflaterDynHeader
    {
        private static readonly int[] BlOrder = new int[] { 
            0x10, 0x11, 0x12, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 
            14, 1, 15
         };
        private byte[] _blLens;
        private const int Bllens = 3;
        private int _blnum;
        private const int Blnum = 2;
        private InflaterHuffmanTree _blTree;
        private int _dnum;
        private const int Dnum = 1;
        private byte _lastLen;
        private const int Lens = 4;
        private byte[] _litdistLens;
        private int _lnum;
        private const int Lnum = 0;
        private int _mode;
        private int _num;
        private int _ptr;
        private static readonly int[] RepBits = { 2, 3, 7 };
        private static readonly int[] RepMin = { 3, 3, 11 };
        private const int Reps = 5;
        private int _repSymbol;

        public InflaterHuffmanTree BuildDistTree()
        {
            byte[] destinationArray = new byte[this._dnum];
            Array.Copy(this._litdistLens, this._lnum, destinationArray, 0, this._dnum);
            return new InflaterHuffmanTree(destinationArray);
        }

        public InflaterHuffmanTree BuildLitLenTree()
        {
            byte[] destinationArray = new byte[this._lnum];
            Array.Copy(this._litdistLens, 0, destinationArray, 0, this._lnum);
            return new InflaterHuffmanTree(destinationArray);
        }

        public bool Decode(StreamManipulator input)
        {
            int num2;
            int num3;
        Label_0000:
            switch (this._mode)
            {
                case 0:
                    this._lnum = input.PeekBits(Reps);
                    if (this._lnum >= 0)
                    {
                        this._lnum += 0x101;
                        input.DropBits(Reps);
                        this._mode = 1;
                        break;
                    }
                    return false;

                case 1:
                    break;

                case 2:
                    goto Label_00B9;

                case 3:
                    goto Label_013B;

                case Lens:
                    goto Label_01A8;

                case Reps:
                    goto Label_01EE;

                default:
                    goto Label_0000;
            }
        _dnum = input.PeekBits(Reps);
            if (_dnum < 0)
            {
                return false;
            }
            _dnum++;
            input.DropBits(Reps);
            this._num = _lnum + _dnum;
            _litdistLens = new byte[this._num];
            _mode = 2;
        Label_00B9:
            this._blnum = input.PeekBits(4);
            if (this._blnum < 0)
            {
                return false;
            }
            _blnum += Lens;
            input.DropBits(Lens);
            _blLens = new byte[0x13];
            this._ptr = 0;
            this._mode = 3;
        Label_013B:
            while (this._ptr < this._blnum)
            {
                int num = input.PeekBits(3);
                if (num < 0)
                {
                    return false;
                }
                input.DropBits(3);
                this._blLens[BlOrder[this._ptr]] = (byte)num;
                this._ptr++;
            }
            this._blTree = new InflaterHuffmanTree(this._blLens);
            this._blLens = null;
            this._ptr = 0;
            this._mode = Lens;
        Label_01A8:
            while (((num2 = this._blTree.GetSymbol(input)) & -16) == 0)
            {
                this._litdistLens[this._ptr++] = this._lastLen = (byte)num2;
                if (this._ptr == this._num)
                {
                    return true;
                }
            }
            if (num2 < 0)
            {
                return false;
            }
            if (num2 >= 0x11)
            {
                this._lastLen = 0;
            }
            else if (_ptr == 0)
            {
                throw new SharpZipBaseException();
            }
            _repSymbol = num2 - 0x10;
            _mode = Reps;
        Label_01EE:
            num3 = RepBits[_repSymbol];
            int num4 = input.PeekBits(num3);
            if (num4 < 0)
            {
                return false;
            }
            input.DropBits(num3);
            num4 += RepMin[_repSymbol];
            if ((_ptr + num4) > this._num)
            {
                throw new SharpZipBaseException();
            }
            while (num4-- > 0)
            {
                _litdistLens[_ptr++] = _lastLen;
            }
            if (this._ptr == this._num)
            {
                return true;
            }
            this._mode = Lens;
            goto Label_0000;
        }
    }
}
