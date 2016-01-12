using System;
using System.Collections;
using System.IO;
using Lte.Domain.Lz4Net.Core;

namespace Lte.Domain.ZipLib.Zip
{
    public class FastZip
    {
        private byte[] buffer_;
        private ConfirmOverwriteDelegate confirmDelegate_;
        private bool continueRunning_;
        private bool createEmptyDirectories_;
        private NameFilter directoryFilter_;
        private IEntryFactory entryFactory_;
        private FastZipEvents events_;
        private INameTransform extractNameTransform_;
        private NameFilter fileFilter_;
        private ZipOutputStream outputStream_;
        private Overwrite overwrite_;
        private string password_;
        private bool restoreAttributesOnExtract_;
        private bool restoreDateTimeOnExtract_;
        private string sourceDirectory_;
        private UseZip64 useZip64_;
        private ZipFile zipFile_;

        public FastZip()
        {
            entryFactory_ = new ZipEntryFactory();
            useZip64_ = UseZip64.Dynamic;
        }

        public FastZip(FastZipEvents events)
        {
            entryFactory_ = new ZipEntryFactory();
            useZip64_ = UseZip64.Dynamic;
            events_ = events;
        }

        private void AddFileContents(string name, Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (buffer_ == null)
            {
                buffer_ = new byte[0x1000];
            }
            if ((events_ != null) && (events_.Progress != null))
            {
                StreamUtils.Copy(stream, outputStream_, buffer_, events_.Progress, events_.ProgressInterval, this, name);
            }
            else
            {
                StreamUtils.Copy(stream, outputStream_, buffer_);
            }
            if (events_ != null)
            {
                continueRunning_ = events_.OnCompletedFile(name);
            }
        }

