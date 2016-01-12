namespace Lte.Domain.ZipLib.Zip
{
    public class TestStatus
    {
        private long bytesTested_;
        private ZipEntry entry_;
        private bool entryValid_;
        private int errorCount_;
        private ZipFile file_;
        private TestOperation operation_;

        public TestStatus(ZipFile file)
        {
            file_ = file;
        }

        internal void AddError()
        {
            errorCount_++;
            entryValid_ = false;
        }

        internal void SetBytesTested(long value)
        {
            bytesTested_ = value;
        }

        internal void SetEntry(ZipEntry entry)
        {
            entry_ = entry;
            entryValid_ = true;
            bytesTested_ = 0L;
        }

        internal void SetOperation(TestOperation operation)
        {
            operation_ = operation;
        }

        public long BytesTested
        {
            get
            {
                return bytesTested_;
            }
        }

        public ZipEntry Entry
        {
            get
            {
                return entry_;
            }
        }

        public bool EntryValid
        {
            get
            {
                return entryValid_;
            }
        }

        public int ErrorCount
        {
            get
            {
                return errorCount_;
            }
        }

        public ZipFile File
        {
            get
            {
                return file_;
            }
        }

        public TestOperation Operation
        {
            get
            {
                return operation_;
            }
        }
    }
}
