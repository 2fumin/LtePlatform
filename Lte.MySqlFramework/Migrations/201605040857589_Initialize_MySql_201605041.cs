namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize_MySql_201605041 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FlowHuaweis", "PdcpDownlinkFlow", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "PdcpUplinkFlow", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "AverageUsers", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "MaxUsers", c => c.Int(nullable: false));
            AddColumn("dbo.FlowHuaweis", "UplinkAverageUsers", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "UplinkMaxUsers", c => c.Int(nullable: false));
            AddColumn("dbo.FlowHuaweis", "DownlinkAverageUsers", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "DownlinkMaxUsers", c => c.Int(nullable: false));
            AddColumn("dbo.FlowHuaweis", "DownlinkDuration", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "UplinkDuration", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "PagingUsers", c => c.Int(nullable: false));
            AddColumn("dbo.FlowHuaweis", "DownlinkAveragePrbs", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "DownlinkDrbPbs", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "UplinkAveragePrbs", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "UplinkDrbPbs", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "GroupAPreambles", c => c.Int(nullable: false));
            AddColumn("dbo.FlowHuaweis", "GroupBPreambles", c => c.Int(nullable: false));
            AddColumn("dbo.FlowHuaweis", "DedicatedPreambles", c => c.Int(nullable: false));
            AddColumn("dbo.FlowHuaweis", "UplinkDciCceRate", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "DownlinkDciCceRate", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "PucchPrbs", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "LastTtiUplinkFlow", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "ButLastUplinkDuration", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "LastTtiDownlinkFlow", c => c.Double(nullable: false));
            AddColumn("dbo.FlowHuaweis", "ButLastDownlinkDuration", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FlowHuaweis", "ButLastDownlinkDuration");
            DropColumn("dbo.FlowHuaweis", "LastTtiDownlinkFlow");
            DropColumn("dbo.FlowHuaweis", "ButLastUplinkDuration");
            DropColumn("dbo.FlowHuaweis", "LastTtiUplinkFlow");
            DropColumn("dbo.FlowHuaweis", "PucchPrbs");
            DropColumn("dbo.FlowHuaweis", "DownlinkDciCceRate");
            DropColumn("dbo.FlowHuaweis", "UplinkDciCceRate");
            DropColumn("dbo.FlowHuaweis", "DedicatedPreambles");
            DropColumn("dbo.FlowHuaweis", "GroupBPreambles");
            DropColumn("dbo.FlowHuaweis", "GroupAPreambles");
            DropColumn("dbo.FlowHuaweis", "UplinkDrbPbs");
            DropColumn("dbo.FlowHuaweis", "UplinkAveragePrbs");
            DropColumn("dbo.FlowHuaweis", "DownlinkDrbPbs");
            DropColumn("dbo.FlowHuaweis", "DownlinkAveragePrbs");
            DropColumn("dbo.FlowHuaweis", "PagingUsers");
            DropColumn("dbo.FlowHuaweis", "UplinkDuration");
            DropColumn("dbo.FlowHuaweis", "DownlinkDuration");
            DropColumn("dbo.FlowHuaweis", "DownlinkMaxUsers");
            DropColumn("dbo.FlowHuaweis", "DownlinkAverageUsers");
            DropColumn("dbo.FlowHuaweis", "UplinkMaxUsers");
            DropColumn("dbo.FlowHuaweis", "UplinkAverageUsers");
            DropColumn("dbo.FlowHuaweis", "MaxUsers");
            DropColumn("dbo.FlowHuaweis", "AverageUsers");
            DropColumn("dbo.FlowHuaweis", "PdcpUplinkFlow");
            DropColumn("dbo.FlowHuaweis", "PdcpDownlinkFlow");
        }
    }
}
