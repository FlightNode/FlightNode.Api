using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Services.Controllers;
using FlightNode.DataCollection.Services.Models.Survey;
using System;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Controller.RookeryCensusControllerTests
{

    public class Fixture : LoggingControllerBaseFixture<RookeryCensusController, ISurveyManager>
    {
        protected readonly Guid Identifier = new Guid("a507f681-c111-447a-bc1f-195916891226");
        protected const int AccessPoint = 1;
        protected const string Disturbed = "disturbed";
        protected const string DisturbedBehavior = "!#%#@#%";
        protected const int DisturbedTypeId = 2;
        protected const int DisturbedDuration = 234;
        protected const int DisturbedQuantity = 3;
        protected readonly DateTime EndDate = new DateTime(2016, 6, 11, 20, 05, 0);
        protected readonly string EndDateString = "2016-06-11T05:00:00.000Z";
        protected readonly string EndDateStringShort = "2016-06-11";
        protected readonly string EndTimeString = "1970-01-01T20:05:00.000Z";
        protected readonly string EndTimeStringShort = "8:05 PM";
        protected readonly DateTime StartDate = new DateTime(2016, 6, 11, 18, 01, 0);
        protected readonly string StartDateString = "2016-06-11T05:00:00.000Z";
        protected readonly string StartDateStringShort = "2016-06-11";
        protected readonly string StartTimeString = "1970-01-01T18:01:00.000Z";
        protected readonly string StartTimeStringShort = "6:01 PM";
        protected const int LocationId = 4;
        protected const int SpeciesId = 7;
        protected const int ObserverId = 13;
        protected const string Observers = "a, b, and c";
        protected const int SiteTypeId = 14;
        protected const int Step = 1;
        protected const string SurveyComments = "Survey comments";
        protected const int VantagePoint = 18;
        protected const int SurveyId = 21;
        protected const int ObservationId = 22;
        protected const int DisturbanceId = 23;
        protected const string LocationName = "Charlie's Pasture";
        protected const int SubmittedBy = 25;
        protected const decimal PrepTime = 23.99m;
        protected const bool NestsPresent = true;
        protected const bool ChicksPresent = true;
        protected const bool FledglingsPresent = true;
        protected const int NumberOfAdults = 90203;

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
                Bin1 = NumberOfAdults,
                BirdSpeciesId = SpeciesId,                
                SurveyIdentifier = Identifier,
                ChicksPresent = ChicksPresent,
                FledglingPresent = FledglingsPresent,
                NestPresent = NestsPresent
            });
            domainResult.Observers = Observers;
            domainResult.StartDate = StartDate;
            domainResult.SubmittedBy = 14;
            domainResult.SurveyIdentifier = Identifier;
            domainResult.SurveyTypeId = 15;
            domainResult.VantagePointId = VantagePoint;
            domainResult.LocationName = LocationName;
            domainResult.PrepTimeHours = PrepTime;

            return domainResult;
        }

        protected RookeryCensusModel CreateDefautInput()
        {
            var input = new RookeryCensusModel
            {
                AccessPointId = AccessPoint,
                DisturbanceComments = Disturbed,
                LocationId = LocationId,
                SiteTypeId = SiteTypeId,
                SurveyComments = SurveyComments,
                VantagePointId = VantagePoint,
                SurveyId = SurveyId,
                StartDate = StartDateString,
                StartTime = StartTimeString,
                EndTime = EndTimeString,
                Observers = Observers,
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
                Adults = NumberOfAdults,
                BirdSpeciesId = SpeciesId,
                ObservationId = ObservationId,
                NestsPresent = NestsPresent,
                FledglingsPresent = FledglingsPresent,
                ChicksPresent = ChicksPresent
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
                VantagePointId = VantagePoint,
                Id = SurveyId,
                StartDate = StartDate,
                EndDate = EndDate,
                Observers = Observers,
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
                Bin1 = NumberOfAdults,
                BirdSpeciesId = SpeciesId,
                NestPresent = NestsPresent,
                ChicksPresent = ChicksPresent,
                FledglingPresent = FledglingsPresent,
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
            Assert.Throws<ArgumentNullException>(() => new RookeryCensusController(null));
        }
    }



}
