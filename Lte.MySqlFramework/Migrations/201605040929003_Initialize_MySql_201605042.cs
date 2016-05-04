namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize_MySql_201605042 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FlowHuaweis", "AverageActiveUsers", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "MaxActiveUsers", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FlowHuaweis", "MaxActiveUsers");
            DropColumn("dbo.FlowHuaweis", "AverageActiveUsers");
        }
    }
}
