namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CellStatMysql_20160511 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CellStatMysqls", "SectorId", c => c.Byte(nullable: false));
            DropColumn("dbo.CellStatMysqls", "Pci");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CellStatMysqls", "Pci", c => c.Int(nullable: false));
            DropColumn("dbo.CellStatMysqls", "SectorId");
        }
    }
}
