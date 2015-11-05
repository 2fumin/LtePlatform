using System;
using System.Collections;
using System.IO;
using Lte.Domain.ZipLib.CheckSums;
using Lte.Domain.ZipLib.Compression;
using Lte.Domain.ZipLib.Streams;

namespace Lte.Domain.ZipLib.Zip
{
    public class ZipOutputStream : DeflaterOutputStream
    {
        private Crc32 crc;
        private long crcPatchPos;
        private ZipEntry curEntry;
        private CompressionMethod curMethod;
        private int defaultCompressionLevel;
        private ArrayList entries;
        private long offset;
        private bool patchEntryHeader;
        private long size;
        private long sizePatchPos;
        private UseZip64 useZip64_;
        private byte[] zipComment;

        public ZipOutputStream(Stream baseOutputStream)
            : base(baseOutputStream, new Deflater(-1, true))
        {
            entries = new ArrayList();
            crc = new Crc32();
            defaultCompressionLevel = -1;
            curMethod = CompressionMethod.Deflated;
            zipComment = new byte[0];
            crcPatchPos = -1L;
            sizePatchPos = -1L;
            useZip64_ = UseZip64.Dynamic;
        }

        public ZipOutputStream(Stream baseOutputStream, int bufferSize)
            : base(baseOutputStream, new Deflater(-1, true), bufferSize)
        {
            entries = new ArrayList();
            crc = new Crc32();
            defaultCompressionLevel = -1;
            curMethod = CompressionMethod.Deflated;
            zipComment = new byte[0];
            crcPatchPos = -1L;
            sizePatchPos = -1L;
            useZip64_ = UseZip64.Dynamic;
        }

        private static void AddExtraDataAES(ZipEntry entry, ZipExtraData extraData)
        {
            extraData.StartNewEntry();
            extraData.AddLeShort(2);
            extraData.AddLeShort(0x4541);
            extraData.AddData(entry.AESEncryptionStrength);
            extraData.AddLeShort((int)entry.CompressionMethod);
            extraData.AddNewEntry(0x9901);
        }

        public void CloseEntry()
        {
            if (curEntry == null)
            {
                throw new InvalidOperationException("No open entry");
            }
            long compressedSize = size;
            if (curMethod == CompressionMethod.Deflated)
            {
                if (size >= 0L)
                {
                    Finish();
                    compressedSize = deflater_.TotalOut;
                }
                else
                {
                    deflater_.Reset();
                }
            }
            if (curEntry.AESKeySize > 0)
            {
                baseOutputStream_.Write(AESAuthCode, 0, 10);
            }
            if (curEntry.Size < 0L)
            {
                curEntry.Size = size;
            }
            else if (curEntry.Size != size)
            {
                throw new ZipException(string.Concat(new object[] { "size was ", size, ", but I expected ", curEntry.Size }));
            }
            if (curEntry.CompressedSize < 0L)
            {
                curEntry.CompressedSize = compressedSize;
            }
            else if (curEntry.CompressedSize != compressedSize)
            {
                throw new ZipException(string.Concat(new object[] { "compressed size was ", compressedSize, ", but I expected ", curEntry.CompressedSize }));
            }
            if (curEntry.Crc < 0L)
            {
                curEntry.Crc = crc.Value;
            }
            else if (curEntry.Crc != crc.Value)
            {
                throw new ZipException(string.Concat(new object[] { "crc was ", crc.Value, ", but I expected ", curEntry.Crc }));
            }
            offset += compressedSize;
            if (curEntry.IsCrypted)
            {
                if (curEntry.AESKeySize > 0)
                {
                    curEntry.CompressedSize += curEntry.AESOverheadSize;
                }
                else
                {
                    curEntry.CompressedSize += 12L;
                }
            }
            if (patchEntryHeader)
            {
                patchEntryHeader = false;
                long position = baseOutputStream_.Position;
                baseOutputStream_.Seek(crcPatchPos, SeekOrigin.Begin);
                WriteLeInt((int)curEntry.Crc);
                if (curEntry.LocalHeaderRequiresZip64)
                {
                    if (sizePatchPos == -1L)
                    {
                        throw new ZipException("Entry requires zip64 but this has been turned off");
                    }
                    baseOutputStream_.Seek(sizePatchPos, SeekOrigin.Begin);
                    WriteLeLong(curEntry.Size);
                    WriteLeLong(curEntry.CompressedSize);
                }
                else
                {
                    WriteLeInt((int)curEntry.CompressedSize);
                    WriteLeInt((int)curEntry.Size);
                }
                baseOutputStream_.Seek(position, SeekOrigin.Begin);
            }
            if ((curEntry.Flags & 8) != 0)
            {
                WriteLeInt(0x8074b50);
                WriteLeInt((int)curEntry.Crc);
                if (curEntry.LocalHeaderRequiresZip64)
                {
                    WriteLeLong(curEntry.CompressedSize);
                    WriteLeLong(curEntry.Size);
                    offset += 0x18L;
                }
                else
                {
                    WriteLeInt((int)curEntry.CompressedSize);
                    WriteLeInt((int)curEntry.Size);
                    offset += 0x10L;
                }
            }
            entries.Add(curEntry);
            curEntry = null;
        }

