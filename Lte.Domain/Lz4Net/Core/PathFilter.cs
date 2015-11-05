using System.IO;

namespace Lte.Domain.Lz4Net.Core
{
    public class PathFilter : IScanFilter
    {
        private NameFilter nameFilter_;

        public PathFilter(string filter)
        {
            nameFilter_ = new NameFilter(filter);
        }

        public virtual bool IsMatch(string name)
        {
            bool flag = false;
            if (name != null)
            {
                string str = (name.Length > 0) ? Path.GetFullPath(name) : "";
                flag = nameFilter_.IsMatch(str);
            }
            return flag;
        }
    }
}

