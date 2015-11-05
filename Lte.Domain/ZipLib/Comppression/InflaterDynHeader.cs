using System;
using Lte.Domain.ZipLib.Streams;

namespace Lte.Domain.ZipLib.Compression
{
    internal class InflaterDynHeader
    {
        private static readonly int[] BL_ORDER = new int[] { 
            0x10, 0x11, 0x12, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 
            14, 1, 15
         };
        private byte[] blLens;
        private const int BLLENS = 3;
        private int blnum;
        private const int BLNUM = 2;
        private InflaterHuffmanTree blTree;
        private int dnum;
        private const int DNUM = 1;
        private byte lastLen;
        private const int LENS = 4;
        private byte[] litdistLens;
        private int lnum;
        private const int LNUM = 0;
        private int mode;
        private int num;
        private int ptr;
        private static readonly int[] repBits = { 2, 3, 7 };
        private static readonly int[] repMin = { 3, 3, 11 };
        private const int REPS = 5;
        private int repSymbol;

        public InflaterHuffmanTree BuildDistTree()
        {
            byte[] destinationArray = new byte[this.dnum];
            Array.Copy(this.litdistLens, this.lnum, destinationArray, 0, this.dnum);
            return new InflaterHuffmanTree(destinationArray);
        }

        public InflaterHuffmanTree BuildLitLenTree()
        {
            byte[] destinationArray = new byte[this.lnum];
            Array.Copy(this.litdistLens, 0, destinationArray, 0, this.lnum);
            return new InflaterHuffmanTree(destinationArray);
        }

        public bool Decode(StreamManipulator input)
        {
            int num2;
            int num3;
        Label_0000:
            switch (this.mode)
            {
                case 0:
                    this.lnum = input.PeekBits(REPS);
                    if (this.lnum >= 0)
                    {
                        this.lnum += 0x101;
                        input.DropBits(REPS);
                        this.mode = 1;
                        break;
                    }
                    return false;

                case 1:
                    break;

                case 2:
                    goto Label_00B9;

                case 3:
                    goto Label_013B;

                case LENS:
                    goto Label_01A8;

                case REPS:
                    goto Label_01EE;

                default:
                    goto Label_0000;
            }
        dnum = input.PeekBits(REPS);
            if (dnum < 0)
            {
                return false;
            }
            dnum++;
            input.DropBits(REPS);
            this.num = lnum + dnum;
            litdistLens = new byte[this.num];
            mode = 2;
        Label_00B9:
            this.blnum = input.PeekBits(4);
            if (this.blnum < 0)
            {
                return false;
            }
            blnum += LENS;
            input.DropBits(LENS);
            blLens = new byte[0x13];
            this.ptr = 0;
            this.mode = 3;
        Label_013B:
            while (this.ptr < this.blnum)
            {
                int num = input.PeekBits(3);
                if (num < 0)
                {
                    return false;
                }
                input.DropBits(3);
                this.blLens[BL_ORDER[this.ptr]] = (byte)num;
                this.ptr++;
            }
            this.blTree = new InflaterHuffmanTree(this.blLens);
            this.blLens = null;
            this.ptr = 0;
            this.mode = LENS;
        Label_01A8:
            while (((num2 = this.blTree.GetSymbol(input)) & -16) == 0)
            {
                this.litdistLens[this.ptr++] = this.lastLen = (byte)num2;
                if (this.ptr == this.num)
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
                this.lastLen = 0;
            }
            else if (ptr == 0)
            {
                throw new SharpZipBaseException();
            }
            repSymbol = num2 - 0x10;
            mode = REPS;
        Label_01EE:
            num3 = repBits[repSymbol];
            int num4 = input.PeekBits(num3);
            if (num4 < 0)
            {
                return false;
            }
            input.DropBits(num3);
            num4 += repMin[repSymbol];
            if ((ptr + num4) > this.num)
            {
                throw new SharpZipBaseException();
            }
            while (num4-- > 0)
            {
                litdistLens[ptr++] = lastLen;
            }
            if (this.ptr == this.num)
            {
                return true;
            }
            this.mode = LENS;
            goto Label_0000;
        }
    }
}