        private void CopyAndEncrypt(byte[] buffer, int off, int count)
        {
            byte[] destinationArray = new byte[0x1000];
            while (count > 0)
            {
                int length = (count < 0x1000) ? count : 0x1000;
                Array.Copy(buffer, off, destinationArray, 0, length);
                EncryptBlock(destinationArray, 0, length);
                baseOutputStream_.Write(destinationArray, 0, length);
                count -= length;
                off += length;
            }
        }

        public override void Finish()
        {
            if (entries != null)
            {
                if (curEntry != null)
                {
                    CloseEntry();
                }
                long count = entries.Count;
                long sizeEntries = 0L;
                foreach (ZipEntry entry in entries)
                {
                    WriteLeInt(0x2014b50);
                    WriteLeShort(0x33);
                    WriteLeShort(entry.Version);
                    WriteLeShort(entry.Flags);
                    WriteLeShort((short)entry.CompressionMethodForHeader);
                    WriteLeInt((int)entry.DosTime);
                    WriteLeInt((int)entry.Crc);
                    if (entry.IsZip64Forced() || (entry.CompressedSize >= 0xffffffffL))
                    {
                        WriteLeInt(-1);
                    }
                    else
                    {
                        WriteLeInt((int)entry.CompressedSize);
                    }
                    if (entry.IsZip64Forced() || (entry.Size >= 0xffffffffL))
                    {
                        WriteLeInt(-1);
                    }
                    else
                    {
                        WriteLeInt((int)entry.Size);
                    }
                    byte[] buffer = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
                    if (buffer.Length > 0xffff)
                    {
                        throw new ZipException("Name too long.");
                    }
                    ZipExtraData extraData = new ZipExtraData(entry.ExtraData);
                    if (entry.CentralHeaderRequiresZip64)
                    {
                        extraData.StartNewEntry();
                        if (entry.IsZip64Forced() || (entry.Size >= 0xffffffffL))
                        {
                            extraData.AddLeLong(entry.Size);
                        }
                        if (entry.IsZip64Forced() || (entry.CompressedSize >= 0xffffffffL))
                        {
                            extraData.AddLeLong(entry.CompressedSize);
                        }
                        if (entry.Offset >= 0xffffffffL)
                        {
                            extraData.AddLeLong(entry.Offset);
                        }
                        extraData.AddNewEntry(1);
                    }
                    else
                    {
                        extraData.Delete(1);
                    }
                    if (entry.AESKeySize > 0)
                    {
                        AddExtraDataAES(entry, extraData);
                    }
                    byte[] entryData = extraData.GetEntryData();
                    byte[] buffer3 = (entry.Comment != null) ? ZipConstants.ConvertToArray(entry.Flags, entry.Comment) : new byte[0];
                    if (buffer3.Length > 0xffff)
                    {
                        throw new ZipException("Comment too long.");
                    }
                    WriteLeShort(buffer.Length);
                    WriteLeShort(entryData.Length);
                    WriteLeShort(buffer3.Length);
                    WriteLeShort(0);
                    WriteLeShort(0);
                    if (entry.ExternalFileAttributes != -1)
                    {
                        WriteLeInt(entry.ExternalFileAttributes);
                    }
                    else if (entry.IsDirectory)
                    {
                        WriteLeInt(0x10);
                    }
                    else
                    {
                        WriteLeInt(0);
                    }
                    if (entry.Offset >= 0xffffffffL)
                    {
                        WriteLeInt(-1);
                    }
                    else
                    {
                        WriteLeInt((int)entry.Offset);
                    }
                    if (buffer.Length > 0)
                    {
                        baseOutputStream_.Write(buffer, 0, buffer.Length);
                    }
                    if (entryData.Length > 0)
                    {
                        baseOutputStream_.Write(entryData, 0, entryData.Length);
                    }
                    if (buffer3.Length > 0)
                    {
                        baseOutputStream_.Write(buffer3, 0, buffer3.Length);
                    }
                    sizeEntries += ((0x2e + buffer.Length) + entryData.Length) + buffer3.Length;
                }
                using (ZipHelperStream stream = new ZipHelperStream(baseOutputStream_))
                {
                    stream.WriteEndOfCentralDirectory(count, sizeEntries, offset, zipComment);
                }
                entries = null;
            }
        }

