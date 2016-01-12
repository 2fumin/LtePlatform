using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Lte.Domain.Lz4Net.Core;
using Lte.Domain.Lz4Net.Encryption;
using ZipLib.CheckSums;
using ZipLib.Comppression;
using ZipLib.Encryption;
using ZipLib.Streams;

namespace ZipLib.Zip
{
    public class ZipFile : IEnumerable, IDisposable
    {
        private IArchiveStorage _archiveStorage;
        private Stream _baseStream;
        private int _bufferSize;
        private string _comment;
        private bool _commentEdited;
        private bool _contentsEdited;
        private byte[] _copyBuffer;
        private const int DefaultBufferSize = 0x1000;
        private ZipEntry[] _entries;
        private bool _isDisposed;
        private bool _isNewArchive;
        private bool _isStreamOwner;
        private byte[] _key;
        public KeysRequiredEventHandler KeysRequired;
        private string _name;
        private ZipString _newComment;
        private long _offsetOfFirstEntry;
        private string _rawPassword;
        private long _updateCount;
        private IDynamicDataSource _updateDataSource;
        private IEntryFactory _updateEntryFactory;
        private Hashtable _updateIndex;
        private ArrayList _updates;
        private UseZip64 _useZip64;

        internal ZipFile()
        {
            _useZip64 = UseZip64.Dynamic;
            _bufferSize = DefaultBufferSize;
            _updateEntryFactory = new ZipEntryFactory();
            _entries = new ZipEntry[0];
            _isNewArchive = true;
        }

        public ZipFile(FileStream file)
        {
            _useZip64 = UseZip64.Dynamic;
            _bufferSize = DefaultBufferSize;
            _updateEntryFactory = new ZipEntryFactory();
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            if (!file.CanSeek)
            {
                throw new ArgumentException("Stream is not seekable", nameof(file));
            }
            _baseStream = file;
            _name = file.Name;
            _isStreamOwner = true;
            try
            {
                ReadEntries();
            }
            catch
            {
                DisposeInternal(true);
                throw;
            }
        }

        public ZipFile(Stream stream)
        {
            _useZip64 = UseZip64.Dynamic;
            _bufferSize = DefaultBufferSize;
            _updateEntryFactory = new ZipEntryFactory();
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (!stream.CanSeek)
            {
                throw new ArgumentException("Stream is not seekable", nameof(stream));
            }
            _baseStream = stream;
            _isStreamOwner = true;
            if (_baseStream.Length > 0L)
            {
                try
                {
                    ReadEntries();
                    return;
                }
                catch
                {
                    DisposeInternal(true);
                    throw;
                }
            }
            _entries = new ZipEntry[0];
            _isNewArchive = true;
        }

        public ZipFile(string name)
        {
            _useZip64 = UseZip64.Dynamic;
            _bufferSize = DefaultBufferSize;
            _updateEntryFactory = new ZipEntryFactory();
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            _name = name;
            _baseStream = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            _isStreamOwner = true;
            try
            {
                ReadEntries();
            }
            catch
            {
                DisposeInternal(true);
                throw;
            }
        }

        public void AbortUpdate()
        {
            PostUpdateCleanup();
        }

        public void Add(ZipEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            CheckUpdating();
            if ((entry.Size != 0L) || (entry.CompressedSize != 0L))
            {
                throw new ZipException("Entry cannot have any data");
            }
            AddUpdate(new ZipUpdate(UpdateCommand.Add, entry));
        }

        public void Add(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            CheckUpdating();
            AddUpdate(new ZipUpdate(fileName, EntryFactory.MakeFileEntry(fileName)));
        }

