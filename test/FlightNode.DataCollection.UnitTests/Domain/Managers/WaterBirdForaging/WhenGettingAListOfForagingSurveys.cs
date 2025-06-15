using Moq;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using Xunit;
using FlightNode.DataCollection.Domain.Entities;
using System;
using FlightNode.DataCollection.Domain.Managers;
using System.Linq;

namespace FlightNode.DataCollection.Domain.UnitTests.Domain.Managers.WaterBirdForaging
{

    public class WhenGettingAListOfForagingSurveys
    {
        public class Fixture : IDisposable
        {
            protected MockRepository MockRepository { get; set; }
            protected Mock<ISurveyPersistence> SurveyPersistenceMock { get; set; }
            protected SurveyManager Manager { get; set; }

            public Fixture()
            {
                MockRepository = new MockRepository(MockBehavior.Strict);
                SurveyPersistenceMock = MockRepository.Create<ISurveyPersistence>();

                Manager = new SurveyManager(SurveyPersistenceMock.Object);
            }

            public void Dispose()
            {
                MockRepository.VerifyAll();
            }
        }

        public class GivenThereIsAPendingForagingSurvey : Fixture
        {
            const int LocationId = 13;
            const int UserId = 99;
            static Guid _identifier = new Guid("a507f681-c111-447a-bc1f-195916891226");
            const string SiteCode = "3333";
            const string SiteName = "897987987";
            static DateTime _startDate = new DateTime(2000, 1, 2, 3, 4, 5);
            const string FirstName = "C3";
            const string LastName = "PIO";
            const string ExpectedName = "C3 PIO";
            const int SurveyTypeId = 2;

            [Fact]
            public void ThenReturnOneRecord()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(1, result.Count);
            }

            [Fact]
            public void ThenMapStatusAsPending()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal("Pending", result.First().Status);
            }