        public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter)
        {
            CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, null);
        }

        public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
        {
            NameTransform = new ZipNameTransform(sourceDirectory);
            sourceDirectory_ = sourceDirectory;
            using (outputStream_ = new ZipOutputStream(outputStream))
            {
                if (password_ != null)
                {
                    outputStream_.Password = password_;
                }
                outputStream_.UseZip64 = UseZip64;
                FileSystemScanner scanner = new FileSystemScanner(fileFilter, directoryFilter);
                scanner.ProcessFile =
                    (ProcessFileHandler) Delegate.Combine(scanner.ProcessFile, new ProcessFileHandler(ProcessFile));
                if (CreateEmptyDirectories)
                {
                    scanner.ProcessDirectory 
                        = (ProcessDirectoryHandler)Delegate.Combine(scanner.ProcessDirectory, 
                        new ProcessDirectoryHandler(ProcessDirectory));
                }
                if (events_ != null)
                {
                    if (events_.FileFailure != null)
                    {
                        scanner.FileFailure = (FileFailureHandler)Delegate.Combine(scanner.FileFailure, events_.FileFailure);
                    }
                    if (events_.DirectoryFailure != null)
                    {
                        scanner.DirectoryFailure = (DirectoryFailureHandler)Delegate.Combine(scanner.DirectoryFailure, events_.DirectoryFailure);
                    }
                }
                scanner.Scan(sourceDirectory, recurse);
            }
        }

        public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
        {
            CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter);
        }

        private void ExtractEntry(ZipEntry entry)
        {
            bool flag = entry.IsCompressionMethodSupported();
            string name = entry.Name;
            if (flag)
            {
                if (entry.IsFile)
                {
                    name = extractNameTransform_.TransformFile(name);
                }
                else if (entry.IsDirectory)
                {
                    name = extractNameTransform_.TransformDirectory(name);
                }
                flag = (name != null) && (name.Length != 0);
            }
            string path = null;
            if (flag)
            {
                if (entry.IsDirectory)
                {
                    path = name;
                }
                else
                {
                    path = Path.GetDirectoryName(Path.GetFullPath(name));
                }
            }
            if ((flag && !Directory.Exists(path)) && (!entry.IsDirectory || CreateEmptyDirectories))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception exception)
                {
                    flag = false;
                    if (events_ != null)
                    {
                        if (entry.IsDirectory)
                        {
                            continueRunning_ = events_.OnDirectoryFailure(name, exception);
                        }
                        else
                        {
                            continueRunning_ = events_.OnFileFailure(name, exception);
                        }
                    }
                    else
                    {
                        continueRunning_ = false;
                        throw;
                    }
                }
            }
            if (flag && entry.IsFile)
            {
                ExtractFileEntry(entry, name);
            }
        }

        private void ExtractFileEntry(ZipEntry entry, string targetName)
        {
            bool flag = true;
            if ((overwrite_ != Overwrite.Always) && File.Exists(targetName))
            {
                if ((overwrite_ == Overwrite.Prompt) && (confirmDelegate_ != null))
                {
                    flag = confirmDelegate_(targetName);
                }
                else
                {
                    flag = false;
                }
            }
            if (flag)
            {
                if (events_ != null)
                {
                    continueRunning_ = events_.OnProcessFile(entry.Name);
                }
                if (continueRunning_)
                {
                    try
                    {
                        using (FileStream stream = File.Create(targetName))
                        {
                            if (buffer_ == null)
                            {
                                buffer_ = new byte[0x1000];
                            }
                            if ((events_ != null) && (events_.Progress != null))
                            {
                                StreamUtils.Copy(zipFile_.GetInputStream(entry), stream, buffer_, events_.Progress, events_.ProgressInterval, this, entry.Name, entry.Size);
                            }
                            else
                            {
                                StreamUtils.Copy(zipFile_.GetInputStream(entry), stream, buffer_);
                            }
                            if (events_ != null)
                            {
                                continueRunning_ = events_.OnCompletedFile(entry.Name);
                            }
                        }
                        if (restoreDateTimeOnExtract_)
                        {
                            File.SetLastWriteTime(targetName, entry.DateTime);
                        }
                        if ((RestoreAttributesOnExtract && entry.IsDOSEntry) && (entry.ExternalFileAttributes != -1))
                        {
                            FileAttributes fileAttributes = ((FileAttributes)entry.ExternalFileAttributes) & (FileAttributes.Normal | FileAttributes.Archive | FileAttributes.Hidden | FileAttributes.ReadOnly);
                            File.SetAttributes(targetName, fileAttributes);
                        }
                    }
                    catch (Exception exception)
                    {
                        if (events_ == null)
                        {
                            continueRunning_ = false;
                            throw;
                        }
                        continueRunning_ = events_.OnFileFailure(targetName, exception);
                    }
                }
            }
        }

        public void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
        {
            ExtractZip(zipFileName, targetDirectory, Overwrite.Always, null, fileFilter, null, restoreDateTimeOnExtract_);
        }

        public void ExtractZip(string zipFileName, string targetDirectory, Overwrite overwrite, ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime)
        {
            Stream inputStream = File.Open(zipFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            ExtractZip(inputStream, targetDirectory, overwrite, confirmDelegate, fileFilter, directoryFilter, restoreDateTime, true);
        }

        public void ExtractZip(Stream inputStream, string targetDirectory, Overwrite overwrite, ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime, bool isStreamOwner)
        {
            if ((overwrite == Overwrite.Prompt) && (confirmDelegate == null))
            {
                throw new ArgumentNullException("confirmDelegate");
            }
            continueRunning_ = true;
            overwrite_ = overwrite;
            confirmDelegate_ = confirmDelegate;
            extractNameTransform_ = new WindowsNameTransform(targetDirectory);
            fileFilter_ = new NameFilter(fileFilter);
            directoryFilter_ = new NameFilter(directoryFilter);
            restoreDateTimeOnExtract_ = restoreDateTime;
            using (zipFile_ = new ZipFile(inputStream))
            {
                if (password_ != null)
                {
                    zipFile_.Password = password_;
                }
                zipFile_.IsStreamOwner = isStreamOwner;
                IEnumerator enumerator = zipFile_.GetEnumerator();
                while (continueRunning_ && enumerator.MoveNext())
                {
                    ZipEntry current = (ZipEntry)enumerator.Current;
                    if (current.IsFile)
                    {
                        if (directoryFilter_.IsMatch(Path.GetDirectoryName(current.Name)) && fileFilter_.IsMatch(current.Name))
                        {
                            ExtractEntry(current);
                        }
                    }
                    else if ((current.IsDirectory && directoryFilter_.IsMatch(current.Name)) && CreateEmptyDirectories)
                    {
                        ExtractEntry(current);
                    }
                }
            }
        }

        private static int MakeExternalAttributes(FileInfo info)
        {
            return (int)info.Attributes;
        }

        private static bool NameIsValid(string name)
        {
            return (((!string.IsNullOrEmpty(name))) && (name.IndexOfAny(Path.GetInvalidPathChars()) < 0));
        }

        private void ProcessDirectory(object sender, DirectoryEventArgs e)
        {
            if (!e.HasMatchingFiles && CreateEmptyDirectories)
            {
                if (events_ != null)
                {
                    events_.OnProcessDirectory(e.Name, e.HasMatchingFiles);
                }
                if (e.ContinueRunning && (e.Name != sourceDirectory_))
                {
                    ZipEntry entry = entryFactory_.MakeDirectoryEntry(e.Name);
                    outputStream_.PutNextEntry(entry);
                }
            }
        }

        private void ProcessFile(object sender, ScanEventArgs e)
        {
            if ((events_ != null) && (events_.ProcessFile != null))
            {
                events_.ProcessFile(sender, e);
            }
            if (e.ContinueRunning)
            {
                try
                {
                    using (FileStream stream = File.Open(e.Name, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        ZipEntry entry = entryFactory_.MakeFileEntry(e.Name);
                        outputStream_.PutNextEntry(entry);
                        AddFileContents(e.Name, stream);
                    }
                }
                catch (Exception exception)
                {
                    if (events_ == null)
                    {
                        continueRunning_ = false;
                        throw;
                    }
                    continueRunning_ = events_.OnFileFailure(e.Name, exception);
                }
            }
        }

        public bool CreateEmptyDirectories
        {
            get
            {
                return createEmptyDirectories_;
            }
            set
            {
                createEmptyDirectories_ = value;
            }
        }

        public IEntryFactory EntryFactory
        {
            get
            {
                return entryFactory_;
            }
            set
            {
                if (value == null)
                {
                    entryFactory_ = new ZipEntryFactory();
                }
                else
                {
                    entryFactory_ = value;
                }
            }
        }

        public INameTransform NameTransform
        {
            get
            {
                return entryFactory_.NameTransform;
            }
            set
            {
                entryFactory_.NameTransform = value;
            }
        }

        public string Password
        {
            get
            {
                return password_;
            }
            set
            {
                password_ = value;
            }
        }

        public bool RestoreAttributesOnExtract
        {
            get
            {
                return restoreAttributesOnExtract_;
            }
            set
            {
                restoreAttributesOnExtract_ = value;
            }
        }

        public bool RestoreDateTimeOnExtract
        {
            get
            {
                return restoreDateTimeOnExtract_;
            }
            set
            {
                restoreDateTimeOnExtract_ = value;
            }
        }

        public UseZip64 UseZip64
        {
            get
            {
                return useZip64_;
            }
            set
            {
                useZip64_ = value;
            }
        }

        public delegate bool ConfirmOverwriteDelegate(string fileName);

        public enum Overwrite
        {
            Prompt,
            Never,
            Always
        }
    }
}
