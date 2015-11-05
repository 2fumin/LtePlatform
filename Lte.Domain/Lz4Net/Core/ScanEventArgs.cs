using System;

namespace Lte.Domain.Lz4Net.Core
{
    public class ScanEventArgs : EventArgs
    {
        private bool continueRunning_ = true;
        private string name_;

        public ScanEventArgs(string name)
        {
            name_ = name;
        }

        public bool ContinueRunning
        {
            get
            {
                return continueRunning_;
            }
            set
            {
                continueRunning_ = value;
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

