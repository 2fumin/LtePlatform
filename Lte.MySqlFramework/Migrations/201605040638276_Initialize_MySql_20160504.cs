namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize_MySql_20160504 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlowHuaweis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatTime = c.DateTime(nullable: false, precision: 0),
                        ENodebId = c.Int(nullable: false),
                        LocalCellId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FlowHuaweis");
        }
    }
}
