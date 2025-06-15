namespace FlightNode.DataCollection.Domain.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class WindDrivenTide : DbMigration
	{
		public override void Up()
		{
			DropForeignKey("dbo.SurveyCompleted", "FK_dbo.SurveyCompleted_dbo.Tides_TideId");
			DropForeignKey("dbo.SurveyPending", "FK_dbo.SurveyPending_dbo.Tides_TideId");
			AddColumn("dbo.SurveyCompleted", "WindDrivenTide", c => c.Boolean());
			AddColumn("dbo.SurveyPending", "WindDrivenTide", c => c.Boolean());
			DropColumn("dbo.SurveyCompleted", "TideId");
			DropColumn("dbo.SurveyPending", "TideId");
			DropTable("dbo.Tides");

			this.DropStoredProcedure("dbo.ExportForagingSurveyResults");
			this.CreateStoredProcedure("dbo.ExportForagingSurveyResults", @"
BEGIN

	SET NOCOUNT ON;

	CREATE TABLE #Observations (
		Id INT,
		SurveyIdentifier UNIQUEIDENTIFIER,
		SiteCode VARCHAR(8000),
		SiteName VARCHAR(8000),
		City VARCHAR(8000),
		County VARCHAR(8000),
		Longitude VARCHAR(8000),
		Latitude VARCHAR(8000),
		Assessment  VARCHAR(8000),
		StartDate VARCHAR(8000),
		EndDate VARCHAR(8000),
		StartTemperature VARCHAR(8000),
		Weather VARCHAR(8000),
        TimeOfLowTide VARCHAR(8000),
		WaterHeight VARCHAR(8000),
		TideIsWindDriven VARCHAR(8000),
		WindSpeed VARCHAR(8000),
		VantagePoint VARCHAR(8000),
		AccessPoint VARCHAR(8000),
		SubmittedBy VARCHAR(8000),
		Observers VARCHAR(8000),
		GeneralComments VARCHAR(8000),
		Genus VARCHAR(8000),
		Species VARCHAR(8000),
		CommonAlphaCode VARCHAR(8000),
		CommonName VARCHAR(8000),
		NumberOfAdults VARCHAR(8000),
		NumberOfJuveniles VARCHAR(8000),
		PrimaryActivity VARCHAR(8000),
		SecondaryActivity VARCHAR(8000),
		FeedingSuccessRate VARCHAR(8000),
		HabitatType VARCHAR(8000),
		DisturbanceComments VARCHAR(8000),
		KayakerQuantity VARCHAR(8000),
		KayakerDurationMinutes VARCHAR(8000),
		KayakResult VARCHAR(8000),
		FishermenWadingQuantity VARCHAR(8000),
		FishermenWaidingDurationMinutes VARCHAR(8000),
		FishermenWaidingResult VARCHAR(8000),
		StationaryBoatsQuantity VARCHAR(8000),
		StationaryBoatsDurationMinutes VARCHAR(8000),
		StationaryBoatsResult VARCHAR(8000),
		MovingBoatsQuantity VARCHAR(8000),
		MovingBoatsDurationMinutes VARCHAR(8000),
		MovingBoatsResult VARCHAR(8000),
		PersonalWatercraftQuantity VARCHAR(8000),
		PersonalWatercraftDurationMinutes VARCHAR(8000),
		PersonalWatercraftResult VARCHAR(8000),
		HumansQuantity VARCHAR(8000),
		HumansMinutes VARCHAR(8000),
		HumansResult VARCHAR(8000),
		NoiseQuantity VARCHAR(8000),
		NoiseMinutes VARCHAR(8000),
		NoiseResult VARCHAR(8000),
		OtherDisturbanceQuantity VARCHAR(8000),
		OtherDisturbanceMinutes VARCHAR(8000),
		OtherDisturbanceResult VARCHAR(8000)
	)

	-- Load the initial observations list
	INSERT INTO #Observations (
		Id,
		SurveyIdentifier,
		SiteCode,
		SiteName,
		City,
		County,
		Longitude,
		Latitude,
		Assessment,
		StartDate,
		EndDate,
		StartTemperature,
		Weather,
        TimeOfLowTide,
		WaterHeight,
		TideIsWindDriven,
		WindSpeed,
		VantagePoint,
		AccessPoint,
		SubmittedBy,
		Observers,
		GeneralComments,
		Genus,
		Species,
		CommonAlphaCode,
		CommonName,
		NumberOfAdults,
		NumberOfJuveniles,
		PrimaryActivity,
		SecondaryActivity,
		FeedingSuccessRate,
		HabitatType,
		DisturbanceComments,
		KayakerQuantity,
		KayakerDurationMinutes,
		KayakResult,
		FishermenWadingQuantity,
		FishermenWaidingDurationMinutes,
		FishermenWaidingResult,
		StationaryBoatsQuantity,
		StationaryBoatsDurationMinutes,
		StationaryBoatsResult,
		MovingBoatsQuantity,
		MovingBoatsDurationMinutes,
		MovingBoatsResult,
		PersonalWatercraftQuantity,
		PersonalWatercraftDurationMinutes,
		PersonalWatercraftResult,
		HumansQuantity,
		HumansMinutes,
		HumansResult,
		NoiseQuantity,
		NoiseMinutes,
		NoiseResult,
		OtherDisturbanceQuantity,
		OtherDisturbanceMinutes,
		OtherDisturbanceResult
	)
	SELECT 
		obs.Id,
		svy.SurveyIdentifier,
		loc.SiteCode,
		loc.SiteName,
		loc.City,
		loc.County,
		loc.Longitude,
		loc.Latitude,
		assess.Description as Assessment,
		svy.StartDate,
		svy.EndDate,
		svy.StartTemperature,
		weather.Description as Weather,
        svy.TimeOfLowTide,
		water.Description as WaterHeight,
		CASE WHEN svy.WindDrivenTide = 1 THEN 'true' else 'false' END as TideIsWindDriven,
		svy.WindSpeed,
		vntg.Description as VantagePoint,
		access.Description as AccessPoint,
		usr.GivenName + ' ' + usr.FamilyName as SubmittedBy,
		ISNULL(svy.Observers,'') as Observers,
		ISNULL(svy.GeneralComments, '') as GeneralComments,
		bs.Genus,
		bs.Species,
		bs.CommonAlphaCode,
		bs.CommonName,
		obs.Bin1 as NumberOfAdults,
		obs.Bin2 as NumberOfJuveniles,
		actp.Description as PrimaryActivity,
		acts.Description as SecondaryActivity,
		fr.Description as FeedingSuccessRate,
		hab.Description as HabitatType,
		ISNULL(svy.DisturbanceComments, '') as DisturbanceComments,
		'0' as KayakerQuantity,
		'0' as KayakerDurationMinutes,
		'' as KayakResult,
		'0' as FishermenWadingQuantity,
		'0' as FishermenWaidingDurationMinutes,
		'' as FishermenWaidingResult,
		'0' as StationaryBoatsQuantity,
		'0' as StationaryBoatsDurationMinutes,
		'' as StationaryBoatsResult,
		'0' as MovingBoatsQuantity,
		'0' as MovingBoatsDurationMinutes,
		'' as MovingBoatsResult,
		'0' as PersonalWatercraftQuantity,
		'0' as PersonalWatercraftDurationMinutes,
		'' as PersonalWatercraftResult,
		'0' as HumansQuantity,
		'0' as HumansMinutes,
		'' as HumansResult,
		'0' as NoiseQuantity,
		'0' as NoiseMinutes,
		'' as NoiseResult,
		'0' as OtherDisturbanceQuantity,
		'0' as OtherDisturbanceMinutes,
		'' as OtherDisturbanceResult
	FROM dbo.SurveyCompleted svy
	LEFT OUTER JOIN dbo.Observations obs ON svy.SurveyIdentifier = obs.SurveyIdentifier
										AND (obs.Bin1 > 0 OR obs.Bin2 > 0)
	LEFT OUTER JOIN dbo.BirdSpecies bs ON obs.BirdSpeciesId = bs.Id
	LEFT OUTER JOIN dbo.FeedingSuccessRates fr ON obs.FeedingSuccessRate = fr.Id
	LEFT OUTER JOIN dbo.SurveyActivities actp ON obs.PrimaryActivityId = actp.Id
	LEFT OUTER JOIN dbo.SurveyActivities acts ON obs.SecondaryActivityId = acts.Id
	LEFT OUTER JOIN dbo.HabitatTypes hab ON obs.HabitatTypeId = hab.Id
	INNER JOIN dbo.Locations loc ON svy.LocationId = loc.Id
	INNER JOIN dbo.AccessPoints access ON svy.AccessPointId = access.Id
	INNER JOIN dbo.SiteAssessments assess ON svy.AssessmentId = assess.Id
	INNER JOIN dbo.WaterHeights water ON svy.WaterHeightId = water.Id
	INNER JOIN dbo.Users usr ON svy.SubmittedBy = usr.Id
	INNER JOIN dbo.VantagePoints vntg ON svy.VantagePointId = vntg.Id
	INNER JOIN dbo.Weather weather ON svy.WeatherId = weather.Id
	WHERE svy.SurveyTypeId = 2


	-- Add an index on SurveyIdentifier for performance
	CREATE INDEX IX_SurveyIdentifier ON #Observations (SurveyIdentifier);

	-- Kayers
	UPDATE o SET 
		KayakerDurationMinutes = d.DurationMinutes,
		KayakerQuantity = d.Quantity,
		KayakResult = d.Result
	FROM #Observations o
	INNER JOIN dbo.Disturbances d ON o.SurveyIdentifier = d.SurveyIdentifier
	WHERE d.DisturbanceTypeId = 1

	-- Fishermen wading
	UPDATE o SET 
		FishermenWaidingDurationMinutes = d.DurationMinutes,
		FishermenWadingQuantity = d.Quantity,
		FishermenWaidingResult = d.Result
	FROM #Observations o
	INNER JOIN dbo.Disturbances d ON o.SurveyIdentifier = d.SurveyIdentifier
	WHERE d.DisturbanceTypeId = 2

	-- Stationary boats
	UPDATE o SET 
		StationaryBoatsDurationMinutes = d.DurationMinutes,
		StationaryBoatsQuantity = d.Quantity,
		StationaryBoatsResult = d.Result
	FROM #Observations o
	INNER JOIN dbo.Disturbances d ON o.SurveyIdentifier = d.SurveyIdentifier
	WHERE d.DisturbanceTypeId = 3

	-- Moving boats
	UPDATE o SET 
		MovingBoatsDurationMinutes = d.DurationMinutes,
		MovingBoatsQuantity = d.Quantity,
		MovingBoatsResult = d.Result
	FROM #Observations o
	INNER JOIN dbo.Disturbances d ON o.SurveyIdentifier = d.SurveyIdentifier
	WHERE d.DisturbanceTypeId = 4

	-- Personal Watercraft
	UPDATE o SET 
		PersonalWatercraftDurationMinutes = d.DurationMinutes,
		PersonalWatercraftQuantity = d.Quantity,
		PersonalWatercraftResult = d.Result
	FROM #Observations o
	INNER JOIN dbo.Disturbances d ON o.SurveyIdentifier = d.SurveyIdentifier
	WHERE d.DisturbanceTypeId = 5

	-- Humans
	UPDATE o SET 
		HumansMinutes = d.DurationMinutes,
		HumansQuantity = d.Quantity,
		HumansResult = d.Result
	FROM #Observations o
	INNER JOIN dbo.Disturbances d ON o.SurveyIdentifier = d.SurveyIdentifier
	WHERE d.DisturbanceTypeId = 6

	-- Humans
	UPDATE o SET 
		NoiseMinutes = d.DurationMinutes,
		NoiseQuantity = d.Quantity,
		NoiseResult = d.Result
	FROM #Observations o
	INNER JOIN dbo.Disturbances d ON o.SurveyIdentifier = d.SurveyIdentifier
	WHERE d.DisturbanceTypeId = 7

	-- Other
	UPDATE o SET 
		OtherDisturbanceMinutes = d.DurationMinutes,
		OtherDisturbanceQuantity = d.Quantity,
		OtherDisturbanceResult = d.Result
	FROM #Observations o
	INNER JOIN dbo.Disturbances d ON o.SurveyIdentifier = d.SurveyIdentifier
	WHERE d.DisturbanceTypeId = 8


	-- Final selection
	SELECT * FROM #Observations

	DROP TABLE #Observations;

END");
		}
		
		public override void Down()
		{
			CreateTable(
				"dbo.Tides",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						Description = c.String(nullable: false, maxLength: 100),
					})
				.PrimaryKey(t => t.Id);
			
			AddColumn("dbo.SurveyPending", "TideId", c => c.Int());
			AddColumn("dbo.SurveyCompleted", "TideId", c => c.Int());
			DropColumn("dbo.SurveyPending", "WindDrivenTide");
			DropColumn("dbo.SurveyCompleted", "WindDrivenTide");
		}
	}
}
