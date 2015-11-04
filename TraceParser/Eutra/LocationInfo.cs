using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class LocationInfo_r10
    {
        public void InitDefaults()
        {
        }

        public string gnss_TOD_msec_r10 { get; set; }

        public string horizontalVelocity_r10 { get; set; }

        public locationCoordinates_r10_Type locationCoordinates_r10 { get; set; }

        [Serializable]
        public class locationCoordinates_r10_Type
        {
            public void InitDefaults()
            {
            }

            public string ellipsoid_Point_r10 { get; set; }

            public string ellipsoidArc_r11 { get; set; }

            public string ellipsoidPointWithAltitude_r10 { get; set; }

            public string ellipsoidPointWithAltitudeAndUncertaintyEllipsoid_r11 { get; set; }

            public string ellipsoidPointWithUncertaintyCircle_r11 { get; set; }

            public string ellipsoidPointWithUncertaintyEllipse_r11 { get; set; }

            public string polygon_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public locationCoordinates_r10_Type Decode(BitArrayInputStream input)
                {
                    int nBits;
                    locationCoordinates_r10_Type type = new locationCoordinates_r10_Type();
                    type.InitDefaults();
                    bool flag = input.readBit() != 0;
                    switch (input.readBits(3))
                    {
                        case 0:
                            nBits = input.readBits(8);
                            type.ellipsoid_Point_r10 = input.readOctetString(nBits);
                            return type;

                        case 1:
                            nBits = input.readBits(8);
                            type.ellipsoidPointWithAltitude_r10 = input.readOctetString(nBits);
                            return type;

                        case 2:
                            if (flag)
                            {
                                nBits = input.readBits(8);
                                type.ellipsoidPointWithUncertaintyCircle_r11 = input.readOctetString(nBits);
                            }
                            return type;

                        case 3:
                            if (flag)
                            {
                                nBits = input.readBits(8);
                                type.ellipsoidPointWithUncertaintyEllipse_r11 = input.readOctetString(nBits);
                            }
                            return type;

                        case 4:
                            if (flag)
                            {
                                nBits = input.readBits(8);
                                type.ellipsoidPointWithAltitudeAndUncertaintyEllipsoid_r11 = input.readOctetString(nBits);
                            }
                            return type;

                        case 5:
                            if (flag)
                            {
                                nBits = input.readBits(8);
                                type.ellipsoidArc_r11 = input.readOctetString(nBits);
                            }
                            return type;

                        case 6:
                            if (flag)
                            {
                                nBits = input.readBits(8);
                                type.polygon_r11 = input.readOctetString(nBits);
                            }
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public LocationInfo_r10 Decode(BitArrayInputStream input)
            {
                int nBits;
                LocationInfo_r10 _r = new LocationInfo_r10();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                _r.locationCoordinates_r10 = locationCoordinates_r10_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    nBits = input.readBits(8);
                    _r.horizontalVelocity_r10 = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    nBits = input.readBits(8);
                    _r.gnss_TOD_msec_r10 = input.readOctetString(nBits);
                }
                return _r;
            }
        }
    }
}
