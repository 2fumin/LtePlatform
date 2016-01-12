using System;
using System.Collections;
using System.IO;
using ZipLib.CheckSums;
using ZipLib.Comppression;
using ZipLib.Streams;

namespace ZipLib.Zip
{
    public class ZipOutputStream : DeflaterOutputStream
    {
        private readonly Crc32 _crc;
        private long _crcPatchPos;
        private ZipEntry _curEntry;
        private CompressionMethod _curMethod;
        private int _defaultCompressionLevel;
        private ArrayList _entries;
        private long _offset;
        private bool _patchEntryHeader;
        private long _size;
        private long _sizePatchPos;
        private UseZip64 _useZip64;
        private byte[] _zipComment;

        public ZipOutputStream(Stream baseOutputStream)
            : base(baseOutputStream, new Deflater(-1, true))
        {
            _entries = new ArrayList();
            _crc = new Crc32();
            _defaultCompressionLevel = -1;
            _curMethod = CompressionMethod.Deflated;
            _zipComment = new byte[0];
            _crcPatchPos = -1L;
            _sizePatchPos = -1L;
            _useZip64 = UseZip64.Dynamic;
        }

        public ZipOutputStream(Stream baseOutputStream, int bufferSize)
            : base(baseOutputStream, new Deflater(-1, true), bufferSize)
        {
            _entries = new ArrayList();
            _crc = new Crc32();
            _defaultCompressionLevel = -1;
            _curMethod = CompressionMethod.Deflated;
            _zipComment = new byte[0];
            _crcPatchPos = -1L;
            _sizePatchPos = -1L;
            _useZip64 = UseZip64.Dynamic;
        }

        private static void AddExtraDataAes(ZipEntry entry, ZipExtraData extraData)
        {
            extraData.StartNewEntry();
            extraData.AddLeShort(2);
            extraData.AddLeShort(0x4541);
            extraData.AddData(entry.AesEncryptionStrength);
            extraData.AddLeShort((int)entry.CompressionMethod);
            extraData.AddNewEntry(0x9901);
        }

        public void CloseEntry()
        {
            if (_curEntry == null)
            {
                throw new InvalidOperationException("No open entry");
            }
            long compressedSize = _size;
            if (_curMethod == CompressionMethod.Deflated)
            {
                if (_size >= 0L)
                {
                    Finish();
                    compressedSize = Deflater.TotalOut;
                }
                else
                {
                    Deflater.Reset();
                }
            }
            if (_curEntry.AesKeySize > 0)
            {
                BaseOutputStream.Write(AesAuthCode, 0, 10);
            }
            if (_curEntry.Size < 0L)
            {
                _curEntry.Size = _size;
            }
            else if (_curEntry.Size != _size)
            {
                throw new ZipException(string.Concat("size was ", _size, ", but I expected ", _curEntry.Size));
            }
            if (_curEntry.CompressedSize < 0L)
            {
                _curEntry.CompressedSize = compressedSize;
            }
            else if (_curEntry.CompressedSize != compressedSize)
            {
                throw new ZipException(string.Concat("compressed size was ", compressedSize, ", but I expected ", _curEntry.CompressedSize));
            }
            if (_curEntry.Crc < 0L)
            {
                _curEntry.Crc = _crc.Value;
            }
            else if (_curEntry.Crc != _crc.Value)
            {
                throw new ZipException(string.Concat("crc was ", _crc.Value, ", but I expected ", _curEntry.Crc));
            }
            _offset += compressedSize;
            if (_curEntry.IsCrypted)
            {
                if (_curEntry.AesKeySize > 0)
                {
                    _curEntry.CompressedSize += _curEntry.AesOverheadSize;
                }
                else
                {
                    _curEntry.CompressedSize += 12L;
                }
            }
            if (_patchEntryHeader)
            {
                _patchEntryHeader = false;
                long position = BaseOutputStream.Position;
                BaseOutputStream.Seek(_crcPatchPos, SeekOrigin.Begin);
                WriteLeInt((int)_curEntry.Crc);
                if (_curEntry.LocalHeaderRequiresZip64)
                {
                    if (_sizePatchPos == -1L)
                    {
                        throw new ZipException("Entry requires zip64 but this has been turned off");
                    }
                    BaseOutputStream.Seek(_sizePatchPos, SeekOrigin.Begin);
                    WriteLeLong(_curEntry.Size);
                    WriteLeLong(_curEntry.CompressedSize);
                }
                else
                {
                    WriteLeInt((int)_curEntry.CompressedSize);
                    WriteLeInt((int)_curEntry.Size);
                }
                BaseOutputStream.Seek(position, SeekOrigin.Begin);
            }
            if ((_curEntry.Flags & 8) != 0)
            {
                WriteLeInt(ZipConstants.DataDescriptorSignature);
                WriteLeInt((int)_curEntry.Crc);
                if (_curEntry.LocalHeaderRequiresZip64)
                {
                    WriteLeLong(_curEntry.CompressedSize);
                    WriteLeLong(_curEntry.Size);
                    _offset += 0x18L;
                }
                else
                {
                    WriteLeInt((int)_curEntry.CompressedSize);
                    WriteLeInt((int)_curEntry.Size);
                    _offset += 0x10L;
                }
            }
            _entries.Add(_curEntry);
            _curEntry = null;
        }

