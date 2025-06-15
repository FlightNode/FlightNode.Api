namespace FlightNode.DataCollection.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkdayNumVolunteersAndTasksCompleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkLog", "NumberOfVolunteers", c => c.Int(nullable: false));
            AddColumn("dbo.WorkLog", "TasksCompleted", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkLog", "TasksCompleted");
            DropColumn("dbo.WorkLog", "NumberOfVolunteers");
        }
    }
}