            [Fact]
            public void ThenMapTheSurveyIdentifier()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(_identifier, result.First().SurveyIdentifier);
            }

            [Fact]
            public void ThenMapTheSiteName()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(SiteName, result.First().SiteName);
            }

            [Fact]
            public void ThenMapTheSiteCode()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(SiteCode, result.First().SiteCode);
            }

            [Fact]
            public void ThenMapTheStartdate()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(_startDate, result.First().StartDate);
            }

            [Fact]
            public void ThenMapTheUserFullName()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(ExpectedName, result.First().SubmittedBy);
            }

            private void ArrangeTest()
            {
                var survey = new SurveyPending
                {
                    LocationId = LocationId,
                    SubmittedBy = UserId,
                    SurveyIdentifier = _identifier,
                    StartDate = _startDate,
                    SurveyTypeId = SurveyTypeId
                };
                var location = new Location
                {
                    Id = LocationId,
                    SiteCode = SiteCode,
                    SiteName = SiteName
                };
                var user = new User
                {
                    FamilyName = LastName,
                    GivenName = FirstName,
                    Id = UserId
                };

                SurveyPersistenceMock.SetupGet(x => x.SurveysPending)
                    .Returns(FakeDbSet<SurveyPending>.Create(survey));

                SurveyPersistenceMock.SetupGet(x => x.Locations)
                    .Returns(FakeDbSet<Location>.Create(location));

                SurveyPersistenceMock.SetupGet(x => x.Users)
                    .Returns(FakeDbSet<User>.Create(user));

                SurveyPersistenceMock.SetupGet(x => x.SurveysCompleted)
                    .Returns(new FakeDbSet<SurveyCompleted>());
            }
        }


        public class GivenThereIsACompletedForagingSurvey : Fixture
        {
            const int LocationId = 13;
            const int UserId = 99;
            static Guid _identifier = new Guid("a507f681-c111-447a-bc1f-195916891226");
            const string SiteCode = "3333";
            const string SiteName = "897987987";
            static DateTime _startDate = new DateTime(2000, 1, 2, 3, 4, 5);
            const string FirstName = "C3";
            const string LastName = "PIO";
            const string ExpectedName = "C3 PIO";
            const int SurveyTypeId = 2;

            [Fact]
            public void ThenReturnOneRecord()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(1, result.Count);
            }

            [Fact]
            public void ThenMapStatusAsPending()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal("Complete", result.First().Status);
            }

            [Fact]
            public void ThenMapTheSurveyIdentifier()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(_identifier, result.First().SurveyIdentifier);
            }

            [Fact]
            public void ThenMapTheSiteName()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(SiteName, result.First().SiteName);
            }

            [Fact]
            public void ThenMapTheSiteCode()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(SiteCode, result.First().SiteCode);
            }

            [Fact]
            public void ThenMapTheStartdate()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(_startDate, result.First().StartDate);
            }

            [Fact]
            public void ThenMapTheUserFullName()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(ExpectedName, result.First().SubmittedBy);
            }

            private void ArrangeTest()
            {
                var survey = new SurveyCompleted
                {
                    LocationId = LocationId,
                    SubmittedBy = UserId,
                    SurveyIdentifier = _identifier,
                    StartDate = _startDate,
                    SurveyTypeId = SurveyTypeId
                };
                var location = new Location
                {
                    Id = LocationId,
                    SiteCode = SiteCode,
                    SiteName = SiteName
                };
                var user = new User
                {
                    FamilyName = LastName,
                    GivenName = FirstName,
                    Id = UserId
                };

                SurveyPersistenceMock.SetupGet(x => x.SurveysCompleted)
                    .Returns(FakeDbSet<SurveyCompleted>.Create(survey));

                SurveyPersistenceMock.SetupGet(x => x.Locations)
                    .Returns(FakeDbSet<Location>.Create(location));

                SurveyPersistenceMock.SetupGet(x => x.Users)
                    .Returns(FakeDbSet<User>.Create(user));

                SurveyPersistenceMock.SetupGet(x => x.SurveysPending)
                    .Returns(new FakeDbSet<SurveyPending>());
            }
        }

        public class GivenThereAreBothPendingAndCompleteForagingSurveys : Fixture
        {
            private const int LocationId = 13;
            private const int UserId = 99;
            private static readonly Guid Identifier = new Guid("a507f681-c111-447a-bc1f-195916891226");
            private const string SiteCode = "3333";
            private const string SiteName = "897987987";
            private static readonly DateTime StartDate = new DateTime(2000, 1, 2, 3, 4, 5);
            private const string FirstName = "C3";
            private const string LastName = "PIO";
            private const int SurveyTypeId = 2;

            [Fact]
            public void ThenReturnBothRecords()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyTypeId);

                Assert.Equal(2, result.Count);
            }


            private void ArrangeTest()
            {
                var pending = new SurveyPending
                {
                    LocationId = LocationId,
                    SubmittedBy = UserId,
                    SurveyIdentifier = Identifier,
                    StartDate = StartDate,
                    SurveyTypeId = SurveyTypeId
                };
                var complete = new SurveyCompleted
                {
                    LocationId = LocationId,
                    SubmittedBy = UserId,
                    SurveyIdentifier = Identifier,
                    StartDate = StartDate,
                    SurveyTypeId = SurveyTypeId
                };
                var location = new Location
                {
                    Id = LocationId,
                    SiteCode = SiteCode,
                    SiteName = SiteName
                };
                var user = new User
                {
                    FamilyName = LastName,
                    GivenName = FirstName,
                    Id = UserId
                };

                SurveyPersistenceMock.SetupGet(x => x.SurveysCompleted)
                    .Returns(FakeDbSet<SurveyCompleted>.Create(complete));

                SurveyPersistenceMock.SetupGet(x => x.SurveysPending)
                    .Returns(FakeDbSet<SurveyPending>.Create(pending));

                SurveyPersistenceMock.SetupGet(x => x.Locations)
                    .Returns(FakeDbSet<Location>.Create(location));

                SurveyPersistenceMock.SetupGet(x => x.Users)
                    .Returns(FakeDbSet<User>.Create(user));
            }
        }

        public class GivenThereIsAPendingOtherSurvey : Fixture
        {
            const int LocationId = 13;
            const int UserId = 99;
            static Guid _identifier = new Guid("a507f681-c111-447a-bc1f-195916891226");
            const string SiteCode = "3333";
            const string SiteName = "897987987";
            static DateTime _startDate = new DateTime(2000, 1, 2, 3, 4, 5);
            const string FirstName = "C3";
            const string LastName = "PIO";
            const int SurveyTypeId = 22;

            [Fact]
            public void ThenReturnNoRecords()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyType.Foraging);

                Assert.Equal(0, result.Count);
            }

            private void ArrangeTest()
            {
                var survey = new SurveyPending
                {
                    LocationId = LocationId,
                    SubmittedBy = UserId,
                    SurveyIdentifier = _identifier,
                    StartDate = _startDate,
                    SurveyTypeId = SurveyTypeId
                };
                var location = new Location
                {
                    Id = LocationId,
                    SiteCode = SiteCode,
                    SiteName = SiteName
                };
                var user = new User
                {
                    FamilyName = LastName,
                    GivenName = FirstName,
                    Id = UserId
                };

                SurveyPersistenceMock.SetupGet(x => x.SurveysPending)
                    .Returns(FakeDbSet<SurveyPending>.Create(survey));

                SurveyPersistenceMock.SetupGet(x => x.SurveysCompleted)
                    .Returns(new FakeDbSet<SurveyCompleted>());

                SurveyPersistenceMock.SetupGet(x => x.Locations)
                    .Returns(FakeDbSet<Location>.Create(location));

                SurveyPersistenceMock.SetupGet(x => x.Users)
                    .Returns(FakeDbSet<User>.Create(user));
            }
        }

        public class GivenThereIsACompleteOtherSurvey : Fixture
        {
            const int LocationId = 13;
            const int UserId = 99;
            static Guid _identifier = new Guid("a507f681-c111-447a-bc1f-195916891226");
            const string SiteCode = "3333";
            const string SiteName = "897987987";
            static DateTime _startDate = new DateTime(2000, 1, 2, 3, 4, 5);
            const string FirstName = "C3";
            const string LastName = "PIO";
            const string ExpectedName = "C3 PIO";
            const int SurveyTypeId = 22;

            [Fact]
            public void ThenReturnNoRecords()
            {
                ArrangeTest();

                var result = Manager.GetSurveyListByTypeAndUser(SurveyType.Foraging);

                Assert.Equal(0, result.Count);
            }

            private void ArrangeTest()
            {
                var survey = new SurveyCompleted
                {
                    LocationId = LocationId,
                    SubmittedBy = UserId,
                    SurveyIdentifier = _identifier,
                    StartDate = _startDate,
                    SurveyTypeId = SurveyTypeId
                };
                var location = new Location
                {
                    Id = LocationId,
                    SiteCode = SiteCode,
                    SiteName = SiteName
                };
                var user = new User
                {
                    FamilyName = LastName,
                    GivenName = FirstName,
                    Id = UserId
                };

                SurveyPersistenceMock.SetupGet(x => x.SurveysPending)
                    .Returns(new FakeDbSet<SurveyPending>());

                SurveyPersistenceMock.SetupGet(x => x.SurveysCompleted)
                    .Returns(FakeDbSet<SurveyCompleted>.Create(survey));

                SurveyPersistenceMock.SetupGet(x => x.Locations)
                    .Returns(FakeDbSet<Location>.Create(location));

                SurveyPersistenceMock.SetupGet(x => x.Users)
                    .Returns(FakeDbSet<User>.Create(user));
            }
        }
    }
}
