using System;
using System.Collections;
using System.IO;
using Lte.Domain.Lz4Net.Core;

namespace ZipLib.Zip
{
    public class FastZip
    {
        private byte[] _buffer;
        private ConfirmOverwriteDelegate _confirmDelegate;
        private bool _continueRunning;
        private bool _createEmptyDirectories;
        private NameFilter _directoryFilter;
        private IEntryFactory _entryFactory;
        private readonly FastZipEvents _events;
        private INameTransform _extractNameTransform;
        private NameFilter _fileFilter;
        private ZipOutputStream _outputStream;
        private Overwrite _overwrite;
        private string _password;
        private bool _restoreAttributesOnExtract;
        private bool _restoreDateTimeOnExtract;
        private string _sourceDirectory;
        private UseZip64 _useZip64;
        private ZipFile _zipFile;

        public FastZip()
        {
            _entryFactory = new ZipEntryFactory();
            _useZip64 = UseZip64.Dynamic;
        }

        public FastZip(FastZipEvents events)
        {
            _entryFactory = new ZipEntryFactory();
            _useZip64 = UseZip64.Dynamic;
            _events = events;
        }

        private void AddFileContents(string name, Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (_buffer == null)
            {
                _buffer = new byte[0x1000];
            }
            if (_events?.Progress != null)
            {
                StreamUtils.Copy(stream, _outputStream, _buffer, _events.Progress, _events.ProgressInterval, this, name);
            }
            else
            {
                StreamUtils.Copy(stream, _outputStream, _buffer);
            }
            if (_events != null)
            {
                _continueRunning = _events.OnCompletedFile(name);
            }
        }