        public int GetLevel()
        {
            return deflater_.GetLevel();
        }

        public void PutNextEntry(ZipEntry entry)
        {
            bool hasCrc;
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }
            if (entries == null)
            {
                throw new InvalidOperationException("ZipOutputStream was finished");
            }
            if (curEntry != null)
            {
                CloseEntry();
            }
            if (entries.Count == 0x7fffffff)
            {
                throw new ZipException("Too many entries for Zip file");
            }
            CompressionMethod compressionMethod = entry.CompressionMethod;
            int compressionLevel = defaultCompressionLevel;
            entry.Flags &= 0x800;
            patchEntryHeader = false;
            if (entry.Size == 0L)
            {
                entry.CompressedSize = entry.Size;
                entry.Crc = 0L;
                compressionMethod = CompressionMethod.Stored;
                hasCrc = true;
            }
            else
            {
                hasCrc = (entry.Size >= 0L) && entry.HasCrc;
                if (compressionMethod == CompressionMethod.Stored)
                {
                    if (!hasCrc)
                    {
                        if (!CanPatchEntries)
                        {
                            compressionMethod = CompressionMethod.Deflated;
                            compressionLevel = 0;
                        }
                    }
                    else
                    {
                        entry.CompressedSize = entry.Size;
                        hasCrc = entry.HasCrc;
                    }
                }
            }
            if (!hasCrc)
            {
                if (!CanPatchEntries)
                {
                    entry.Flags |= 8;
                }
                else
                {
                    patchEntryHeader = true;
                }
            }
            if (Password != null)
            {
                entry.IsCrypted = true;
                if (entry.Crc < 0L)
                {
                    entry.Flags |= 8;
                }
            }
            entry.Offset = offset;
            entry.CompressionMethod = compressionMethod;
            curMethod = compressionMethod;
            sizePatchPos = -1L;
            if ((useZip64_ == UseZip64.On) || ((entry.Size < 0L) && (useZip64_ == UseZip64.Dynamic)))
            {
                entry.ForceZip64();
            }
            WriteLeInt(0x4034b50);
            WriteLeShort(entry.Version);
            WriteLeShort(entry.Flags);
            WriteLeShort((byte)entry.CompressionMethodForHeader);
            WriteLeInt((int)entry.DosTime);
            if (hasCrc)
            {
                WriteLeInt((int)entry.Crc);
                if (entry.LocalHeaderRequiresZip64)
                {
                    WriteLeInt(-1);
                    WriteLeInt(-1);
                }
                else
                {
                    WriteLeInt(entry.IsCrypted ? (((int)entry.CompressedSize) + 12) : ((int)entry.CompressedSize));
                    WriteLeInt((int)entry.Size);
                }
            }
            else
            {
                if (patchEntryHeader)
                {
                    crcPatchPos = baseOutputStream_.Position;
                }
                WriteLeInt(0);
                if (patchEntryHeader)
                {
                    sizePatchPos = baseOutputStream_.Position;
                }
                if (entry.LocalHeaderRequiresZip64 || patchEntryHeader)
                {
                    WriteLeInt(-1);
                    WriteLeInt(-1);
                }
                else
                {
                    WriteLeInt(0);
                    WriteLeInt(0);
                }
            }
            byte[] buffer = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
            if (buffer.Length > 0xffff)
            {
                throw new ZipException("Entry name too long.");
            }
            ZipExtraData extraData = new ZipExtraData(entry.ExtraData);
            if (entry.LocalHeaderRequiresZip64)
            {
                extraData.StartNewEntry();
                if (hasCrc)
                {
                    extraData.AddLeLong(entry.Size);
                    extraData.AddLeLong(entry.CompressedSize);
                }
                else
                {
                    extraData.AddLeLong(-1L);
                    extraData.AddLeLong(-1L);
                }
                extraData.AddNewEntry(1);
                if (!extraData.Find(1))
                {
                    throw new ZipException("Internal error cant find extra data");
                }
                if (patchEntryHeader)
                {
                    sizePatchPos = extraData.CurrentReadIndex;
                }
            }
            else
            {
                extraData.Delete(1);
            }
            if (entry.AESKeySize > 0)
            {
                AddExtraDataAES(entry, extraData);
            }
            byte[] entryData = extraData.GetEntryData();
            WriteLeShort(buffer.Length);
            WriteLeShort(entryData.Length);
            if (buffer.Length > 0)
            {
                baseOutputStream_.Write(buffer, 0, buffer.Length);
            }
            if (entry.LocalHeaderRequiresZip64 && patchEntryHeader)
            {
                sizePatchPos += baseOutputStream_.Position;
            }
            if (entryData.Length > 0)
            {
                baseOutputStream_.Write(entryData, 0, entryData.Length);
            }
            offset += (30 + buffer.Length) + entryData.Length;
            if (entry.AESKeySize > 0)
            {
                offset += entry.AESOverheadSize;
            }
            curEntry = entry;
            crc.Reset();
            if (compressionMethod == CompressionMethod.Deflated)
            {
                deflater_.Reset();
                deflater_.SetLevel(compressionLevel);
            }
            size = 0L;
            if (entry.IsCrypted)
            {
                if (entry.AESKeySize > 0)
                {
                    WriteAESHeader(entry);
                }
                else if (entry.Crc < 0L)
                {
                    WriteEncryptionHeader(entry.DosTime << 0x10);
                }
                else
                {
                    WriteEncryptionHeader(entry.Crc);
                }
            }
        }

