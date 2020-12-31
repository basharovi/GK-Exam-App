namespace GKExamApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedQuestionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Point", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Questions", "TimeDuration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "TimeDuration");
            DropColumn("dbo.Questions", "Point");
        }
    }
}
