using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TraceParser.Zte
{
    [XmlRoot(ElementName = "traceCollecFile", 
        Namespace = "http://www.3gpp.org/ftp/specs/archive/32_series/32.423#traceData")]
    public class ZteTraceCollecFile
    {
        public FileHeader fileHeader { get; set; }

        [XmlElement(ElementName = "traceRecSession", IsNullable = false)]
        public List<ZteTraceRecSession> traceRecSessions { get; set; }
    }
}
