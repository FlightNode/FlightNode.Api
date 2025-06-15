using FlightNode.Common.Exceptions;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Services.Models.WorkLog;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Web.Http.Results;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.WorkLogControllerTests
{
    public class WhenUpdateAWorkLogEntry : Fixture
    {
        private const int Id = 123;
        private const decimal TravelHours = 3.53m;
        private const decimal WorkHours = 23.32m;
        private readonly DateTime _workDay = DateTime.Parse("2015-12-07 13:45");
        private const int UserId = 223;
        private const int WorkTypeId = 3;
        private const int LocationId = 4;

        [Fact]
        public void ConfirmMapsId()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Update(It.Is<WorkLog>(y => y.Id == Id)));
        }

        [Fact]
        public void ConfirmMapsLocationId()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Update(It.Is<WorkLog>(y => y.LocationId == LocationId)));
        }

        [Fact]
        public void ConfirmMapsTravelTimeHours()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Update(It.Is<WorkLog>(y => y.TravelTimeHours == TravelHours)));
        }

        [Fact]
        public void ConfirmMapsUserId()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Update(It.Is<WorkLog>(y => y.UserId == UserId)));
        }

        [Fact]
        public void ConfirmMapsWorkDate()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Update(It.Is<WorkLog>(y => y.WorkDate == _workDay)));
        }

        [Fact]
        public void ConfirmMapsWorkHours()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Update(It.Is<WorkLog>(y => y.WorkHours == WorkHours)));
        }

        [Fact]
        public void ConfirmMapsWorkTypeId()
        {
            RunPositiveTest();
            MockDomainManager.Verify(x => x.Update(It.Is<WorkLog>(y => y.WorkTypeId == WorkTypeId)));
        }


        [Fact]
        public void ConfirmReturnsNoContenttatus()
        {
            Assert.Equal(HttpStatusCode.NoContent, RunPositiveTest().StatusCode);
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
                Id = Id
            };

            MockDomainManager.Setup(x => x.Update(It.IsAny<WorkLog>()))
                .Returns(1);

            MockSanitizer.Setup(x => x.RemoveAllHtml(It.IsAny<string>()))
                .Returns((string input) => input);


            // Act
            var result = BuildSystemWithSanitizer().Put(record);

            var message = result.ExecuteAsync(new System.Threading.CancellationToken()).Result;

            return message;

        }
        

        private const string Message1 = "asdfasdfa";
        private const string Field1 = "fieeeeeld";
        private const string Message2 = "as8df7a89psdfp";
        private const string Field2 = "sdk;kl;hl;";

        [Fact]
        public void ConfirmDoesNotCactchDomainValidationException()
        {
            Assert.Throws<DomainValidationException>(() => RunValidationTest());
        }

        private InvalidModelStateResult RunValidationTest()
        {
            var list = new List<ValidationResult>
                    {
                        new ValidationResult(Message1, new [] { Field1 }),
                        new ValidationResult(Message2, new [] { Field2 })
                    };

            var e = DomainValidationException.Create(list);

            MockDomainManager.Setup(x => x.Update(It.IsAny<WorkLog>()))
                                   .Throws(e);

            MockSanitizer.Setup(x => x.RemoveAllHtml(It.IsAny<string>()))
                .Returns(string.Empty);

            
            return BuildSystemWithSanitizer().Put(new WorkLogModel()) as InvalidModelStateResult;

        }
    }
}
