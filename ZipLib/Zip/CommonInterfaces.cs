using System.IO;
using Lte.Domain.Lz4Net.Core;

namespace ZipLib.Zip
{
    public interface IArchiveStorage
    {
        Stream ConvertTemporaryToFinal();
        void Dispose();
        Stream GetTemporaryOutput();
        Stream MakeTemporaryCopy(Stream stream);
        Stream OpenForDirectUpdate(Stream stream);

        FileUpdateMode UpdateMode { get; }
    }

    public interface IDynamicDataSource
    {
        Stream GetSource(ZipEntry entry, string name);
    }

    public interface IEntryFactory
    {
        ZipEntry MakeDirectoryEntry(string directoryName);
        ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem);
        ZipEntry MakeFileEntry(string fileName);
        ZipEntry MakeFileEntry(string fileName, bool useFileSystem);

        INameTransform NameTransform { get; set; }
    }

    public interface IStaticDataSource
    {
        Stream GetSource();
    }

    public interface ITaggedData
    {
        byte[] GetData();
        void SetData(byte[] data, int offset, int count);

        short TagID { get; }
    }

    internal interface ITaggedDataFactory
    {
        ITaggedData Create(short tag, byte[] data, int offset, int count);
    }
}

