using System;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Services.Models.WorkLog;
using Moq;
using System.Net;
using System.Net.Http;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.WorkLogControllerTests
{

    public class WhenCreatingAWorkLogEntry : Fixture
    {
        private const int Id = 123;
        private const decimal TravelHours = 3.53m;
        private const decimal WorkHours = 23.32m;
        private readonly DateTime _workDay = DateTime.Parse("2015-12-07 13:45");
        private const int UserId = 223;
        private const int WorkTypeId = 3;
        private const int LocationId = 4;
        private const int NumberOfVolunteers = 234;
        private const string TasksCompletedSafe = "asdf asdfasdfasdf";
        private const string TasksCompletedWithHtml = "asdf asdfa<i>something</i>sdfasdf";

        [Fact]
        public void ConfirmMapsNumberOfVolunteers()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Create(It.Is<WorkLog>(y => y.NumberOfVolunteers == NumberOfVolunteers)));
        }

        [Fact]
        public void ConfirmMapsTasksCompleted()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Create(It.Is<WorkLog>(y => y.TasksCompleted == TasksCompletedSafe)));
        }


        [Fact]
        public void ConfirmStripsHtmlFromTasksCompleted()
        {

            // Arrange 
            var record = new WorkLogModel
            {
                LocationId = LocationId,
                TravelTimeHours = TravelHours,
                WorkTypeId = WorkTypeId,
                WorkHours = WorkHours,
                UserId = UserId,
                WorkDate = _workDay,
                Id = Id,
                NumberOfVolunteers = NumberOfVolunteers,
                TasksCompleted = TasksCompletedWithHtml
            };

            MockDomainManager.Setup(x => x.Create(It.IsAny<WorkLog>()))
            .Returns((WorkLog actual) =>
            {
                // inject an ID value so we can confirm that it is passed in the response
                actual.Id = Id;
                return actual;
            });

            MockSanitizer.Setup(x => x.RemoveAllHtml(TasksCompletedWithHtml))
                .Returns(TasksCompletedSafe);


            // Act
            var result = BuildSystemWithSanitizer().Post(record);

            var unused = result.ExecuteAsync(new System.Threading.CancellationToken()).Result;

            //Assert
            MockDomainManager.Verify(x => x.Create(It.Is<WorkLog>(y => y.TasksCompleted == TasksCompletedSafe)));
        }

        [Fact]
        public void ConfirmMapsId()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Create(It.Is<WorkLog>(y => y.Id == Id)));
        }

        [Fact]
        public void ConfirmMapsLocationId()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Create(It.Is<WorkLog>(y => y.LocationId == LocationId)));
        }

        [Fact]
        public void ConfirmMapsTravelTimeHours()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Create(It.Is<WorkLog>(y => y.TravelTimeHours == TravelHours)));
        }

        [Fact]
        public void ConfirmMapsUserId()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Create(It.Is<WorkLog>(y => y.UserId == UserId)));
        }

        [Fact]
        public void ConfirmMapsWorkDate()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Create(It.Is<WorkLog>(y => y.WorkDate == _workDay)));
        }

        [Fact]
        public void ConfirmMapsWorkHours()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Create(It.Is<WorkLog>(y => y.WorkHours == WorkHours)));
        }

        [Fact]
        public void ConfirmMapsWorkTypeId()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Create(It.Is<WorkLog>(y => y.WorkTypeId == WorkTypeId)));
        }

        [Fact]
        public void ConfirmUpdatesLocationHeader()
        {
            var expected = "http://some/where/" + Id.ToString();

            Assert.Equal(expected, RunPositiveTest().Headers.Location.ToString());
        }

        [Fact]
        public void ConfirmReturnsCreatedStatus()
        {
            Assert.Equal(HttpStatusCode.Created, RunPositiveTest().StatusCode);
        }

        private HttpResponseMessage RunPositiveTest()
        {
            // Arrange 
            var record = new WorkLogModel
            {
                LocationId = LocationId,
                TravelTimeHours = TravelHours,
                WorkTypeId = WorkTypeId,
                WorkHours = WorkHours,
                UserId = UserId,
                WorkDate = _workDay,
                Id = Id,
                NumberOfVolunteers = NumberOfVolunteers,
                TasksCompleted = TasksCompletedSafe
            };

            MockDomainManager.Setup(x => x.Create(It.IsAny<WorkLog>()))
            .Returns((WorkLog actual) =>
            {
                // inject an ID value so we can confirm that it is passed in the response
                actual.Id = Id;
                return actual;
            });

            MockSanitizer.Setup(x => x.RemoveAllHtml(It.IsAny<string>()))
                .Returns((string input) => input);


            // Act
            var result = BuildSystemWithSanitizer().Post(record);

            var message = result.ExecuteAsync(new System.Threading.CancellationToken()).Result;

            return message;

        }




    }

}
