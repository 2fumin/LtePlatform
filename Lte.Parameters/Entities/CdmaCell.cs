using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Lte.Parameters.Entities
{
    public class CdmaCell : Entity
    {
        public int BtsId { get; set; } = -1;

        public byte SectorId { get; set; } = 31;

        public string CellType { get; set; } = "DO";

        public int Frequency { get; set; } = 0;

        public int CellId { get; set; }

        public string Lac { get; set; }

        public short Pn { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public double Height { get; set; }

        public double MTilt { get; set; }

        public double ETilt { get; set; }

        public double Azimuth { get; set; }

        public double AntennaGain { get; set; }

        public bool IsOutdoor { get; set; }

        public short Frequency1 { get; set; } = -1;

        public short Frequency2 { get; set; } = -1;

        public short Frequency3 { get; set; } = -1;

        public short Frequency4 { get; set; } = -1;

        public short Frequency5 { get; set; } = -1;

        public short Pci { get; set; }

        public double RsPower { get; set; }

        public static bool UpdateFirstFrequency { get; set; }

        public CdmaCell() { }
        
        public void AddFrequency(int freq)
        {
            switch (freq)
            {
                case 37:
                    Frequency += 1;
                    break;
                case 78:
                    Frequency += 2;
                    break;
                case 119:
                    Frequency += 4;
                    break;
                case 160:
                    Frequency += 8;
                    break;
                case 201:
                    Frequency += 16;
                    break;
                case 242:
                    Frequency += 32;
                    break;
                case 283:
                    Frequency += 64;
                    break;
                case 1013:
                    Frequency += 128;
                    break;
                default:
                    Frequency += 256;
                    break;
            }
        }

        public bool HasFrequency(int freq)
        {
            switch (freq)
            {
                case 37:
                    return (Frequency & 1) != 0;
                case 78:
                    return (Frequency & 2) != 0;
                case 119:
                    return (Frequency & 4) != 0;
                case 160:
                    return (Frequency & 8) != 0;
                case 201:
                    return (Frequency & 16) != 0;
                case 242:
                    return (Frequency & 32) != 0;
                case 283:
                    return (Frequency & 64) != 0;
                case 1013:
                    return (Frequency & 128) != 0;
                default:
                    return (Frequency & 256) != 0;
            }
        }

        public string FrequencyList
        {
            get
            {
                if (Frequency1 == -1) { return "空"; }
                string result = Frequency1.ToString(CultureInfo.InvariantCulture);
                if (Frequency2 == -1) { return result; }
                result += "&" + Frequency2;
                if (Frequency3 == -1) { return result; }
                result += "&" + Frequency3;
                if (Frequency4 == -1) { return result; }
                result += "&" + Frequency4;
                if (Frequency5 == -1) { return result; }
                result += "&" + Frequency5;
                return result;
            }
        }
    }
}
