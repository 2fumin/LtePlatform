namespace ZipLib.Zip
{
    public class DescriptorData
    {
        private long _crc;

        public long CompressedSize { get; set; }

        public long Crc
        {
            get
            {
                return _crc;
            }
            set
            {
                _crc = value & 0xffffffffL;
            }
        }

        public long Size { get; set; }
    }
}

