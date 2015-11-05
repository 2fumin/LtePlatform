using System;
using System.IO;

namespace Lte.Domain.Lz4Net.Core
{
    [Obsolete("Use ExtendedPathFilter instead")]
    public class NameAndSizeFilter : PathFilter
    {
        private long maxSize_;
        private long minSize_;

        public NameAndSizeFilter(string filter, long minSize, long maxSize)
            : base(filter)
        {
            maxSize_ = 0x7fffffffffffffffL;
            MinSize = minSize;
            MaxSize = maxSize;
        }

        public override bool IsMatch(string name)
        {
            bool flag = base.IsMatch(name);
            if (flag)
            {
                FileInfo info = new FileInfo(name);
                long length = info.Length;
                flag = (MinSize <= length) && (MaxSize >= length);
            }
            return flag;
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

