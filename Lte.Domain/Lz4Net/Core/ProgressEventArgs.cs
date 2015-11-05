using System;

namespace Lte.Domain.Lz4Net.Core
{
    public class ProgressEventArgs : EventArgs
    {
        private bool continueRunning_ = true;
        private string name_;
        private long processed_;
        private long target_;

        public ProgressEventArgs(string name, long processed, long target)
        {
            name_ = name;
            processed_ = processed;
            target_ = target;
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

        public float PercentComplete
        {
            get
            {
                if (target_ <= 0L)
                {
                    return 0f;
                }
                return ((processed_ / ((float)target_)) * 100f);
            }
        }

        public long Processed
        {
            get
            {
                return processed_;
            }
        }

        public long Target
        {
            get
            {
                return target_;
            }
        }
    }
}