        public void Add(IStaticDataSource dataSource, string entryName)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException(nameof(dataSource));
            }
            if (entryName == null)
            {
                throw new ArgumentNullException(nameof(entryName));
            }
            CheckUpdating();
            AddUpdate(new ZipUpdate(dataSource, EntryFactory.MakeFileEntry(entryName, false)));
        }

        public void Add(string fileName, CompressionMethod compressionMethod)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (!ZipEntry.IsCompressionMethodSupported(compressionMethod))
            {
                throw new ArgumentOutOfRangeException(nameof(compressionMethod));
            }
            CheckUpdating();
            _contentsEdited = true;
            var entry = EntryFactory.MakeFileEntry(fileName);
            entry.CompressionMethod = compressionMethod;
            AddUpdate(new ZipUpdate(fileName, entry));
        }

        public void Add(string fileName, string entryName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (entryName == null)
            {
                throw new ArgumentNullException(nameof(entryName));
            }
            CheckUpdating();
            AddUpdate(new ZipUpdate(fileName, EntryFactory.MakeFileEntry(entryName)));
        }

        public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException(nameof(dataSource));
            }
            if (entryName == null)
            {
                throw new ArgumentNullException(nameof(entryName));
            }
            CheckUpdating();
            var entry = EntryFactory.MakeFileEntry(entryName, false);
            entry.CompressionMethod = compressionMethod;
            AddUpdate(new ZipUpdate(dataSource, entry));
        }

        public void Add(string fileName, CompressionMethod compressionMethod, bool useUnicodeText)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            if (!ZipEntry.IsCompressionMethodSupported(compressionMethod))
            {
                throw new ArgumentOutOfRangeException(nameof(compressionMethod));
            }
            CheckUpdating();
            _contentsEdited = true;
            var entry = EntryFactory.MakeFileEntry(fileName);
            entry.IsUnicodeText = useUnicodeText;
            entry.CompressionMethod = compressionMethod;
            AddUpdate(new ZipUpdate(fileName, entry));
        }

        public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod, bool useUnicodeText)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException(nameof(dataSource));
            }
            if (entryName == null)
            {
                throw new ArgumentNullException(nameof(entryName));
            }
            CheckUpdating();
            var entry = EntryFactory.MakeFileEntry(entryName, false);
            entry.IsUnicodeText = useUnicodeText;
            entry.CompressionMethod = compressionMethod;
            AddUpdate(new ZipUpdate(dataSource, entry));
        }

        public void AddDirectory(string directoryName)
        {
            if (directoryName == null)
            {
                throw new ArgumentNullException(nameof(directoryName));
            }
            CheckUpdating();
            var entry = EntryFactory.MakeDirectoryEntry(directoryName);
            AddUpdate(new ZipUpdate(UpdateCommand.Add, entry));
        }

        private void AddEntry(ZipFile workFile, ZipUpdate update)
        {
            Stream source = null;
            if (update.Entry.IsFile)
            {
                source = update.GetSource();
                if (source == null)
                {
                    source = _updateDataSource.GetSource(update.Entry, update.Filename);
                }
            }
            if (source != null)
            {
                using (source)
                {
                    var length = source.Length;
                    if (update.OutEntry.Size < 0L)
                    {
                        update.OutEntry.Size = length;
                    }
                    else if (update.OutEntry.Size != length)
                    {
                        throw new ZipException("Entry size/stream size mismatch");
                    }
                    workFile.WriteLocalEntryHeader(update);
                    var position = workFile._baseStream.Position;
                    using (var stream2 = workFile.GetOutputStream(update.OutEntry))
                    {
                        CopyBytes(update, stream2, source, length, true);
                    }
                    var num3 = workFile._baseStream.Position;
                    update.OutEntry.CompressedSize = num3 - position;
                    if ((update.OutEntry.Flags & 8) == 8)
                    {
                        new ZipHelperStream(workFile._baseStream).WriteDataDescriptor(update.OutEntry);
                    }
                    return;
                }
            }
            workFile.WriteLocalEntryHeader(update);
            update.OutEntry.CompressedSize = 0L;
        }

        private void AddUpdate(ZipUpdate update)
        {
            _contentsEdited = true;
            var num = FindExistingUpdate(update.Entry.Name);
            if (num >= 0)
            {
                if (_updates[num] == null)
                {
                    _updateCount += 1L;
                }
                _updates[num] = update;
            }
            else
            {
                num = _updates.Add(update);
                _updateCount += 1L;
                _updateIndex.Add(update.Entry.Name, num);
            }
        }

        public void BeginUpdate()
        {
            if (Name == null)
            {
                BeginUpdate(new MemoryArchiveStorage(), new DynamicDiskDataSource());
            }
            else
            {
                BeginUpdate(new DiskArchiveStorage(this), new DynamicDiskDataSource());
            }
        }

        public void BeginUpdate(IArchiveStorage archiveStorage)
        {
            BeginUpdate(archiveStorage, new DynamicDiskDataSource());
        }

        public void BeginUpdate(IArchiveStorage archiveStorage, IDynamicDataSource dataSource)
        {
            if (archiveStorage == null)
            {
                throw new ArgumentNullException(nameof(archiveStorage));
            }
            if (dataSource == null)
            {
                throw new ArgumentNullException(nameof(dataSource));
            }
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            if (IsEmbeddedArchive)
            {
                throw new ZipException("Cannot update embedded/SFX archives");
            }
            _archiveStorage = archiveStorage;
            _updateDataSource = dataSource;
            _updateIndex = new Hashtable();
            _updates = new ArrayList(_entries.Length);
            foreach (var entry in _entries)
            {
                var num = _updates.Add(new ZipUpdate(entry));
                _updateIndex.Add(entry.Name, num);
            }
            _updates.Sort(new UpdateComparer());
            var num2 = 0;
            foreach (ZipUpdate update in _updates)
            {
                if (num2 == (_updates.Count - 1))
                {
                    break;
                }
                update.OffsetBasedSize = ((ZipUpdate)_updates[num2 + 1]).Entry.Offset - update.Entry.Offset;
                num2++;
            }
            _updateCount = _updates.Count;
            _contentsEdited = false;
            _commentEdited = false;
            _newComment = null;
        }

        private static void CheckClassicPassword(CryptoStream classicCryptoStream, ZipEntry entry)
        {
            var buffer = new byte[12];
            StreamUtils.ReadFully(classicCryptoStream, buffer);
            if (buffer[11] != entry.CryptoCheckValue)
            {
                throw new ZipException("Invalid password");
            }
        }

        private void CheckUpdating()
        {
            if (_updates == null)
            {
                throw new InvalidOperationException("BeginUpdate has not been called");
            }
        }

        public void Close()
        {
            DisposeInternal(true);
            GC.SuppressFinalize(this);
        }

        public void CommitUpdate()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            CheckUpdating();
            try
            {
                _updateIndex.Clear();
                _updateIndex = null;
                if (_contentsEdited)
                {
                    RunUpdates();
                }
                else if (_commentEdited)
                {
                    UpdateCommentOnly();
                }
                else if (_entries.Length == 0)
                {
                    var comment = (_newComment != null) ? _newComment.RawComment : ZipConstants.ConvertToArray(_comment);
                    using (var stream = new ZipHelperStream(_baseStream))
                    {
                        stream.WriteEndOfCentralDirectory(0L, 0L, 0L, comment);
                    }
                }
            }
            finally
            {
                PostUpdateCleanup();
            }
        }

        private void CopyBytes(ZipUpdate update, Stream destination, Stream source, long bytesToCopy, bool updateCrc)
        {
            int num3;
            if (destination == source)
            {
                throw new InvalidOperationException("Destination and source are the same");
            }
            var crc = new Crc32();
            var buffer = GetBuffer();
            var num = bytesToCopy;
            var num2 = 0L;
            do
            {
                var length = buffer.Length;
                if (bytesToCopy < length)
                {
                    length = (int)bytesToCopy;
                }
                num3 = source.Read(buffer, 0, length);
                if (num3 > 0)
                {
                    if (updateCrc)
                    {
                        crc.Update(buffer, 0, num3);
                    }
                    destination.Write(buffer, 0, num3);
                    bytesToCopy -= num3;
                    num2 += num3;
                }
            }
            while ((num3 > 0) && (bytesToCopy > 0L));
            if (num2 != num)
            {
                throw new ZipException($"Failed to copy bytes expected {num} read {num2}");
            }
            if (updateCrc)
            {
                update.OutEntry.Crc = crc.Value;
            }
        }

        private void CopyDescriptorBytes(ZipUpdate update, Stream dest, Stream source)
        {
            var descriptorSize = GetDescriptorSize(update);
            if (descriptorSize > 0)
            {
                var buffer = GetBuffer();
                while (descriptorSize > 0)
                {
                    var count = Math.Min(buffer.Length, descriptorSize);
                    var num3 = source.Read(buffer, 0, count);
                    if (num3 <= 0)
                    {
                        throw new ZipException("Unxpected end of stream");
                    }
                    dest.Write(buffer, 0, num3);
                    descriptorSize -= num3;
                }
            }
        }

        private void CopyDescriptorBytesDirect(ZipUpdate update, Stream stream, ref long destinationPosition, long sourcePosition)
        {
            var descriptorSize = GetDescriptorSize(update);
            while (descriptorSize > 0)
            {
                var count = descriptorSize;
                var buffer = GetBuffer();
                stream.Position = sourcePosition;
                var num3 = stream.Read(buffer, 0, count);
                if (num3 <= 0)
                {
                    throw new ZipException("Unxpected end of stream");
                }
                stream.Position = destinationPosition;
                stream.Write(buffer, 0, num3);
                descriptorSize -= num3;
                destinationPosition += num3;
                sourcePosition += num3;
            }
        }

        private void CopyEntry(ZipFile workFile, ZipUpdate update)
        {
            workFile.WriteLocalEntryHeader(update);
            if (update.Entry.CompressedSize > 0L)
            {
                var offset = update.Entry.Offset + 0x1aL;
                _baseStream.Seek(offset, SeekOrigin.Begin);
                uint num2 = ReadLeUshort();
                uint num3 = ReadLeUshort();
                _baseStream.Seek(num2 + num3, SeekOrigin.Current);
                CopyBytes(update, workFile._baseStream, _baseStream, update.Entry.CompressedSize, false);
            }
            CopyDescriptorBytes(update, workFile._baseStream, _baseStream);
        }

        private void CopyEntryDataDirect(ZipUpdate update, Stream stream, bool updateCrc, ref long destinationPosition, ref long sourcePosition)
        {
            int num4;
            var compressedSize = update.Entry.CompressedSize;
            var crc = new Crc32();
            var buffer = GetBuffer();
            var num2 = compressedSize;
            var num3 = 0L;
            do
            {
                var length = buffer.Length;
                if (compressedSize < length)
                {
                    length = (int)compressedSize;
                }
                stream.Position = sourcePosition;
                num4 = stream.Read(buffer, 0, length);
                if (num4 > 0)
                {
                    if (updateCrc)
                    {
                        crc.Update(buffer, 0, num4);
                    }
                    stream.Position = destinationPosition;
                    stream.Write(buffer, 0, num4);
                    destinationPosition += num4;
                    sourcePosition += num4;
                    compressedSize -= num4;
                    num3 += num4;
                }
            }
            while ((num4 > 0) && (compressedSize > 0L));
            if (num3 != num2)
            {
                throw new ZipException($"Failed to copy bytes expected {num2} read {num3}");
            }
            if (updateCrc)
            {
                update.OutEntry.Crc = crc.Value;
            }
        }

        private void CopyEntryDirect(ZipFile workFile, ZipUpdate update, ref long destinationPosition)
        {
            var flag = update.Entry.Offset == destinationPosition;
            if (!flag)
            {
                _baseStream.Position = destinationPosition;
                workFile.WriteLocalEntryHeader(update);
                destinationPosition = _baseStream.Position;
            }
            var offset = update.Entry.Offset + 0x1aL;
            _baseStream.Seek(offset, SeekOrigin.Begin);
            uint num3 = ReadLeUshort();
            uint num4 = ReadLeUshort();
            var sourcePosition = (_baseStream.Position + num3) + num4;
            if (flag)
            {
                if (update.OffsetBasedSize != -1L)
                {
                    destinationPosition += update.OffsetBasedSize;
                }
                else
                {
                    destinationPosition += (((sourcePosition - offset) + 0x1aL) + update.Entry.CompressedSize) + GetDescriptorSize(update);
                }
            }
            else
            {
                if (update.Entry.CompressedSize > 0L)
                {
                    CopyEntryDataDirect(update, _baseStream, false, ref destinationPosition, ref sourcePosition);
                }
                CopyDescriptorBytesDirect(update, _baseStream, ref destinationPosition, sourcePosition);
            }
        }

        public static ZipFile Create(Stream outStream)
        {
            if (outStream == null)
            {
                throw new ArgumentNullException(nameof(outStream));
            }
            if (!outStream.CanWrite)
            {
                throw new ArgumentException("Stream is not writeable", nameof(outStream));
            }
            if (!outStream.CanSeek)
            {
                throw new ArgumentException("Stream is not seekable", nameof(outStream));
            }
            return new ZipFile { _baseStream = outStream };
        }

        public static ZipFile Create(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            var stream = File.Create(fileName);
            return new ZipFile { _name = fileName, _baseStream = stream, _isStreamOwner = true };
        }

        private Stream CreateAndInitDecryptionStream(Stream baseStream, ZipEntry entry)
        {
            if ((entry.Version < 50) || ((entry.Flags & 0x40) == 0))
            {
                var managed = new PkzipClassicManaged();
                OnKeysRequired(entry.Name);
                if (!HaveKeys)
                {
                    throw new ZipException("No password available for encrypted stream");
                }
                var classicCryptoStream 
                    = new CryptoStream(baseStream, managed.CreateDecryptor(_key, null), CryptoStreamMode.Read);
                CheckClassicPassword(classicCryptoStream, entry);
                return classicCryptoStream;
            }
            if (entry.Version != 0x33)
            {
                throw new ZipException("Decryption method not supported");
            }
            OnKeysRequired(entry.Name);
            if (!HaveKeys)
            {
                throw new ZipException("No password available for AES encrypted stream");
            }
            var aEsSaltLen = entry.AESSaltLen;
            var buffer = new byte[aEsSaltLen];
            var num2 = baseStream.Read(buffer, 0, aEsSaltLen);
            if (num2 != aEsSaltLen)
            {
                throw new ZipException(string.Concat(new object[] { "AES Salt expected ", aEsSaltLen, " got ", num2 }));
            }
            var buffer2 = new byte[2];
            baseStream.Read(buffer2, 0, 2);
            var blockSize = entry.AESKeySize / 8;
            var transform = new ZipAESTransform(_rawPassword, buffer, blockSize, false);
            var pwdVerifier = transform.PwdVerifier;
            if ((pwdVerifier[0] != buffer2[0]) || (pwdVerifier[1] != buffer2[1]))
            {
                throw new Exception("Invalid password for AES");
            }
            return new ZipAESStream(baseStream, transform, CryptoStreamMode.Read);
        }

        private Stream CreateAndInitEncryptionStream(Stream baseStream, ZipEntry entry)
        {
            CryptoStream stream = null;
            if ((entry.Version < 50) || ((entry.Flags & 0x40) == 0))
            {
                var managed = new PkzipClassicManaged();
                OnKeysRequired(entry.Name);
                if (!HaveKeys)
                {
                    throw new ZipException("No password available for encrypted stream");
                }
                stream = new CryptoStream(new UncompressedStream(baseStream), managed.CreateEncryptor(_key, null), CryptoStreamMode.Write);
                if ((entry.Crc < 0L) || ((entry.Flags & 8) != 0))
                {
                    WriteEncryptionHeader(stream, entry.DosTime << 0x10);
                    return stream;
                }
                WriteEncryptionHeader(stream, entry.Crc);
            }
            return stream;
        }

        public void Delete(ZipEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            CheckUpdating();
            var num = FindExistingUpdate(entry);
            if (num < 0)
            {
                throw new ZipException("Cannot find entry to delete");
            }
            _contentsEdited = true;
            _updates[num] = null;
            _updateCount -= 1L;
        }

        public bool Delete(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            CheckUpdating();
            var num = FindExistingUpdate(fileName);
            if ((num < 0) || (_updates[num] == null))
            {
                throw new ZipException("Cannot find entry to delete");
            }
            const bool flag = true;
            _contentsEdited = true;
            _updates[num] = null;
            _updateCount -= 1L;
            return flag;
        }

        protected virtual void Dispose(bool disposing)
        {
            DisposeInternal(disposing);
        }

        private void DisposeInternal(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _entries = new ZipEntry[0];
                if (IsStreamOwner && (_baseStream != null))
                {
                    lock (_baseStream)
                    {
                        _baseStream.Close();
                    }
                }
                PostUpdateCleanup();
            }
        }

        ~ZipFile()
        {
            Dispose(false);
        }

        public int FindEntry(string name, bool ignoreCase)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            for (var i = 0; i < _entries.Length; i++)
            {
                if (string.Compare(name, _entries[i].Name, ignoreCase, CultureInfo.InvariantCulture) == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        private int FindExistingUpdate(ZipEntry entry)
        {
            var num = -1;
            var transformedFileName = GetTransformedFileName(entry.Name);
            if (_updateIndex.ContainsKey(transformedFileName))
            {
                num = (int)_updateIndex[transformedFileName];
            }
            return num;
        }

        private int FindExistingUpdate(string fileName)
        {
            var num = -1;
            var transformedFileName = GetTransformedFileName(fileName);
            if (_updateIndex.ContainsKey(transformedFileName))
            {
                num = (int)_updateIndex[transformedFileName];
            }
            return num;
        }

        private byte[] GetBuffer()
        {
            if (_copyBuffer == null)
            {
                _copyBuffer = new byte[_bufferSize];
            }
            return _copyBuffer;
        }

        private int GetDescriptorSize(ZipUpdate update)
        {
            var num = 0;
            if ((update.Entry.Flags & 8) != 0)
            {
                num = 12;
                if (update.Entry.LocalHeaderRequiresZip64)
                {
                    num = 20;
                }
            }
            return num;
        }

        public ZipEntry GetEntry(string name)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            var index = FindEntry(name, true);
            if (index < 0)
            {
                return null;
            }
            return (ZipEntry)_entries[index].Clone();
        }

        public IEnumerator GetEnumerator()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            return new ZipEntryEnumerator(_entries);
        }

        public Stream GetInputStream(ZipEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            var zipFileIndex = entry.ZipFileIndex;
            if (((zipFileIndex < 0L) || (zipFileIndex >= _entries.Length)) 
                || (_entries[(int)((IntPtr)zipFileIndex)].Name != entry.Name))
            {
                zipFileIndex = FindEntry(entry.Name, true);
                if (zipFileIndex < 0L)
                {
                    throw new ZipException("Entry cannot be found");
                }
            }
            return GetInputStream(zipFileIndex);
        }

        public Stream GetInputStream(long entryIndex)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            var start = LocateEntry(_entries[(int)((IntPtr)entryIndex)]);
            var compressionMethod = _entries[(int)((IntPtr)entryIndex)].CompressionMethod;
            Stream baseStream = new PartialInputStream(this, start, _entries[(int)((IntPtr)entryIndex)].CompressedSize);
            if (_entries[(int)((IntPtr)entryIndex)].IsCrypted)
            {
                baseStream = CreateAndInitDecryptionStream(baseStream, _entries[(int)((IntPtr)entryIndex)]);
                if (baseStream == null)
                {
                    throw new ZipException("Unable to decrypt this entry");
                }
            }
            switch (compressionMethod)
            {
                case CompressionMethod.Stored:
                    return baseStream;

                case CompressionMethod.Deflated:
                    return new InflaterInputStream(baseStream, new Inflater(true));
            }
            throw new ZipException("Unsupported compression method " + compressionMethod);
        }

        private Stream GetOutputStream(ZipEntry entry)
        {
            var baseStream = _baseStream;
            if (entry.IsCrypted)
            {
                baseStream = CreateAndInitEncryptionStream(baseStream, entry);
            }
            var compressionMethod = entry.CompressionMethod;
            if (compressionMethod != CompressionMethod.Stored)
            {
                if (compressionMethod != CompressionMethod.Deflated)
                {
                    throw new ZipException("Unknown compression method " + entry.CompressionMethod);
                }
            }
            else
            {
                return new UncompressedStream(baseStream);
            }
            return new DeflaterOutputStream(baseStream, new Deflater(9, true)) { IsStreamOwner = false };
        }

        private string GetTransformedDirectoryName(string name)
        {
            var nameTransform = NameTransform;
            if (nameTransform == null)
            {
                return name;
            }
            return nameTransform.TransformDirectory(name);
        }

        private string GetTransformedFileName(string name)
        {
            var nameTransform = NameTransform;
            if (nameTransform == null)
            {
                return name;
            }
            return nameTransform.TransformFile(name);
        }

        private long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
        {
            using (var stream = new ZipHelperStream(_baseStream))
            {
                return stream.LocateBlockWithSignature(signature, endLocation, minimumBlockSize, maximumVariableData);
            }
        }

        private long LocateEntry(ZipEntry entry)
        {
            return TestLocalHeader(entry, HeaderTest.Extract);
        }

        private void ModifyEntry(ZipFile workFile, ZipUpdate update)
        {
            workFile.WriteLocalEntryHeader(update);
            var position = workFile._baseStream.Position;
            if (update.Entry.IsFile && (update.Filename != null))
            {
                using (var stream = workFile.GetOutputStream(update.OutEntry))
                {
                    using (var stream2 = GetInputStream(update.Entry))
                    {
                        CopyBytes(update, stream, stream2, stream2.Length, true);
                    }
                }
            }
            var num2 = workFile._baseStream.Position;
            update.Entry.CompressedSize = num2 - position;
        }

        private void OnKeysRequired(string fileName)
        {
            if (KeysRequired != null)
            {
                var e = new KeysRequiredEventArgs(fileName, _key);
                KeysRequired(this, e);
                _key = e.Key;
            }
        }

        private void PostUpdateCleanup()
        {
            _updateDataSource = null;
            _updates = null;
            _updateIndex = null;
            if (_archiveStorage != null)
            {
                _archiveStorage.Dispose();
                _archiveStorage = null;
            }
        }

        private void ReadEntries()
        {
            if (!_baseStream.CanSeek)
            {
                throw new ZipException("ZipFile stream must be seekable");
            }
            var endLocation = LocateBlockWithSignature(0x6054b50, _baseStream.Length, 0x16, 0xffff);
            if (endLocation < 0L)
            {
                throw new ZipException("Cannot find central directory");
            }
            var num2 = ReadLeUshort();
            var num3 = ReadLeUshort();
            ulong num4 = ReadLeUshort();
            ulong num5 = ReadLeUshort();
            ulong num6 = ReadLeUint();
            long num7 = ReadLeUint();
            uint num8 = ReadLeUshort();
            if (num8 > 0)
            {
                var buffer = new byte[num8];
                StreamUtils.ReadFully(_baseStream, buffer);
                _comment = ZipConstants.ConvertToString(buffer);
            }
            else
            {
                _comment = string.Empty;
            }
            var flag = false;
            if ((((num2 == 0xffff) || (num3 == 0xffff)) || ((num4 == 0xffffL) 
                || (num5 == 0xffffL))) || ((num6 == 0xffffffffL) || (num7 == 0xffffffffL)))
            {
                flag = true;
                if (LocateBlockWithSignature(0x7064b50, endLocation, 0, DefaultBufferSize) < 0L)
                {
                    throw new ZipException("Cannot find Zip64 locator");
                }
                ReadLeUint();
                var num10 = ReadLeUlong();
                ReadLeUint();
                _baseStream.Position = (long)num10;
                long num11 = ReadLeUint();
                if (num11 != 0x6064b50L)
                {
                    throw new ZipException($"Invalid Zip64 Central directory signature at {num10:X}");
                }
                ReadLeUlong();
                ReadLeUshort();
                ReadLeUshort();
                ReadLeUint();
                ReadLeUint();
                num4 = ReadLeUlong();
                num5 = ReadLeUlong();
                num6 = ReadLeUlong();
                num7 = (long)ReadLeUlong();
            }
            _entries = new ZipEntry[num4];
            if (!flag && (num7 < (long)(((ulong)endLocation) - (4L + num6))))
            {
                _offsetOfFirstEntry = endLocation - ((4L + ((long)num6)) + num7);
                if (_offsetOfFirstEntry <= 0L)
                {
                    throw new ZipException("Invalid embedded zip archive");
                }
            }
            _baseStream.Seek(_offsetOfFirstEntry + num7, SeekOrigin.Begin);
            for (ulong i = 0L; i < num4; i += (ulong)1L)
            {
                if (ReadLeUint() != 0x2014b50)
                {
                    throw new ZipException("Wrong Central Directory signature");
                }
                int madeByInfo = ReadLeUshort();
                int versionRequiredToExtract = ReadLeUshort();
                int flags = ReadLeUshort();
                int num16 = ReadLeUshort();
                var num17 = ReadLeUint();
                var num18 = ReadLeUint();
                long num19 = ReadLeUint();
                long num20 = ReadLeUint();
                int num21 = ReadLeUshort();
                int num22 = ReadLeUshort();
                int num23 = ReadLeUshort();
                ReadLeUshort();
                ReadLeUshort();
                var num24 = ReadLeUint();
                long num25 = ReadLeUint();
                var buffer2 = new byte[Math.Max(num21, num23)];
                StreamUtils.ReadFully(_baseStream, buffer2, 0, num21);
                var entry = new ZipEntry(ZipConstants.ConvertToStringExt(flags, buffer2, num21), versionRequiredToExtract, madeByInfo, (CompressionMethod)num16)
                {
                    Crc = num18 & 0xffffffffL,
                    Size = num20 & 0xffffffffL,
                    CompressedSize = num19 & 0xffffffffL,
                    Flags = flags,
                    DosTime = num17,
                    ZipFileIndex = (long)i,
                    Offset = num25,
                    ExternalFileAttributes = (int)num24
                };
                if ((flags & 8) == 0)
                {
                    entry.CryptoCheckValue = (byte)(num18 >> 0x18);
                }
                else
                {
                    entry.CryptoCheckValue = (byte)((num17 >> 8) & 0xff);
                }
                if (num22 > 0)
                {
                    var buffer3 = new byte[num22];
                    StreamUtils.ReadFully(_baseStream, buffer3);
                    entry.ExtraData = buffer3;
                }
                entry.ProcessExtraData(false);
                if (num23 > 0)
                {
                    StreamUtils.ReadFully(_baseStream, buffer2, 0, num23);
                    entry.Comment = ZipConstants.ConvertToStringExt(flags, buffer2, num23);
                }
                _entries[(int)((IntPtr)i)] = entry;
            }
        }

        private uint ReadLeUint()
        {
            return (uint)(ReadLeUshort() | (ReadLeUshort() << 0x10));
        }

        private ulong ReadLeUlong()
        {
            return (ReadLeUint() | (ReadLeUint() << 0x20));
        }

        private ushort ReadLeUshort()
        {
            var num = _baseStream.ReadByte();
            if (num < 0)
            {
                throw new EndOfStreamException("End of stream");
            }
            var num2 = _baseStream.ReadByte();
            if (num2 < 0)
            {
                throw new EndOfStreamException("End of stream");
            }
            return (ushort)(((ushort)num) | ((ushort)(num2 << 8)));
        }

        private void Reopen()
        {
            if (Name == null)
            {
                throw new InvalidOperationException("Name is not known cannot Reopen");
            }
            Reopen(File.Open(Name, FileMode.Open, FileAccess.Read, FileShare.Read));
        }

        private void Reopen(Stream source)
        {
            if (source == null)
            {
                throw new ZipException("Failed to reopen archive - no source");
            }
            _isNewArchive = false;
            _baseStream = source;
            ReadEntries();
        }

        private void RunUpdates()
        {
            ZipFile file;
            var sizeEntries = 0L;
            long num2;
            var flag = false;
            var destinationPosition = 0L;
            if (IsNewArchive)
            {
                file = this;
                file._baseStream.Position = 0L;
                flag = true;
            }
            else if (_archiveStorage.UpdateMode == FileUpdateMode.Direct)
            {
                file = this;
                file._baseStream.Position = 0L;
                flag = true;
                _updates.Sort(new UpdateComparer());
            }
            else
            {
                file = Create(_archiveStorage.GetTemporaryOutput());
                file.UseZip64 = UseZip64;
                if (_key != null)
                {
                    file._key = (byte[])_key.Clone();
                }
            }
            try
            {
                foreach (ZipUpdate update in _updates)
                {
                    if (update != null)
                    {
                        switch (update.Command)
                        {
                            case UpdateCommand.Copy:
                                if (!flag)
                                {
                                    goto Label_00EC;
                                }
                                CopyEntryDirect(file, update, ref destinationPosition);
                                break;

                            case UpdateCommand.Modify:
                                ModifyEntry(file, update);
                                break;

                            case UpdateCommand.Add:
                                goto Label_0104;
                        }
                    }
                    continue;
                Label_00EC:
                    CopyEntry(file, update);
                    continue;
                Label_0104:
                    if (!IsNewArchive && flag)
                    {
                        file._baseStream.Position = destinationPosition;
                    }
                    AddEntry(file, update);
                    if (flag)
                    {
                        destinationPosition = file._baseStream.Position;
                    }
                }
                if (!IsNewArchive && flag)
                {
                    file._baseStream.Position = destinationPosition;
                }
                var position = file._baseStream.Position;
                foreach (ZipUpdate update2 in _updates)
                {
                    if (update2 != null)
                    {
                        sizeEntries += file.WriteCentralDirectoryHeader(update2.OutEntry);
                    }
                }
                var comment = (_newComment != null) ? _newComment.RawComment : ZipConstants.ConvertToArray(_comment);
                using (var stream = new ZipHelperStream(file._baseStream))
                {
                    stream.WriteEndOfCentralDirectory(_updateCount, sizeEntries, position, comment);
                }
                num2 = file._baseStream.Position;
                foreach (ZipUpdate update3 in _updates)
                {
                    if (update3 != null)
                    {
                        if ((update3.CrcPatchOffset > 0L) && (update3.OutEntry.CompressedSize > 0L))
                        {
                            file._baseStream.Position = update3.CrcPatchOffset;
                            file.WriteLeInt((int)update3.OutEntry.Crc);
                        }
                        if (update3.SizePatchOffset > 0L)
                        {
                            file._baseStream.Position = update3.SizePatchOffset;
                            if (update3.OutEntry.LocalHeaderRequiresZip64)
                            {
                                file.WriteLeLong(update3.OutEntry.Size);
                                file.WriteLeLong(update3.OutEntry.CompressedSize);
                            }
                            else
                            {
                                file.WriteLeInt((int)update3.OutEntry.CompressedSize);
                                file.WriteLeInt((int)update3.OutEntry.Size);
                            }
                        }
                    }
                }
            }
            catch
            {
                file.Close();
                if (!flag && (file.Name != null))
                {
                    File.Delete(file.Name);
                }
                throw;
            }
            if (flag)
            {
                file._baseStream.SetLength(num2);
                file._baseStream.Flush();
                _isNewArchive = false;
                ReadEntries();
            }
            else
            {
                _baseStream.Close();
                Reopen(_archiveStorage.ConvertTemporaryToFinal());
            }
        }

        public void SetComment(string comment)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            CheckUpdating();
            _newComment = new ZipString(comment);
            if (_newComment.RawLength > 0xffff)
            {
                _newComment = null;
                throw new ZipException("Comment length exceeds maximum - 65535");
            }
            _commentEdited = true;
        }

        void IDisposable.Dispose()
        {
            Close();
        }

        public bool TestArchive(bool testData, 
            TestStrategy strategy = TestStrategy.FindFirstError, 
            ZipTestResultHandler resultHandler = null)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            var status = new TestStatus(this);
            resultHandler?.Invoke(status, null);
            var tests = testData ? (HeaderTest.Header | HeaderTest.Extract) : HeaderTest.Header;
            var flag = true;
            try
            {
                for (var i = 0; flag && (i < Count); i++)
                {
                    if (resultHandler != null)
                    {
                        status.SetEntry(this[i]);
                        status.SetOperation(TestOperation.EntryHeader);
                        resultHandler(status, null);
                    }
                    try
                    {
                        TestLocalHeader(this[i], tests);
                    }
                    catch (ZipException exception)
                    {
                        status.AddError();
                        resultHandler?.Invoke(status, $"Exception during test - '{exception.Message}'");
                        if (strategy == TestStrategy.FindFirstError)
                        {
                            flag = false;
                        }
                    }
                    if ((flag && testData) && this[i].IsFile)
                    {
                        if (resultHandler != null)
                        {
                            status.SetOperation(TestOperation.EntryData);
                            resultHandler(status, null);
                        }
                        var crc = new Crc32();
                        using (var stream = GetInputStream(this[i]))
                        {
                            int num3;
                            var buffer = new byte[DefaultBufferSize];
                            var num2 = 0L;
                            while ((num3 = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                crc.Update(buffer, 0, num3);
                                if (resultHandler != null)
                                {
                                    num2 += num3;
                                    status.SetBytesTested(num2);
                                    resultHandler(status, null);
                                }
                            }
                        }
                        if (this[i].Crc != crc.Value)
                        {
                            status.AddError();
                            resultHandler?.Invoke(status, "CRC mismatch");
                            if (strategy == TestStrategy.FindFirstError)
                            {
                                flag = false;
                            }
                        }
                        if ((this[i].Flags & 8) != 0)
                        {
                            var stream2 = new ZipHelperStream(_baseStream);
                            var data = new DescriptorData();
                            stream2.ReadDataDescriptor(this[i].LocalHeaderRequiresZip64, data);
                            if (this[i].Crc != data.Crc)
                            {
                                status.AddError();
                            }
                            if (this[i].CompressedSize != data.CompressedSize)
                            {
                                status.AddError();
                            }
                            if (this[i].Size != data.Size)
                            {
                                status.AddError();
                            }
                        }
                    }
                    if (resultHandler != null)
                    {
                        status.SetOperation(TestOperation.EntryComplete);
                        resultHandler(status, null);
                    }
                }
                if (resultHandler != null)
                {
                    status.SetOperation(TestOperation.MiscellaneousTests);
                    resultHandler(status, null);
                }
            }
            catch (Exception exception2)
            {
                status.AddError();
                resultHandler?.Invoke(status, $"Exception during test - '{exception2.Message}'");
            }
            if (resultHandler != null)
            {
                status.SetOperation(TestOperation.Complete);
                status.SetEntry(null);
                resultHandler(status, null);
            }
            return (status.ErrorCount == 0);
        }

        private long TestLocalHeader(ZipEntry entry, HeaderTest tests)
        {
            lock (_baseStream)
            {
                var flag = (tests & HeaderTest.Header) != 0;
                var flag2 = (tests & HeaderTest.Extract) != 0;
                _baseStream.Seek(_offsetOfFirstEntry + entry.Offset, SeekOrigin.Begin);
                if (ReadLeUint() != 0x4034b50)
                {
                    throw new ZipException($"Wrong local header signature @{_offsetOfFirstEntry + entry.Offset:X}");
                }
                var num = (short)ReadLeUshort();
                var flags = (short)ReadLeUshort();
                var num3 = (short)ReadLeUshort();
                var num4 = (short)ReadLeUshort();
                var num5 = (short)ReadLeUshort();
                var num6 = ReadLeUint();
                long num7 = ReadLeUint();
                long num8 = ReadLeUint();
                int num9 = ReadLeUshort();
                int num10 = ReadLeUshort();
                var buffer = new byte[num9];
                StreamUtils.ReadFully(_baseStream, buffer);
                var buffer2 = new byte[num10];
                StreamUtils.ReadFully(_baseStream, buffer2);
                var data = new ZipExtraData(buffer2);
                if (data.Find(1))
                {
                    num8 = data.ReadLong();
                    num7 = data.ReadLong();
                    if ((flags & 8) != 0)
                    {
                        if ((num8 != -1L) && (num8 != entry.Size))
                        {
                            throw new ZipException("Size invalid for descriptor");
                        }
                        if ((num7 != -1L) && (num7 != entry.CompressedSize))
                        {
                            throw new ZipException("Compressed size invalid for descriptor");
                        }
                    }
                }
                else if ((num >= 0x2d) && ((((uint)num8) == uint.MaxValue) || (((uint)num7) == uint.MaxValue)))
                {
                    throw new ZipException("Required Zip64 extended information missing");
                }
                if (flag2 && entry.IsFile)
                {
                    if (!entry.IsCompressionMethodSupported())
                    {
                        throw new ZipException("Compression method not supported");
                    }
                    if ((num > 0x33) || ((num > 20) && (num < 0x2d)))
                    {
                        throw new ZipException($"Version required to extract this entry not supported ({num})");
                    }
                    if ((flags & 0x3060) != 0)
                    {
                        throw new ZipException("The library does not support the zip version required to extract this entry");
                    }
                }
                if (flag)
                {
                    if (((((num <= 0x3f) && (num != 10)) && ((num != 11) && (num != 20))) && (((num != 0x15) && (num != 0x19)) && ((num != 0x1b) && (num != 0x2d)))) && ((((num != 0x2e) && (num != 50)) && ((num != 0x33) && (num != 0x34))) && (((num != 0x3d) && (num != 0x3e)) && (num != 0x3f))))
                    {
                        throw new ZipException($"Version required to extract this entry is invalid ({num})");
                    }
                    if ((flags & 0xc010) != 0)
                    {
                        throw new ZipException("Reserved bit flags cannot be set.");
                    }
                    if (((flags & 1) != 0) && (num < 20))
                    {
                        throw new ZipException(
                            $"Version required to extract this entry is too low for encryption ({num})");
                    }
                    if ((flags & 0x40) != 0)
                    {
                        if ((flags & 1) == 0)
                        {
                            throw new ZipException("Strong encryption flag set but encryption flag is not set");
                        }
                        if (num < 50)
                        {
                            throw new ZipException(
                                $"Version required to extract this entry is too low for encryption ({num})");
                        }
                    }
                    if (((flags & 0x20) != 0) && (num < 0x1b))
                    {
                        throw new ZipException($"Patched data requires higher version than ({num})");
                    }
                    if (flags != entry.Flags)
                    {
                        throw new ZipException("Central header/local header flags mismatch");
                    }
                    if (entry.CompressionMethod != ((CompressionMethod)num3))
                    {
                        throw new ZipException("Central header/local header compression method mismatch");
                    }
                    if (entry.Version != num)
                    {
                        throw new ZipException("Extract version mismatch");
                    }
                    if (((flags & 0x40) != 0) && (num < 0x3e))
                    {
                        throw new ZipException("Strong encryption flag set but version not high enough");
                    }
                    if (((flags & 0x2000) != 0) && ((num4 != 0) || (num5 != 0)))
                    {
                        throw new ZipException("Header masked set but date/time values non-zero");
                    }
                    if (((flags & 8) == 0) && (num6 != ((uint)entry.Crc)))
                    {
                        throw new ZipException("Central header/local header crc mismatch");
                    }
                    if (((num8 == 0L) && (num7 == 0L)) && (num6 != 0))
                    {
                        throw new ZipException("Invalid CRC for empty entry");
                    }
                    if (entry.Name.Length > num9)
                    {
                        throw new ZipException("File name length mismatch");
                    }
                    var name = ZipConstants.ConvertToStringExt(flags, buffer);
                    if (name != entry.Name)
                    {
                        throw new ZipException("Central header and local header file name mismatch");
                    }
                    if (entry.IsDirectory)
                    {
                        if (num8 > 0L)
                        {
                            throw new ZipException("Directory cannot have size");
                        }
                        if (entry.IsCrypted)
                        {
                            if (num7 > 14L)
                            {
                                throw new ZipException("Directory compressed size invalid");
                            }
                        }
                        else if (num7 > 2L)
                        {
                            throw new ZipException("Directory compressed size invalid");
                        }
                    }
                    if (!ZipNameTransform.IsValidName(name, true))
                    {
                        throw new ZipException("Name is invalid");
                    }
                }
                if ((((flags & 8) == 0) || (num8 > 0L)) || (num7 > 0L))
                {
                    if (num8 != entry.Size)
                    {
                        throw new ZipException(
                            $"Size mismatch between central header({entry.Size}) and local header({num8})");
                    }
                    if (((num7 != entry.CompressedSize) && (num7 != 0xffffffffL)) && (num7 != -1L))
                    {
                        throw new ZipException(
                            $"Compressed size mismatch between central header({entry.CompressedSize}) and local header({num7})");
                    }
                }
                var num11 = num9 + num10;
                return (((_offsetOfFirstEntry + entry.Offset) + 30L) + num11);
            }
        }

        private void UpdateCommentOnly()
        {
            var length = _baseStream.Length;
            ZipHelperStream stream;
            if (_archiveStorage.UpdateMode == FileUpdateMode.Safe)
            {
                stream = new ZipHelperStream(_archiveStorage.MakeTemporaryCopy(_baseStream))
                {
                    IsStreamOwner = true
                };
                _baseStream.Close();
                _baseStream = null;
            }
            else if (_archiveStorage.UpdateMode == FileUpdateMode.Direct)
            {
                _baseStream = _archiveStorage.OpenForDirectUpdate(_baseStream);
                stream = new ZipHelperStream(_baseStream);
            }
            else
            {
                _baseStream.Close();
                _baseStream = null;
                stream = new ZipHelperStream(Name);
            }
            using (stream)
            {
                if (stream.LocateBlockWithSignature(0x6054b50, length, 0x16, 0xffff) < 0L)
                {
                    throw new ZipException("Cannot find central directory");
                }
                stream.Position += 0x10L;
                var rawComment = _newComment.RawComment;
                stream.WriteLeShort(rawComment.Length);
                stream.Write(rawComment, 0, rawComment.Length);
                stream.SetLength(stream.Position);
            }
            if (_archiveStorage.UpdateMode == FileUpdateMode.Safe)
            {
                Reopen(_archiveStorage.ConvertTemporaryToFinal());
            }
            else
            {
                ReadEntries();
            }
        }

        private int WriteCentralDirectoryHeader(ZipEntry entry)
        {
            if (entry.CompressedSize < 0L)
            {
                throw new ZipException("Attempt to write central directory entry with unknown csize");
            }
            if (entry.Size < 0L)
            {
                throw new ZipException("Attempt to write central directory entry with unknown size");
            }
            if (entry.Crc < 0L)
            {
                throw new ZipException("Attempt to write central directory entry with unknown crc");
            }
            WriteLeInt(0x2014b50);
            WriteLeShort(0x33);
            WriteLeShort(entry.Version);
            WriteLeShort(entry.Flags);
            WriteLeShort((byte)entry.CompressionMethod);
            WriteLeInt((int)entry.DosTime);
            WriteLeInt((int)entry.Crc);
            if (entry.IsZip64Forced() || (entry.CompressedSize >= 0xffffffffL))
            {
                WriteLeInt(-1);
            }
            else
            {
                WriteLeInt((int)(((ulong)entry.CompressedSize) & 0xffffffffL));
            }
            if (entry.IsZip64Forced() || (entry.Size >= 0xffffffffL))
            {
                WriteLeInt(-1);
            }
            else
            {
                WriteLeInt((int)entry.Size);
            }
            var buffer = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
            if (buffer.Length > 0xffff)
            {
                throw new ZipException("Entry name is too long.");
            }
            WriteLeShort(buffer.Length);
            var data = new ZipExtraData(entry.ExtraData);
            if (entry.CentralHeaderRequiresZip64)
            {
                data.StartNewEntry();
                if ((entry.Size >= 0xffffffffL) || (_useZip64 == UseZip64.On))
                {
                    data.AddLeLong(entry.Size);
                }
                if ((entry.CompressedSize >= 0xffffffffL) || (_useZip64 == UseZip64.On))
                {
                    data.AddLeLong(entry.CompressedSize);
                }
                if (entry.Offset >= 0xffffffffL)
                {
                    data.AddLeLong(entry.Offset);
                }
                data.AddNewEntry(1);
            }
            else
            {
                data.Delete(1);
            }
            var entryData = data.GetEntryData();
            WriteLeShort(entryData.Length);
            WriteLeShort(entry.Comment?.Length ?? 0);
            WriteLeShort(0);
            WriteLeShort(0);
            if (entry.ExternalFileAttributes != -1)
            {
                WriteLeInt(entry.ExternalFileAttributes);
            }
            else if (entry.IsDirectory)
            {
                WriteLeUint(0x10);
            }
            else
            {
                WriteLeUint(0);
            }
            if (entry.Offset >= 0xffffffffL)
            {
                WriteLeUint(uint.MaxValue);
            }
            else
            {
                WriteLeUint((uint)((int)entry.Offset));
            }
            if (buffer.Length > 0)
            {
                _baseStream.Write(buffer, 0, buffer.Length);
            }
            if (entryData.Length > 0)
            {
                _baseStream.Write(entryData, 0, entryData.Length);
            }
            var buffer3 = (entry.Comment != null) ? Encoding.ASCII.GetBytes(entry.Comment) : new byte[0];
            if (buffer3.Length > 0)
            {
                _baseStream.Write(buffer3, 0, buffer3.Length);
            }
            return (((0x2e + buffer.Length) + entryData.Length) + buffer3.Length);
        }

        private static void WriteEncryptionHeader(Stream stream, long crcValue)
        {
            var buffer = new byte[12];
            new Random().NextBytes(buffer);
            buffer[11] = (byte)(crcValue >> 0x18);
            stream.Write(buffer, 0, buffer.Length);
        }

        private void WriteLeInt(int value)
        {
            WriteLeShort(value & 0xffff);
            WriteLeShort(value >> 0x10);
        }

        private void WriteLeLong(long value)
        {
            WriteLeInt((int)(((ulong)value) & 0xffffffffL));
            WriteLeInt((int)(value >> 0x20));
        }

        private void WriteLeShort(int value)
        {
            _baseStream.WriteByte((byte)(value & 0xff));
            _baseStream.WriteByte((byte)((value >> 8) & 0xff));
        }

        private void WriteLeUint(uint value)
        {
            WriteLeUshort((ushort)(value & 0xffff));
            WriteLeUshort((ushort)(value >> 0x10));
        }

        private void WriteLeUlong(ulong value)
        {
            WriteLeUint((uint)(value & 0xffffffffL));
            WriteLeUint((uint)(value >> 0x20));
        }

        private void WriteLeUshort(ushort value)
        {
            _baseStream.WriteByte((byte)(value & 0xff));
            _baseStream.WriteByte((byte)(value >> 8));
        }

        private void WriteLocalEntryHeader(ZipUpdate update)
        {
            var outEntry = update.OutEntry;
            outEntry.Offset = _baseStream.Position;
            if (update.Command != UpdateCommand.Copy)
            {
                if (outEntry.CompressionMethod == CompressionMethod.Deflated)
                {
                    if (outEntry.Size == 0L)
                    {
                        outEntry.CompressedSize = outEntry.Size;
                        outEntry.Crc = 0L;
                        outEntry.CompressionMethod = CompressionMethod.Stored;
                    }
                }
                else if (outEntry.CompressionMethod == CompressionMethod.Stored)
                {
                    outEntry.Flags &= -9;
                }
                if (HaveKeys)
                {
                    outEntry.IsCrypted = true;
                    if (outEntry.Crc < 0L)
                    {
                        outEntry.Flags |= 8;
                    }
                }
                else
                {
                    outEntry.IsCrypted = false;
                }
                switch (_useZip64)
                {
                    case UseZip64.On:
                        outEntry.ForceZip64();
                        break;

                    case UseZip64.Dynamic:
                        if (outEntry.Size < 0L)
                        {
                            outEntry.ForceZip64();
                        }
                        break;
                }
            }
            WriteLeInt(0x4034b50);
            WriteLeShort(outEntry.Version);
            WriteLeShort(outEntry.Flags);
            WriteLeShort((byte)outEntry.CompressionMethod);
            WriteLeInt((int)outEntry.DosTime);
            if (!outEntry.HasCrc)
            {
                update.CrcPatchOffset = _baseStream.Position;
                WriteLeInt(0);
            }
            else
            {
                WriteLeInt((int)outEntry.Crc);
            }
            if (outEntry.LocalHeaderRequiresZip64)
            {
                WriteLeInt(-1);
                WriteLeInt(-1);
            }
            else
            {
                if ((outEntry.CompressedSize < 0L) || (outEntry.Size < 0L))
                {
                    update.SizePatchOffset = _baseStream.Position;
                }
                WriteLeInt((int)outEntry.CompressedSize);
                WriteLeInt((int)outEntry.Size);
            }
            var buffer = ZipConstants.ConvertToArray(outEntry.Flags, outEntry.Name);
            if (buffer.Length > 0xffff)
            {
                throw new ZipException("Entry name too long.");
            }
            var data = new ZipExtraData(outEntry.ExtraData);
            if (outEntry.LocalHeaderRequiresZip64)
            {
                data.StartNewEntry();
                data.AddLeLong(outEntry.Size);
                data.AddLeLong(outEntry.CompressedSize);
                data.AddNewEntry(1);
            }
            else
            {
                data.Delete(1);
            }
            outEntry.ExtraData = data.GetEntryData();
            WriteLeShort(buffer.Length);
            WriteLeShort(outEntry.ExtraData.Length);
            if (buffer.Length > 0)
            {
                _baseStream.Write(buffer, 0, buffer.Length);
            }
            if (outEntry.LocalHeaderRequiresZip64)
            {
                if (!data.Find(1))
                {
                    throw new ZipException("Internal error cannot find extra data");
                }
                update.SizePatchOffset = _baseStream.Position + data.CurrentReadIndex;
            }
            if (outEntry.ExtraData.Length > 0)
            {
                _baseStream.Write(outEntry.ExtraData, 0, outEntry.ExtraData.Length);
            }
        }

        public int BufferSize
        {
            get
            {
                return _bufferSize;
            }
            set
            {
                if (value < 0x400)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "cannot be below 1024");
                }
                if (_bufferSize != value)
                {
                    _bufferSize = value;
                    _copyBuffer = null;
                }
            }
        }

        public long Count => _entries.Length;

        public ZipEntry this[int index] => (ZipEntry)_entries[index].Clone();

        public IEntryFactory EntryFactory
        {
            get
            {
                return _updateEntryFactory;
            }
            set {
                _updateEntryFactory = value ?? new ZipEntryFactory();
            }
        }

        private bool HaveKeys => (_key != null);

        public bool IsEmbeddedArchive => (_offsetOfFirstEntry > 0L);

        public bool IsNewArchive => _isNewArchive;

        public bool IsStreamOwner
        {
            get
            {
                return _isStreamOwner;
            }
            set
            {
                _isStreamOwner = value;
            }
        }

        public bool IsUpdating => (_updates != null);

        private byte[] Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        public string Name => _name;

        public INameTransform NameTransform
        {
            get
            {
                return _updateEntryFactory.NameTransform;
            }
            set
            {
                _updateEntryFactory.NameTransform = value;
            }
        }

        public string Password
        {
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    _key = null;
                }
                else
                {
                    _rawPassword = value;
                    _key = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(value));
                }
            }
        }

        [Obsolete("Use the Count property instead")]
        public int Size => _entries.Length;

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

        public string ZipFileComment => _comment;

        [Flags]
        private enum HeaderTest
        {
            Extract = 1,
            Header = 2
        }

        public delegate void KeysRequiredEventHandler(object sender, KeysRequiredEventArgs e);

        private class PartialInputStream : Stream
        {
            private Stream _baseStream;
            private long _end;
            private long _length;
            private long _readPos;
            private long _start;

            public PartialInputStream(ZipFile zipFile, long start, long length)
            {
                _start = start;
                _length = length;
                _baseStream = zipFile._baseStream;
                _readPos = start;
                _end = start + length;
            }

            public override void Close()
            {
            }

            public override void Flush()
            {
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                lock (_baseStream)
                {
                    if (count > (_end - _readPos))
                    {
                        count = (int)(_end - _readPos);
                        if (count == 0)
                        {
                            return 0;
                        }
                    }
                    _baseStream.Seek(_readPos, SeekOrigin.Begin);
                    var num = _baseStream.Read(buffer, offset, count);
                    if (num > 0)
                    {
                        _readPos += num;
                    }
                    return num;
                }
            }

            public override int ReadByte()
            {
                if (_readPos >= _end)
                {
                    return -1;
                }
                lock (_baseStream)
                {
                    long num2;
                    _readPos = (num2 = _readPos) + 1L;
                    _baseStream.Seek(num2, SeekOrigin.Begin);
                    return _baseStream.ReadByte();
                }
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                var num = _readPos;
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        num = _start + offset;
                        break;

                    case SeekOrigin.Current:
                        num = _readPos + offset;
                        break;

                    case SeekOrigin.End:
                        num = _end + offset;
                        break;
                }
                if (num < _start)
                {
                    throw new ArgumentException("Negative position is invalid");
                }
                if (num >= _end)
                {
                    throw new IOException("Cannot seek past end");
                }
                _readPos = num;
                return _readPos;
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            public override bool CanRead => true;

            public override bool CanSeek => true;

            public override bool CanTimeout => _baseStream.CanTimeout;

            public override bool CanWrite => false;

            public override long Length => _length;

            public override long Position
            {
                get
                {
                    return (_readPos - _start);
                }
                set
                {
                    var num = _start + value;
                    if (num < _start)
                    {
                        throw new ArgumentException("Negative position is invalid");
                    }
                    if (num >= _end)
                    {
                        throw new InvalidOperationException("Cannot seek past end");
                    }
                    _readPos = num;
                }
            }
        }

        private class UncompressedStream : Stream
        {
            private Stream _baseStream;

            public UncompressedStream(Stream baseStream)
            {
                _baseStream = baseStream;
            }

            public override void Close()
            {
            }

            public override void Flush()
            {
                _baseStream.Flush();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return 0;
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return 0L;
            }

            public override void SetLength(long value)
            {
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                _baseStream.Write(buffer, offset, count);
            }

            public override bool CanRead => false;

            public override bool CanSeek => false;

            public override bool CanWrite => _baseStream.CanWrite;

            public override long Length => 0L;

            public override long Position
            {
                get
                {
                    return _baseStream.Position;
                }
                set
                {
                }
            }
        }

        private enum UpdateCommand
        {
            Copy,
            Modify,
            Add
        }

        private class UpdateComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                var update = x as ZipUpdate;
                var update2 = y as ZipUpdate;
                if (update == null)
                {
                    if (update2 == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (update2 != null)
                {
                    var num2 = ((update.Command == UpdateCommand.Copy) || (update.Command == UpdateCommand.Modify)) ? 0 : 1;
                    var num3 = ((update2.Command == UpdateCommand.Copy) || (update2.Command == UpdateCommand.Modify)) ? 0 : 1;
                    var num = num2 - num3;
                    if (num != 0)
                    {
                        return num;
                    }
                    var num4 = update.Entry.Offset - update2.Entry.Offset;
                    if (num4 < 0L)
                    {
                        return -1;
                    }
                    if (num4 == 0L)
                    {
                        return 0;
                    }
                }
                return 1;
            }
        }

        private class ZipEntryEnumerator : IEnumerator
        {
            private ZipEntry[] _array;
            private int _index = -1;

            public ZipEntryEnumerator(ZipEntry[] entries)
            {
                _array = entries;
            }

            public bool MoveNext()
            {
                return (++_index < _array.Length);
            }

            public void Reset()
            {
                _index = -1;
            }

            public object Current => _array[_index];
        }

        private class ZipString
        {
            private string _comment;
            private bool _isSourceString;
            private byte[] _rawComment;

            public ZipString(string comment)
            {
                _comment = comment;
                _isSourceString = true;
            }

            public ZipString(byte[] rawString)
            {
                _rawComment = rawString;
            }

            private void MakeBytesAvailable()
            {
                if (_rawComment == null)
                {
                    _rawComment = ZipConstants.ConvertToArray(_comment);
                }
            }

            private void MakeTextAvailable()
            {
                if (_comment == null)
                {
                    _comment = ZipConstants.ConvertToString(_rawComment);
                }
            }

            public static implicit operator string(ZipString zipString)
            {
                zipString.MakeTextAvailable();
                return zipString._comment;
            }

            public void Reset()
            {
                if (_isSourceString)
                {
                    _rawComment = null;
                }
                else
                {
                    _comment = null;
                }
            }

            public bool IsSourceString => _isSourceString;

            public byte[] RawComment
            {
                get
                {
                    MakeBytesAvailable();
                    return (byte[])_rawComment.Clone();
                }
            }

            public int RawLength
            {
                get
                {
                    MakeBytesAvailable();
                    return _rawComment.Length;
                }
            }
        }

        private class ZipUpdate
        {
            private long _offsetBasedSize;
            private UpdateCommand _command;
            private long _crcPatchOffset;
            private IStaticDataSource _dataSource;
            private ZipEntry _entry;
            private string _filename;
            private ZipEntry _outEntry;
            private long _sizePatchOffset;

            public ZipUpdate(ZipEntry entry)
                : this(UpdateCommand.Copy, entry)
            {
            }

            public ZipUpdate(IStaticDataSource dataSource, ZipEntry entry)
            {
                _sizePatchOffset = -1L;
                _crcPatchOffset = -1L;
                _offsetBasedSize = -1L;
                _command = UpdateCommand.Add;
                _entry = entry;
                _dataSource = dataSource;
            }

            public ZipUpdate(ZipEntry original, ZipEntry updated)
            {
                _sizePatchOffset = -1L;
                _crcPatchOffset = -1L;
                _offsetBasedSize = -1L;
                throw new ZipException("Modify not currently supported");
            }

            public ZipUpdate(UpdateCommand command, ZipEntry entry)
            {
                _sizePatchOffset = -1L;
                _crcPatchOffset = -1L;
                _offsetBasedSize = -1L;
                _command = command;
                _entry = (ZipEntry)entry.Clone();
            }

            public ZipUpdate(string fileName, ZipEntry entry)
            {
                _sizePatchOffset = -1L;
                _crcPatchOffset = -1L;
                _offsetBasedSize = -1L;
                _command = UpdateCommand.Add;
                _entry = entry;
                _filename = fileName;
            }

            [Obsolete]
            public ZipUpdate(string fileName, string entryName)
                : this(fileName, entryName, CompressionMethod.Deflated)
            {
            }

            [Obsolete]
            public ZipUpdate(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
            {
                _sizePatchOffset = -1L;
                _crcPatchOffset = -1L;
                _offsetBasedSize = -1L;
                _command = UpdateCommand.Add;
                _entry = new ZipEntry(entryName);
                _entry.CompressionMethod = compressionMethod;
                _dataSource = dataSource;
            }

            [Obsolete]
            public ZipUpdate(string fileName, string entryName, CompressionMethod compressionMethod)
            {
                _sizePatchOffset = -1L;
                _crcPatchOffset = -1L;
                _offsetBasedSize = -1L;
                _command = UpdateCommand.Add;
                _entry = new ZipEntry(entryName);
                _entry.CompressionMethod = compressionMethod;
                _filename = fileName;
            }

            public Stream GetSource()
            {
                Stream source = null;
                if (_dataSource != null)
                {
                    source = _dataSource.GetSource();
                }
                return source;
            }

            public UpdateCommand Command => _command;

            public long CrcPatchOffset
            {
                get
                {
                    return _crcPatchOffset;
                }
                set
                {
                    _crcPatchOffset = value;
                }
            }

            public ZipEntry Entry => _entry;

            public string Filename => _filename;

            public long OffsetBasedSize
            {
                get
                {
                    return _offsetBasedSize;
                }
                set
                {
                    _offsetBasedSize = value;
                }
            }

            public ZipEntry OutEntry
            {
                get
                {
                    if (_outEntry == null)
                    {
                        _outEntry = (ZipEntry)_entry.Clone();
                    }
                    return _outEntry;
                }
            }

            public long SizePatchOffset
            {
                get
                {
                    return _sizePatchOffset;
                }
                set
                {
                    _sizePatchOffset = value;
                }
            }
        }
    }
}

