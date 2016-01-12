namespace ZipLib.Zip
{
    public class TestStatus
    {
        private long _bytesTested;
        private ZipEntry _entry;
        private bool _entryValid;
        private int _errorCount;
        private readonly ZipFile _file;
        private TestOperation _operation;

        public TestStatus(ZipFile file)
        {
            _file = file;
        }

        internal void AddError()
        {
            _errorCount++;
            _entryValid = false;
        }

        internal void SetBytesTested(long value)
        {
            _bytesTested = value;
        }

        internal void SetEntry(ZipEntry entry)
        {
            _entry = entry;
            _entryValid = true;
            _bytesTested = 0L;
        }

        internal void SetOperation(TestOperation operation)
        {
            _operation = operation;
        }

        public long BytesTested
        {
            get
            {
                return _bytesTested;
            }
        }

        public ZipEntry Entry
        {
            get
            {
                return _entry;
            }
        }

        public bool EntryValid
        {
            get
            {
                return _entryValid;
            }
        }

        public int ErrorCount
        {
            get
            {
                return _errorCount;
            }
        }

        public ZipFile File
        {
            get
            {
                return _file;
            }
        }

        public TestOperation Operation
        {
            get
            {
                return _operation;
            }
        }
    }
}
