using System;
using System.IO;
using Lte.Domain.Lz4Net.Core;

namespace ZipLib.Zip
{
    public class ZipEntryFactory : IEntryFactory
    {
        private DateTime _fixedDateTime;
        private int _getAttributes;
        private bool _isUnicodeText;
        private INameTransform _nameTransform;
        private int _setAttributes;
        private TimeSetting _timeSetting;

        public ZipEntryFactory()
        {
            _fixedDateTime = DateTime.Now;
            _getAttributes = -1;
            _nameTransform = new ZipNameTransform();
        }

        public ZipEntryFactory(TimeSetting timeSetting)
        {
            _fixedDateTime = DateTime.Now;
            _getAttributes = -1;
            _timeSetting = timeSetting;
            _nameTransform = new ZipNameTransform();
        }

        public ZipEntryFactory(DateTime time)
        {
            _fixedDateTime = DateTime.Now;
            _getAttributes = -1;
            _timeSetting = TimeSetting.Fixed;
            FixedDateTime = time;
            _nameTransform = new ZipNameTransform();
        }

        public ZipEntry MakeDirectoryEntry(string directoryName)
        {
            return MakeDirectoryEntry(directoryName, true);
        }

        public ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem)
        {
            ZipEntry entry = new ZipEntry(_nameTransform.TransformDirectory(directoryName))
            {
                IsUnicodeText = _isUnicodeText,
                Size = 0L
            };
            int num = 0;
            DirectoryInfo info = null;
            if (useFileSystem)
            {
                info = new DirectoryInfo(directoryName);
            }
            if ((info == null) || !info.Exists)
            {
                if (_timeSetting == TimeSetting.Fixed)
                {
                    entry.DateTime = _fixedDateTime;
                }
            }
            else
            {
                switch (_timeSetting)
                {
                    case TimeSetting.LastWriteTime:
                        entry.DateTime = info.LastWriteTime;
                        break;

                    case TimeSetting.LastWriteTimeUtc:
                        entry.DateTime = info.LastWriteTimeUtc;
                        break;

                    case TimeSetting.CreateTime:
                        entry.DateTime = info.CreationTime;
                        break;

                    case TimeSetting.CreateTimeUtc:
                        entry.DateTime = info.CreationTimeUtc;
                        break;

                    case TimeSetting.LastAccessTime:
                        entry.DateTime = info.LastAccessTime;
                        break;

                    case TimeSetting.LastAccessTimeUtc:
                        entry.DateTime = info.LastAccessTimeUtc;
                        break;

                    case TimeSetting.Fixed:
                        entry.DateTime = _fixedDateTime;
                        break;

                    default:
                        throw new ZipException("Unhandled time setting in MakeDirectoryEntry");
                }
                num = ((int)info.Attributes) & _getAttributes;
            }
            num |= _setAttributes | 0x10;
            entry.ExternalFileAttributes = num;
            return entry;
        }

        public ZipEntry MakeFileEntry(string fileName)
        {
            return MakeFileEntry(fileName, true);
        }

        public ZipEntry MakeFileEntry(string fileName, bool useFileSystem)
        {
            ZipEntry entry = new ZipEntry(_nameTransform.TransformFile(fileName))
            {
                IsUnicodeText = _isUnicodeText
            };
            int num = 0;
            bool flag = _setAttributes != 0;
            FileInfo info = null;
            if (useFileSystem)
            {
                info = new FileInfo(fileName);
            }
            if ((info == null) || !info.Exists)
            {
                if (_timeSetting == TimeSetting.Fixed)
                {
                    entry.DateTime = _fixedDateTime;
                }
            }
            else
            {
                switch (_timeSetting)
                {
                    case TimeSetting.LastWriteTime:
                        entry.DateTime = info.LastWriteTime;
                        break;

                    case TimeSetting.LastWriteTimeUtc:
                        entry.DateTime = info.LastWriteTimeUtc;
                        break;

                    case TimeSetting.CreateTime:
                        entry.DateTime = info.CreationTime;
                        break;

                    case TimeSetting.CreateTimeUtc:
                        entry.DateTime = info.CreationTimeUtc;
                        break;

                    case TimeSetting.LastAccessTime:
                        entry.DateTime = info.LastAccessTime;
                        break;

                    case TimeSetting.LastAccessTimeUtc:
                        entry.DateTime = info.LastAccessTimeUtc;
                        break;

                    case TimeSetting.Fixed:
                        entry.DateTime = _fixedDateTime;
                        break;

                    default:
                        throw new ZipException("Unhandled time setting in MakeFileEntry");
                }
                entry.Size = info.Length;
                flag = true;
                num = ((int)info.Attributes) & _getAttributes;
            }
            if (flag)
            {
                num |= _setAttributes;
                entry.ExternalFileAttributes = num;
            }
            return entry;
        }

        public DateTime FixedDateTime
        {
            get
            {
                return _fixedDateTime;
            }
            set
            {
                if (value.Year < 0x7b2)
                {
                    throw new ArgumentException("Value is too old to be valid", "value");
                }
                _fixedDateTime = value;
            }
        }

        public int GetAttributes
        {
            get
            {
                return _getAttributes;
            }
            set
            {
                _getAttributes = value;
            }
        }

        public bool IsUnicodeText
        {
            get
            {
                return _isUnicodeText;
            }
            set
            {
                _isUnicodeText = value;
            }
        }

        public INameTransform NameTransform
        {
            get
            {
                return _nameTransform;
            }
            set
            {
                if (value == null)
                {
                    _nameTransform = new ZipNameTransform();
                }
                else
                {
                    _nameTransform = value;
                }
            }
        }

        public int SetAttributes
        {
            get
            {
                return _setAttributes;
            }
            set
            {
                _setAttributes = value;
            }
        }

        public TimeSetting Setting
        {
            get
            {
                return _timeSetting;
            }
            set
            {
                _timeSetting = value;
            }
        }

        public enum TimeSetting
        {
            LastWriteTime,
            LastWriteTimeUtc,
            CreateTime,
            CreateTimeUtc,
            LastAccessTime,
            LastAccessTimeUtc,
            Fixed
        }
    }
}
