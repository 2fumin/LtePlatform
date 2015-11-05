using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Lte.Domain.Lz4Net.Core;
using Lte.Domain.Lz4Net.Encryption;
using Lte.Domain.ZipLib.CheckSums;
using Lte.Domain.ZipLib.Compression;
using Lte.Domain.ZipLib.Streams;

namespace Lte.Domain.ZipLib.Zip
{
    public class ZipFile : IEnumerable, IDisposable
    {
        private IArchiveStorage archiveStorage_;
        private Stream baseStream_;
        private int bufferSize_;
        private string comment_;
        private bool commentEdited_;
        private bool contentsEdited_;
        private byte[] copyBuffer_;
        private const int DefaultBufferSize = 0x1000;
        private ZipEntry[] entries_;
        private bool isDisposed_;
        private bool isNewArchive_;
        private bool isStreamOwner;
        private byte[] key;
        public KeysRequiredEventHandler KeysRequired;
        private string name_;
        private ZipString newComment_;
        private long offsetOfFirstEntry;
        private string rawPassword_;
        private long updateCount_;
        private IDynamicDataSource updateDataSource_;
        private IEntryFactory updateEntryFactory_;
        private Hashtable updateIndex_;
        private ArrayList updates_;
        private UseZip64 useZip64_;

        internal ZipFile()
        {
            useZip64_ = UseZip64.Dynamic;
            bufferSize_ = DefaultBufferSize;
            updateEntryFactory_ = new ZipEntryFactory();
            entries_ = new ZipEntry[0];
            isNewArchive_ = true;
        }

        public ZipFile(FileStream file)
        {
            useZip64_ = UseZip64.Dynamic;
            bufferSize_ = DefaultBufferSize;
            updateEntryFactory_ = new ZipEntryFactory();
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }
            if (!file.CanSeek)
            {
                throw new ArgumentException("Stream is not seekable", "file");
            }
            baseStream_ = file;
            name_ = file.Name;
            isStreamOwner = true;
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
            useZip64_ = UseZip64.Dynamic;
            bufferSize_ = DefaultBufferSize;
            updateEntryFactory_ = new ZipEntryFactory();
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (!stream.CanSeek)
            {
                throw new ArgumentException("Stream is not seekable", "stream");
            }
            baseStream_ = stream;
            isStreamOwner = true;
            if (baseStream_.Length > 0L)
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
            entries_ = new ZipEntry[0];
            isNewArchive_ = true;
        }

