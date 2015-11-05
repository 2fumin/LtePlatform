using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common;
using TraceParser.Common;

namespace TraceParser.Huawei
{
    [Serializable]
    public class Msg0Block
    {
        private bool _HasError;

        public void ParseAsn()
        {
            if (body_object != null)
            {
                return;
            }
            _HasError = true;
            using (BitArrayInputStream stream = new BitArrayInputStream(new MemoryStream(body_bytes)))
            {
                try
                {
                    body_object = CommonTraceParser.DecodeMsg(stream, AsnParseClass);
                    if (((stream.bitLength - stream.bitPosition) > 8L) && (AsnParseClass != null))
                    {
                        _HasError = true;
                    }
                    else
                    {
                        _HasError = false;
                    }
                }
                catch (Exception)
                {
                    stream.bitPosition = 0L;
                }
            }
        }

        public void setBody_bytes(byte[] bytes)
        {
            _body_bytes = bytes;
        }

        private byte[] _body_bytes { get; set; }

        public string AsnParseClass { get; set; }

        public byte[] body_bytes
        {
            get
            {
                if (_body_bytes == null)
                {
                    return null;
                }
                if (((AsnParseClass != null) && !AsnParseClass.Equals("X2AP_PDU")) && !AsnParseClass.Equals("S1AP_PDU"))
                {
                    List<byte> list = _body_bytes.ToList();
                    list.RemoveAt(0);
                    return list.ToArray();
                }
                return _body_bytes;
            }
        }

        public ushort body_length_ui2 { get; set; }

        public ITraceMessage body_object { get; set; }

        public uint callid_ui4 { get; set; }

        public uint cell_id_ui4 { get; set; }

        public sbyte day_ui1 { get; set; }

        public sbyte direction_ui1 { get; set; }

        public bool HasError
        {
            get
            {
                return _HasError;
            }
            set
            {
                _HasError = value;
            }
        }

        public ushort head_length_ui2 { get; set; }

        public sbyte hour_ui1 { get; set; }

        public sbyte localcell_id_ui1 { get; set; }

        public uint micro_second_ui4 { get; set; }

        public sbyte minute_ui1 { get; set; }

        public sbyte month_ui1 { get; set; }

        public sbyte reserved_ui1 { get; set; }

        public sbyte second_ui1 { get; set; }

        public string type_name
        {
            get
            {
                return "";
            }
        }

        public uint typeid_ui4 { get; set; }

        public ushort year_ui2 { get; set; }
    }
}
