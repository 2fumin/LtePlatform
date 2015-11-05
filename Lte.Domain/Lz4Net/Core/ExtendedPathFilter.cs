using System;
using System.IO;

namespace Lte.Domain.Lz4Net.Core
{
    public class ExtendedPathFilter : PathFilter
    {
        private DateTime maxDate_;
        private long maxSize_;
        private DateTime minDate_;
        private long minSize_;

        public ExtendedPathFilter(string filter, DateTime minDate, DateTime maxDate)
            : base(filter)
        {
            maxSize_ = 0x7fffffffffffffffL;
            minDate_ = DateTime.MinValue;
            maxDate_ = DateTime.MaxValue;
            MinDate = minDate;
            MaxDate = maxDate;
        }

        public ExtendedPathFilter(string filter, long minSize, long maxSize)
            : base(filter)
        {
            maxSize_ = 0x7fffffffffffffffL;
            minDate_ = DateTime.MinValue;
            maxDate_ = DateTime.MaxValue;
            MinSize = minSize;
            MaxSize = maxSize;
        }

        public ExtendedPathFilter(string filter, long minSize, long maxSize, DateTime minDate, DateTime maxDate)
            : base(filter)
        {
            maxSize_ = 0x7fffffffffffffffL;
            minDate_ = DateTime.MinValue;
            maxDate_ = DateTime.MaxValue;
            MinSize = minSize;
            MaxSize = maxSize;
            MinDate = minDate;
            MaxDate = maxDate;
        }

        public override bool IsMatch(string name)
        {
            bool flag = base.IsMatch(name);
            if (flag)
            {
                FileInfo info = new FileInfo(name);
                flag = (((MinSize <= info.Length) && (MaxSize >= info.Length)) && (MinDate <= info.LastWriteTime)) && (MaxDate >= info.LastWriteTime);
            }
            return flag;
        }

        public DateTime MaxDate
        {
            get
            {
                return maxDate_;
            }
            set
            {
                if (minDate_ > value)
                {
                    throw new ArgumentOutOfRangeException("value", "Exceeds MinDate");
                }
                maxDate_ = value;
            }
        }

        public long MaxSize
        {
            get
            {
                return maxSize_;
            }
            set
            {
                if ((value < 0L) || (minSize_ > value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                maxSize_ = value;
            }
        }

        public DateTime MinDate
        {
            get
            {
                return minDate_;
            }
            set
            {
                if (value > maxDate_)
                {
                    throw new ArgumentOutOfRangeException("value", "Exceeds MaxDate");
                }
                minDate_ = value;
            }
        }

        public long MinSize
        {
            get
            {
                return minSize_;
            }
            set
            {
                if ((value < 0L) || (maxSize_ < value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                minSize_ = value;
            }
        }
    }
}