        public ZipFile(string name)
        {
            useZip64_ = UseZip64.Dynamic;
            bufferSize_ = DefaultBufferSize;
            updateEntryFactory_ = new ZipEntryFactory();
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            name_ = name;
            baseStream_ = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            isStreamOwner = true;
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
                throw new ArgumentNullException("entry");
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
                throw new ArgumentNullException("fileName");
            }
            CheckUpdating();
            AddUpdate(new ZipUpdate(fileName, EntryFactory.MakeFileEntry(fileName)));
        }

        public void Add(IStaticDataSource dataSource, string entryName)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException("dataSource");
            }
            if (entryName == null)
            {
                throw new ArgumentNullException("entryName");
            }
            CheckUpdating();
            AddUpdate(new ZipUpdate(dataSource, EntryFactory.MakeFileEntry(entryName, false)));
        }

        public void Add(string fileName, CompressionMethod compressionMethod)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }
            if (!ZipEntry.IsCompressionMethodSupported(compressionMethod))
            {
                throw new ArgumentOutOfRangeException("compressionMethod");
            }
            CheckUpdating();
            contentsEdited_ = true;
            ZipEntry entry = EntryFactory.MakeFileEntry(fileName);
            entry.CompressionMethod = compressionMethod;
            AddUpdate(new ZipUpdate(fileName, entry));
        }

        public void Add(string fileName, string entryName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }
            if (entryName == null)
            {
                throw new ArgumentNullException("entryName");
            }
            CheckUpdating();
            AddUpdate(new ZipUpdate(fileName, EntryFactory.MakeFileEntry(entryName)));
        }

        public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException("dataSource");
            }
            if (entryName == null)
            {
                throw new ArgumentNullException("entryName");
            }
            CheckUpdating();
            ZipEntry entry = EntryFactory.MakeFileEntry(entryName, false);
            entry.CompressionMethod = compressionMethod;
            AddUpdate(new ZipUpdate(dataSource, entry));
        }

        public void Add(string fileName, CompressionMethod compressionMethod, bool useUnicodeText)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }
            if (isDisposed_)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            if (!ZipEntry.IsCompressionMethodSupported(compressionMethod))
            {
                throw new ArgumentOutOfRangeException("compressionMethod");
            }
            CheckUpdating();
            contentsEdited_ = true;
            ZipEntry entry = EntryFactory.MakeFileEntry(fileName);
            entry.IsUnicodeText = useUnicodeText;
            entry.CompressionMethod = compressionMethod;
            AddUpdate(new ZipUpdate(fileName, entry));
        }

        public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod, bool useUnicodeText)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException("dataSource");
            }
            if (entryName == null)
            {
                throw new ArgumentNullException("entryName");
            }
            CheckUpdating();
            ZipEntry entry = EntryFactory.MakeFileEntry(entryName, false);
            entry.IsUnicodeText = useUnicodeText;
            entry.CompressionMethod = compressionMethod;
            AddUpdate(new ZipUpdate(dataSource, entry));
        }

        public void AddDirectory(string directoryName)
        {
            if (directoryName == null)
            {
                throw new ArgumentNullException("directoryName");
            }
            CheckUpdating();
            ZipEntry entry = EntryFactory.MakeDirectoryEntry(directoryName);
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
                    source = updateDataSource_.GetSource(update.Entry, update.Filename);
                }
            }
            if (source != null)
            {
                using (source)
                {
                    long length = source.Length;
                    if (update.OutEntry.Size < 0L)
                    {
                        update.OutEntry.Size = length;
                    }
                    else if (update.OutEntry.Size != length)
                    {
                        throw new ZipException("Entry size/stream size mismatch");
                    }
                    workFile.WriteLocalEntryHeader(update);
                    long position = workFile.baseStream_.Position;
                    using (Stream stream2 = workFile.GetOutputStream(update.OutEntry))
                    {
                        CopyBytes(update, stream2, source, length, true);
                    }
                    long num3 = workFile.baseStream_.Position;
                    update.OutEntry.CompressedSize = num3 - position;
                    if ((update.OutEntry.Flags & 8) == 8)
                    {
                        new ZipHelperStream(workFile.baseStream_).WriteDataDescriptor(update.OutEntry);
                    }
                    return;
                }
            }
            workFile.WriteLocalEntryHeader(update);
            update.OutEntry.CompressedSize = 0L;
        }

        private void AddUpdate(ZipUpdate update)
        {
            contentsEdited_ = true;
            int num = FindExistingUpdate(update.Entry.Name);
            if (num >= 0)
            {
                if (updates_[num] == null)
                {
                    updateCount_ += 1L;
                }
                updates_[num] = update;
            }
            else
            {
                num = updates_.Add(update);
                updateCount_ += 1L;
                updateIndex_.Add(update.Entry.Name, num);
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
                throw new ArgumentNullException("archiveStorage");
            }
            if (dataSource == null)
            {
                throw new ArgumentNullException("dataSource");
            }
            if (isDisposed_)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            if (IsEmbeddedArchive)
            {
                throw new ZipException("Cannot update embedded/SFX archives");
            }
            archiveStorage_ = archiveStorage;
            updateDataSource_ = dataSource;
            updateIndex_ = new Hashtable();
            updates_ = new ArrayList(entries_.Length);
            foreach (ZipEntry entry in entries_)
            {
                int num = updates_.Add(new ZipUpdate(entry));
                updateIndex_.Add(entry.Name, num);
            }
            updates_.Sort(new UpdateComparer());
            int num2 = 0;
            foreach (ZipUpdate update in updates_)
            {
                if (num2 == (updates_.Count - 1))
                {
                    break;
                }
                update.OffsetBasedSize = ((ZipUpdate)updates_[num2 + 1]).Entry.Offset - update.Entry.Offset;
                num2++;
            }
            updateCount_ = updates_.Count;
            contentsEdited_ = false;
            commentEdited_ = false;
            newComment_ = null;
        }

        private static void CheckClassicPassword(CryptoStream classicCryptoStream, ZipEntry entry)
        {
            byte[] buffer = new byte[12];
            StreamUtils.ReadFully(classicCryptoStream, buffer);
            if (buffer[11] != entry.CryptoCheckValue)
            {
                throw new ZipException("Invalid password");
            }
        }

        private void CheckUpdating()
        {
            if (updates_ == null)
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
            if (isDisposed_)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            CheckUpdating();
            try
            {
                updateIndex_.Clear();
                updateIndex_ = null;
                if (contentsEdited_)
                {
                    RunUpdates();
                }
                else if (commentEdited_)
                {
                    UpdateCommentOnly();
                }
                else if (entries_.Length == 0)
                {
                    byte[] comment = (newComment_ != null) ? newComment_.RawComment : ZipConstants.ConvertToArray(comment_);
                    using (ZipHelperStream stream = new ZipHelperStream(baseStream_))
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
            Crc32 crc = new Crc32();
            byte[] buffer = GetBuffer();
            long num = bytesToCopy;
            long num2 = 0L;
            do
            {
                int length = buffer.Length;
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
                throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", num, num2));
            }
            if (updateCrc)
            {
                update.OutEntry.Crc = crc.Value;
            }
        }

        private void CopyDescriptorBytes(ZipUpdate update, Stream dest, Stream source)
        {
            int descriptorSize = GetDescriptorSize(update);
            if (descriptorSize > 0)
            {
                byte[] buffer = GetBuffer();
                while (descriptorSize > 0)
                {
                    int count = Math.Min(buffer.Length, descriptorSize);
                    int num3 = source.Read(buffer, 0, count);
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
            int descriptorSize = GetDescriptorSize(update);
            while (descriptorSize > 0)
            {
                int count = descriptorSize;
                byte[] buffer = GetBuffer();
                stream.Position = sourcePosition;
                int num3 = stream.Read(buffer, 0, count);
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
                long offset = update.Entry.Offset + 0x1aL;
                baseStream_.Seek(offset, SeekOrigin.Begin);
                uint num2 = ReadLEUshort();
                uint num3 = ReadLEUshort();
                baseStream_.Seek(num2 + num3, SeekOrigin.Current);
                CopyBytes(update, workFile.baseStream_, baseStream_, update.Entry.CompressedSize, false);
            }
            CopyDescriptorBytes(update, workFile.baseStream_, baseStream_);
        }

        private void CopyEntryDataDirect(ZipUpdate update, Stream stream, bool updateCrc, ref long destinationPosition, ref long sourcePosition)
        {
            int num4;
            long compressedSize = update.Entry.CompressedSize;
            Crc32 crc = new Crc32();
            byte[] buffer = GetBuffer();
            long num2 = compressedSize;
            long num3 = 0L;
            do
            {
                int length = buffer.Length;
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
                throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", num2, num3));
            }
            if (updateCrc)
            {
                update.OutEntry.Crc = crc.Value;
            }
        }

        private void CopyEntryDirect(ZipFile workFile, ZipUpdate update, ref long destinationPosition)
        {
            bool flag = false;
            if (update.Entry.Offset == destinationPosition)
            {
                flag = true;
            }
            if (!flag)
            {
                baseStream_.Position = destinationPosition;
                workFile.WriteLocalEntryHeader(update);
                destinationPosition = baseStream_.Position;
            }
            long offset = update.Entry.Offset + 0x1aL;
            baseStream_.Seek(offset, SeekOrigin.Begin);
            uint num3 = ReadLEUshort();
            uint num4 = ReadLEUshort();
            long sourcePosition = (baseStream_.Position + num3) + num4;
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
                    CopyEntryDataDirect(update, baseStream_, false, ref destinationPosition, ref sourcePosition);
                }
                CopyDescriptorBytesDirect(update, baseStream_, ref destinationPosition, sourcePosition);
            }
        }

        public static ZipFile Create(Stream outStream)
        {
            if (outStream == null)
            {
                throw new ArgumentNullException("outStream");
            }
            if (!outStream.CanWrite)
            {
                throw new ArgumentException("Stream is not writeable", "outStream");
            }
            if (!outStream.CanSeek)
            {
                throw new ArgumentException("Stream is not seekable", "outStream");
            }
            return new ZipFile { baseStream_ = outStream };
        }

        public static ZipFile Create(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }
            FileStream stream = File.Create(fileName);
            return new ZipFile { name_ = fileName, baseStream_ = stream, isStreamOwner = true };
        }

        private Stream CreateAndInitDecryptionStream(Stream baseStream, ZipEntry entry)
        {
            if ((entry.Version < 50) || ((entry.Flags & 0x40) == 0))
            {
                PkzipClassicManaged managed = new PkzipClassicManaged();
                OnKeysRequired(entry.Name);
                if (!HaveKeys)
                {
                    throw new ZipException("No password available for encrypted stream");
                }
                CryptoStream classicCryptoStream 
                    = new CryptoStream(baseStream, managed.CreateDecryptor(key, null), CryptoStreamMode.Read);
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
            int aESSaltLen = entry.AESSaltLen;
            byte[] buffer = new byte[aESSaltLen];
            int num2 = baseStream.Read(buffer, 0, aESSaltLen);
            if (num2 != aESSaltLen)
            {
                throw new ZipException(string.Concat(new object[] { "AES Salt expected ", aESSaltLen, " got ", num2 }));
            }
            byte[] buffer2 = new byte[2];
            baseStream.Read(buffer2, 0, 2);
            int blockSize = entry.AESKeySize / 8;
            ZipAESTransform transform = new ZipAESTransform(rawPassword_, buffer, blockSize, false);
            byte[] pwdVerifier = transform.PwdVerifier;
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
                PkzipClassicManaged managed = new PkzipClassicManaged();
                OnKeysRequired(entry.Name);
                if (!HaveKeys)
                {
                    throw new ZipException("No password available for encrypted stream");
                }
                stream = new CryptoStream(new UncompressedStream(baseStream), managed.CreateEncryptor(key, null), CryptoStreamMode.Write);
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
                throw new ArgumentNullException("entry");
            }
            CheckUpdating();
            int num = FindExistingUpdate(entry);
            if (num < 0)
            {
                throw new ZipException("Cannot find entry to delete");
            }
            contentsEdited_ = true;
            updates_[num] = null;
            updateCount_ -= 1L;
        }

        public bool Delete(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }
            CheckUpdating();
            int num = FindExistingUpdate(fileName);
            if ((num < 0) || (updates_[num] == null))
            {
                throw new ZipException("Cannot find entry to delete");
            }
            const bool flag = true;
            contentsEdited_ = true;
            updates_[num] = null;
            updateCount_ -= 1L;
            return flag;
        }

        protected virtual void Dispose(bool disposing)
        {
            DisposeInternal(disposing);
        }

        private void DisposeInternal(bool disposing)
        {
            if (!isDisposed_)
            {
                isDisposed_ = true;
                entries_ = new ZipEntry[0];
                if (IsStreamOwner && (baseStream_ != null))
                {
                    lock (baseStream_)
                    {
                        baseStream_.Close();
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
            if (isDisposed_)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            for (int i = 0; i < entries_.Length; i++)
            {
                if (string.Compare(name, entries_[i].Name, ignoreCase, CultureInfo.InvariantCulture) == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        private int FindExistingUpdate(ZipEntry entry)
        {
            int num = -1;
            string transformedFileName = GetTransformedFileName(entry.Name);
            if (updateIndex_.ContainsKey(transformedFileName))
            {
                num = (int)updateIndex_[transformedFileName];
            }
            return num;
        }

        private int FindExistingUpdate(string fileName)
        {
            int num = -1;
            string transformedFileName = GetTransformedFileName(fileName);
            if (updateIndex_.ContainsKey(transformedFileName))
            {
                num = (int)updateIndex_[transformedFileName];
            }
            return num;
        }

        private byte[] GetBuffer()
        {
            if (copyBuffer_ == null)
            {
                copyBuffer_ = new byte[bufferSize_];
            }
            return copyBuffer_;
        }

        private int GetDescriptorSize(ZipUpdate update)
        {
            int num = 0;
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
            if (isDisposed_)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            int index = FindEntry(name, true);
            if (index < 0)
            {
                return null;
            }
            return (ZipEntry)entries_[index].Clone();
        }

        public IEnumerator GetEnumerator()
        {
            if (isDisposed_)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            return new ZipEntryEnumerator(entries_);
        }

        public Stream GetInputStream(ZipEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }
            if (isDisposed_)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            long zipFileIndex = entry.ZipFileIndex;
            if (((zipFileIndex < 0L) || (zipFileIndex >= entries_.Length)) 
                || (entries_[(int)((IntPtr)zipFileIndex)].Name != entry.Name))
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
            if (isDisposed_)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            long start = LocateEntry(entries_[(int)((IntPtr)entryIndex)]);
            CompressionMethod compressionMethod = entries_[(int)((IntPtr)entryIndex)].CompressionMethod;
            Stream baseStream = new PartialInputStream(this, start, entries_[(int)((IntPtr)entryIndex)].CompressedSize);
            if (entries_[(int)((IntPtr)entryIndex)].IsCrypted)
            {
                baseStream = CreateAndInitDecryptionStream(baseStream, entries_[(int)((IntPtr)entryIndex)]);
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
            Stream baseStream = baseStream_;
            if (entry.IsCrypted)
            {
                baseStream = CreateAndInitEncryptionStream(baseStream, entry);
            }
            CompressionMethod compressionMethod = entry.CompressionMethod;
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
            INameTransform nameTransform = NameTransform;
            if (nameTransform == null)
            {
                return name;
            }
            return nameTransform.TransformDirectory(name);
        }

        private string GetTransformedFileName(string name)
        {
            INameTransform nameTransform = NameTransform;
            if (nameTransform == null)
            {
                return name;
            }
            return nameTransform.TransformFile(name);
        }

        private long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
        {
            using (ZipHelperStream stream = new ZipHelperStream(baseStream_))
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
            long position = workFile.baseStream_.Position;
            if (update.Entry.IsFile && (update.Filename != null))
            {
                using (Stream stream = workFile.GetOutputStream(update.OutEntry))
                {
                    using (Stream stream2 = GetInputStream(update.Entry))
                    {
                        CopyBytes(update, stream, stream2, stream2.Length, true);
                    }
                }
            }
            long num2 = workFile.baseStream_.Position;
            update.Entry.CompressedSize = num2 - position;
        }

        private void OnKeysRequired(string fileName)
        {
            if (KeysRequired != null)
            {
                KeysRequiredEventArgs e = new KeysRequiredEventArgs(fileName, key);
                KeysRequired(this, e);
                key = e.Key;
            }
        }

        private void PostUpdateCleanup()
        {
            updateDataSource_ = null;
            updates_ = null;
            updateIndex_ = null;
            if (archiveStorage_ != null)
            {
                archiveStorage_.Dispose();
                archiveStorage_ = null;
            }
        }

        private void ReadEntries()
        {
            if (!baseStream_.CanSeek)
            {
                throw new ZipException("ZipFile stream must be seekable");
            }
            long endLocation = LocateBlockWithSignature(0x6054b50, baseStream_.Length, 0x16, 0xffff);
            if (endLocation < 0L)
            {
                throw new ZipException("Cannot find central directory");
            }
            ushort num2 = ReadLEUshort();
            ushort num3 = ReadLEUshort();
            ulong num4 = ReadLEUshort();
            ulong num5 = ReadLEUshort();
            ulong num6 = ReadLEUint();
            long num7 = ReadLEUint();
            uint num8 = ReadLEUshort();
            if (num8 > 0)
            {
                byte[] buffer = new byte[num8];
                StreamUtils.ReadFully(baseStream_, buffer);
                comment_ = ZipConstants.ConvertToString(buffer);
            }
            else
            {
                comment_ = string.Empty;
            }
            bool flag = false;
            if ((((num2 == 0xffff) || (num3 == 0xffff)) || ((num4 == 0xffffL) 
                || (num5 == 0xffffL))) || ((num6 == 0xffffffffL) || (num7 == 0xffffffffL)))
            {
                flag = true;
                if (LocateBlockWithSignature(0x7064b50, endLocation, 0, DefaultBufferSize) < 0L)
                {
                    throw new ZipException("Cannot find Zip64 locator");
                }
                ReadLEUint();
                ulong num10 = ReadLEUlong();
                ReadLEUint();
                baseStream_.Position = (long)num10;
                long num11 = ReadLEUint();
                if (num11 != 0x6064b50L)
                {
                    throw new ZipException(string.Format("Invalid Zip64 Central directory signature at {0:X}", num10));
                }
                ReadLEUlong();
                ReadLEUshort();
                ReadLEUshort();
                ReadLEUint();
                ReadLEUint();
                num4 = ReadLEUlong();
                num5 = ReadLEUlong();
                num6 = ReadLEUlong();
                num7 = (long)ReadLEUlong();
            }
            entries_ = new ZipEntry[num4];
            if (!flag && (num7 < (long)(((ulong)endLocation) - (4L + num6))))
            {
                offsetOfFirstEntry = endLocation - ((4L + ((long)num6)) + num7);
                if (offsetOfFirstEntry <= 0L)
                {
                    throw new ZipException("Invalid embedded zip archive");
                }
            }
            baseStream_.Seek(offsetOfFirstEntry + num7, SeekOrigin.Begin);
            for (ulong i = 0L; i < num4; i += (ulong)1L)
            {
                if (ReadLEUint() != 0x2014b50)
                {
                    throw new ZipException("Wrong Central Directory signature");
                }
                int madeByInfo = ReadLEUshort();
                int versionRequiredToExtract = ReadLEUshort();
                int flags = ReadLEUshort();
                int num16 = ReadLEUshort();
                uint num17 = ReadLEUint();
                uint num18 = ReadLEUint();
                long num19 = ReadLEUint();
                long num20 = ReadLEUint();
                int num21 = ReadLEUshort();
                int num22 = ReadLEUshort();
                int num23 = ReadLEUshort();
                ReadLEUshort();
                ReadLEUshort();
                uint num24 = ReadLEUint();
                long num25 = ReadLEUint();
                byte[] buffer2 = new byte[Math.Max(num21, num23)];
                StreamUtils.ReadFully(baseStream_, buffer2, 0, num21);
                ZipEntry entry = new ZipEntry(ZipConstants.ConvertToStringExt(flags, buffer2, num21), versionRequiredToExtract, madeByInfo, (CompressionMethod)num16)
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
                    byte[] buffer3 = new byte[num22];
                    StreamUtils.ReadFully(baseStream_, buffer3);
                    entry.ExtraData = buffer3;
                }
                entry.ProcessExtraData(false);
                if (num23 > 0)
                {
                    StreamUtils.ReadFully(baseStream_, buffer2, 0, num23);
                    entry.Comment = ZipConstants.ConvertToStringExt(flags, buffer2, num23);
                }
                entries_[(int)((IntPtr)i)] = entry;
            }
        }

        private uint ReadLEUint()
        {
            return (uint)(ReadLEUshort() | (ReadLEUshort() << 0x10));
        }

        private ulong ReadLEUlong()
        {
            return (ReadLEUint() | (ReadLEUint() << 0x20));
        }

        private ushort ReadLEUshort()
        {
            int num = baseStream_.ReadByte();
            if (num < 0)
            {
                throw new EndOfStreamException("End of stream");
            }
            int num2 = baseStream_.ReadByte();
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
            isNewArchive_ = false;
            baseStream_ = source;
            ReadEntries();
        }

        private void RunUpdates()
        {
            ZipFile file;
            long sizeEntries = 0L;
            long num2;
            bool flag = false;
            long destinationPosition = 0L;
            if (IsNewArchive)
            {
                file = this;
                file.baseStream_.Position = 0L;
                flag = true;
            }
            else if (archiveStorage_.UpdateMode == FileUpdateMode.Direct)
            {
                file = this;
                file.baseStream_.Position = 0L;
                flag = true;
                updates_.Sort(new UpdateComparer());
            }
            else
            {
                file = Create(archiveStorage_.GetTemporaryOutput());
                file.UseZip64 = UseZip64;
                if (key != null)
                {
                    file.key = (byte[])key.Clone();
                }
            }
            try
            {
                foreach (ZipUpdate update in updates_)
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
                        file.baseStream_.Position = destinationPosition;
                    }
                    AddEntry(file, update);
                    if (flag)
                    {
                        destinationPosition = file.baseStream_.Position;
                    }
                }
                if (!IsNewArchive && flag)
                {
                    file.baseStream_.Position = destinationPosition;
                }
                long position = file.baseStream_.Position;
                foreach (ZipUpdate update2 in updates_)
                {
                    if (update2 != null)
                    {
                        sizeEntries += file.WriteCentralDirectoryHeader(update2.OutEntry);
                    }
                }
                byte[] comment = (newComment_ != null) ? newComment_.RawComment : ZipConstants.ConvertToArray(comment_);
                using (ZipHelperStream stream = new ZipHelperStream(file.baseStream_))
                {
                    stream.WriteEndOfCentralDirectory(updateCount_, sizeEntries, position, comment);
                }
                num2 = file.baseStream_.Position;
                foreach (ZipUpdate update3 in updates_)
                {
                    if (update3 != null)
                    {
                        if ((update3.CrcPatchOffset > 0L) && (update3.OutEntry.CompressedSize > 0L))
                        {
                            file.baseStream_.Position = update3.CrcPatchOffset;
                            file.WriteLEInt((int)update3.OutEntry.Crc);
                        }
                        if (update3.SizePatchOffset > 0L)
                        {
                            file.baseStream_.Position = update3.SizePatchOffset;
                            if (update3.OutEntry.LocalHeaderRequiresZip64)
                            {
                                file.WriteLeLong(update3.OutEntry.Size);
                                file.WriteLeLong(update3.OutEntry.CompressedSize);
                            }
                            else
                            {
                                file.WriteLEInt((int)update3.OutEntry.CompressedSize);
                                file.WriteLEInt((int)update3.OutEntry.Size);
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
                file.baseStream_.SetLength(num2);
                file.baseStream_.Flush();
                isNewArchive_ = false;
                ReadEntries();
            }
            else
            {
                baseStream_.Close();
                Reopen(archiveStorage_.ConvertTemporaryToFinal());
            }
        }

        public void SetComment(string comment)
        {
            if (isDisposed_)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            CheckUpdating();
            newComment_ = new ZipString(comment);
            if (newComment_.RawLength > 0xffff)
            {
                newComment_ = null;
                throw new ZipException("Comment length exceeds maximum - 65535");
            }
            commentEdited_ = true;
        }

        void IDisposable.Dispose()
        {
            Close();
        }

        public bool TestArchive(bool testData, 
            TestStrategy strategy = TestStrategy.FindFirstError, 
            ZipTestResultHandler resultHandler = null)
        {
            if (isDisposed_)
            {
                throw new ObjectDisposedException("ZipFile");
            }
            TestStatus status = new TestStatus(this);
            if (resultHandler != null)
            {
                resultHandler(status, null);
            }
            HeaderTest tests = testData ? (HeaderTest.Header | HeaderTest.Extract) : HeaderTest.Header;
            bool flag = true;
            try
            {
                for (int i = 0; flag && (i < Count); i++)
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
                        if (resultHandler != null)
                        {
                            resultHandler(status, string.Format("Exception during test - '{0}'", exception.Message));
                        }
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
                        Crc32 crc = new Crc32();
                        using (Stream stream = GetInputStream(this[i]))
                        {
                            int num3;
                            byte[] buffer = new byte[DefaultBufferSize];
                            long num2 = 0L;
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
                            if (resultHandler != null)
                            {
                                resultHandler(status, "CRC mismatch");
                            }
                            if (strategy == TestStrategy.FindFirstError)
                            {
                                flag = false;
                            }
                        }
                        if ((this[i].Flags & 8) != 0)
                        {
                            ZipHelperStream stream2 = new ZipHelperStream(baseStream_);
                            DescriptorData data = new DescriptorData();
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
                if (resultHandler != null)
                {
                    resultHandler(status, string.Format("Exception during test - '{0}'", exception2.Message));
                }
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
            lock (baseStream_)
            {
                bool flag = (tests & HeaderTest.Header) != 0;
                bool flag2 = (tests & HeaderTest.Extract) != 0;
                baseStream_.Seek(offsetOfFirstEntry + entry.Offset, SeekOrigin.Begin);
                if (ReadLEUint() != 0x4034b50)
                {
                    throw new ZipException(string.Format("Wrong local header signature @{0:X}", offsetOfFirstEntry + entry.Offset));
                }
                short num = (short)ReadLEUshort();
                short flags = (short)ReadLEUshort();
                short num3 = (short)ReadLEUshort();
                short num4 = (short)ReadLEUshort();
                short num5 = (short)ReadLEUshort();
                uint num6 = ReadLEUint();
                long num7 = ReadLEUint();
                long num8 = ReadLEUint();
                int num9 = ReadLEUshort();
                int num10 = ReadLEUshort();
                byte[] buffer = new byte[num9];
                StreamUtils.ReadFully(baseStream_, buffer);
                byte[] buffer2 = new byte[num10];
                StreamUtils.ReadFully(baseStream_, buffer2);
                ZipExtraData data = new ZipExtraData(buffer2);
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
                        throw new ZipException(string.Format("Version required to extract this entry not supported ({0})", num));
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
                        throw new ZipException(string.Format("Version required to extract this entry is invalid ({0})", num));
                    }
                    if ((flags & 0xc010) != 0)
                    {
                        throw new ZipException("Reserved bit flags cannot be set.");
                    }
                    if (((flags & 1) != 0) && (num < 20))
                    {
                        throw new ZipException(string.Format("Version required to extract this entry is too low for encryption ({0})", num));
                    }
                    if ((flags & 0x40) != 0)
                    {
                        if ((flags & 1) == 0)
                        {
                            throw new ZipException("Strong encryption flag set but encryption flag is not set");
                        }
                        if (num < 50)
                        {
                            throw new ZipException(string.Format("Version required to extract this entry is too low for encryption ({0})", num));
                        }
                    }
                    if (((flags & 0x20) != 0) && (num < 0x1b))
                    {
                        throw new ZipException(string.Format("Patched data requires higher version than ({0})", num));
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
                    string name = ZipConstants.ConvertToStringExt(flags, buffer);
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
                        throw new ZipException(string.Format("Size mismatch between central header({0}) and local header({1})", entry.Size, num8));
                    }
                    if (((num7 != entry.CompressedSize) && (num7 != 0xffffffffL)) && (num7 != -1L))
                    {
                        throw new ZipException(string.Format("Compressed size mismatch between central header({0}) and local header({1})", entry.CompressedSize, num7));
                    }
                }
                int num11 = num9 + num10;
                return (((offsetOfFirstEntry + entry.Offset) + 30L) + num11);
            }
        }

        private void UpdateCommentOnly()
        {
            long length = baseStream_.Length;
            ZipHelperStream stream;
            if (archiveStorage_.UpdateMode == FileUpdateMode.Safe)
            {
                stream = new ZipHelperStream(archiveStorage_.MakeTemporaryCopy(baseStream_))
                {
                    IsStreamOwner = true
                };
                baseStream_.Close();
                baseStream_ = null;
            }
            else if (archiveStorage_.UpdateMode == FileUpdateMode.Direct)
            {
                baseStream_ = archiveStorage_.OpenForDirectUpdate(baseStream_);
                stream = new ZipHelperStream(baseStream_);
            }
            else
            {
                baseStream_.Close();
                baseStream_ = null;
                stream = new ZipHelperStream(Name);
            }
            using (stream)
            {
                if (stream.LocateBlockWithSignature(0x6054b50, length, 0x16, 0xffff) < 0L)
                {
                    throw new ZipException("Cannot find central directory");
                }
                stream.Position += 0x10L;
                byte[] rawComment = newComment_.RawComment;
                stream.WriteLEShort(rawComment.Length);
                stream.Write(rawComment, 0, rawComment.Length);
                stream.SetLength(stream.Position);
            }
            if (archiveStorage_.UpdateMode == FileUpdateMode.Safe)
            {
                Reopen(archiveStorage_.ConvertTemporaryToFinal());
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
            WriteLEInt(0x2014b50);
            WriteLEShort(0x33);
            WriteLEShort(entry.Version);
            WriteLEShort(entry.Flags);
            WriteLEShort((byte)entry.CompressionMethod);
            WriteLEInt((int)entry.DosTime);
            WriteLEInt((int)entry.Crc);
            if (entry.IsZip64Forced() || (entry.CompressedSize >= 0xffffffffL))
            {
                WriteLEInt(-1);
            }
            else
            {
                WriteLEInt((int)(((ulong)entry.CompressedSize) & 0xffffffffL));
            }
            if (entry.IsZip64Forced() || (entry.Size >= 0xffffffffL))
            {
                WriteLEInt(-1);
            }
            else
            {
                WriteLEInt((int)entry.Size);
            }
            byte[] buffer = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
            if (buffer.Length > 0xffff)
            {
                throw new ZipException("Entry name is too long.");
            }
            WriteLEShort(buffer.Length);
            ZipExtraData data = new ZipExtraData(entry.ExtraData);
            if (entry.CentralHeaderRequiresZip64)
            {
                data.StartNewEntry();
                if ((entry.Size >= 0xffffffffL) || (useZip64_ == UseZip64.On))
                {
                    data.AddLeLong(entry.Size);
                }
                if ((entry.CompressedSize >= 0xffffffffL) || (useZip64_ == UseZip64.On))
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
            byte[] entryData = data.GetEntryData();
            WriteLEShort(entryData.Length);
            WriteLEShort((entry.Comment != null) ? entry.Comment.Length : 0);
            WriteLEShort(0);
            WriteLEShort(0);
            if (entry.ExternalFileAttributes != -1)
            {
                WriteLEInt(entry.ExternalFileAttributes);
            }
            else if (entry.IsDirectory)
            {
                WriteLEUint(0x10);
            }
            else
            {
                WriteLEUint(0);
            }
            if (entry.Offset >= 0xffffffffL)
            {
                WriteLEUint(uint.MaxValue);
            }
            else
            {
                WriteLEUint((uint)((int)entry.Offset));
            }
            if (buffer.Length > 0)
            {
                baseStream_.Write(buffer, 0, buffer.Length);
            }
            if (entryData.Length > 0)
            {
                baseStream_.Write(entryData, 0, entryData.Length);
            }
            byte[] buffer3 = (entry.Comment != null) ? Encoding.ASCII.GetBytes(entry.Comment) : new byte[0];
            if (buffer3.Length > 0)
            {
                baseStream_.Write(buffer3, 0, buffer3.Length);
            }
            return (((0x2e + buffer.Length) + entryData.Length) + buffer3.Length);
        }

        private static void WriteEncryptionHeader(Stream stream, long crcValue)
        {
            byte[] buffer = new byte[12];
            new Random().NextBytes(buffer);
            buffer[11] = (byte)(crcValue >> 0x18);
            stream.Write(buffer, 0, buffer.Length);
        }

        private void WriteLEInt(int value)
        {
            WriteLEShort(value & 0xffff);
            WriteLEShort(value >> 0x10);
        }

        private void WriteLeLong(long value)
        {
            WriteLEInt((int)(((ulong)value) & 0xffffffffL));
            WriteLEInt((int)(value >> 0x20));
        }

        private void WriteLEShort(int value)
        {
            baseStream_.WriteByte((byte)(value & 0xff));
            baseStream_.WriteByte((byte)((value >> 8) & 0xff));
        }

        private void WriteLEUint(uint value)
        {
            WriteLEUshort((ushort)(value & 0xffff));
            WriteLEUshort((ushort)(value >> 0x10));
        }

        private void WriteLEUlong(ulong value)
        {
            WriteLEUint((uint)(value & 0xffffffffL));
            WriteLEUint((uint)(value >> 0x20));
        }

        private void WriteLEUshort(ushort value)
        {
            baseStream_.WriteByte((byte)(value & 0xff));
            baseStream_.WriteByte((byte)(value >> 8));
        }

        private void WriteLocalEntryHeader(ZipUpdate update)
        {
            ZipEntry outEntry = update.OutEntry;
            outEntry.Offset = baseStream_.Position;
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
                switch (useZip64_)
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
            WriteLEInt(0x4034b50);
            WriteLEShort(outEntry.Version);
            WriteLEShort(outEntry.Flags);
            WriteLEShort((byte)outEntry.CompressionMethod);
            WriteLEInt((int)outEntry.DosTime);
            if (!outEntry.HasCrc)
            {
                update.CrcPatchOffset = baseStream_.Position;
                WriteLEInt(0);
            }
            else
            {
                WriteLEInt((int)outEntry.Crc);
            }
            if (outEntry.LocalHeaderRequiresZip64)
            {
                WriteLEInt(-1);
                WriteLEInt(-1);
            }
            else
            {
                if ((outEntry.CompressedSize < 0L) || (outEntry.Size < 0L))
                {
                    update.SizePatchOffset = baseStream_.Position;
                }
                WriteLEInt((int)outEntry.CompressedSize);
                WriteLEInt((int)outEntry.Size);
            }
            byte[] buffer = ZipConstants.ConvertToArray(outEntry.Flags, outEntry.Name);
            if (buffer.Length > 0xffff)
            {
                throw new ZipException("Entry name too long.");
            }
            ZipExtraData data = new ZipExtraData(outEntry.ExtraData);
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
            WriteLEShort(buffer.Length);
            WriteLEShort(outEntry.ExtraData.Length);
            if (buffer.Length > 0)
            {
                baseStream_.Write(buffer, 0, buffer.Length);
            }
            if (outEntry.LocalHeaderRequiresZip64)
            {
                if (!data.Find(1))
                {
                    throw new ZipException("Internal error cannot find extra data");
                }
                update.SizePatchOffset = baseStream_.Position + data.CurrentReadIndex;
            }
            if (outEntry.ExtraData.Length > 0)
            {
                baseStream_.Write(outEntry.ExtraData, 0, outEntry.ExtraData.Length);
            }
        }

        public int BufferSize
        {
            get
            {
                return bufferSize_;
            }
            set
            {
                if (value < 0x400)
                {
                    throw new ArgumentOutOfRangeException("value", "cannot be below 1024");
                }
                if (bufferSize_ != value)
                {
                    bufferSize_ = value;
                    copyBuffer_ = null;
                }
            }
        }

        public long Count
        {
            get
            {
                return entries_.Length;
            }
        }

        public ZipEntry this[int index]
        {
            get
            {
                return (ZipEntry)entries_[index].Clone();
            }
        }

        public IEntryFactory EntryFactory
        {
            get
            {
                return updateEntryFactory_;
            }
            set
            {
                if (value == null)
                {
                    updateEntryFactory_ = new ZipEntryFactory();
                }
                else
                {
                    updateEntryFactory_ = value;
                }
            }
        }

        private bool HaveKeys
        {
            get
            {
                return (key != null);
            }
        }

        public bool IsEmbeddedArchive
        {
            get
            {
                return (offsetOfFirstEntry > 0L);
            }
        }

        public bool IsNewArchive
        {
            get
            {
                return isNewArchive_;
            }
        }

        public bool IsStreamOwner
        {
            get
            {
                return isStreamOwner;
            }
            set
            {
                isStreamOwner = value;
            }
        }

        public bool IsUpdating
        {
            get
            {
                return (updates_ != null);
            }
        }

        private byte[] Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        public string Name
        {
            get
            {
                return name_;
            }
        }

        public INameTransform NameTransform
        {
            get
            {
                return updateEntryFactory_.NameTransform;
            }
            set
            {
                updateEntryFactory_.NameTransform = value;
            }
        }

        public string Password
        {
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    key = null;
                }
                else
                {
                    rawPassword_ = value;
                    key = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(value));
                }
            }
        }

        [Obsolete("Use the Count property instead")]
        public int Size
        {
            get
            {
                return entries_.Length;
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

        public string ZipFileComment
        {
            get
            {
                return comment_;
            }
        }

        [Flags]
        private enum HeaderTest
        {
            Extract = 1,
            Header = 2
        }

        public delegate void KeysRequiredEventHandler(object sender, KeysRequiredEventArgs e);

        private class PartialInputStream : Stream
        {
            private Stream baseStream_;
            private long end_;
            private long length_;
            private long readPos_;
            private long start_;

            public PartialInputStream(ZipFile zipFile, long start, long length)
            {
                start_ = start;
                length_ = length;
                baseStream_ = zipFile.baseStream_;
                readPos_ = start;
                end_ = start + length;
            }

            public override void Close()
            {
            }

            public override void Flush()
            {
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                lock (baseStream_)
                {
                    if (count > (end_ - readPos_))
                    {
                        count = (int)(end_ - readPos_);
                        if (count == 0)
                        {
                            return 0;
                        }
                    }
                    baseStream_.Seek(readPos_, SeekOrigin.Begin);
                    int num = baseStream_.Read(buffer, offset, count);
                    if (num > 0)
                    {
                        readPos_ += num;
                    }
                    return num;
                }
            }

            public override int ReadByte()
            {
                if (readPos_ >= end_)
                {
                    return -1;
                }
                lock (baseStream_)
                {
                    long num2;
                    readPos_ = (num2 = readPos_) + 1L;
                    baseStream_.Seek(num2, SeekOrigin.Begin);
                    return baseStream_.ReadByte();
                }
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                long num = readPos_;
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        num = start_ + offset;
                        break;

                    case SeekOrigin.Current:
                        num = readPos_ + offset;
                        break;

                    case SeekOrigin.End:
                        num = end_ + offset;
                        break;
                }
                if (num < start_)
                {
                    throw new ArgumentException("Negative position is invalid");
                }
                if (num >= end_)
                {
                    throw new IOException("Cannot seek past end");
                }
                readPos_ = num;
                return readPos_;
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            public override bool CanRead
            {
                get
                {
                    return true;
                }
            }

            public override bool CanSeek
            {
                get
                {
                    return true;
                }
            }

            public override bool CanTimeout
            {
                get
                {
                    return baseStream_.CanTimeout;
                }
            }

            public override bool CanWrite
            {
                get
                {
                    return false;
                }
            }

            public override long Length
            {
                get
                {
                    return length_;
                }
            }

            public override long Position
            {
                get
                {
                    return (readPos_ - start_);
                }
                set
                {
                    long num = start_ + value;
                    if (num < start_)
                    {
                        throw new ArgumentException("Negative position is invalid");
                    }
                    if (num >= end_)
                    {
                        throw new InvalidOperationException("Cannot seek past end");
                    }
                    readPos_ = num;
                }
            }
        }

        private class UncompressedStream : Stream
        {
            private Stream baseStream_;

            public UncompressedStream(Stream baseStream)
            {
                baseStream_ = baseStream;
            }

            public override void Close()
            {
            }

            public override void Flush()
            {
                baseStream_.Flush();
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
                baseStream_.Write(buffer, offset, count);
            }

            public override bool CanRead
            {
                get
                {
                    return false;
                }
            }

            public override bool CanSeek
            {
                get
                {
                    return false;
                }
            }

            public override bool CanWrite
            {
                get
                {
                    return baseStream_.CanWrite;
                }
            }

            public override long Length
            {
                get
                {
                    return 0L;
                }
            }

            public override long Position
            {
                get
                {
                    return baseStream_.Position;
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
                ZipUpdate update = x as ZipUpdate;
                ZipUpdate update2 = y as ZipUpdate;
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
                    int num2 = ((update.Command == UpdateCommand.Copy) || (update.Command == UpdateCommand.Modify)) ? 0 : 1;
                    int num3 = ((update2.Command == UpdateCommand.Copy) || (update2.Command == UpdateCommand.Modify)) ? 0 : 1;
                    int num = num2 - num3;
                    if (num != 0)
                    {
                        return num;
                    }
                    long num4 = update.Entry.Offset - update2.Entry.Offset;
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
            private ZipEntry[] array;
            private int index = -1;

            public ZipEntryEnumerator(ZipEntry[] entries)
            {
                array = entries;
            }

            public bool MoveNext()
            {
                return (++index < array.Length);
            }

            public void Reset()
            {
                index = -1;
            }

            public object Current
            {
                get
                {
                    return array[index];
                }
            }
        }

        private class ZipString
        {
            private string comment_;
            private bool isSourceString_;
            private byte[] rawComment_;

            public ZipString(string comment)
            {
                comment_ = comment;
                isSourceString_ = true;
            }

            public ZipString(byte[] rawString)
            {
                rawComment_ = rawString;
            }

            private void MakeBytesAvailable()
            {
                if (rawComment_ == null)
                {
                    rawComment_ = ZipConstants.ConvertToArray(comment_);
                }
            }

            private void MakeTextAvailable()
            {
                if (comment_ == null)
                {
                    comment_ = ZipConstants.ConvertToString(rawComment_);
                }
            }

            public static implicit operator string(ZipString zipString)
            {
                zipString.MakeTextAvailable();
                return zipString.comment_;
            }

            public void Reset()
            {
                if (isSourceString_)
                {
                    rawComment_ = null;
                }
                else
                {
                    comment_ = null;
                }
            }

            public bool IsSourceString
            {
                get
                {
                    return isSourceString_;
                }
            }

            public byte[] RawComment
            {
                get
                {
                    MakeBytesAvailable();
                    return (byte[])rawComment_.Clone();
                }
            }

            public int RawLength
            {
                get
                {
                    MakeBytesAvailable();
                    return rawComment_.Length;
                }
            }
        }

        private class ZipUpdate
        {
            private long _offsetBasedSize;
            private UpdateCommand command_;
            private long crcPatchOffset_;
            private IStaticDataSource dataSource_;
            private ZipEntry entry_;
            private string filename_;
            private ZipEntry outEntry_;
            private long sizePatchOffset_;

            public ZipUpdate(ZipEntry entry)
                : this(UpdateCommand.Copy, entry)
            {
            }

            public ZipUpdate(IStaticDataSource dataSource, ZipEntry entry)
            {
                sizePatchOffset_ = -1L;
                crcPatchOffset_ = -1L;
                _offsetBasedSize = -1L;
                command_ = UpdateCommand.Add;
                entry_ = entry;
                dataSource_ = dataSource;
            }

            public ZipUpdate(ZipEntry original, ZipEntry updated)
            {
                sizePatchOffset_ = -1L;
                crcPatchOffset_ = -1L;
                _offsetBasedSize = -1L;
                throw new ZipException("Modify not currently supported");
            }

            public ZipUpdate(UpdateCommand command, ZipEntry entry)
            {
                sizePatchOffset_ = -1L;
                crcPatchOffset_ = -1L;
                _offsetBasedSize = -1L;
                command_ = command;
                entry_ = (ZipEntry)entry.Clone();
            }

            public ZipUpdate(string fileName, ZipEntry entry)
            {
                sizePatchOffset_ = -1L;
                crcPatchOffset_ = -1L;
                _offsetBasedSize = -1L;
                command_ = UpdateCommand.Add;
                entry_ = entry;
                filename_ = fileName;
            }

            [Obsolete]
            public ZipUpdate(string fileName, string entryName)
                : this(fileName, entryName, CompressionMethod.Deflated)
            {
            }

            [Obsolete]
            public ZipUpdate(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
            {
                sizePatchOffset_ = -1L;
                crcPatchOffset_ = -1L;
                _offsetBasedSize = -1L;
                command_ = UpdateCommand.Add;
                entry_ = new ZipEntry(entryName);
                entry_.CompressionMethod = compressionMethod;
                dataSource_ = dataSource;
            }

            [Obsolete]
            public ZipUpdate(string fileName, string entryName, CompressionMethod compressionMethod)
            {
                sizePatchOffset_ = -1L;
                crcPatchOffset_ = -1L;
                _offsetBasedSize = -1L;
                command_ = UpdateCommand.Add;
                entry_ = new ZipEntry(entryName);
                entry_.CompressionMethod = compressionMethod;
                filename_ = fileName;
            }

            public Stream GetSource()
            {
                Stream source = null;
                if (dataSource_ != null)
                {
                    source = dataSource_.GetSource();
                }
                return source;
            }

            public UpdateCommand Command
            {
                get
                {
                    return command_;
                }
            }

            public long CrcPatchOffset
            {
                get
                {
                    return crcPatchOffset_;
                }
                set
                {
                    crcPatchOffset_ = value;
                }
            }

            public ZipEntry Entry
            {
                get
                {
                    return entry_;
                }
            }

            public string Filename
            {
                get
                {
                    return filename_;
                }
            }

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
                    if (outEntry_ == null)
                    {
                        outEntry_ = (ZipEntry)entry_.Clone();
                    }
                    return outEntry_;
                }
            }

            public long SizePatchOffset
            {
                get
                {
                    return sizePatchOffset_;
                }
                set
                {
                    sizePatchOffset_ = value;
                }
            }
        }
    }
}