        private void CopyAndEncrypt(byte[] buffer, int off, int count)
        {
            byte[] destinationArray = new byte[0x1000];
            while (count > 0)
            {
                int length = (count < 0x1000) ? count : 0x1000;
                Array.Copy(buffer, off, destinationArray, 0, length);
                EncryptBlock(destinationArray, 0, length);
                BaseOutputStream.Write(destinationArray, 0, length);
                count -= length;
                off += length;
            }
        }

        public override void Finish()
        {
            if (_entries != null)
            {
                if (_curEntry != null)
                {
                    CloseEntry();
                }
                long count = _entries.Count;
                long sizeEntries = 0L;
                foreach (ZipEntry entry in _entries)
                {
                    WriteLeInt(ZipConstants.CentralHeaderSignature);
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
                    if (entry.AesKeySize > 0)
                    {
                        AddExtraDataAes(entry, extraData);
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
                        BaseOutputStream.Write(buffer, 0, buffer.Length);
                    }
                    if (entryData.Length > 0)
                    {
                        BaseOutputStream.Write(entryData, 0, entryData.Length);
                    }
                    if (buffer3.Length > 0)
                    {
                        BaseOutputStream.Write(buffer3, 0, buffer3.Length);
                    }
                    sizeEntries += ((ZipConstants.CentralHeaderBaseSize + buffer.Length) + entryData.Length) + buffer3.Length;
                }
                using (ZipHelperStream stream = new ZipHelperStream(BaseOutputStream))
                {
                    stream.WriteEndOfCentralDirectory(count, sizeEntries, _offset, _zipComment);
                }
                _entries = null;
            }
        }

        public int GetLevel()
        {
            return Deflater.GetLevel();
        }

