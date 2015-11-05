using System;
using System.IO;
using Lte.Domain.Lz4Net.Core;

namespace Lte.Domain.ZipLib.Zip
{
    public class ZipEntryFactory : IEntryFactory
    {
        private DateTime fixedDateTime_;
        private int getAttributes_;
        private bool isUnicodeText_;
        private INameTransform nameTransform_;
        private int setAttributes_;
        private TimeSetting timeSetting_;

        public ZipEntryFactory()
        {
            fixedDateTime_ = DateTime.Now;
            getAttributes_ = -1;
            nameTransform_ = new ZipNameTransform();
        }

        public ZipEntryFactory(TimeSetting timeSetting)
        {
            fixedDateTime_ = DateTime.Now;
            getAttributes_ = -1;
            timeSetting_ = timeSetting;
            nameTransform_ = new ZipNameTransform();
        }

        public ZipEntryFactory(DateTime time)
        {
            fixedDateTime_ = DateTime.Now;
            getAttributes_ = -1;
            timeSetting_ = TimeSetting.Fixed;
            FixedDateTime = time;
            nameTransform_ = new ZipNameTransform();
        }

        public ZipEntry MakeDirectoryEntry(string directoryName)
        {
            return MakeDirectoryEntry(directoryName, true);
        }

        public ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem)
        {
            ZipEntry entry = new ZipEntry(nameTransform_.TransformDirectory(directoryName))
            {
                IsUnicodeText = isUnicodeText_,
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
                if (timeSetting_ == TimeSetting.Fixed)
                {
                    entry.DateTime = fixedDateTime_;
                }
            }
            else
            {
                switch (timeSetting_)
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
                        entry.DateTime = fixedDateTime_;
                        break;

                    default:
                        throw new ZipException("Unhandled time setting in MakeDirectoryEntry");
                }
                num = ((int)info.Attributes) & getAttributes_;
            }
            num |= setAttributes_ | 0x10;
            entry.ExternalFileAttributes = num;
            return entry;
        }

        public ZipEntry MakeFileEntry(string fileName)
        {
            return MakeFileEntry(fileName, true);
        }

        public ZipEntry MakeFileEntry(string fileName, bool useFileSystem)
        {
            ZipEntry entry = new ZipEntry(nameTransform_.TransformFile(fileName))
            {
                IsUnicodeText = isUnicodeText_
            };
            int num = 0;
            bool flag = setAttributes_ != 0;
            FileInfo info = null;
            if (useFileSystem)
            {
                info = new FileInfo(fileName);
            }
            if ((info == null) || !info.Exists)
            {
                if (timeSetting_ == TimeSetting.Fixed)
                {
                    entry.DateTime = fixedDateTime_;
                }
            }
            else
            {
                switch (timeSetting_)
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
                        entry.DateTime = fixedDateTime_;
                        break;

                    default:
                        throw new ZipException("Unhandled time setting in MakeFileEntry");
                }
                entry.Size = info.Length;
                flag = true;
                num = ((int)info.Attributes) & getAttributes_;
            }
            if (flag)
            {
                num |= setAttributes_;
                entry.ExternalFileAttributes = num;
            }
            return entry;
        }

        public DateTime FixedDateTime
        {
            get
            {
                return fixedDateTime_;
            }
            set
            {
                if (value.Year < 0x7b2)
                {
                    throw new ArgumentException("Value is too old to be valid", "value");
                }
                fixedDateTime_ = value;
            }
        }

        public int GetAttributes
        {
            get
            {
                return getAttributes_;
            }
            set
            {
                getAttributes_ = value;
            }
        }

        public bool IsUnicodeText
        {
            get
            {
                return isUnicodeText_;
            }
            set
            {
                isUnicodeText_ = value;
            }
        }

        public INameTransform NameTransform
        {
            get
            {
                return nameTransform_;
            }
            set
            {
                if (value == null)
                {
                    nameTransform_ = new ZipNameTransform();
                }
                else
                {
                    nameTransform_ = value;
                }
            }
        }

        public int SetAttributes
        {
            get
            {
                return setAttributes_;
            }
            set
            {
                setAttributes_ = value;
            }
        }

        public TimeSetting Setting
        {
            get
            {
                return timeSetting_;
            }
            set
            {
                timeSetting_ = value;
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
