namespace FlightNode.DataCollection.Domain.Migrations
{
	using Infrastructure.Customization;
	using System;
	using System.Data.Entity.Migrations;

	public partial class WorkLogReport_UpdateLocationSiteName : DbMigration
	{

		public override void Up()
		{
			this.DropView("dbo.WorkLogReport");
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
	l.Description as LocationName, 
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
