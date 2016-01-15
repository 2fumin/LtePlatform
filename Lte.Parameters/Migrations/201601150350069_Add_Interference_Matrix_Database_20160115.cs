namespace Lte.Parameters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Interference_Matrix_Database_20160115 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InterferenceMatrixStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ENodebId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        DestPci = c.Short(nullable: false),
                        DestENodebId = c.Int(nullable: false),
                        DestSectorId = c.Byte(nullable: false),
                        Mod3Interferences = c.Int(nullable: false),
                        Mod6Interferences = c.Int(nullable: false),
                        OverInterferences6Db = c.Int(nullable: false),
                        OverInterferences10Db = c.Int(nullable: false),
                        InterferenceLevel = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InterferenceMatrixStats");
        }
    }
}
