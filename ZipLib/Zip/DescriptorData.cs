namespace Lte.Domain.ZipLib.Zip
{
    public class DescriptorData
    {
        private long crc;

        public long CompressedSize { get; set; }

        public long Crc
        {
            get
            {
                return crc;
            }
            set
            {
                crc = value & 0xffffffffL;
            }
        }

        public long Size { get; set; }
    }
}

