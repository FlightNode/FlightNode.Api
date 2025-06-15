using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Services.Models.WorkLog;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.WorkLogControllerTests
{
    public class WhenGettingAllEntries : Fixture
    {
        private const int Id = 123;
        private const decimal TravelHours = 3.53m;
        private const decimal WorkHours = 23.32m;
        private readonly DateTime _workDay = DateTime.Parse("2015-12-07 13:45");
        private const int UserId = 223;
        private const int WorkTypeId = 3;
        private const int LocationId = 4;

        [Fact]
        public void ConfirmGetMapsWorkTypeId()
        {
            Assert.Equal(WorkTypeId, RunPositiveTest().First().WorkTypeId);
        }


        [Fact]
        public void ConfirmGetMapsWorkHours()
        {
            Assert.Equal(WorkHours, RunPositiveTest().First().WorkHours);
        }

        [Fact]
        public void ConfirmGetMapsWorkDate()
        {
            Assert.Equal(_workDay, RunPositiveTest().First().WorkDate);
        }

        [Fact]
        public void ConfirmGetMapsUserId()
        {
            Assert.Equal(UserId, RunPositiveTest().First().UserId);
        }

        [Fact]
        public void ConfirmGetMapsTravelTimeHours()
        {
            Assert.Equal(TravelHours, RunPositiveTest().First().TravelTimeHours);
        }

        [Fact]
        public void ConfirmGetMapsLocationId()
        {
            Assert.Equal(LocationId, RunPositiveTest().First().LocationId);
        }

        [Fact]
        public void ConfirmGetMapsId()
        {
            Assert.Equal(Id, RunPositiveTest().First().Id);
        }

        private List<WorkLogModel> RunPositiveTest()
        {
            // Arrange 
            var records = new List<WorkLog>
                {
                    new WorkLog
                    {
                        LocationId = LocationId,
                        TravelTimeHours = TravelHours,
                        WorkTypeId = WorkTypeId,
                        WorkHours = WorkHours,
                        UserId = UserId,
                        WorkDate = _workDay,
                        Id = Id
                    }
                };

            MockDomainManager.Setup(x => x.FindAll())
                .Returns(records);

            // Act
            var result = BuildSystemWithSanitizer().Get();

            var message = result.ExecuteAsync(new System.Threading.CancellationToken()).Result;

            Assert.Equal(HttpStatusCode.OK, message.StatusCode);

            return message.Content.ReadAsAsync<List<WorkLogModel>>().Result;
        }
        
    }
}
