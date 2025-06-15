using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Services.Models.Survey;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Controller.WaterbirdForagingSurveyControllerTests
{
    public class WhenGettingASingleForagingSurvey : Fixture
    {
        [Fact]
        public void MapsPrepTimeHours()
        {
            var domain = BuildDefaultSurvey();
            var result = RunHappyPath(domain);
            Assert.Equal(domain.PrepTimeHours, result.PrepTimeHours);
        }

        [Fact]
        public void MapsSubmittedBy()
        {
            var domain = BuildDefaultSurvey();
            var result = RunHappyPath(domain);
            Assert.Equal(domain.SubmittedBy, result.SubmittedBy);
        }

        [Fact]
        public void MapsWindDirection()
        {
            var domain = BuildDefaultSurvey();
            var result = RunHappyPath(domain);
            Assert.Equal(domain.WindDirection, result.WindDirection);
        }

        [Fact]
        public void MapsWaterHeightId()
        {
            var domain = BuildDefaultSurvey();
            var result = RunHappyPath(domain);
            Assert.Equal(domain.WaterHeightId, result.WaterHeightId);
        }

        [Fact]
        public void MapsAccessPointId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.AccessPointId, result.AccessPointId);
        }

        [Fact]
        public void MapsAssessmentId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.AssessmentId, result.SiteTypeId);
        }

        [Fact]
        public void MapsDisturbanceComments()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.DisturbanceComments, result.DisturbanceComments);
        }

        [Fact]
        public void MapsDisturbance_Id()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Disturbances.First().Id, result.Disturbances.First().DisturbanceId);
        }

        [Fact]
        public void MapsDisturbance_DisturbanceTypeId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Disturbances.First().DisturbanceTypeId, result.Disturbances.First().DisturbanceTypeId);
        }

        [Fact]
        public void MapsDisturbance_Result()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Disturbances.First().Result, result.Disturbances.First().Behavior);
        }


        [Fact]
        public void MapsDisturbance_DurationMinutes()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Disturbances.First().DurationMinutes, result.Disturbances.First().DurationMinutes);
        }

        [Fact]
        public void MapsDisturbance_Quantity()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Disturbances.First().Quantity, result.Disturbances.First().Quantity);
        }

        [Fact]
        public void MapsEndDate()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            // TODO: date and time
            //Assert.Equal(domain.EndDate, result.EndDate);
        }

        [Fact]
        public void MapsLocationId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.LocationId, result.LocationId);
        }

        [Fact]
        public void MapsObservation_Adults()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Observations.First().Bin1, result.Observations.First().Adults);
        }

        [Fact]
        public void MapsObservation_Juveniles()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Observations.First().Bin2, result.Observations.First().Juveniles);
        }

        [Fact]
        public void MapsObservation_BirdSpeciesId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Observations.First().BirdSpeciesId, result.Observations.First().BirdSpeciesId);
        }

        [Fact]
        public void MapsObservation_FeedingId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Observations.First().FeedingSuccessRate, result.Observations.First().FeedingId);
        }



        [Fact]
        public void MapsObservation_HabitatId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Observations.First().HabitatTypeId, result.Observations.First().HabitatId);
        }

        [Fact]
        public void MapsObservation_ObservationId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Observations.First().Id, result.Observations.First().ObservationId);
        }

        [Fact]
        public void MapsObservation_PrimaryActivityId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Observations.First().PrimaryActivityId, result.Observations.First().PrimaryActivityId);
        }

        [Fact]
        public void MapsObservation_SecondaryActivityId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Observations.First().SecondaryActivityId, result.Observations.First().SecondaryActivityId);
        }


        [Fact]
        public void MapsStartDate()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            // TODO: date and time
            //Assert.Equal(domain.StartDate, result.StartDate);
        }


        [Fact]
        public void MapsSurveyComments()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.GeneralComments, result.SurveyComments);
        }

        [Fact]
        public void MapsSurveyId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Id, result.SurveyId);
        }

        [Fact]
        public void MapsSurveyIdentifier()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.SurveyIdentifier, result.SurveyIdentifier);
        }

        [Fact]
        public void MapsTemperature()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.Temperature, result.Temperature);
        }

        [Fact]
        public void MapsTideInfoId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.WindDrivenTide, result.WindDrivenTide);
        }

        [Fact]
        public void MapsVantagePointInfoId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.VantagePointId, result.VantagePointId);
        }

        [Fact]
        public void MapsWeatherInfoId()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.WeatherId, result.WeatherId);
        }

        [Fact]
        public void MapsWindSpeed()
        {
            var domain = BuildDefaultSurvey();

            var result = RunHappyPath(domain);

            Assert.Equal(domain.WindSpeed, result.WindSpeed);
        }

        [Fact]
        public void MapsObservers()
        {
            var domain = BuildDefaultSurvey();
            var result = RunHappyPath(domain);
            Assert.Equal(domain.Observers, result.Observers);
        }

        private WaterbirdForagingModel RunHappyPath(ISurvey domain)
        {
            SetupMockResult(domain, SurveyType.Foraging);

            var result = (RunTest() as OkNegotiatedContentResult<WaterbirdForagingModel>)
                            .Content;
            return result;
        }



        [Fact]
        public void MatchingItemGeneratesStatus200()
        {
            SetupMockResult(BuildDefaultSurvey(), SurveyType.Foraging);

            var result = RunTest();
            Assert.IsType<OkNegotiatedContentResult<WaterbirdForagingModel>>(result);
        }

        [Fact]
        public void NoMatchGenerates404()
        {
            //
            // Arrange
            SetupMockResult(null, SurveyType.Foraging);
            //
            // Act
            var result = RunTest();

            //
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private void SetupMockResult(ISurvey domainResult, int surveyTypeId)
        {
            MockDomainManager.Setup(x => x.FindBySurveyId(Identifier, surveyTypeId))
                .Returns(domainResult);
        }

        private IHttpActionResult RunTest()
        {
            var system = BuildSystem();

            system.Logger = MockLogger.Object;

            return system.Get(Identifier);
        }
    }
}
