namespace Lte.Domain.Lz4Net.Core
{
    public class DirectoryEventArgs : ScanEventArgs
    {
        private bool hasMatchingFiles_;

        public DirectoryEventArgs(string name, bool hasMatchingFiles)
            : base(name)
        {
            hasMatchingFiles_ = hasMatchingFiles;
        }

        public bool HasMatchingFiles
        {
            get
            {
                return hasMatchingFiles_;
            }
        }
    }
}

