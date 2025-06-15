using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Services.Controllers;
using FlightNode.DataCollection.Services.Models.Survey;
using System;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Controller.WaterbirdForagingSurveyControllerTests
{

    public class Fixture : LoggingControllerBaseFixture<WaterbirdForagingSurveyController, ISurveyManager>
    {
        protected readonly Guid Identifier = new Guid("a507f681-c111-447a-bc1f-195916891226");
        protected const int AccessPoint = 1;
        protected const string Disturbed = "disturbed";
        protected const string DisturbedBehavior = "!#%#@#%";
        protected const int DisturbedTypeId = 2;
        protected const int DisturbedDuration = 234;
        protected const int DisturbedQuantity = 3;
        protected readonly DateTime EndDate = new DateTime(2016, 6, 11, 20, 05, 0);
        protected const string EndDateString = "2016-06-11T05:00:00.000Z";
        protected const string EndDateStringShort = "2016-06-11";
        protected const string EndTimeString = "1970-01-01T20:05:00.000Z";
        protected const string EndTimeStringShort = "8:05 PM";
        protected readonly DateTime StartDate = new DateTime(2016, 6, 11, 18, 01, 0);
        protected const string StartDateStringUsFormat = "06/11/2016";
        protected const string StartDateString = "2016-06-11T05:00:00.000Z";
        protected const string StartDateStringShort = "2016-06-11";
        protected const string StartTimeString = "1970-01-01T18:01:00.000Z";
        protected const string StartTimeStringShort = "6:01 PM";
        protected const int LocationId = 4;
        protected const int Adults = 6;
        protected const int SpeciesId = 7;
        protected const int FeedingId = 8;
        protected const int HabitatId = 9;
        protected const int Juveniles = 10;
        protected const int PrimaryActivityId = 11;
        protected const int SecondaryActivityId = 12;
        protected const int ObserverId = 13;
        protected const string Observers = "a, b, and c";
        protected const int SiteTypeId = 14;
        protected const int Step = 1;
        protected const string SurveyComments = "Survey comments";
        protected const decimal Temperature = 16.0m;
        protected const bool WindDrivenTide = true;
        protected const int VantagePoint = 18;
        protected const int Weather = 19;
        protected const int WindSpeed = 20;
        protected const int WindDirection = 201;
        protected const int SurveyId = 21;
        protected const int ObservationId = 22;
        protected const int DisturbanceId = 23;
        protected const string LocationName = "Charlie's Pasture";
        protected const int WaterHeightId = 24;
        protected const int SubmittedBy = 25;
        protected const decimal PrepTime = 23.99m;

        protected ISurvey BuildDefaultSurvey()
        {
            var domainResult = new SurveyPending();
            domainResult.AccessPointId = AccessPoint;
            domainResult.AssessmentId = SiteTypeId;
            domainResult.DisturbanceComments = Disturbed;
            domainResult.Disturbances.Add(new Disturbance
            {
                Id = DisturbanceId,
                DisturbanceTypeId = DisturbedTypeId,
                DurationMinutes = DisturbedDuration,
                Quantity = DisturbedQuantity,
                Result = DisturbedBehavior,
                SurveyIdentifier = Identifier
            });
            domainResult.EndDate = EndDate;
            domainResult.GeneralComments = SurveyComments;
            domainResult.Id = SurveyId;
            domainResult.LocationId = LocationId;
            domainResult.Observations.Add(new Observation
            {
                Id = ObservationId,
                Bin1 = Adults,
                Bin2 = Juveniles,
                BirdSpeciesId = SpeciesId,
                FeedingSuccessRate = FeedingId,
                HabitatTypeId = HabitatId,
                PrimaryActivityId = PrimaryActivityId,
                SecondaryActivityId = SecondaryActivityId,
                SurveyIdentifier = Identifier
            });
            domainResult.Observers = Observers;
            domainResult.StartDate = StartDate;
            domainResult.Temperature = Temperature;
            domainResult.SubmittedBy = 14;
            domainResult.SurveyIdentifier = Identifier;
            domainResult.SurveyTypeId = 15;
            domainResult.WindDrivenTide = WindDrivenTide;
            domainResult.VantagePointId = VantagePoint;
            domainResult.WeatherId = Weather;
            domainResult.WindSpeed = WindSpeed;
            domainResult.LocationName = LocationName;
            domainResult.WaterHeightId = WaterHeightId;
            domainResult.WindDirection = WindDirection;
            domainResult.PrepTimeHours = PrepTime;

            return domainResult;
        }

        protected WaterbirdForagingModel CreateDefautInput()
        {
            var input = new WaterbirdForagingModel
            {
                AccessPointId = AccessPoint,
                DisturbanceComments = Disturbed,
                LocationId = LocationId,
                SiteTypeId = SiteTypeId,
                SurveyComments = SurveyComments,
                Temperature = Temperature,
                WindDrivenTide = WindDrivenTide,
                VantagePointId = VantagePoint,
                WeatherId = Weather,
                WindSpeed = WindSpeed,
                SurveyId = SurveyId,
                WaterHeightId = WaterHeightId,
                StartDate = StartDateStringUsFormat,
                StartTime = StartTimeString,
                EndTime = EndTimeString,
                Observers = Observers,
                WindDirection = WindDirection,
                SubmittedBy = SubmittedBy,
                PrepTimeHours = PrepTime
            };
            input.Disturbances.Add(new DisturbanceModel
            {
                Behavior = DisturbedBehavior,
                DisturbanceTypeId = DisturbedTypeId,
                DurationMinutes = DisturbedDuration,
                Quantity = DisturbedQuantity,
                DisturbanceId = DisturbanceId
            });
            input.Observations.Add(new ObservationModel
            {
                Adults = Adults,
                BirdSpeciesId = SpeciesId,
                FeedingId = FeedingId,
                HabitatId = HabitatId,
                Juveniles = Juveniles,
                PrimaryActivityId = PrimaryActivityId,
                SecondaryActivityId = SecondaryActivityId,
                ObservationId = ObservationId
            });
            return input;
        }
        protected SurveyPending CreateDefaultEntity()
        {
            var input = new SurveyPending
            {
                AccessPointId = AccessPoint,
                DisturbanceComments = Disturbed,
                LocationId = LocationId,
                AssessmentId = SiteTypeId,
                GeneralComments = SurveyComments,
                Temperature = Temperature,
                WindDrivenTide = WindDrivenTide,
                VantagePointId = VantagePoint,
                WeatherId = Weather,
                WindSpeed = WindSpeed,
                Id = SurveyId,
                WaterHeightId = WaterHeightId,
                StartDate = StartDate,
                EndDate = EndDate,
                Observers = Observers,
                WindDirection = WindDirection,
                SubmittedBy = SubmittedBy,
                PrepTimeHours = PrepTime
            };
            input.Disturbances.Add(new Disturbance
            {
                Result = DisturbedBehavior,
                DisturbanceTypeId = DisturbedTypeId,
                DurationMinutes = DisturbedDuration,
                Quantity = DisturbedQuantity,
                Id = DisturbanceId
            });
            input.Observations.Add(new Observation
            {
                Bin1 = Adults,
                BirdSpeciesId = SpeciesId,
                FeedingSuccessRate = FeedingId,
                HabitatTypeId = HabitatId,
                Bin2 = Juveniles,
                PrimaryActivityId = PrimaryActivityId,
                SecondaryActivityId = SecondaryActivityId,
                Id = ObservationId
            });
            return input;
        }

        protected bool DateValuesAreClose(DateTime one, DateTime two)
        {
            return Math.Abs((one - two).Milliseconds) < 1000;
        }

    }

    public class Constructor : Fixture
    {
        [Fact]
        public void HappyPath()
        {
            BuildSystem();
        }

        [Fact]
        public void RejectsNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new WaterbirdForagingSurveyController(null));
        }
    }



}
