using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Lz4Net.Core
{
    public delegate void CompletedFileHandler(object sender, ScanEventArgs e);

    public delegate void DirectoryFailureHandler(object sender, ScanFailureEventArgs e);

    public delegate void FileFailureHandler(object sender, ScanFailureEventArgs e);

    public delegate void ProcessDirectoryHandler(object sender, DirectoryEventArgs e);

    public delegate void ProcessFileHandler(object sender, ScanEventArgs e);

    public delegate void ProgressHandler(object sender, ProgressEventArgs e);

}
