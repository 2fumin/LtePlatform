namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CellStatMysql_20160511 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CellStatMysqls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ENodebId = c.Int(nullable: false),
                        Pci = c.Int(nullable: false),
                        Mod3Count = c.Int(nullable: false),
                        WeakCoverCount = c.Int(nullable: false),
                        Mod6Count = c.Int(nullable: false),
                        CurrentDate = c.DateTime(nullable: false, precision: 0),
                        OverCoverCount = c.Int(nullable: false),
                        PreciseCount = c.Int(nullable: false),
                        MrCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CellStatMysqls");
        }
    }
}
