namespace Lte.Parameters.Entities.Mr
{
    public class InterferenceMatrixPci
    {
        public int ENodebId { get; set; }

        public short SourcePci { get; set; }

        public short DestPci { get; set; }

        public int Frequency { get; set; }

        public int Mod3Interferences { get; set; }
        
        public int Mod6Interferences { get; set; }
        
        public int OverInterferences6Db { get; set; }
        
        public int OverInterferences10Db { get; set; }
        
        public double InterferenceLevel { get; set; }
    }
}
