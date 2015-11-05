using System;
using System.IO;

namespace Lte.Domain.Lz4Net.Core
{
    public class FileSystemScanner
    {
        private bool alive_;
        public CompletedFileHandler CompletedFile { get; set; }
        public DirectoryFailureHandler DirectoryFailure;
        private IScanFilter directoryFilter_;
        public FileFailureHandler FileFailure;
        private IScanFilter fileFilter_;
        public ProcessDirectoryHandler ProcessDirectory;
        public ProcessFileHandler ProcessFile;

        public FileSystemScanner(IScanFilter fileFilter)
        {
            fileFilter_ = fileFilter;
        }

        public FileSystemScanner(string filter)
        {
            fileFilter_ = new PathFilter(filter);
        }

        public FileSystemScanner(IScanFilter fileFilter, IScanFilter directoryFilter)
        {
            fileFilter_ = fileFilter;
            directoryFilter_ = directoryFilter;
        }

        public FileSystemScanner(string fileFilter, string directoryFilter)
        {
            fileFilter_ = new PathFilter(fileFilter);
            directoryFilter_ = new PathFilter(directoryFilter);
        }

        private void OnCompleteFile(string file)
        {
            CompletedFileHandler completedFile = CompletedFile;
            if (completedFile != null)
            {
                ScanEventArgs e = new ScanEventArgs(file);
                completedFile(this, e);
                alive_ = e.ContinueRunning;
            }
        }

        private bool OnDirectoryFailure(string directory, Exception e)
        {
            DirectoryFailureHandler directoryFailure = DirectoryFailure;
            bool flag = directoryFailure != null;
            if (flag)
            {
                ScanFailureEventArgs args = new ScanFailureEventArgs(directory, e);
                directoryFailure(this, args);
                alive_ = args.ContinueRunning;
            }
            return flag;
        }

        private bool OnFileFailure(string file, Exception e)
        {
            bool flag = FileFailure != null;
            if (flag)
            {
                ScanFailureEventArgs args = new ScanFailureEventArgs(file, e);
                FileFailure(this, args);
                alive_ = args.ContinueRunning;
            }
            return flag;
        }

        private void OnProcessDirectory(string directory, bool hasMatchingFiles)
        {
            ProcessDirectoryHandler processDirectory = ProcessDirectory;
            if (processDirectory != null)
            {
                DirectoryEventArgs e = new DirectoryEventArgs(directory, hasMatchingFiles);
                processDirectory(this, e);
                alive_ = e.ContinueRunning;
            }
        }

        private void OnProcessFile(string file)
        {
            ProcessFileHandler processFile = ProcessFile;
            if (processFile != null)
            {
                ScanEventArgs e = new ScanEventArgs(file);
                processFile(this, e);
                alive_ = e.ContinueRunning;
            }
        }

        public void Scan(string directory, bool recurse)
        {
            alive_ = true;
            ScanDir(directory, recurse);
        }

        private void ScanDir(string directory, bool recurse)
        {
            try
            {
                string[] files = Directory.GetFiles(directory);
                bool hasMatchingFiles = false;
                for (int i = 0; i < files.Length; i++)
                {
                    if (!fileFilter_.IsMatch(files[i]))
                    {
                        files[i] = null;
                    }
                    else
                    {
                        hasMatchingFiles = true;
                    }
                }
                OnProcessDirectory(directory, hasMatchingFiles);
                if (alive_ && hasMatchingFiles)
                {
                    foreach (string str in files)
                    {
                        try
                        {
                            if (str != null)
                            {
                                OnProcessFile(str);
                                if (!alive_)
                                {
                                    goto Label_0098;
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            if (!OnFileFailure(str, exception))
                            {
                                throw;
                            }
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                if (!OnDirectoryFailure(directory, exception2))
                {
                    throw;
                }
            }
        Label_0098:
            if (alive_ && recurse)
            {
                try
                {
                    foreach (string str2 in Directory.GetDirectories(directory))
                    {
                        if ((directoryFilter_ == null) || directoryFilter_.IsMatch(str2))
                        {
                            ScanDir(str2, true);
                            if (!alive_)
                            {
                                return;
                            }
                        }
                    }
                }
                catch (Exception exception3)
                {
                    if (!OnDirectoryFailure(directory, exception3))
                    {
                        throw;
                    }
                }
            }
        }
    }
}

