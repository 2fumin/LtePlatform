using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TraceParser.Zte
{
    public class ZteTraceRecSession
    {
        private DateTime _stime { get; set; }

        public string AsnParseClass { get; set; }

        [XmlAttribute(AttributeName = "dnPrefix")]
        public string dnPrefix { get; set; }

        [XmlAttribute(AttributeName = "eNodeB")]
        public int eNodeB { get; set; }

        public int intTraceRecSessionRef
        {
            get
            {
                if (!string.IsNullOrEmpty(traceRecSessionRef))
                {
                    return Convert.ToInt32(traceRecSessionRef, 0x10);
                }
                return -1;
            }
        }

        public Msg msg { get; set; }

        [XmlAttribute(AttributeName = "stime", DataType = "dateTime")]
        public DateTime stime
        {
            get
            {
                return _stime;
            }
            set
            {
                try
                {
                    _stime = value;
                }
                catch (Exception)
                {
                    Console.WriteLine("{0}转换为stime失败", value);
                }
            }
        }

        [XmlAttribute(AttributeName = "traceRecSessionRef")]
        public string traceRecSessionRef { get; set; }

        public TraceSessionRef traceSessionRef { get; set; }

        public Ue ue { get; set; }

        public class Msg
        {
            [XmlAttribute(AttributeName = "changeTime")]
            public decimal changeTime { get; set; }

            [XmlAttribute(AttributeName = "direction")]
            public int direction { get; set; }

            [XmlAttribute(AttributeName = "function")]
            public string function { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string name { get; set; }

            public TawMsg rawMsg { get; set; }

            public Target target { get; set; }

            [XmlAttribute(AttributeName = "vendorSpecific")]
            public bool vendorSpecific { get; set; }

            public class Target
            {
                [XmlAttribute(AttributeName = "type")]
                public string type { get; set; }

                [XmlText]
                public string value { get; set; }
            }

            public class TawMsg
            {
                private string _value;

                [XmlAttribute(AttributeName = "NumOfTargets")]
                public int NumOfTargets { get; set; }

                [XmlAttribute(AttributeName = "protocol")]
                public string protocol { get; set; }

                [XmlText]
                public string value
                {
                    get
                    {
                        return _value;
                    }
                    set
                    {
                        _value = value;
                    }
                }

                [XmlAttribute(AttributeName = "version")]
                public string version { get; set; }
            }
        }

        public class TraceSessionRef
        {
            public string MCC { get; set; }

            public string MNC { get; set; }

            public string TRACE_ID { get; set; }
        }

        public class Ue
        {
            [XmlAttribute(AttributeName = "idType")]
            public string idType { get; set; }

            [XmlAttribute(AttributeName = "idValue")]
            public string idValue { get; set; }
        }
    }
}
