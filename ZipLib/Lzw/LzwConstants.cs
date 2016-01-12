namespace ZipLib.Lzw
{
    public static class LzwConstants
    {
        public const int BIT_MASK = 0x1f;
        public const int BLOCK_MODE_MASK = 0x80;
        public const int EXTENDED_MASK = 0x20;
        public const int HDR_SIZE = 3;
        public const int INIT_BITS = 9;
        public const int MAGIC = 0x1f9d;
        public const int MAX_BITS = 0x10;
        public const int RESERVED_MASK = 0x60;
    }
}

