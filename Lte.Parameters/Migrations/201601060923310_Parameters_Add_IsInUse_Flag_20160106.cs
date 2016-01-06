namespace Lte.Parameters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Parameters_Add_IsInUse_Flag_20160106 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CdmaBts", "IsInUse", c => c.Boolean(nullable: false));
            AddColumn("dbo.CdmaCells", "IsInUse", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cells", "IsInUse", c => c.Boolean(nullable: false));
            AddColumn("dbo.ENodebs", "IsInUse", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ENodebs", "IsInUse");
            DropColumn("dbo.Cells", "IsInUse");
            DropColumn("dbo.CdmaCells", "IsInUse");
            DropColumn("dbo.CdmaBts", "IsInUse");
        }
    }
}
