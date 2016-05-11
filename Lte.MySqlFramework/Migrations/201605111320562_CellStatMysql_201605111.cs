namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CellStatMysql_201605111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CellStatMysqls", "Pci", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CellStatMysqls", "Pci");
        }
    }
}
