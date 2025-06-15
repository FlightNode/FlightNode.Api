using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Services.Models.WorkLog;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.WorkLogControllerTests
{
   public class WhenGettingASingleWorkLogEntry : Fixture
    {
        private const int Id = 123;
        private const decimal TravelHours = 3.53m;
        private const decimal WorkHours = 23.32m;
        private readonly DateTime _workDay = DateTime.Parse("2015-12-07 13:45");
        private const int UserId = 223;
        private const int WorkTypeId = 3;
        private const int LocationId = 4;
        private const string VolunteerName = "J. Doe";

        [Fact]
        public void ConfirmGetMapsVolunteerName()
        {
            Assert.Equal(VolunteerName, RunPositiveTest().VolunteerName);
        }

        [Fact]
        public void ConfirmGetMapsWorkTypeId()
        {
            Assert.Equal(WorkTypeId, RunPositiveTest().WorkTypeId);
        }


        [Fact]
        public void ConfirmGetMapsWorkHours()
        {
            Assert.Equal(WorkHours, RunPositiveTest().WorkHours);
        }

        [Fact]
        public void ConfirmGetMapsWorkDate()
        {
            Assert.Equal(_workDay, RunPositiveTest().WorkDate);
        }

        [Fact]
        public void ConfirmGetMapsUserId()
        {
            Assert.Equal(UserId, RunPositiveTest().UserId);
        }

        [Fact]
        public void ConfirmGetMapsTravelTimeHours()
        {
            Assert.Equal(TravelHours, RunPositiveTest().TravelTimeHours);
        }

        [Fact]
        public void ConfirmGetMapsLocationId()
        {
            Assert.Equal(LocationId, RunPositiveTest().LocationId);
        }

        [Fact]
        public void ConfirmGetMapsId()
        {
            Assert.Equal(Id, RunPositiveTest().Id);
        }


        private WorkLogModel RunPositiveTest()
        {
            // Arrange 
            var record = new WorkLogWithVolunteerName
            {
                LocationId = LocationId,
                TravelTimeHours = TravelHours,
                WorkTypeId = WorkTypeId,
                WorkHours = WorkHours,
                UserId = UserId,
                WorkDate = _workDay,
                Id = Id,
                VolunteerName = VolunteerName
            };

            MockDomainManager.Setup(x => x.FindById(It.Is<int>(y => y == Id)))
                .Returns(record);


            // Act
            var result = BuildSystemWithSanitizer().Get(Id);

            var message = result.ExecuteAsync(new System.Threading.CancellationToken()).Result;

            Assert.Equal(HttpStatusCode.OK, message.StatusCode);

            return message.Content.ReadAsAsync<WorkLogModel>().Result;
        }

    }
}
