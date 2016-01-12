using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TraceParser.Common;
using ZipLib.Zip;

namespace TraceParser.Zte
{
    public class ZteTraceParser
    {
        private static readonly List<ZteEvent> ListZteEvents = ZteEvent.GetZtePmEvents();
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ZteTraceCollecFile));

        private static ZteEvent GetAsnParseClass(ZteTraceRecSession traceCollecFile)
        {
            var function = traceCollecFile.msg.function;
            var name = traceCollecFile.msg.name;
            var event2 = ListZteEvents.FirstOrDefault(p => p.EventType.Equals(function) && p.EventName.Equals(name));
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
                return ListZteEvents.FirstOrDefault(predicate);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return event2;
        }

        public static ZteTraceCollecFile ParseRaw(string zipPath)
        {
            var stream = UnzipToMemoryStream(zipPath);
            return ParseRaw(stream);
        }

        public static ZteTraceCollecFile ParseRaw(MemoryStream uzip)
        {
            uzip.Position = 0L;
            return Serializer.Deserialize(uzip) as ZteTraceCollecFile;
        }

        public static TraceResultList Parse(List<ZteTraceRecSession> sessions, bool isParseAsn)
        {
            var list = new TraceResultList();
            foreach (var session in sessions)
            {
                if (session != null)
                {
                    var asnParseClass = GetAsnParseClass(session);
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
            var stream = new MemoryStream();
            var count = 0x100000;
            using (var stream2 = File.OpenRead(binfile))
            {
                using (ZipInputStream stream3 = new ZipInputStream(stream2))
                {
                    while (stream3.GetNextEntry() != null)
                    {
                        var buffer = new byte[count];
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