        public void SetComment(string comment)
        {
            byte[] buffer = ZipConstants.ConvertToArray(comment);
            if (buffer.Length > 0xffff)
            {
                throw new ArgumentOutOfRangeException("comment");
            }
            zipComment = buffer;
        }

        public void SetLevel(int level)
        {
            deflater_.SetLevel(level);
            defaultCompressionLevel = level;
        }

        public override void Write(byte[] buffer, int off, int count)
        {
            if (curEntry == null)
            {
                throw new InvalidOperationException("No open entry.");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (off < 0)
            {
                throw new ArgumentOutOfRangeException("off", "Cannot be negative");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", "Cannot be negative");
            }
            if ((buffer.Length - off) < count)
            {
                throw new ArgumentException("Invalid offset/count combination");
            }
            crc.Update(buffer, off, count);
            size += count;
            switch (curMethod)
            {
                case CompressionMethod.Stored:
                    if (Password != null)
                    {
                        CopyAndEncrypt(buffer, off, count);
                    }
                    else
                    {
                        baseOutputStream_.Write(buffer, off, count);
                    }
                    break;

                case CompressionMethod.Deflated:
                    Write(buffer, off, count);
                    break;
            }
        }

        private void WriteAESHeader(ZipEntry entry)
        {
            byte[] buffer;
            byte[] buffer2;
            InitializeAESPassword(entry, Password, out buffer, out buffer2);
            baseOutputStream_.Write(buffer, 0, buffer.Length);
            baseOutputStream_.Write(buffer2, 0, buffer2.Length);
        }

        private void WriteEncryptionHeader(long crcValue)
        {
            offset += 12L;
            InitializePassword(Password);
            byte[] buffer = new byte[12];
            new Random().NextBytes(buffer);
            buffer[11] = (byte)(crcValue >> 0x18);
            EncryptBlock(buffer, 0, buffer.Length);
            baseOutputStream_.Write(buffer, 0, buffer.Length);
        }

        private void WriteLeInt(int value)
        {
            WriteLeShort(value);
            WriteLeShort(value >> 0x10);
        }

        private void WriteLeLong(long value)
        {
            WriteLeInt((int)value);
            WriteLeInt((int)(value >> 0x20));
        }

        private void WriteLeShort(int value)
        {
            baseOutputStream_.WriteByte((byte)(value & 0xff));
            baseOutputStream_.WriteByte((byte)((value >> 8) & 0xff));
        }

        public bool IsFinished
        {
            get
            {
                return (entries == null);
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
    }
}
