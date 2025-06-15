using FlightNode.Common.Exceptions;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Infrastructure.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Domain.Managers
{
    public class WorkLogDomainManagertests
    {
        public class Fixture : IDisposable
        {
            protected MockRepository MockRepository { get; set; }
            protected Mock<IWorkLogPersistence> WorkLogPersistenceMock { get; set; }


            public Fixture()
            {
                MockRepository = new MockRepository(MockBehavior.Loose);
                WorkLogPersistenceMock = MockRepository.Create<IWorkLogPersistence>();
            }

            protected WorkLogDomainManager BuildSystem()
            {
                return new WorkLogDomainManager(WorkLogPersistenceMock.Object);
            }

            public void Dispose()
            {
            }
        }

        public class ArgumentTests : Fixture
        {
            [Fact]
            public void ConfirmHappyPath()
            {
                BuildSystem();
            }

            [Fact]
            public void ConfirmThatNullArgumentIsNotAllowed()
            {
                Assert.Throws<ArgumentNullException>(() => new WorkLogDomainManager(null));
            }

            [Fact]
            public void ConfirmCreateDoesNotAcceptNullArgument()
            {
                // need to move this to another class, maybe in a renamed constructor class.
                Assert.Throws<ArgumentNullException>(() => BuildSystem().Create(null));
            }


            [Fact]
            public void ConfirmUpdateDoesNotAcceptNullArgument()
            {
                Assert.Throws<ArgumentNullException>(() => BuildSystem().Update(null));
            }
        }

        public class CreateFakeSet : Fixture
        {
            protected FakeDbSet<WorkLog> FakeSet = new FakeDbSet<WorkLog>();


            protected void SetupFakeDbSets()
            {
                WorkLogPersistenceMock.SetupGet(x => x.Collection)
                    .Returns(FakeSet);

            }

            protected void ExpectToUpdateEntity()
            {
                WorkLogPersistenceMock.Setup(x => x.Update(It.IsAny<object>()));
            }
        }

        public class Create : CreateFakeSet
        {
            private const int RecordCount = 1;
            private WorkLog _item = new WorkLog
            {
                LocationId = 1,
                WorkTypeId = 1,
                TravelTimeHours = 1.0m,
                UserId = 1,
                WorkDate = DateTime.Parse("2015-11-12 6:04 PM"),
                WorkHours = 1.1m,
                Id = 3
            };


            [Fact]
            public void ConfirmReturnsModifiedRecord()
            {

                // Arrange
                var id = 34234;
                SetupFakeDbSets();
                WorkLogPersistenceMock.Setup(x => x.SaveChanges())
                    .Callback(() => _item.LocationId = id)
                    .Returns(RecordCount);
                WorkLogPersistenceMock.Setup(x => x.Add(_item));

                // Act
                var result = BuildSystem().Create(_item);

                // Assert
                Assert.Equal(id, _item.LocationId);
            }


            [Fact]
            public void ConfirmInputIsSavedToPersistenceLayer()
            {

                // Arrange
                var id = 34234;
                SetupFakeDbSets();
                WorkLogPersistenceMock.Setup(x => x.SaveChanges())
                    .Callback(() => _item.LocationId = id)
                    .Returns(RecordCount);
                WorkLogPersistenceMock.Setup(x => x.Add(_item));

                // Act
                var result = BuildSystem().Create(_item);

                // Assert
                WorkLogPersistenceMock.Verify(x =>x.SaveChanges(), Times.Once);
                WorkLogPersistenceMock.Verify(x => x.Add(_item), Times.Once);
            }


            public class Validation : CreateFakeSet
            {
                private WorkLog _item = new WorkLog
                {
                    LocationId = 1,
                    WorkTypeId = 1,
                    TravelTimeHours = 1.0m,
                    UserId = 1,
                    WorkDate = DateTime.Parse("2015-11-12 6:04 PM"),
                    WorkHours = 1.1m,
                    Id = 3
                };

                private void RunNegativeTest(string memberName)
                {
                    try
                    {
                        BuildSystem().Create(_item);
                        throw new Exception("this should have failed");
                    }
                    catch (DomainValidationException de)
                    {
                        Assert.True(de.ValidationResults.Any(x => x.MemberNames.Any(y => y == memberName)));
                    }
                }

                private void RunPositiveTest()
                {
                    SetupFakeDbSets();
                    WorkLogPersistenceMock.Setup(x => x.Add(_item));
                    WorkLogPersistenceMock.Setup(x => x.SaveChanges())
                        .Returns(1);

                    BuildSystem().Create(_item);
                }

                [Fact]
                public void ConfirmWorkHoursMustBeGreaterThanZero()
                {
                    _item.WorkHours = 0.0m;

                    RunNegativeTest("WorkHours");
                }

                [Fact]
                public void ConfirmTravelTimeMustBeGreaterThanZero()
                {
                    _item.TravelTimeHours = 0.0m;

                    RunNegativeTest("TravelTimeHours");
                }

                [Fact]
                public void ConfirmWorkHoursAcceptsUpTo24Hours()
                {
                    _item.WorkHours = 24.0m;

                    RunPositiveTest();
                }

                [Fact]
                public void ConfirmTravelTimeAcceptsUpTo24Hours()
                {
                    _item.TravelTimeHours = 24.0m;

                    RunPositiveTest();
                }

                [Fact]
                public void ConfirmWorkHoursCannotBeMoreThan24()
                {
                    _item.WorkHours = 24.01m;

                    RunNegativeTest("WorkHours");
                }

                [Fact]
                public void ConfirmTravelTimeHoursCannotBeMoreThan24()
                {
                    _item.TravelTimeHours = 24.01m;

                    RunNegativeTest("TravelTimeHours");
                }

            }
        }

        public class FindAll : CreateFakeSet
        {
            [Fact]
            public void ConfirmHappyPath()
            {
                // Arrange
                SetupFakeDbSets();
                var one = new WorkLog();
                FakeSet.List.Add(one);

                // Act
                var result = BuildSystem().FindAll();

                // Assert
                Assert.Collection(result, x => Assert.Same(one, x));
            }
        }

        public class FindById : CreateFakeSet
        {

            const int Id = 23423;
            const int VolunteerId = 883;
            const string VolunteerFirstName = "Juana";
            const string VolunteerLastName = "Coneja";
            const string ExpectedName = "Juana Coneja";
            const int LocationId = 993;
            const int NumberOfVolunteers = 9923;
            const string TasksCompleted = "asdfasdf";
            const decimal TravelTimeHours = 20.39m;
            static DateTime _workDate = new DateTime(2016, 10, 16);
            const int WorkHours = 93;
            const int WorkTypeId = 1;

            protected FakeDbSet<User> UsersSet = new FakeDbSet<User>();

            private void SetupUserSet()
            {

                WorkLogPersistenceMock.SetupGet(x => x.Users)
                    .Returns(UsersSet);
            }

            [Fact]
            public void ConfirmMapsVolunteerNameFromUserTable()
            {
                Assert.Equal(ExpectedName, RunTest().VolunteerName);
            }

            [Fact]
            public void ConfirmMapsUserId()
            {
                Assert.Equal(VolunteerId, RunTest().UserId);
            }

            [Fact]
            public void ConfirmMapsLocationId()
            {
                Assert.Equal(LocationId, RunTest().LocationId);
            }

            [Fact]
            public void ConfirmMapsNumberOfVolunteers()
            {
                Assert.Equal(NumberOfVolunteers, RunTest().NumberOfVolunteers);
            }

            [Fact]
            public void ConfirmMapsTasksCompleted()
            {
                Assert.Equal(TasksCompleted, RunTest().TasksCompleted);
            }

            [Fact]
            public void ConfirmMapsTravelTimeHours()
            {
                Assert.Equal(TravelTimeHours, RunTest().TravelTimeHours);
            }

            [Fact]
            public void ConfirmMapsWorkDate()
            {
                Assert.Equal(_workDate, RunTest().WorkDate);
            }

            [Fact]
            public void ConfirmMapsWorkHours()
            {
                Assert.Equal(WorkHours, RunTest().WorkHours);
            }

            [Fact]
            public void ConfirmMapsWorkTypeId()
            {
                Assert.Equal(WorkTypeId, RunTest().WorkTypeId);
            }

            private WorkLogWithVolunteerName RunTest()
            {
                // Arrange
                SetupFakeDbSets();
                FakeSet.List.Add(new WorkLog {
                    Id = Id,
                    UserId = VolunteerId,
                    LocationId = LocationId,
                    NumberOfVolunteers = NumberOfVolunteers,
                    TasksCompleted = TasksCompleted,
                    TravelTimeHours = TravelTimeHours,
                    WorkDate = _workDate,
                    WorkHours = WorkHours,
                    WorkTypeId = WorkTypeId
                });

                SetupUserSet();
                UsersSet.List.Add(new User
                {
                    Id = VolunteerId,
                    GivenName = VolunteerFirstName,
                    FamilyName = VolunteerLastName
                });

                // Act
                var result = BuildSystem().FindById(Id);
                return result;
            }

            [Fact]
            public void ConfirmQueriesByIdValue()
            {
                // Arrange
                SetupFakeDbSets();
                FakeSet.List.Add(new WorkLog
                {
                    Id = Id,
                    UserId = VolunteerId,
                    LocationId = LocationId,
                    NumberOfVolunteers = NumberOfVolunteers,
                    TasksCompleted = TasksCompleted,
                    TravelTimeHours = TravelTimeHours,
                    WorkDate = _workDate,
                    WorkHours = WorkHours,
                    WorkTypeId = WorkTypeId
                });

                SetupUserSet();
                UsersSet.List.Add(new User
                {
                    Id = VolunteerId,
                    GivenName = VolunteerFirstName,
                    FamilyName = VolunteerLastName
                });

                // Act
                Action act = () => BuildSystem().FindById(Id + 1909);

                // Assert
                Assert.Throws<DoesNotExistException>(act);
            }


        }

        public class Update : CreateFakeSet
        {
            private const int RecordCount = 1;
            private WorkLog _original = new WorkLog
            {
                LocationId = 2,
                WorkTypeId = 2,
                TravelTimeHours = 1.1m,
                UserId = 1,
                WorkDate = DateTime.Parse("2015-11-12 6:05 PM"),
                WorkHours = 1.2m,
                Id = 3
            };
            private WorkLog _item = new WorkLog
            {
                LocationId = 1,
                WorkTypeId = 1,
                TravelTimeHours = 1.0m,
                UserId = 1,
                WorkDate = DateTime.Parse("2015-11-12 6:04 PM"),
                WorkHours = 1.1m,
                Id = 3
            };
            private WorkLog _differentUser = new WorkLog
            {
                LocationId = 1,
                WorkTypeId = 1,
                TravelTimeHours = 1.0m,
                UserId = 2,
                WorkDate = DateTime.Parse("2015-11-12 6:04 PM"),
                WorkHours = 1.1m,
                Id = 3
            };


            [Fact]
            public void RunHappyPathTest()
            {
                // Arrange
                SetupFakeDbSets();
                WorkLogPersistenceMock.Setup(x => x.SaveChanges())
                    .Returns(RecordCount);
                ExpectToUpdateEntity();

                // ... for validating that the userid was not changed
                FakeSet.List.Add(_original);

                // Act
                BuildSystem().Update(_item);
            }


            private void RunPositiveTest()
            {
                SetupFakeDbSets();
                WorkLogPersistenceMock.Setup(x => x.SaveChanges())
                       .Returns(1);


                // ... for validating that the userid was not changed
                SetupFakeDbSets();
                FakeSet.List.Add(_item);

                BuildSystem().Update(_item);
            }

            private void RunNegativeTest(string memberName)
            {
                try
                {
                    BuildSystem().Update(_item);
                    throw new Exception("this should have failed");
                }
                catch (DomainValidationException de)
                {
                    Assert.True(de.ValidationResults.Any(x => x.MemberNames.Any(y => y == memberName)));
                }
            }

            [Fact]
            public void ConfirmWorkHoursMustBeGreaterThanZero()
            {
                _item.WorkHours = 0.0m;

                RunNegativeTest("WorkHours");
            }

            [Fact]
            public void ConfirmTravelTimeMustBeGreaterThanZero()
            {
                _item.TravelTimeHours = 0.0m;

                RunNegativeTest("TravelTimeHours");
            }

            [Fact]
            public void ConfirmWorkHoursAcceptsUpTo24Hours()
            {
                _item.WorkHours = 24.0m;
                ExpectToUpdateEntity();

                RunPositiveTest();
            }

            [Fact]
            public void ConfirmTravelTimeAcceptsUpTo24Hours()
            {
                _item.TravelTimeHours = 24.0m;
                ExpectToUpdateEntity();

                RunPositiveTest();
            }

            [Fact]
            public void ConfirmWorkHoursCannotBeMoreThan24()
            {
                _item.WorkHours = 24.01m;

                RunNegativeTest("WorkHours");
            }

            [Fact]
            public void ConfirmTravelTimeHoursCannotBeMoreThan24()
            {
                _item.TravelTimeHours = 24.01m;

                RunNegativeTest("TravelTimeHours");
            }
        }

        public class GetForUser : CreateFakeSet
        {
            [Fact]
            public void RetrievesRecordCreatedByThisUser()
            {
                // Arrange
                const int userId = 323;
                WorkLogPersistenceMock.Setup(x => x.GetWorkLogReportRecords(It.IsAny<int>()))
                    .Returns(new List<WorkLogReportRecord>() { new WorkLogReportRecord() });

                // Act
                var result = BuildSystem().GetForUser(userId);

                // Assert
                Assert.Equal(1, result.Count());
            }

            [Fact]
            public void QueriesByProperId()
            {
                // Arrange
                const int userId = 323;
                WorkLogPersistenceMock.Setup(x => x.GetWorkLogReportRecords(It.IsAny<int>()))
                    .Returns(new List<WorkLogReportRecord>() { new WorkLogReportRecord() });

                // Act
                var result = BuildSystem().GetForUser(userId);

                // Assert
                WorkLogPersistenceMock.Verify(x => x.GetWorkLogReportRecords(userId));
            }
        }
    }
}
