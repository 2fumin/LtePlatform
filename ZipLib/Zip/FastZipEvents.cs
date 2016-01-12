using System;
using Lte.Domain.Lz4Net.Core;

namespace Lte.Domain.ZipLib.Zip
{
    public class FastZipEvents
    {
        public CompletedFileHandler CompletedFile { get; set; }
        public DirectoryFailureHandler DirectoryFailure { get; set; }
        public FileFailureHandler FileFailure { get; set; }
        public ProcessDirectoryHandler ProcessDirectory { get; set; }
        public ProcessFileHandler ProcessFile { get; set; }
        public ProgressHandler Progress { get; set; }

        private TimeSpan progressInterval_ = TimeSpan.FromSeconds(3.0);

        public bool OnCompletedFile(string file)
        {
            bool continueRunning = true;
            CompletedFileHandler completedFile = CompletedFile;
            if (completedFile != null)
            {
                ScanEventArgs e = new ScanEventArgs(file);
                completedFile(this, e);
                continueRunning = e.ContinueRunning;
            }
            return continueRunning;
        }

        public bool OnDirectoryFailure(string directory, Exception e)
        {
            bool continueRunning = false;
            DirectoryFailureHandler directoryFailure = DirectoryFailure;
            if (directoryFailure != null)
            {
                ScanFailureEventArgs args = new ScanFailureEventArgs(directory, e);
                directoryFailure(this, args);
                continueRunning = args.ContinueRunning;
            }
            return continueRunning;
        }

        public bool OnFileFailure(string file, Exception e)
        {
            FileFailureHandler fileFailure = FileFailure;
            bool continueRunning = fileFailure != null;
            if (continueRunning)
            {
                ScanFailureEventArgs args = new ScanFailureEventArgs(file, e);
                fileFailure(this, args);
                continueRunning = args.ContinueRunning;
            }
            return continueRunning;
        }

        public bool OnProcessDirectory(string directory, bool hasMatchingFiles)
        {
            bool continueRunning = true;
            ProcessDirectoryHandler processDirectory = ProcessDirectory;
            if (processDirectory != null)
            {
                DirectoryEventArgs e = new DirectoryEventArgs(directory, hasMatchingFiles);
                processDirectory(this, e);
                continueRunning = e.ContinueRunning;
            }
            return continueRunning;
        }

        public bool OnProcessFile(string file)
        {
            bool continueRunning = true;
            ProcessFileHandler processFile = ProcessFile;
            if (processFile != null)
            {
                ScanEventArgs e = new ScanEventArgs(file);
                processFile(this, e);
                continueRunning = e.ContinueRunning;
            }
            return continueRunning;
        }

        public TimeSpan ProgressInterval
        {
            get
            {
                return progressInterval_;
            }
            set
            {
                progressInterval_ = value;
            }
        }
    }
}