        public void PutNextEntry(ZipEntry entry)
        {
            bool hasCrc;
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            if (_entries == null)
            {
                throw new InvalidOperationException("ZipOutputStream was finished");
            }
            if (_curEntry != null)
            {
                CloseEntry();
            }
            if (_entries.Count == 0x7fffffff)
            {
                throw new ZipException("Too many entries for Zip file");
            }
            CompressionMethod compressionMethod = entry.CompressionMethod;
            int compressionLevel = _defaultCompressionLevel;
            entry.Flags &= 0x800;
            _patchEntryHeader = false;
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
                    _patchEntryHeader = true;
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
            entry.Offset = _offset;
            entry.CompressionMethod = compressionMethod;
            _curMethod = compressionMethod;
            _sizePatchPos = -1L;
            if ((_useZip64 == UseZip64.On) || ((entry.Size < 0L) && (_useZip64 == UseZip64.Dynamic)))
            {
                entry.ForceZip64();
            }
            WriteLeInt(ZipConstants.LocalHeaderSignature);
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
                if (_patchEntryHeader)
                {
                    _crcPatchPos = BaseOutputStream.Position;
                }
                WriteLeInt(0);
                if (_patchEntryHeader)
                {
                    _sizePatchPos = BaseOutputStream.Position;
                }
                if (entry.LocalHeaderRequiresZip64 || _patchEntryHeader)
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
                if (_patchEntryHeader)
                {
                    _sizePatchPos = extraData.CurrentReadIndex;
                }
            }
            else
            {
                extraData.Delete(1);
            }
            if (entry.AesKeySize > 0)
            {
                AddExtraDataAes(entry, extraData);
            }
            byte[] entryData = extraData.GetEntryData();
            WriteLeShort(buffer.Length);
            WriteLeShort(entryData.Length);
            if (buffer.Length > 0)
            {
                BaseOutputStream.Write(buffer, 0, buffer.Length);
            }
            if (entry.LocalHeaderRequiresZip64 && _patchEntryHeader)
            {
                _sizePatchPos += BaseOutputStream.Position;
            }
            if (entryData.Length > 0)
            {
                BaseOutputStream.Write(entryData, 0, entryData.Length);
            }
            _offset += (30 + buffer.Length) + entryData.Length;
            if (entry.AesKeySize > 0)
            {
                _offset += entry.AesOverheadSize;
            }
            _curEntry = entry;
            _crc.Reset();
            if (compressionMethod == CompressionMethod.Deflated)
            {
                Deflater.Reset();
                Deflater.SetLevel(compressionLevel);
            }
            _size = 0L;
            if (entry.IsCrypted)
            {
                if (entry.AesKeySize > 0)
                {
                    WriteAesHeader(entry);
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
                throw new ArgumentOutOfRangeException(nameof(comment));
            }
            _zipComment = buffer;
        }

        public void SetLevel(int level)
        {
            Deflater.SetLevel(level);
            _defaultCompressionLevel = level;
        }

        public override void Write(byte[] buffer, int off, int count)
        {
            if (_curEntry == null)
            {
                throw new InvalidOperationException("No open entry.");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (off < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(off), "Cannot be negative");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            }
            if ((buffer.Length - off) < count)
            {
                throw new ArgumentException("Invalid offset/count combination");
            }
            _crc.Update(buffer, off, count);
            _size += count;
            switch (_curMethod)
            {
                case CompressionMethod.Stored:
                    if (Password != null)
                    {
                        CopyAndEncrypt(buffer, off, count);
                    }
                    else
                    {
                        BaseOutputStream.Write(buffer, off, count);
                    }
                    break;

                case CompressionMethod.Deflated:
                    Write(buffer, off, count);
                    break;
            }
        }

        private void WriteAesHeader(ZipEntry entry)
        {
            byte[] buffer;
            byte[] buffer2;
            InitializeAesPassword(entry, Password, out buffer, out buffer2);
            BaseOutputStream.Write(buffer, 0, buffer.Length);
            BaseOutputStream.Write(buffer2, 0, buffer2.Length);
        }

        private void WriteEncryptionHeader(long crcValue)
        {
            _offset += 12L;
            InitializePassword(Password);
            byte[] buffer = new byte[12];
            new Random().NextBytes(buffer);
            buffer[11] = (byte)(crcValue >> 0x18);
            EncryptBlock(buffer, 0, buffer.Length);
            BaseOutputStream.Write(buffer, 0, buffer.Length);
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
            BaseOutputStream.WriteByte((byte)(value & 0xff));
            BaseOutputStream.WriteByte((byte)((value >> 8) & 0xff));
        }

        public bool IsFinished => (_entries == null);

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
    }
}