        public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter)
        {
            CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, null);
        }

        public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
        {
            NameTransform = new ZipNameTransform(sourceDirectory);
            _sourceDirectory = sourceDirectory;
            using (_outputStream = new ZipOutputStream(outputStream))
            {
                if (_password != null)
                {
                    _outputStream.Password = _password;
                }
                _outputStream.UseZip64 = UseZip64;
                FileSystemScanner scanner = new FileSystemScanner(fileFilter, directoryFilter);
                scanner.ProcessFile =
                    (ProcessFileHandler) Delegate.Combine(scanner.ProcessFile, new ProcessFileHandler(ProcessFile));
                if (CreateEmptyDirectories)
                {
                    scanner.ProcessDirectory 
                        = (ProcessDirectoryHandler)Delegate.Combine(scanner.ProcessDirectory, 
                        new ProcessDirectoryHandler(ProcessDirectory));
                }
                if (_events != null)
                {
                    if (_events.FileFailure != null)
                    {
                        scanner.FileFailure = (FileFailureHandler)Delegate.Combine(scanner.FileFailure, _events.FileFailure);
                    }
                    if (_events.DirectoryFailure != null)
                    {
                        scanner.DirectoryFailure = (DirectoryFailureHandler)Delegate.Combine(scanner.DirectoryFailure, _events.DirectoryFailure);
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
                    name = _extractNameTransform.TransformFile(name);
                }
                else if (entry.IsDirectory)
                {
                    name = _extractNameTransform.TransformDirectory(name);
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
                    if (_events != null)
                    {
                        if (entry.IsDirectory)
                        {
                            _continueRunning = _events.OnDirectoryFailure(name, exception);
                        }
                        else
                        {
                            _continueRunning = _events.OnFileFailure(name, exception);
                        }
                    }
                    else
                    {
                        _continueRunning = false;
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
            if ((_overwrite != Overwrite.Always) && File.Exists(targetName))
            {
                if ((_overwrite == Overwrite.Prompt) && (_confirmDelegate != null))
                {
                    flag = _confirmDelegate(targetName);
                }
                else
                {
                    flag = false;
                }
            }
            if (flag)
            {
                if (_events != null)
                {
                    _continueRunning = _events.OnProcessFile(entry.Name);
                }
                if (_continueRunning)
                {
                    try
                    {
                        using (FileStream stream = File.Create(targetName))
                        {
                            if (_buffer == null)
                            {
                                _buffer = new byte[0x1000];
                            }
                            if ((_events != null) && (_events.Progress != null))
                            {
                                StreamUtils.Copy(_zipFile.GetInputStream(entry), stream, _buffer, _events.Progress, _events.ProgressInterval, this, entry.Name, entry.Size);
                            }
                            else
                            {
                                StreamUtils.Copy(_zipFile.GetInputStream(entry), stream, _buffer);
                            }
                            if (_events != null)
                            {
                                _continueRunning = _events.OnCompletedFile(entry.Name);
                            }
                        }
                        if (_restoreDateTimeOnExtract)
                        {
                            File.SetLastWriteTime(targetName, entry.DateTime);
                        }
                        if ((RestoreAttributesOnExtract && entry.IsDosEntry) && (entry.ExternalFileAttributes != -1))
                        {
                            FileAttributes fileAttributes = ((FileAttributes)entry.ExternalFileAttributes) & (FileAttributes.Normal | FileAttributes.Archive | FileAttributes.Hidden | FileAttributes.ReadOnly);
                            File.SetAttributes(targetName, fileAttributes);
                        }
                    }
                    catch (Exception exception)
                    {
                        if (_events == null)
                        {
                            _continueRunning = false;
                            throw;
                        }
                        _continueRunning = _events.OnFileFailure(targetName, exception);
                    }
                }
            }
        }

        public void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
        {
            ExtractZip(zipFileName, targetDirectory, Overwrite.Always, null, fileFilter, null, _restoreDateTimeOnExtract);
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
                throw new ArgumentNullException(nameof(confirmDelegate));
            }
            _continueRunning = true;
            _overwrite = overwrite;
            _confirmDelegate = confirmDelegate;
            _extractNameTransform = new WindowsNameTransform(targetDirectory);
            _fileFilter = new NameFilter(fileFilter);
            _directoryFilter = new NameFilter(directoryFilter);
            _restoreDateTimeOnExtract = restoreDateTime;
            using (_zipFile = new ZipFile(inputStream))
            {
                if (_password != null)
                {
                    _zipFile.Password = _password;
                }
                _zipFile.IsStreamOwner = isStreamOwner;
                IEnumerator enumerator = _zipFile.GetEnumerator();
                while (_continueRunning && enumerator.MoveNext())
                {
                    ZipEntry current = (ZipEntry)enumerator.Current;
                    if (current.IsFile)
                    {
                        if (_directoryFilter.IsMatch(Path.GetDirectoryName(current.Name)) && _fileFilter.IsMatch(current.Name))
                        {
                            ExtractEntry(current);
                        }
                    }
                    else if ((current.IsDirectory && _directoryFilter.IsMatch(current.Name)) && CreateEmptyDirectories)
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
                if (_events != null)
                {
                    _events.OnProcessDirectory(e.Name, e.HasMatchingFiles);
                }
                if (e.ContinueRunning && (e.Name != _sourceDirectory))
                {
                    ZipEntry entry = _entryFactory.MakeDirectoryEntry(e.Name);
                    _outputStream.PutNextEntry(entry);
                }
            }
        }

        private void ProcessFile(object sender, ScanEventArgs e)
        {
            if ((_events != null) && (_events.ProcessFile != null))
            {
                _events.ProcessFile(sender, e);
            }
            if (e.ContinueRunning)
            {
                try
                {
                    using (FileStream stream = File.Open(e.Name, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        ZipEntry entry = _entryFactory.MakeFileEntry(e.Name);
                        _outputStream.PutNextEntry(entry);
                        AddFileContents(e.Name, stream);
                    }
                }
                catch (Exception exception)
                {
                    if (_events == null)
                    {
                        _continueRunning = false;
                        throw;
                    }
                    _continueRunning = _events.OnFileFailure(e.Name, exception);
                }
            }
        }

        public bool CreateEmptyDirectories
        {
            get
            {
                return _createEmptyDirectories;
            }
            set
            {
                _createEmptyDirectories = value;
            }
        }

        public IEntryFactory EntryFactory
        {
            get
            {
                return _entryFactory;
            }
            set
            {
                if (value == null)
                {
                    _entryFactory = new ZipEntryFactory();
                }
                else
                {
                    _entryFactory = value;
                }
            }
        }

        public INameTransform NameTransform
        {
            get
            {
                return _entryFactory.NameTransform;
            }
            set
            {
                _entryFactory.NameTransform = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public bool RestoreAttributesOnExtract
        {
            get
            {
                return _restoreAttributesOnExtract;
            }
            set
            {
                _restoreAttributesOnExtract = value;
            }
        }

        public bool RestoreDateTimeOnExtract
        {
            get
            {
                return _restoreDateTimeOnExtract;
            }
            set
            {
                _restoreDateTimeOnExtract = value;
            }
        }

        public UseZip64 UseZip64
        {
            get
            {
                return _useZip64;
            }
            set
            {
                _useZip64 = value;
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
