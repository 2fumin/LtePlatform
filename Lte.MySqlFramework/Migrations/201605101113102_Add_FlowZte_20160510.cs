namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_FlowZte_20160510 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlowZtes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatTime = c.DateTime(nullable: false, precision: 0),
                        ENodebId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        MaxRrcUsers = c.Int(nullable: false),
                        UplinkAverageActiveUsers = c.Double(nullable: false),
                        DownlinkAverageActiveUsers = c.Double(nullable: false),
                        AverageRrcUsers = c.Double(nullable: false),
                        AverageActiveUsers = c.Double(nullable: false),
                        MaxActiveUsers = c.Int(nullable: false),
                        PdcpUplinkDuration = c.Int(nullable: false),
                        PdcpDownlinkDuration = c.Int(nullable: false),
                        UplindPdcpFlow = c.Double(nullable: false),
                        DownlinkPdcpFlow = c.Double(nullable: false),
                        Qci8UplinkIpThroughput = c.Double(nullable: false),
                        Qci8UplinkIpDuration = c.Double(nullable: false),
                        Qci9UplinkIpThroughput = c.Double(nullable: false),
                        Qci9UplinkIpDuration = c.Double(nullable: false),
                        Qci8DownlinkIpThroughput = c.Double(nullable: false),
                        Qci8DownlinkIpDuration = c.Double(nullable: false),
                        Qci9DownlinkIpThroughput = c.Double(nullable: false),
                        Qci9DownlinkIpDuration = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FlowZtes");
        }
    }
}
