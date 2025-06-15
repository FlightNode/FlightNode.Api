using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Infrastructure.Customization;
using FlightNode.DataCollection.Infrastructure.Persistence;
using System.Data.Entity.Migrations;

namespace FlightNode.DataCollection.Domain.Migrations
{


    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public sealed class Configuration : DbMigrationsConfiguration<DataCollectionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(DataCollectionContext context)
        {
            LoadDisturbanceTypesForWaterbirdForaging(context);
            LoadWeatherForWaterbirdForaging(context);
            LoadHabitatTypes(context);
            LoadFeedingSuccessRates(context);
            LoadActivityTypes(context);
            LoadSiteAssessments(context);
            LoadVantagePoints(context);
            LoadAccessPoints(context);
            LoadWaterHeightsForWaterbirdForaging(context);
            LoadWindDirection(context);
            LoadWindSpeeds(context);
        }

        private void LoadWindSpeeds(DataCollectionContext context)
        {
            context.WindSpeed.AddOrUpdate(x => x.Id, WindSpeed.FiveToTen);
            context.WindSpeed.AddOrUpdate(x => x.Id, WindSpeed.MoreThanFifteen);
            context.WindSpeed.AddOrUpdate(x => x.Id, WindSpeed.TenToFifteen);
            context.WindSpeed.AddOrUpdate(x => x.Id, WindSpeed.ZeroToFive);
        }

        private void LoadWindDirection(DataCollectionContext context)
        {
            context.WindDirections.AddOrUpdate(x => x.Id, WindDirection.East);
            context.WindDirections.AddOrUpdate(x => x.Id, WindDirection.North);
            context.WindDirections.AddOrUpdate(x => x.Id, WindDirection.NorthEast);
            context.WindDirections.AddOrUpdate(x => x.Id, WindDirection.NorthWest);
            context.WindDirections.AddOrUpdate(x => x.Id, WindDirection.South);
            context.WindDirections.AddOrUpdate(x => x.Id, WindDirection.SouthEast);
            context.WindDirections.AddOrUpdate(x => x.Id, WindDirection.SouthWest);
            context.WindDirections.AddOrUpdate(x => x.Id, WindDirection.West);
        }

        private void LoadAccessPoints(DataCollectionContext context)
        {
            context.AccessPoints.AddOrUpdate(x => x.Description, new AccessPoint { Description = "On Foot" });
            context.AccessPoints.AddOrUpdate(x => x.Description, new AccessPoint { Description = "Vessel (motor)" });
            context.AccessPoints.AddOrUpdate(x => x.Description, new AccessPoint { Description = "Kayak/Canoe" });
        }

        private void LoadVantagePoints(DataCollectionContext context)
        {
            context.VantagePoints.AddOrUpdate(x => x.Description, new VantagePoint { Description = "On-Site Visit" });
            context.VantagePoints.AddOrUpdate(x => x.Description, new VantagePoint { Description = "View from Adjacent Area by Vehicle/Boat/Foot" });
        }

        private void LoadSiteAssessments(DataCollectionContext context)
        {
            context.SiteAssessments.AddOrUpdate(x => x.Description, new SiteAssessment { Description = "New Site" });
            context.SiteAssessments.AddOrUpdate(x => x.Description, new SiteAssessment { Description = "Previously Submitted Site" });
            context.SiteAssessments.AddOrUpdate(x => x.Description, new SiteAssessment { Description = "Unknown" });
        }

        private void LoadActivityTypes(DataCollectionContext context)
        {
            context.SurveyActivities.AddOrUpdate(x => x.Description, new SurveyActivity { Description = "Feeding" });
            context.SurveyActivities.AddOrUpdate(x => x.Description, new SurveyActivity { Description = "Preening" });
            context.SurveyActivities.AddOrUpdate(x => x.Description, new SurveyActivity { Description = "Loafing" });
            context.SurveyActivities.AddOrUpdate(x => x.Description, new SurveyActivity { Description = "Fly Over" });
            context.SurveyActivities.AddOrUpdate(x => x.Description, new SurveyActivity { Description = "N/A" });
        }

        private void LoadFeedingSuccessRates(DataCollectionContext context)
        {
            context.FeedingSuccessRates.AddOrUpdate(x => x.Description, new FeedingSuccessRate { Description = "0-25% capture/strikes low success" });
            context.FeedingSuccessRates.AddOrUpdate(x => x.Description, new FeedingSuccessRate { Description = "25-50% capture/strikes medium success" });
            context.FeedingSuccessRates.AddOrUpdate(x => x.Description, new FeedingSuccessRate { Description = "50-75% capture/strikes high success" });
            context.FeedingSuccessRates.AddOrUpdate(x => x.Description, new FeedingSuccessRate { Description = "N/A" });
        }

        private void LoadHabitatTypes(DataCollectionContext context)
        {
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "Open Water Below Knee" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "Open Water Above Knee" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "Manmade (ditch, culvert, etc.)" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "Forest" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "Stream" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "Low Marsh" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "High Marsh" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "Scrub-Shrub" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "Tall Grass" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "OpenWater" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "Mudflat" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "Pond" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "Shoreline" });
            context.HabitatTypes.AddOrUpdate(x => x.Description, new HabitatType { Description = "N/A" });
        }

        private void LoadWeatherForWaterbirdForaging(DataCollectionContext context)
        {
            context.Weather.AddOrUpdate(x => x.Description, new Weather { Description = "Clear" });
            context.Weather.AddOrUpdate(x => x.Description, new Weather { Description = "Fog or smoke" });
            context.Weather.AddOrUpdate(x => x.Description, new Weather { Description = "Partly cloudy" });
            context.Weather.AddOrUpdate(x => x.Description, new Weather { Description = "Drizzle" });
            context.Weather.AddOrUpdate(x => x.Description, new Weather { Description = "Overcast" });
            context.Weather.AddOrUpdate(x => x.Description, new Weather { Description = "Showers" });
        }        

        private void LoadWaterHeightsForWaterbirdForaging(DataCollectionContext context)
        {
            context.WaterHeights.AddOrUpdate(x => x.Id, new WaterHeight { Id = 1, Description = "Appears High" });
            context.WaterHeights.AddOrUpdate(x => x.Id, new WaterHeight { Id = 2, Description = "Normal" });
            context.WaterHeights.AddOrUpdate(x => x.Id, new WaterHeight { Id = 3, Description = "Appears Low" });
        }

        private void LoadDisturbanceTypesForWaterbirdForaging(DataCollectionContext context)
        {
            context.DisturbanceTypes.AddOrUpdate(x => x.Description, new DisturbanceType { Description = "Kayakers" });
            context.DisturbanceTypes.AddOrUpdate(x => x.Description, new DisturbanceType { Description = "Fishermen (wading)" });
            context.DisturbanceTypes.AddOrUpdate(x => x.Description, new DisturbanceType { Description = "Stationary boats" });
            context.DisturbanceTypes.AddOrUpdate(x => x.Description, new DisturbanceType { Description = "Moving boats" });
            context.DisturbanceTypes.AddOrUpdate(x => x.Description, new DisturbanceType { Description = "Personal watercraft (jetski, windsurfer, etc.)" });
            context.DisturbanceTypes.AddOrUpdate(x => x.Description, new DisturbanceType { Description = "Humans on foot" });
            context.DisturbanceTypes.AddOrUpdate(x => x.Description, new DisturbanceType { Description = "Noise (specify source)" });
            context.DisturbanceTypes.AddOrUpdate(x => x.Description, new DisturbanceType { Description = "Other (describe in comment box)" });
        }
    }
}
