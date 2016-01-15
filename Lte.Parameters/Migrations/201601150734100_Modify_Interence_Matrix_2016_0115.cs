namespace Lte.Parameters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Interence_Matrix_2016_0115 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InterferenceMatrixStats", "RecordTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InterferenceMatrixStats", "RecordTime");
        }
    }
}
