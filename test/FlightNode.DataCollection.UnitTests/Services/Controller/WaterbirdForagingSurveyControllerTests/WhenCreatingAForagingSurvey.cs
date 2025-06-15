using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Services.Models.Survey;
using Moq;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Controller.WaterbirdForagingSurveyControllerTests
{


    public class WhenCreatingAForagingSurvey
    {

        public class HappyPath : Fixture
        {

            [Fact]
            public void MapsPrepTimeHours()
            {
                RunPositiveTest();

                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => PrepTime == y.PrepTimeHours)));
            }

            [Fact]
            public void MapsWindDirection()
            {
                RunPositiveTest();

                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => WindDirection == y.WindDirection)));
            }

            [Fact]
            public void MapsWaterHeightId()
            {
                RunPositiveTest();

                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => WaterHeightId == y.WaterHeightId)));
            }

            [Fact]
            public void MapsAccessPoint()
            {
                RunPositiveTest();

                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => AccessPoint == y.AccessPointId)));
            }

            [Fact]
            public void MapsDisturbanceComment()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => Disturbed == y.DisturbanceComments)));
            }


            [Fact]
            public void MapsEndDate()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => DateValuesAreClose(EndDate, y.EndDate.Value))));
            }

            [Fact]
            public void MapsShortEndDate()
            {
                RunPositiveTest(true);
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => DateValuesAreClose(EndDate, y.EndDate.Value))));
            }

            [Fact]
            public void MapsStartDate()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => DateValuesAreClose(StartDate, y.StartDate.Value))));
            }

            [Fact]
            public void MapsShortStartDate()
            {
                RunPositiveTest(true);
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => DateValuesAreClose(StartDate, y.StartDate.Value))));
            }

            [Fact]
            public void MapsLocationId()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => LocationId == y.LocationId)));
            }


            [Fact]
            public void MapsSiteTypeIdToAssessment()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => SiteTypeId == y.AssessmentId)));
            }

            [Fact]
            public void MapsSurveyCommentsToGeneralComments()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => SurveyComments == y.GeneralComments)));
            }

            [Fact]
            public void MapsTemperature()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => Temperature == y.Temperature)));
            }

            [Fact]
            public void MapsTide()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => WindDrivenTide == y.WindDrivenTide)));
            }

            [Fact]
            public void MapsVantagePoint()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => VantagePoint == y.VantagePointId)));
            }

            [Fact]
            public void MapsWeather()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => Weather == y.WeatherId)));
            }

            [Fact]
            public void MapsWindSpeed()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => WindSpeed == y.WindSpeed)));
            }

            [Fact]
            public void MapsDisturbedBehavior()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => DisturbedBehavior == y.Disturbances.First().Result)));
            }

            [Fact]
            public void MapsDisturbanceTypeId()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => DisturbedTypeId == y.Disturbances.First().DisturbanceTypeId)));
            }

            [Fact]
            public void MapsDisturbanceId()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => DisturbanceId == y.Disturbances.First().Id)));
            }

            [Fact]
            public void MapsDisturbanceDuration()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => DisturbedDuration == y.Disturbances.First().DurationMinutes)));
            }

            [Fact]
            public void MapsDisturbanceQuantity()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => DisturbedQuantity == y.Disturbances.First().Quantity)));
            }

            [Fact]
            public void MapsIdentifierIntoDisturbance()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => Identifier == y.Disturbances.First().SurveyIdentifier)));
            }

            [Fact]
            public void MapsObserver()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => Observers == y.Observers)));
            }

            [Fact]
            public void MapsIdentifierIntoObservation()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => Identifier == y.Observations.First().SurveyIdentifier)));
            }

            [Fact]
            public void MapsObservationId()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => ObservationId == y.Observations.First().Id)));
            }

            [Fact]
            public void MapAdultsToBin1()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => Adults == y.Observations.First().Bin1)));
            }


            [Fact]
            public void MapJuvenilesToBin2()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => Juveniles == y.Observations.First().Bin2)));
            }

            [Fact]
            public void MapPrimaryBehavior()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => PrimaryActivityId == y.Observations.First().PrimaryActivityId)));
            }

            [Fact]
            public void MapSecondaryBehavior()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => SecondaryActivityId == y.Observations.First().SecondaryActivityId)));
            }

            [Fact]
            public void MapSpeciesId()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => SpeciesId == y.Observations.First().BirdSpeciesId)));
            }

            [Fact]
            public void MapsFeedingId()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => FeedingId == y.Observations.First().FeedingSuccessRate)));
            }

            [Fact]
            public void MapsHabitatId()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => HabitatId == y.Observations.First().HabitatTypeId)));
            }

            [Fact]
            public void MapsSurveyTypeId()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => SurveyType.Foraging == y.SurveyTypeId)));
            }



            public override void Dispose()
            {
                // Restore delegate extension method to default behavior
                ExtensionDelegate.Init();

                base.Dispose();
            }

            [Fact]
            public void MapsSubmittedBy()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<SurveyPending>(y => SubmittedBy == y.SubmittedBy)));
            }


            [Fact]
            public void RespondsWithCreated()
            {
                var result = RunPositiveTest() as CreatedNegotiatedContentResult<WaterbirdForagingModel>;

                //
                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void RespondsWithLocation()
            {
                // Arrange
                var expected = Url + Identifier;

                // Act
                var result = RunPositiveTest() as CreatedNegotiatedContentResult<WaterbirdForagingModel>;

                // Assert
                result.Should().NotBeNull();
                // ReSharper disable once PossibleNullReferenceException
                result.Location.ToString().Should().Be(expected);
            }

            private IHttpActionResult RunPositiveTest(bool useShort = false)
            {
                //
                // Arrange
                WaterbirdForagingModel input = CreateDefautInput();

                if (useShort)
                {
                    input.StartDate = StartDateStringShort;
                    input.StartTime = StartTimeStringShort;
                    input.EndTime = EndTimeStringShort;
                }

                MockDomainManager.Setup(x => x.NewIdentifier())
                    .Returns(Identifier);

                MockDomainManager.Setup(x => x.Create(It.IsAny<SurveyPending>()))
                    .Returns((SurveyPending actual) => actual);


                var system = BuildSystem();

                //
                // Act
                return system.Post(input);
            }


        }

        public class ExceptionHandling : Fixture
        {

            [Fact]
            public void NullInputShouldBeTreatedAsABadRequest()
            {

                var result = BuildSystem().Post(null).ExecuteAsync(new System.Threading.CancellationToken()).Result;

                Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            }
        }
    }
}
