using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Parameters.Entities
{
    public enum HotspotType : byte
    {
        College,
        Hospital,
        ShoppingMall,
        Building,
        Transportation,
        TopPrecise
    }

    public enum InfrastructureType : byte
    {
        ENodeb,
        Cell,
        CdmaBts,
        CdmaCell,
        LteIndoor,
        CdmaIndoor
    }

    public enum RegionType : byte
    {
        Circle,
        Rectangle,
        Polygon,
        PolyLine
    }

    public class RectangleRange
    {
        public double West { get; set; }

        public double East { get; set; }

        public double South { get; set; }

        public double North { get; set; }
    }
}
