using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Lte.Domain.Common;

namespace TraceParser.Eric
{
    public class EricssTrace
    {
        public static void EricParse(string outPath, string binPath)
        {
            ParseEricFile(outPath, binPath, true, true);
        }

        public static void EricParse(string outPath, string binPath, bool isGz)
        {
            ParseEricFile(outPath, binPath, isGz, true);
        }

        public static List<IEric> EricParse(string outPath, string binPath, bool isGz, bool isParseAsn)
        {
            return ParseEricFile(outPath, binPath, isGz, isParseAsn);
        }

        public static List<IEric> ParseEricFile(string outPath, string binPath, bool isGz, bool isParseAsn, 
            uint[] parTraces = null)
        {
            Stream stream;
            if (!string.IsNullOrEmpty(outPath))
            {
                new StringBuilder();
            }
            Hashtable ericPmEvents = EricPmEvent.GetEricPmEvents();
            if (isGz)
            {
                stream = UnzipToMemoryStream(binPath);
            }
            else
            {
                stream = new FileStream(binPath, FileMode.Open, FileAccess.Read);
            }
            stream.Position = 0L;
            BigEndianBinaryReader contentStream = new BigEndianBinaryReader(stream);
            List<IEric> list = new List<IEric>();
            while (!contentStream.Eof())
            {
                uint recordLength = contentStream.ReadUInt16();
                uint recordType = contentStream.ReadUInt16();
                IEric item = null;
                switch (((EricHeadEnum)recordType))
                {
                    case EricHeadEnum.EricHeader:
                        item = new EricHeader(recordLength, recordType, contentStream);
                        break;

                    case EricHeadEnum.EricTcpStream:
                        item = new EricTcpStream(recordLength, recordType, contentStream);
                        break;

                    case EricHeadEnum.EricUdpStream:
                        item = new EricUdpStream(recordLength, recordType, contentStream);
                        break;

                    case EricHeadEnum.EricScanner:
                        item = new EricScanner(recordLength, recordType, contentStream);
                        break;

                    case EricHeadEnum.EricEvent:
                        item = new EricEvent(recordLength, recordType, contentStream, ericPmEvents, false);
                        break;

                    case EricHeadEnum.EricFooter:
                        item = new EricFooter(recordLength, recordType, contentStream);
                        break;
                }
                if ((parTraces != null) && (item is EricEvent))
                {
                    if (parTraces.Contains(((EricEvent)item).EventId))
                    {
                        list.Add(item);
                    }
                    if (isParseAsn)
                    {
                        ((EricEvent)item).ParseAsn();
                    }
                }
                else
                {
                    if (isParseAsn && (item is EricEvent))
                    {
                        ((EricEvent)item).ParseAsn();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public static MemoryStream UnzipToMemoryStream(string gzfile)
        {
            MemoryStream destination = new MemoryStream();
            using (GZipStream stream2 = new GZipStream(new FileStream(gzfile, FileMode.Open, FileAccess.Read, FileShare.Read), CompressionMode.Decompress))
            {
                stream2.CopyTo(destination);
            }
            return destination;
        }
    }
}
