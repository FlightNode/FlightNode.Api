using FlightNode.DataCollection.Infrastructure.Customization;
using System.Data.Entity.Migrations;

namespace FlightNode.DataCollection.Domain.Migrations
{
	public partial class WorkLogReportUpdate : DbMigration
	{
		public override void Up()
		{
			this.DropView("dbo.WorkLogReport");
			this.CreateView("dbo.WorkLogReport", @"SELECT 
	wl.Id,
	CONVERT(varchar(20), wl.WorkDate, 101) as WorkDate, 
	wt.Description as Activity,
	l.County,
	l.SiteName,
	wl.NumberOfVolunteers,
	wl.WorkHours, 
	wl.TravelTimeHours,
	CASE wl.NumberOfVolunteers WHEN 1 THEN u.GivenName  + ' ' + u.FamilyName ELSE 'group' END as Volunteer,
	ISNULL(wl.TasksCompleted,'') as TasksCompleted,
	wl.UserId
FROM dbo.WorkLog wl
INNER JOIN dbo.WorkType wt ON wl.WorkTypeId = wt.Id
INNER JOIN dbo.Locations l ON wl.LocationId = l.Id
INNER JOIN dbo.Users u ON wl.UserId = u.Id
");
		}

		public override void Down()
		{
			this.DropView("dbo.WorkLogReport");

			// Restores the old view
			this.CreateView("dbo.WorkLogReport", @"SELECT wl.Id, 
CONVERT(VARCHAR(10), wl.WorkDate, 101) as WorkDate, 
wl.WorkHours, 
wl.TravelTimeHours, 
wl.WorkTypeId,
wt.Description as WorkType, 
wl.LocationId,
l.SiteName as LocationName, 
l.Longitude, 
l.Latitude, 
wl.UserId,
u.GivenName + ' ' + u.FamilyName as Person
FROM dbo.worklog wl
INNER JOIN dbo.WorkType wt ON wl.WorkTypeId = wt.Id
INNER JOIN dbo.Locations l ON wl.LocationId = l.Id
INNER JOIN dbo.Users u ON wl.UserId = u.Id
");
		}
	}
}
