using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Lte.Domain.Lz4Net.ExtraZip
{
    public class Lz4Package
    {
        private List<Lz4PackageEntry> _entries;
        private Dictionary<string, Lz4PackageEntry> _map;

        public Lz4Package(string filename)
        {
            Filename = filename;
            InfoFile = filename + ".nfo";
        }

        public Stream AddEntry(string name, Lz4Mode mode, int blockSize)
        {
            return AddEntry(name, DateTime.Now, DateTime.Now, mode, blockSize);
        }

        private void AddEntry(Stream stream, Lz4Mode mode, int blockSize, Lz4PackageEntry entry)
        {
            using (FileStream stream2 = OpenWrite(blockSize))
            {
                entry.Entry = stream2.Position;
                using (Lz4CompressionStream stream3 = new Lz4CompressionStream(stream2, blockSize, mode))
                {
                    int num;
                    byte[] buffer = new byte[blockSize];
                    while ((num = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        entry.OriginSize += num;
                        stream3.Write(buffer, 0, num);
                    }
                }
                entry.CompressedSize = stream2.Position - entry.Entry;
            }
        }

        public Lz4PackageEntry AddEntry(string name, Stream stream, Lz4Mode mode = 0, int blockSize = 0x100000)
        {
            return AddEntry(name, stream, DateTime.Now, DateTime.Now, mode, blockSize);
        }

        public Lz4PackageEntry AddEntry(string name, string originFile, Lz4Mode mode = 0, int blockSize = 0x100000)
        {
            FileStream stream = new FileStream(originFile, FileMode.Open, FileAccess.Read, FileShare.Read, blockSize);
            FileInfo info = new FileInfo(originFile);
            Lz4PackageEntry entry = new Lz4PackageEntry
            {
                Filename = name,
                Package = this,
                EntryCreateTime = DateTime.Now.ToString("o"),
                OriginSize = info.Length,
                CreateTime = info.CreationTime.ToString("o"),
                LastModifyTime = info.LastWriteTime.ToString("o")
            };
            AddEntry(stream, mode, blockSize, entry);
            if (_entries != null)
            {
                _entries.Add(entry);
            }
            if (_map != null)
            {
                _map[entry.Filename] = entry;
            }
            Save(entry);
            return entry;
        }

        public Stream AddEntry(string name, DateTime createTime, DateTime lastModify, Lz4Mode mode, int blockSize)
        {
            Lz4PackageEntry item = new Lz4PackageEntry
            {
                Filename = name,
                Package = this,
                EntryCreateTime = DateTime.Now.ToString("o"),
                CreateTime = createTime.ToString("o"),
                LastModifyTime = lastModify.ToString("o")
            };
            if (_entries != null)
            {
                _entries.Add(item);
            }
            if (_map != null)
            {
                _map[item.Filename] = item;
            }
            FileStream stream = OpenWrite(blockSize);
            item.Entry = stream.Position;
            return new LzEntryOutputStream(stream, item, blockSize, mode);
        }

        public Lz4PackageEntry AddEntry(string name, Stream stream, DateTime createTime, DateTime lastModify, 
            Lz4Mode mode = 0, int blockSize = 0x100000)
        {
            Lz4PackageEntry entry = new Lz4PackageEntry
            {
                Filename = name,
                Package = this,
                EntryCreateTime = DateTime.Now.ToString("o"),
                CreateTime = createTime.ToString("o"),
                LastModifyTime = lastModify.ToString("o")
            };
            AddEntry(stream, mode, blockSize, entry);
            if (_entries != null)
            {
                _entries.Add(entry);
            }
            if (_map != null)
            {
                _map[entry.Filename] = entry;
            }
            Save(entry);
            return entry;
        }

        public Lz4PackageEntry Entry(string name)
        {
            return Map[name];
        }

        private List<Lz4PackageEntry> Load()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Lz4PackageEntry));
            List<Lz4PackageEntry> list = new List<Lz4PackageEntry>();
            foreach (string str in File.ReadAllLines(InfoFile, Encoding.UTF8))
            {
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(str)))
                {
                    Lz4PackageEntry item = (Lz4PackageEntry)serializer.ReadObject(stream);
                    item.Package = this;
                    list.Add(item);
                }
            }
            return list;
        }

        private FileStream OpenWrite(int blockSize)
        {
            FileStream stream = new FileStream(Filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, blockSize);
            stream.Seek(0L, SeekOrigin.End);
            return stream;
        }

        internal void Save(Lz4PackageEntry entry)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Lz4PackageEntry));
            using (FileStream stream = new FileStream(InfoFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                stream.Seek(0L, SeekOrigin.End);
                serializer.WriteObject(stream, entry);
                stream.WriteByte(13);
                stream.WriteByte(10);
            }
        }

        public override string ToString()
        {
            return Filename;
        }

        public ReadOnlyCollection<Lz4PackageEntry> AllEntries
        {
            get
            {
                if (_entries == null)
                {
                    _entries = File.Exists(InfoFile) ? Load() : new List<Lz4PackageEntry>();
                }
                return _entries.AsReadOnly();
            }
        }

        public ICollection<Lz4PackageEntry> Entries
        {
            get
            {
                return Map.Values;
            }
        }

        public string Filename { get; private set; }

        public string InfoFile { get; private set; }

        protected Dictionary<string, Lz4PackageEntry> Map
        {
            get
            {
                if (_map == null)
                {
                    _map = new Dictionary<string, Lz4PackageEntry>(StringComparer.OrdinalIgnoreCase);
                    foreach (Lz4PackageEntry entry in AllEntries)
                    {
                        _map[entry.Filename] = entry;
                    }
                }
                return _map;
            }
        }
    }
}

