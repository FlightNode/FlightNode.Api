using FlightNode.DataCollection.Domain.Entities;
using System;
using System.Linq;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Domain.Entities
{
    public class SurveyPendingTests
    {

        public class Constructor
        {
            [Fact]
            public void InitializesObservationsList()
            {
                var system = new SurveyPending();

                Assert.Equal(0, system.Observations.Count());
            }

            [Fact]
            public void InitializesDisturbancesList()
            {
                var system = new SurveyPending();

                Assert.Equal(0, system.Disturbances.Count());
            }
        }

        public class AddObservation
        {
            [Fact]
            public void AddsToCollection()
            {
                var item = new Observation();

                var system = new SurveyPending();

                system.Add(item);

                Assert.True(system.Observations.Contains(item));
            }

            [Fact]
            public void ReturnsSelf()
            {
                var item = new Observation();

                var system = new SurveyPending();

                var actual = system.Add(item);

                Assert.Same(system, actual);
            }

            [Fact]
            public void RejectsNullArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new SurveyPending().Add(null as Observation));
            }
        }

        public class AddDisturbance
        {
            [Fact]
            public void AddsToDisturbance()
            {
                var item = new Disturbance();

                var system = new SurveyPending();

                system.Add(item);

                Assert.True(system.Disturbances.Contains(item));
            }

            [Fact]
            public void ReturnsSelf()
            {
                var item = new Disturbance();

                var system = new SurveyPending();

                var actual = system.Add(item);

                Assert.Same(system, actual);
            }

            [Fact]
            public void RejectsNullArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new SurveyPending().Add(null as Disturbance));
            }
        }

        public class ToSurveyCompleted
        {

            protected readonly Guid Identifier = new Guid("a507f681-c111-447a-bc1f-195916891226");
            protected const int AccessPoint = 1;
            protected const string Disturbed = "disturbed";
            protected const string DisturbedBehavior = "!#%#@#%";
            protected const int DisturbedTypeId = 2;
            protected const int DisturbedDuration = 234;
            protected const int DisturbedQuantity = 3;
            protected readonly DateTime EndDate = new DateTime(2013, 2, 3, 5, 2, 6);
            protected readonly DateTime StartDate = new DateTime(2013, 2, 3, 2, 2, 6);
            protected readonly DateTime LowTide = new DateTime(2013, 2, 3, 2, 2, 8);
            protected const int LocationId = 4;
            protected const int Adults = 6;
            protected const int SpeciesId = 7;
            protected const int FeedingId = 8;
            protected const int HabitatId = 9;
            protected const int Juveniles = 10;
            protected const int PrimaryActivityId = 11;
            protected const int SecondaryActivityId = 12;
            protected const int ObserverId = 13;
            protected const int SiteTypeId = 14;
            protected const int Step = 15;
            protected const string SurveyComments = "Survey comments";
            protected const int Temperature = 16;
            protected const bool WindDrivenTide = true;
            protected const int VantagePoint = 18;
            protected const int Weather = 19;
            protected const int WindSpeed = 20;
            protected const int UserId = 22;
            protected const int SurveyTypeId = 23;
            protected const string Observers = "a, b, and c";
            protected const int WindDirection = 3;
            protected const decimal PrepTimeHours = 0.01m;


            protected SurveyPending CreateDefault()
            {
                var result = new SurveyPending
                {
                    AccessPointId = AccessPoint,
                    AssessmentId = SiteTypeId,
                    DisturbanceComments = Disturbed,
                    EndDate = EndDate,
                    GeneralComments = SurveyComments,
                    LocationId = LocationId,
                    StartDate = StartDate,
                    Temperature = Temperature,
                    SubmittedBy = UserId,
                    SurveyIdentifier = Identifier,
                    SurveyTypeId = SurveyTypeId,
                    WindDrivenTide = WindDrivenTide,
                    TimeOfLowTide = LowTide,
                    VantagePointId = VantagePoint,
                    WeatherId = Weather,
                    WindSpeed = WindSpeed,
                    Observers = Observers,
                    WindDirection = WindDirection,
                    PrepTimeHours = PrepTimeHours
                };
                
                result.Add(new Disturbance
                {
                    DisturbanceTypeId = DisturbedTypeId,
                    DurationMinutes = DisturbedDuration,
                    Quantity = DisturbedQuantity,
                    Result = DisturbedBehavior,
                    SurveyIdentifier = Identifier
                });
                result.Add(new Observation
                {
                    SurveyIdentifier = Identifier,
                    Bin1 = Adults,
                    Bin2 = Juveniles,
                    FeedingSuccessRate = FeedingId,
                    HabitatTypeId = HabitatId,
                    PrimaryActivityId = PrimaryActivityId,
                    SecondaryActivityId = SecondaryActivityId
                });

                return result;
            }

            public class ValidInput : ToSurveyCompleted
            {
                private SurveyCompleted RunTest()
                {
                    return CreateDefault().ToSurveyCompleted();
                }

                [Fact]
                public void MapsPrepTime()
                {
                    Assert.Equal(PrepTimeHours, RunTest().PrepTimeHours);
                }


                [Fact]
                public void MapsWindDirection()
                {
                    Assert.Equal(WindDirection, RunTest().WindDirection);
                }

                [Fact]
                public void MapsAccessPoint()
                {
                    Assert.Equal(AccessPoint, RunTest().AccessPointId);
                }

                [Fact]
                public void MapsAssessment()
                {
                    Assert.Equal(SiteTypeId, RunTest().AssessmentId);
                }

                [Fact]
                public void MapsDisturbanceComments()
                {
                    Assert.Equal(Disturbed, RunTest().DisturbanceComments);
                }

                [Fact]
                public void MapsDisturbance()
                {
                    Assert.Equal(DisturbedBehavior, RunTest().Disturbances[0].Result);
                }

                [Fact] 
                public void MapsEndDate()
                {
                    Assert.Equal(EndDate, RunTest().EndDate);
                }


                [Fact]
                public void MapsGeneralComments()
                {
                    Assert.Equal(SurveyComments, RunTest().GeneralComments);
                }

                [Fact]
                public void MapsLocationId()
                {
                    Assert.Equal(LocationId, RunTest().LocationId);
                }

                [Fact]
                public void MapsStartDate()
                {
                    Assert.Equal(StartDate, RunTest().StartDate);
                }

                [Fact]
                public void MapsTemperature()
                {
                    Assert.Equal(Temperature, RunTest().Temperature);
                }

                [Fact]
                public void MapsSubmittedBy()
                {
                    Assert.Equal(UserId, RunTest().SubmittedBy);
                }

                [Fact]
                public void MapsSurveyIdentifier()
                {
                    Assert.Equal(Identifier, RunTest().SurveyIdentifier);
                }

                [Fact]
                public void MapsSurveyTypeId()
                {
                    Assert.Equal(SurveyTypeId, RunTest().SurveyTypeId);
                }

                [Fact]
                public void MapsWindDrivenTide()
                {
                    Assert.Equal(WindDrivenTide, RunTest().WindDrivenTide);
                }

                [Fact]
                public void MapsTimeOfLowTide()
                {
                    Assert.Equal(LowTide, RunTest().TimeOfLowTide);
                }

                [Fact]
                public void MapsVantagePoint()
                {
                    Assert.Equal(VantagePoint, RunTest().VantagePointId);
                }

                [Fact]
                public void MapsWeatherId()
                {
                    Assert.Equal(Weather, RunTest().WeatherId);
                }

                [Fact]
                public void MapsWindSpeed()
                {
                    Assert.Equal(WindSpeed, RunTest().WindSpeed);
                }

                [Fact]
                public void MapsObservers()
                {
                    Assert.Equal(Observers, RunTest().Observers);
                }
            }
        }
    }
}
