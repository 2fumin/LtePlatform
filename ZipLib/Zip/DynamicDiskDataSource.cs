using System.IO;

namespace ZipLib.Zip
{
    public class DynamicDiskDataSource : IDynamicDataSource
    {
        public Stream GetSource(ZipEntry entry, string name)
        {
            Stream stream = null;
            if (name != null)
            {
                stream = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            return stream;
        }
    }
}
