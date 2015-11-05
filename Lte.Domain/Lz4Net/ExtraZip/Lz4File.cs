using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using Lte.Domain.Common;

namespace Lte.Domain.Lz4Net.ExtraZip
{
    public class Lz4File
    {
        public const int DefaultBlockSize = 0x100000;

        public static CompressResult Compress([NotNull] Stream inputFile, [NotNull] Stream compressFile = null, 
            int blockSize = 0x100000, Lz4Mode mode = 0, Action<CompressResult> blockCallback = null)
        {
            long position;
            if (inputFile == null)
            {
                throw new ArgumentNullException("inputFile");
            }
            if (compressFile == null)
            {
                throw new ArgumentNullException("compressFile");
            }
            CompressResult result = new CompressResult();
            byte[] buffer = new byte[blockSize];
            try
            {
                position = compressFile.Position;
            }
            catch (Exception)
            {
                position = -1L;
                result.CompressedBytes = -1L;
            }
            using (Lz4CompressionStream stream = new Lz4CompressionStream(compressFile, 0x100000, mode))
            {
                goto Label_00E9;
            Label_006D:
                int num2 = inputFile.Read(buffer, 0, buffer.Length);
                if (num2 == 0)
                {
                    return result;
                }
                result.Bytes += num2;
                stream.Write(buffer, 0, num2);
                if (position != -1L)
                {
                    long num3 = compressFile.Position;
                    result.CompressedBytes += num3 - position;
                    position = num3;
                }
                if (blockCallback != null)
                {
                    blockCallback(result);
                }
            Label_00E9:
                goto Label_006D;
            }
        }

        public static CompressResult Compress([NotNull] string inputFile, [CanBeNull] string compressFile = null, int blockSize = 0x100000, Lz4Mode mode = 0, Action<CompressResult> blockCallback = null)
        {
            CompressResult result;
            if (inputFile == null)
            {
                throw new ArgumentNullException("inputFile");
            }
            compressFile = compressFile ?? GetCompressFilename(inputFile);
            using (Stream stream = OpenUncompressedStream(inputFile))
            {
                using (FileStream stream2 = new FileStream(compressFile, FileMode.Create, FileAccess.Write, FileShare.None, 0x100000))
                {
                    stream2.SetLength(0L);
                    result = Compress(stream, stream2, blockSize, mode, blockCallback);
                }
            }
            return result;
        }

        public static CompressResult Decompress([NotNull] Stream compressedFile, [NotNull] Stream outputFile, int bufferSize = 0x100000, Action<CompressResult> blockCallback = null)
        {
            long position;
            if (compressedFile == null)
            {
                throw new ArgumentNullException("compressedFile");
            }
            if (outputFile == null)
            {
                throw new ArgumentNullException("outputFile");
            }
            byte[] buffer = new byte[bufferSize];
            CompressResult result = new CompressResult();
            try
            {
                position = compressedFile.Position;
            }
            catch (Exception)
            {
                position = -1L;
                result.Bytes = -1L;
            }
            using (Lz4DecompressionStream stream = new Lz4DecompressionStream(compressedFile))
            {
                int num2;
                while ((num2 = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputFile.Write(buffer, 0, num2);
                    result.CompressedBytes += num2;
                    if (position != -1L)
                    {
                        long num3 = compressedFile.Position;
                        result.Bytes += num3 - position;
                        position = num3;
                    }
                    if (blockCallback != null)
                    {
                        blockCallback(result);
                    }
                }
            }
            return result;
        }

        public static CompressResult Decompress([NotNull] string compressFile, string outputFile = null, int bufferSize = 0x100000, Action<CompressResult> blockCallback = null)
        {
            CompressResult result;
            if (compressFile == null)
            {
                throw new ArgumentNullException("compressFile");
            }
            outputFile = outputFile ?? Path.ChangeExtension(compressFile, null);
            using (FileStream stream = new FileStream(compressFile, FileMode.Open, FileAccess.Read, FileShare.Read, 0x100000))
            {
                using (FileStream stream2 = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 0x100000))
                {
                    stream2.SetLength(0L);
                    result = Decompress(stream, stream2, bufferSize, blockCallback);
                }
            }
            return result;
        }

        private static string GetCompressFilename([NotNull] string inputFile)
        {
            if (inputFile.EndsWith(".gz", StringComparison.OrdinalIgnoreCase))
            {
                return (inputFile.Substring(0, inputFile.Length - 3) + ".lz4");
            }
            return (inputFile + ".lz4");
        }

        public static Lz4DecompressionStream OpenRead(string filename, int bufferSize = 0x100000)
        {
            return new Lz4DecompressionStream(new FileStream(filename, FileMode.Open, FileAccess.Read, 
                FileShare.Read, bufferSize), true);
        }

        private static Stream OpenUncompressedStream(string filename)
        {
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, 0x100000);
            if (".gz".Equals(Path.GetExtension(filename), StringComparison.OrdinalIgnoreCase))
            {
                stream = new GZipStream(stream, CompressionMode.Decompress);
            }
            return stream;
        }

        public static Lz4CompressionStream OpenWrite(string filename, int blockSize = 0x100000, Lz4Mode mode = 0)
        {
            return new Lz4CompressionStream(new FileStream(filename, FileMode.Create, FileAccess.Write, 
                FileShare.None, blockSize), blockSize, mode, true);
        }

        public class CompressResult
        {
            public long Bytes { get; internal set; }

            public long CompressedBytes { get; internal set; }
        }
    }
}

