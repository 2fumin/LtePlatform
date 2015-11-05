using System;

namespace Lte.Domain.Lz4Net.Core
{
    public class ScanFailureEventArgs : EventArgs
    {
        private Exception exception_;
        private string name_;

        public ScanFailureEventArgs(string name, Exception e)
        {
            name_ = name;
            exception_ = e;
            ContinueRunning = true;
        }

        public bool ContinueRunning { get; set; }

        public Exception Exception
        {
            get
            {
                return exception_;
            }
        }

        public string Name
        {
            get
            {
                return name_;
            }
        }
    }
}

