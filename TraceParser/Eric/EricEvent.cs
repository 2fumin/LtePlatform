using System;
using System.Collections;
using System.IO;
using Lte.Domain.Common;
using TraceParser.Eutra;
using TraceParser.S1ap;
using TraceParser.X2ap;

namespace TraceParser.Eric
{
    public interface IEric
    {
        uint RecordLength { get; set; }

        uint RecordType { get; set; }
    }

    public class EricEvent : IEric
    {
        public EricEvent(uint recordLength, uint recordType, BigEndianBinaryReader _contentStream, Hashtable htEventPm)
        {
            InitailEvent(recordLength, recordType, _contentStream, htEventPm);
            object obj2 = ParseAsn();
            MessageContents = obj2;
        }

        public EricEvent(uint recordLength, uint recordType, BigEndianBinaryReader _contentStream, Hashtable htEventPm, bool IsParseAsn)
        {
            InitailEvent(recordLength, recordType, _contentStream, htEventPm);
            if (IsParseAsn)
            {
                object obj2 = ParseAsn();
                MessageContents = obj2;
            }
        }

        private void InitailEvent(uint recordLength, uint recordType, BigEndianBinaryReader _contentStream, Hashtable htEventPm)
        {
            RecordLength = recordLength;
            RecordType = recordType;
            byte[] buffer = _contentStream.ReadBytes(((int)recordLength) - 4);
            recordLength -= 4;
            MemoryStream input = new MemoryStream(buffer);
            BigEndianBinaryReader reader = new BigEndianBinaryReader(input);
            long position = reader.BaseStream.Position;
            long num3;
            EventId = reader.ReadUInts(3);
            _AsnType = htEventPm[(int)EventId] as EricPmEvent;
            TimeStampHour = reader.ReadByte();
            TimeStampMinute = reader.ReadByte();
            TimeStampSecond = reader.ReadByte();
            TimeStampMilliSec = reader.ReadUInt16();
            ScannerId = reader.ReadUInts(3);
            RbsModuleId = reader.ReadByte();
            GlobalCellId = reader.ReadUInt32();
            if (((_AsnType == null) || _AsnType.EventType.Equals("CELL")) || _AsnType.EventType.Equals("RBS"))
            {
                num3 = reader.BaseStream.Position - position;
                UnPaserTraseContent = reader.ReadHex((int)(recordLength - num3));
            }
            else
            {
                EnbS1ApId = reader.ReadUInts(3);
                MmeS1ApId = reader.ReadUInt32();
                Gummei = reader.ReadHex(7);
                RacUeRef = reader.ReadUInt32();
                TraceRecordingSessionReference = reader.ReadUInts(3);
                MessageDirection = reader.ReadByte();
                if (_AsnType.EventType.Equals("UE"))
                {
                    num3 = reader.BaseStream.Position - position;
                    UnPaserTraseContent = reader.ReadHex((int)(recordLength - num3));
                }
                else
                {
                    L3MessageLength = reader.ReadUInt16();
                    L3MessageContents = reader.ReadBytes(L3MessageLength);
                    num3 = reader.BaseStream.Position - position;
                    PaddingBytes = reader.ReadChars((int)(recordLength - num3));
                }
            }
        }

        public object ParseAsn()
        {
            object obj2 = null;
            if (MessageContents != null)
            {
                return MessageContents;
            }
            if (L3MessageContents == null)
            {
                return null;
            }
            BitArrayInputStream input = new BitArrayInputStream(new MemoryStream(L3MessageContents));
            try
            {
                switch (_AsnType.MsgDepend)
                {
                    case "BCCH_BCH_Message":
                        obj2 = BCCH_BCH_Message.PerDecoder.Instance.Decode(input);
                        goto Label_0199;

                    case "BCCH_DL_SCH_Message":
                        obj2 = BCCH_DL_SCH_Message.PerDecoder.Instance.Decode(input);
                        goto Label_0199;

                    case "MCCH_Message":
                        obj2 = MCCH_Message.PerDecoder.Instance.Decode(input);
                        goto Label_0199;

                    case "DL_CCCH_Message":
                        obj2 = DL_CCCH_Message.PerDecoder.Instance.Decode(input);
                        goto Label_0199;

                    case "DL_DCCH_Message":
                        obj2 = DL_DCCH_Message.PerDecoder.Instance.Decode(input);
                        goto Label_0199;

                    case "UL_CCCH_Message":
                        obj2 = UL_CCCH_Message.PerDecoder.Instance.Decode(input);
                        goto Label_0199;

                    case "UL_DCCH_Message":
                        obj2 = UL_DCCH_Message.PerDecoder.Instance.Decode(input);
                        goto Label_0199;

                    case "X2AP_PDU":
                        obj2 = X2AP_PDU.PerDecoder.Instance.Decode(input);
                        goto Label_0199;

                    case "S1AP_PDU":
                        obj2 = S1AP_PDU.PerDecoder.Instance.Decode(input);
                        goto Label_0199;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                input.Dispose();
            }
            Label_0199:
            MessageContents = obj2;
            return obj2;
        }

        public EricPmEvent _AsnType { get; set; }

        public string AsnParseClass
        {
            get
            {
                if (_AsnType == null)
                {
                    return string.Empty;
                }
                return _AsnType.MsgDepend;
            }
        }

        public uint EnbS1ApId { get; set; }

        public uint EventId { get; set; }

        public uint GlobalCellId { get; set; }

        public string Gummei { get; set; }

        public byte[] L3MessageContents { get; set; }

        public int L3MessageLength { get; set; }

        public object MessageContents { get; set; }

        public uint MessageDirection { get; set; }

        public uint MmeS1ApId { get; set; }

        private char[] PaddingBytes { get; set; }

        public uint RacUeRef { get; set; }

        public uint RbsModuleId { get; set; }

        public uint RecordLength { get; set; }

        public uint RecordType { get; set; }

        public uint ScannerId { get; set; }

        public uint TimeStampHour { get; set; }

        public uint TimeStampMilliSec { get; set; }

        public uint TimeStampMinute { get; set; }

        public uint TimeStampSecond { get; set; }

        public uint TraceRecordingSessionReference { get; set; }

        public string UnPaserTraseContent { get; set; }
    }
}
