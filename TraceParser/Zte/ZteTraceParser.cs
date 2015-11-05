using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Lte.Domain.ZipLib.Zip;
using TraceParser.Common;

namespace TraceParser.Zte
{
    public class ZteTraceParser
    {
        private static List<ZteEvent> listZteEvents = ZteEvent.GetZtePmEvents();
        private static XmlSerializer serializer = new XmlSerializer(typeof(ZteTraceCollecFile));

        private static ZteEvent GetAsnParseClass(ZteTraceRecSession traceCollecFile)
        {
            string function = traceCollecFile.msg.function;
            string name = traceCollecFile.msg.name;
            ZteEvent event2 = listZteEvents.FirstOrDefault(p => p.EventType.Equals(function) && p.EventName.Equals(name));
            try
            {
                if (event2 != null)
                {
                    return event2;
                }
                if (!function.Equals("X2AP") && !function.Equals("S1AP"))
                {
                    return null;
                }
                Func<ZteEvent, bool> predicate = p => p.EventType.Equals(function);
                return listZteEvents.FirstOrDefault(predicate);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return event2;
        }

        public static ZteTraceCollecFile ParseRaw(string zipPath)
        {
            MemoryStream stream = UnzipToMemoryStream(zipPath);
            return ParseRaw(stream);
        }

        public static ZteTraceCollecFile ParseRaw(MemoryStream uzip)
        {
            uzip.Position = 0L;
            return serializer.Deserialize(uzip) as ZteTraceCollecFile;
        }

        public static TraceResultList Parse(List<ZteTraceRecSession> sessions, bool isParseAsn)
        {
            TraceResultList list = new TraceResultList();
            foreach (ZteTraceRecSession session in sessions)
            {
                if (session != null)
                {
                    ZteEvent asnParseClass = GetAsnParseClass(session);
                    if (asnParseClass == null)
                    {
                        Console.WriteLine("{0}:{1}", session.msg.function, session.msg.name);
                    }
                    else
                    {
                        try
                        {
                            session.AsnParseClass = asnParseClass.MsgDepend;
                            if (isParseAsn)
                            {
                                list.Messages.Add(
                                    CommonTraceParser.DecodeMsg(session.msg.rawMsg.value, session.AsnParseClass));
                            }
                        }
                        catch (Exception)
                        {
                            list.FailCounts++;
                        }
                    }
                }
            }
            return list;
        }

        public static MemoryStream UnzipToMemoryStream(string binfile)
        {
            MemoryStream stream = new MemoryStream();
            int count = 0x100000;
            using (FileStream stream2 = File.OpenRead(binfile))
            {
                using (ZipInputStream stream3 = new ZipInputStream(stream2))
                {
                    while (stream3.GetNextEntry() != null)
                    {
                        byte[] buffer = new byte[count];
                        while (true)
                        {
                            count = stream3.Read(buffer, 0, buffer.Length);
                            if (count > 0)
                            {
                                stream.Write(buffer, 0, count);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return stream;
        }
    }
}
