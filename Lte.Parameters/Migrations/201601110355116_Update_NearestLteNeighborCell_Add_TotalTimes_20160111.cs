namespace Lte.Parameters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_NearestLteNeighborCell_Add_TotalTimes_20160111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LteNeighborCells", "TotalTimes", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LteNeighborCells", "TotalTimes");
        }
    }
}
