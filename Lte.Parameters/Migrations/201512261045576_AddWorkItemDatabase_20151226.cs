namespace Lte.Parameters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkItemDatabase_20151226 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TopDrop2GCellDaily", "CdrCallsDistanceInfo_TopDrop2GCellDailyId", "dbo.CdrCallsDistanceInfoes");
            DropForeignKey("dbo.TopDrop2GCellDaily", "CdrCallsHourInfo_TopDrop2GCellDailyId", "dbo.CdrCallsHourInfoes");
            DropForeignKey("dbo.TopDrop2GCellDaily", "CdrDropsDistanceInfo_TopDrop2GCellDailyId", "dbo.CdrDropsDistanceInfoes");
            DropForeignKey("dbo.TopDrop2GCellDaily", "CdrDropsHourInfo_TopDrop2GCellDailyId", "dbo.CdrDropsHourInfoes");
            DropForeignKey("dbo.TopDrop2GCellDaily", "DropEcioDistanceInfo_TopDrop2GCellDailyId", "dbo.DropEcioDistanceInfoes");
            DropForeignKey("dbo.TopDrop2GCellDaily", "DropEcioHourInfo_TopDrop2GCellDailyId", "dbo.DropEcioHourInfoes");
            DropForeignKey("dbo.TopDrop2GCellDaily", "ErasureDropsHourInfo_TopDrop2GCellDailyId", "dbo.ErasureDropsHourInfoes");
            DropForeignKey("dbo.TopDrop2GCellDaily", "GoodEcioDistanceInfo_TopDrop2GCellDailyId", "dbo.GoodEcioDistanceInfoes");
            DropForeignKey("dbo.TopDrop2GCellDaily", "KpiCallsHourInfo_TopDrop2GCellDailyId", "dbo.KpiCallsHourInfoes");
            DropForeignKey("dbo.TopDrop2GCellDaily", "KpiDropsHourInfo_TopDrop2GCellDailyId", "dbo.KpiDropsHourInfoes");
            DropForeignKey("dbo.TopDrop2GCellDaily", "MainRssiHourInfo_TopDrop2GCellDailyId", "dbo.MainRssiHourInfoes");
            DropForeignKey("dbo.NeighborHourInfoes", "TopDrop2GCellDailyId", "dbo.TopDrop2GCellDaily");
            DropForeignKey("dbo.TopDrop2GCellDaily", "SubRssiHourInfo_TopDrop2GCellDailyId", "dbo.SubRssiHourInfoes");
            DropForeignKey("dbo.AlarmHourInfoes", "TopDrop2GCellDailyId", "dbo.TopDrop2GCellDaily");
            DropIndex("dbo.AlarmHourInfoes", new[] { "TopDrop2GCellDailyId" });
            DropIndex("dbo.TopDrop2GCellDaily", new[] { "CdrCallsDistanceInfo_TopDrop2GCellDailyId" });
            DropIndex("dbo.TopDrop2GCellDaily", new[] { "CdrCallsHourInfo_TopDrop2GCellDailyId" });
            DropIndex("dbo.TopDrop2GCellDaily", new[] { "CdrDropsDistanceInfo_TopDrop2GCellDailyId" });
            DropIndex("dbo.TopDrop2GCellDaily", new[] { "CdrDropsHourInfo_TopDrop2GCellDailyId" });
            DropIndex("dbo.TopDrop2GCellDaily", new[] { "DropEcioDistanceInfo_TopDrop2GCellDailyId" });
            DropIndex("dbo.TopDrop2GCellDaily", new[] { "DropEcioHourInfo_TopDrop2GCellDailyId" });
            DropIndex("dbo.TopDrop2GCellDaily", new[] { "ErasureDropsHourInfo_TopDrop2GCellDailyId" });
            DropIndex("dbo.TopDrop2GCellDaily", new[] { "GoodEcioDistanceInfo_TopDrop2GCellDailyId" });
            DropIndex("dbo.TopDrop2GCellDaily", new[] { "KpiCallsHourInfo_TopDrop2GCellDailyId" });
            DropIndex("dbo.TopDrop2GCellDaily", new[] { "KpiDropsHourInfo_TopDrop2GCellDailyId" });
            DropIndex("dbo.TopDrop2GCellDaily", new[] { "MainRssiHourInfo_TopDrop2GCellDailyId" });
            DropIndex("dbo.TopDrop2GCellDaily", new[] { "SubRssiHourInfo_TopDrop2GCellDailyId" });
            DropIndex("dbo.NeighborHourInfoes", new[] { "TopDrop2GCellDailyId" });
            CreateTable(
                "dbo.WorkItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(),
                        Type = c.Byte(nullable: false),
                        Subtype = c.Short(nullable: false),
                        ENodebId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        BeginTime = c.DateTime(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        RepeatTimes = c.Short(nullable: false),
                        RejectTimes = c.Short(nullable: false),
                        StaffName = c.String(),
                        FeedbackTime = c.DateTime(),
                        FinishTime = c.DateTime(),
                        Cause = c.Short(nullable: false),
                        State = c.Byte(nullable: false),
                        Comments = c.String(),
                        FeedbackContents = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.IndoorDistributions", "CollegeName");
            DropColumn("dbo.IndoorDistributions", "Discriminator");
            DropTable("dbo.AlarmHourInfoes");
            DropTable("dbo.TopDrop2GCellDaily");
            DropTable("dbo.CdrCallsDistanceInfoes");
            DropTable("dbo.CdrCallsHourInfoes");
            DropTable("dbo.CdrDropsDistanceInfoes");
            DropTable("dbo.CdrDropsHourInfoes");
            DropTable("dbo.DropEcioDistanceInfoes");
            DropTable("dbo.DropEcioHourInfoes");
            DropTable("dbo.ErasureDropsHourInfoes");
            DropTable("dbo.GoodEcioDistanceInfoes");
            DropTable("dbo.KpiCallsHourInfoes");
            DropTable("dbo.KpiDropsHourInfoes");
            DropTable("dbo.MainRssiHourInfoes");
            DropTable("dbo.NeighborHourInfoes");
            DropTable("dbo.SubRssiHourInfoes");
            DropTable("dbo.CoverageAdjustments");
            DropTable("dbo.ENodebPhotoes");
            DropTable("dbo.InterferenceStats");
            DropTable("dbo.MonthPreciseCoverage4GStat");
            DropTable("dbo.MroRsrpTas");
            DropTable("dbo.MrsCellDates");
            DropTable("dbo.MrsCellTas");
            DropTable("dbo.PureInterferenceStats");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PureInterferenceStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecordDate = c.DateTime(nullable: false),
                        CellId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        VictimCells = c.Int(nullable: false),
                        InterferenceCells = c.Int(nullable: false),
                        FirstVictimCellId = c.Int(nullable: false),
                        FirstVictimSectorId = c.Byte(nullable: false),
                        FirstVictimTimes = c.Int(nullable: false),
                        FirstInterferenceTimes = c.Int(nullable: false),
                        SecondVictimCellId = c.Int(nullable: false),
                        SecondVictimSectorId = c.Byte(nullable: false),
                        SecondVictimTimes = c.Int(nullable: false),
                        SecondInterferenceTimes = c.Int(nullable: false),
                        ThirdVictimCellId = c.Int(nullable: false),
                        ThirdVictimSectorId = c.Byte(nullable: false),
                        ThirdVictimTimes = c.Int(nullable: false),
                        ThirdInterferenceTimes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MrsCellTas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecordDate = c.DateTime(nullable: false),
                        CellId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        TaTo2 = c.Int(nullable: false),
                        TaTo4 = c.Int(nullable: false),
                        TaTo6 = c.Int(nullable: false),
                        TaTo8 = c.Int(nullable: false),
                        TaTo12 = c.Int(nullable: false),
                        TaTo16 = c.Int(nullable: false),
                        TaTo20 = c.Int(nullable: false),
                        TaTo24 = c.Int(nullable: false),
                        TaTo32 = c.Int(nullable: false),
                        TaTo40 = c.Int(nullable: false),
                        TaTo48 = c.Int(nullable: false),
                        TaTo56 = c.Int(nullable: false),
                        TaTo64 = c.Int(nullable: false),
                        TaTo80 = c.Int(nullable: false),
                        TaTo96 = c.Int(nullable: false),
                        TaTo128 = c.Int(nullable: false),
                        TaTo192 = c.Int(nullable: false),
                        TaTo256 = c.Int(nullable: false),
                        TaAbove256 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MrsCellDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecordDate = c.DateTime(nullable: false),
                        CellId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        RsrpTo120 = c.Int(nullable: false),
                        RsrpTo115 = c.Int(nullable: false),
                        RsrpTo110 = c.Int(nullable: false),
                        RsrpTo105 = c.Int(nullable: false),
                        RsrpTo100 = c.Int(nullable: false),
                        RsrpTo95 = c.Int(nullable: false),
                        RsrpTo90 = c.Int(nullable: false),
                        RsrpTo85 = c.Int(nullable: false),
                        RsrpTo80 = c.Int(nullable: false),
                        RsrpTo70 = c.Int(nullable: false),
                        RsrpTo60 = c.Int(nullable: false),
                        RsrpAbove60 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MroRsrpTas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecordDate = c.DateTime(nullable: false),
                        CellId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        RsrpInterval = c.Int(nullable: false),
                        TaTo4 = c.Int(nullable: false),
                        TaTo8 = c.Int(nullable: false),
                        TaTo16 = c.Int(nullable: false),
                        TaTo24 = c.Int(nullable: false),
                        TaTo40 = c.Int(nullable: false),
                        TaTo56 = c.Int(nullable: false),
                        TaTo80 = c.Int(nullable: false),
                        TaTo128 = c.Int(nullable: false),
                        TaAbove128 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MonthPreciseCoverage4GStat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Short(nullable: false),
                        Month = c.Byte(nullable: false),
                        CellId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        TotalMrs = c.Int(nullable: false),
                        ThirdNeighbors = c.Int(nullable: false),
                        SecondNeighbors = c.Int(nullable: false),
                        FirstNeighbors = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InterferenceStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CellId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        VictimCells = c.Int(nullable: false),
                        InterferenceCells = c.Int(nullable: false),
                        SumRtds = c.Double(nullable: false),
                        TotalRtds = c.Int(nullable: false),
                        MinRtd = c.Double(nullable: false),
                        TaOuterIntervalNum = c.Int(nullable: false),
                        TaInnerIntervalNum = c.Int(nullable: false),
                        TaSum = c.Double(nullable: false),
                        TaOuterIntervalExcessNum = c.Int(nullable: false),
                        TaInnerIntervalExcessNum = c.Int(nullable: false),
                        TaMax = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ENodebPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ENodebId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        Angle = c.Short(nullable: false),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CoverageAdjustments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ENodebId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        Frequency = c.Int(nullable: false),
                        Factor165m = c.Double(nullable: false),
                        Factor135m = c.Double(nullable: false),
                        Factor105m = c.Double(nullable: false),
                        Factor75m = c.Double(nullable: false),
                        Factor45m = c.Double(nullable: false),
                        Factor15m = c.Double(nullable: false),
                        Factor15 = c.Double(nullable: false),
                        Factor45 = c.Double(nullable: false),
                        Factor75 = c.Double(nullable: false),
                        Factor105 = c.Double(nullable: false),
                        Factor135 = c.Double(nullable: false),
                        Factor165 = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubRssiHourInfoes",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        Hour0Info = c.Double(nullable: false),
                        Hour10Info = c.Double(nullable: false),
                        Hour11Info = c.Double(nullable: false),
                        Hour12Info = c.Double(nullable: false),
                        Hour13Info = c.Double(nullable: false),
                        Hour14Info = c.Double(nullable: false),
                        Hour15Info = c.Double(nullable: false),
                        Hour16Info = c.Double(nullable: false),
                        Hour17Info = c.Double(nullable: false),
                        Hour18Info = c.Double(nullable: false),
                        Hour19Info = c.Double(nullable: false),
                        Hour1Info = c.Double(nullable: false),
                        Hour20Info = c.Double(nullable: false),
                        Hour21Info = c.Double(nullable: false),
                        Hour22Info = c.Double(nullable: false),
                        Hour23Info = c.Double(nullable: false),
                        Hour2Info = c.Double(nullable: false),
                        Hour3Info = c.Double(nullable: false),
                        Hour4Info = c.Double(nullable: false),
                        Hour5Info = c.Double(nullable: false),
                        Hour6Info = c.Double(nullable: false),
                        Hour7Info = c.Double(nullable: false),
                        Hour8Info = c.Double(nullable: false),
                        Hour9Info = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.NeighborHourInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopDrop2GCellDailyId = c.Int(nullable: false),
                        Hour = c.Short(nullable: false),
                        BscId = c.Short(nullable: false),
                        BtsId = c.Int(nullable: false),
                        CellId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        Frequency = c.Short(nullable: false),
                        NeighborInfo = c.String(maxLength: 20),
                        Problem = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MainRssiHourInfoes",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        Hour0Info = c.Double(nullable: false),
                        Hour10Info = c.Double(nullable: false),
                        Hour11Info = c.Double(nullable: false),
                        Hour12Info = c.Double(nullable: false),
                        Hour13Info = c.Double(nullable: false),
                        Hour14Info = c.Double(nullable: false),
                        Hour15Info = c.Double(nullable: false),
                        Hour16Info = c.Double(nullable: false),
                        Hour17Info = c.Double(nullable: false),
                        Hour18Info = c.Double(nullable: false),
                        Hour19Info = c.Double(nullable: false),
                        Hour1Info = c.Double(nullable: false),
                        Hour20Info = c.Double(nullable: false),
                        Hour21Info = c.Double(nullable: false),
                        Hour22Info = c.Double(nullable: false),
                        Hour23Info = c.Double(nullable: false),
                        Hour2Info = c.Double(nullable: false),
                        Hour3Info = c.Double(nullable: false),
                        Hour4Info = c.Double(nullable: false),
                        Hour5Info = c.Double(nullable: false),
                        Hour6Info = c.Double(nullable: false),
                        Hour7Info = c.Double(nullable: false),
                        Hour8Info = c.Double(nullable: false),
                        Hour9Info = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.KpiDropsHourInfoes",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        Hour0Info = c.Int(nullable: false),
                        Hour10Info = c.Int(nullable: false),
                        Hour11Info = c.Int(nullable: false),
                        Hour12Info = c.Int(nullable: false),
                        Hour13Info = c.Int(nullable: false),
                        Hour14Info = c.Int(nullable: false),
                        Hour15Info = c.Int(nullable: false),
                        Hour16Info = c.Int(nullable: false),
                        Hour17Info = c.Int(nullable: false),
                        Hour18Info = c.Int(nullable: false),
                        Hour19Info = c.Int(nullable: false),
                        Hour1Info = c.Int(nullable: false),
                        Hour20Info = c.Int(nullable: false),
                        Hour21Info = c.Int(nullable: false),
                        Hour22Info = c.Int(nullable: false),
                        Hour23Info = c.Int(nullable: false),
                        Hour2Info = c.Int(nullable: false),
                        Hour3Info = c.Int(nullable: false),
                        Hour4Info = c.Int(nullable: false),
                        Hour5Info = c.Int(nullable: false),
                        Hour6Info = c.Int(nullable: false),
                        Hour7Info = c.Int(nullable: false),
                        Hour8Info = c.Int(nullable: false),
                        Hour9Info = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.KpiCallsHourInfoes",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        Hour0Info = c.Int(nullable: false),
                        Hour10Info = c.Int(nullable: false),
                        Hour11Info = c.Int(nullable: false),
                        Hour12Info = c.Int(nullable: false),
                        Hour13Info = c.Int(nullable: false),
                        Hour14Info = c.Int(nullable: false),
                        Hour15Info = c.Int(nullable: false),
                        Hour16Info = c.Int(nullable: false),
                        Hour17Info = c.Int(nullable: false),
                        Hour18Info = c.Int(nullable: false),
                        Hour19Info = c.Int(nullable: false),
                        Hour1Info = c.Int(nullable: false),
                        Hour20Info = c.Int(nullable: false),
                        Hour21Info = c.Int(nullable: false),
                        Hour22Info = c.Int(nullable: false),
                        Hour23Info = c.Int(nullable: false),
                        Hour2Info = c.Int(nullable: false),
                        Hour3Info = c.Int(nullable: false),
                        Hour4Info = c.Int(nullable: false),
                        Hour5Info = c.Int(nullable: false),
                        Hour6Info = c.Int(nullable: false),
                        Hour7Info = c.Int(nullable: false),
                        Hour8Info = c.Int(nullable: false),
                        Hour9Info = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.GoodEcioDistanceInfoes",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        DistanceTo1000Info = c.Double(nullable: false),
                        DistanceTo1200Info = c.Double(nullable: false),
                        DistanceTo1400Info = c.Double(nullable: false),
                        DistanceTo1600Info = c.Double(nullable: false),
                        DistanceTo1800Info = c.Double(nullable: false),
                        DistanceTo2000Info = c.Double(nullable: false),
                        DistanceTo200Info = c.Double(nullable: false),
                        DistanceTo2200Info = c.Double(nullable: false),
                        DistanceTo2400Info = c.Double(nullable: false),
                        DistanceTo2600Info = c.Double(nullable: false),
                        DistanceTo2800Info = c.Double(nullable: false),
                        DistanceTo3000Info = c.Double(nullable: false),
                        DistanceTo4000Info = c.Double(nullable: false),
                        DistanceTo400Info = c.Double(nullable: false),
                        DistanceTo5000Info = c.Double(nullable: false),
                        DistanceTo6000Info = c.Double(nullable: false),
                        DistanceTo600Info = c.Double(nullable: false),
                        DistanceTo7000Info = c.Double(nullable: false),
                        DistanceTo8000Info = c.Double(nullable: false),
                        DistanceTo800Info = c.Double(nullable: false),
                        DistanceTo9000Info = c.Double(nullable: false),
                        DistanceToInfInfo = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.ErasureDropsHourInfoes",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        Hour0Info = c.Int(nullable: false),
                        Hour10Info = c.Int(nullable: false),
                        Hour11Info = c.Int(nullable: false),
                        Hour12Info = c.Int(nullable: false),
                        Hour13Info = c.Int(nullable: false),
                        Hour14Info = c.Int(nullable: false),
                        Hour15Info = c.Int(nullable: false),
                        Hour16Info = c.Int(nullable: false),
                        Hour17Info = c.Int(nullable: false),
                        Hour18Info = c.Int(nullable: false),
                        Hour19Info = c.Int(nullable: false),
                        Hour1Info = c.Int(nullable: false),
                        Hour20Info = c.Int(nullable: false),
                        Hour21Info = c.Int(nullable: false),
                        Hour22Info = c.Int(nullable: false),
                        Hour23Info = c.Int(nullable: false),
                        Hour2Info = c.Int(nullable: false),
                        Hour3Info = c.Int(nullable: false),
                        Hour4Info = c.Int(nullable: false),
                        Hour5Info = c.Int(nullable: false),
                        Hour6Info = c.Int(nullable: false),
                        Hour7Info = c.Int(nullable: false),
                        Hour8Info = c.Int(nullable: false),
                        Hour9Info = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.DropEcioHourInfoes",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        Hour0Info = c.Double(nullable: false),
                        Hour10Info = c.Double(nullable: false),
                        Hour11Info = c.Double(nullable: false),
                        Hour12Info = c.Double(nullable: false),
                        Hour13Info = c.Double(nullable: false),
                        Hour14Info = c.Double(nullable: false),
                        Hour15Info = c.Double(nullable: false),
                        Hour16Info = c.Double(nullable: false),
                        Hour17Info = c.Double(nullable: false),
                        Hour18Info = c.Double(nullable: false),
                        Hour19Info = c.Double(nullable: false),
                        Hour1Info = c.Double(nullable: false),
                        Hour20Info = c.Double(nullable: false),
                        Hour21Info = c.Double(nullable: false),
                        Hour22Info = c.Double(nullable: false),
                        Hour23Info = c.Double(nullable: false),
                        Hour2Info = c.Double(nullable: false),
                        Hour3Info = c.Double(nullable: false),
                        Hour4Info = c.Double(nullable: false),
                        Hour5Info = c.Double(nullable: false),
                        Hour6Info = c.Double(nullable: false),
                        Hour7Info = c.Double(nullable: false),
                        Hour8Info = c.Double(nullable: false),
                        Hour9Info = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.DropEcioDistanceInfoes",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        DistanceTo1000Info = c.Double(nullable: false),
                        DistanceTo1200Info = c.Double(nullable: false),
                        DistanceTo1400Info = c.Double(nullable: false),
                        DistanceTo1600Info = c.Double(nullable: false),
                        DistanceTo1800Info = c.Double(nullable: false),
                        DistanceTo2000Info = c.Double(nullable: false),
                        DistanceTo200Info = c.Double(nullable: false),
                        DistanceTo2200Info = c.Double(nullable: false),
                        DistanceTo2400Info = c.Double(nullable: false),
                        DistanceTo2600Info = c.Double(nullable: false),
                        DistanceTo2800Info = c.Double(nullable: false),
                        DistanceTo3000Info = c.Double(nullable: false),
                        DistanceTo4000Info = c.Double(nullable: false),
                        DistanceTo400Info = c.Double(nullable: false),
                        DistanceTo5000Info = c.Double(nullable: false),
                        DistanceTo6000Info = c.Double(nullable: false),
                        DistanceTo600Info = c.Double(nullable: false),
                        DistanceTo7000Info = c.Double(nullable: false),
                        DistanceTo8000Info = c.Double(nullable: false),
                        DistanceTo800Info = c.Double(nullable: false),
                        DistanceTo9000Info = c.Double(nullable: false),
                        DistanceToInfInfo = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.CdrDropsHourInfoes",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        Hour0Info = c.Int(nullable: false),
                        Hour10Info = c.Int(nullable: false),
                        Hour11Info = c.Int(nullable: false),
                        Hour12Info = c.Int(nullable: false),
                        Hour13Info = c.Int(nullable: false),
                        Hour14Info = c.Int(nullable: false),
                        Hour15Info = c.Int(nullable: false),
                        Hour16Info = c.Int(nullable: false),
                        Hour17Info = c.Int(nullable: false),
                        Hour18Info = c.Int(nullable: false),
                        Hour19Info = c.Int(nullable: false),
                        Hour1Info = c.Int(nullable: false),
                        Hour20Info = c.Int(nullable: false),
                        Hour21Info = c.Int(nullable: false),
                        Hour22Info = c.Int(nullable: false),
                        Hour23Info = c.Int(nullable: false),
                        Hour2Info = c.Int(nullable: false),
                        Hour3Info = c.Int(nullable: false),
                        Hour4Info = c.Int(nullable: false),
                        Hour5Info = c.Int(nullable: false),
                        Hour6Info = c.Int(nullable: false),
                        Hour7Info = c.Int(nullable: false),
                        Hour8Info = c.Int(nullable: false),
                        Hour9Info = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.CdrDropsDistanceInfoes",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        DistanceTo1000Info = c.Int(nullable: false),
                        DistanceTo1200Info = c.Int(nullable: false),
                        DistanceTo1400Info = c.Int(nullable: false),
                        DistanceTo1600Info = c.Int(nullable: false),
                        DistanceTo1800Info = c.Int(nullable: false),
                        DistanceTo2000Info = c.Int(nullable: false),
                        DistanceTo200Info = c.Int(nullable: false),
                        DistanceTo2200Info = c.Int(nullable: false),
                        DistanceTo2400Info = c.Int(nullable: false),
                        DistanceTo2600Info = c.Int(nullable: false),
                        DistanceTo2800Info = c.Int(nullable: false),
                        DistanceTo3000Info = c.Int(nullable: false),
                        DistanceTo4000Info = c.Int(nullable: false),
                        DistanceTo400Info = c.Int(nullable: false),
                        DistanceTo5000Info = c.Int(nullable: false),
                        DistanceTo6000Info = c.Int(nullable: false),
                        DistanceTo600Info = c.Int(nullable: false),
                        DistanceTo7000Info = c.Int(nullable: false),
                        DistanceTo8000Info = c.Int(nullable: false),
                        DistanceTo800Info = c.Int(nullable: false),
                        DistanceTo9000Info = c.Int(nullable: false),
                        DistanceToInfInfo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.CdrCallsHourInfoes",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        Hour0Info = c.Int(nullable: false),
                        Hour10Info = c.Int(nullable: false),
                        Hour11Info = c.Int(nullable: false),
                        Hour12Info = c.Int(nullable: false),
                        Hour13Info = c.Int(nullable: false),
                        Hour14Info = c.Int(nullable: false),
                        Hour15Info = c.Int(nullable: false),
                        Hour16Info = c.Int(nullable: false),
                        Hour17Info = c.Int(nullable: false),
                        Hour18Info = c.Int(nullable: false),
                        Hour19Info = c.Int(nullable: false),
                        Hour1Info = c.Int(nullable: false),
                        Hour20Info = c.Int(nullable: false),
                        Hour21Info = c.Int(nullable: false),
                        Hour22Info = c.Int(nullable: false),
                        Hour23Info = c.Int(nullable: false),
                        Hour2Info = c.Int(nullable: false),
                        Hour3Info = c.Int(nullable: false),
                        Hour4Info = c.Int(nullable: false),
                        Hour5Info = c.Int(nullable: false),
                        Hour6Info = c.Int(nullable: false),
                        Hour7Info = c.Int(nullable: false),
                        Hour8Info = c.Int(nullable: false),
                        Hour9Info = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.CdrCallsDistanceInfoes",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        DistanceTo1000Info = c.Int(nullable: false),
                        DistanceTo1200Info = c.Int(nullable: false),
                        DistanceTo1400Info = c.Int(nullable: false),
                        DistanceTo1600Info = c.Int(nullable: false),
                        DistanceTo1800Info = c.Int(nullable: false),
                        DistanceTo2000Info = c.Int(nullable: false),
                        DistanceTo200Info = c.Int(nullable: false),
                        DistanceTo2200Info = c.Int(nullable: false),
                        DistanceTo2400Info = c.Int(nullable: false),
                        DistanceTo2600Info = c.Int(nullable: false),
                        DistanceTo2800Info = c.Int(nullable: false),
                        DistanceTo3000Info = c.Int(nullable: false),
                        DistanceTo4000Info = c.Int(nullable: false),
                        DistanceTo400Info = c.Int(nullable: false),
                        DistanceTo5000Info = c.Int(nullable: false),
                        DistanceTo6000Info = c.Int(nullable: false),
                        DistanceTo600Info = c.Int(nullable: false),
                        DistanceTo7000Info = c.Int(nullable: false),
                        DistanceTo8000Info = c.Int(nullable: false),
                        DistanceTo800Info = c.Int(nullable: false),
                        DistanceTo9000Info = c.Int(nullable: false),
                        DistanceToInfInfo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.TopDrop2GCellDaily",
                c => new
                    {
                        TopDrop2GCellDailyId = c.Int(nullable: false, identity: true),
                        StatTime = c.DateTime(nullable: false),
                        BscId = c.Short(nullable: false),
                        BtsId = c.Int(nullable: false),
                        CellId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        Frequency = c.Short(nullable: false),
                        CdrCalls = c.Int(nullable: false),
                        CdrDrops = c.Int(nullable: false),
                        KpiCalls = c.Int(nullable: false),
                        KpiDrops = c.Int(nullable: false),
                        ErasureDrops = c.Int(nullable: false),
                        AverageRssi = c.Double(nullable: false),
                        MainRssi = c.Double(nullable: false),
                        SubRssi = c.Double(nullable: false),
                        AverageDropEcio = c.Double(nullable: false),
                        AverageDropDistance = c.Double(nullable: false),
                        DropCause = c.String(),
                        CdrCallsDistanceInfo_TopDrop2GCellDailyId = c.Int(),
                        CdrCallsHourInfo_TopDrop2GCellDailyId = c.Int(),
                        CdrDropsDistanceInfo_TopDrop2GCellDailyId = c.Int(),
                        CdrDropsHourInfo_TopDrop2GCellDailyId = c.Int(),
                        DropEcioDistanceInfo_TopDrop2GCellDailyId = c.Int(),
                        DropEcioHourInfo_TopDrop2GCellDailyId = c.Int(),
                        ErasureDropsHourInfo_TopDrop2GCellDailyId = c.Int(),
                        GoodEcioDistanceInfo_TopDrop2GCellDailyId = c.Int(),
                        KpiCallsHourInfo_TopDrop2GCellDailyId = c.Int(),
                        KpiDropsHourInfo_TopDrop2GCellDailyId = c.Int(),
                        MainRssiHourInfo_TopDrop2GCellDailyId = c.Int(),
                        SubRssiHourInfo_TopDrop2GCellDailyId = c.Int(),
                    })
                .PrimaryKey(t => t.TopDrop2GCellDailyId);
            
            CreateTable(
                "dbo.AlarmHourInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopDrop2GCellDailyId = c.Int(nullable: false),
                        Hour = c.Short(nullable: false),
                        AlarmType = c.Short(nullable: false),
                        Alarms = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.IndoorDistributions", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.IndoorDistributions", "CollegeName", c => c.String());
            DropTable("dbo.WorkItems");
            CreateIndex("dbo.NeighborHourInfoes", "TopDrop2GCellDailyId");
            CreateIndex("dbo.TopDrop2GCellDaily", "SubRssiHourInfo_TopDrop2GCellDailyId");
            CreateIndex("dbo.TopDrop2GCellDaily", "MainRssiHourInfo_TopDrop2GCellDailyId");
            CreateIndex("dbo.TopDrop2GCellDaily", "KpiDropsHourInfo_TopDrop2GCellDailyId");
            CreateIndex("dbo.TopDrop2GCellDaily", "KpiCallsHourInfo_TopDrop2GCellDailyId");
            CreateIndex("dbo.TopDrop2GCellDaily", "GoodEcioDistanceInfo_TopDrop2GCellDailyId");
            CreateIndex("dbo.TopDrop2GCellDaily", "ErasureDropsHourInfo_TopDrop2GCellDailyId");
            CreateIndex("dbo.TopDrop2GCellDaily", "DropEcioHourInfo_TopDrop2GCellDailyId");
            CreateIndex("dbo.TopDrop2GCellDaily", "DropEcioDistanceInfo_TopDrop2GCellDailyId");
            CreateIndex("dbo.TopDrop2GCellDaily", "CdrDropsHourInfo_TopDrop2GCellDailyId");
            CreateIndex("dbo.TopDrop2GCellDaily", "CdrDropsDistanceInfo_TopDrop2GCellDailyId");
            CreateIndex("dbo.TopDrop2GCellDaily", "CdrCallsHourInfo_TopDrop2GCellDailyId");
            CreateIndex("dbo.TopDrop2GCellDaily", "CdrCallsDistanceInfo_TopDrop2GCellDailyId");
            CreateIndex("dbo.AlarmHourInfoes", "TopDrop2GCellDailyId");
            AddForeignKey("dbo.AlarmHourInfoes", "TopDrop2GCellDailyId", "dbo.TopDrop2GCellDaily", "TopDrop2GCellDailyId", cascadeDelete: true);
            AddForeignKey("dbo.TopDrop2GCellDaily", "SubRssiHourInfo_TopDrop2GCellDailyId", "dbo.SubRssiHourInfoes", "TopDrop2GCellDailyId");
            AddForeignKey("dbo.NeighborHourInfoes", "TopDrop2GCellDailyId", "dbo.TopDrop2GCellDaily", "TopDrop2GCellDailyId", cascadeDelete: true);
            AddForeignKey("dbo.TopDrop2GCellDaily", "MainRssiHourInfo_TopDrop2GCellDailyId", "dbo.MainRssiHourInfoes", "TopDrop2GCellDailyId");
            AddForeignKey("dbo.TopDrop2GCellDaily", "KpiDropsHourInfo_TopDrop2GCellDailyId", "dbo.KpiDropsHourInfoes", "TopDrop2GCellDailyId");
            AddForeignKey("dbo.TopDrop2GCellDaily", "KpiCallsHourInfo_TopDrop2GCellDailyId", "dbo.KpiCallsHourInfoes", "TopDrop2GCellDailyId");
            AddForeignKey("dbo.TopDrop2GCellDaily", "GoodEcioDistanceInfo_TopDrop2GCellDailyId", "dbo.GoodEcioDistanceInfoes", "TopDrop2GCellDailyId");
            AddForeignKey("dbo.TopDrop2GCellDaily", "ErasureDropsHourInfo_TopDrop2GCellDailyId", "dbo.ErasureDropsHourInfoes", "TopDrop2GCellDailyId");
            AddForeignKey("dbo.TopDrop2GCellDaily", "DropEcioHourInfo_TopDrop2GCellDailyId", "dbo.DropEcioHourInfoes", "TopDrop2GCellDailyId");
            AddForeignKey("dbo.TopDrop2GCellDaily", "DropEcioDistanceInfo_TopDrop2GCellDailyId", "dbo.DropEcioDistanceInfoes", "TopDrop2GCellDailyId");
            AddForeignKey("dbo.TopDrop2GCellDaily", "CdrDropsHourInfo_TopDrop2GCellDailyId", "dbo.CdrDropsHourInfoes", "TopDrop2GCellDailyId");
            AddForeignKey("dbo.TopDrop2GCellDaily", "CdrDropsDistanceInfo_TopDrop2GCellDailyId", "dbo.CdrDropsDistanceInfoes", "TopDrop2GCellDailyId");
            AddForeignKey("dbo.TopDrop2GCellDaily", "CdrCallsHourInfo_TopDrop2GCellDailyId", "dbo.CdrCallsHourInfoes", "TopDrop2GCellDailyId");
            AddForeignKey("dbo.TopDrop2GCellDaily", "CdrCallsDistanceInfo_TopDrop2GCellDailyId", "dbo.CdrCallsDistanceInfoes", "TopDrop2GCellDailyId");
        }
    }
}
