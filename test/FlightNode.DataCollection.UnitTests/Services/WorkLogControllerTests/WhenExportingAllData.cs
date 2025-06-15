using System.Collections.Generic;
using System.Linq;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Services.Models.WorkLog;
using System.Net;
using System.Net.Http;
using Xunit;


namespace FlightNode.DataCollection.Domain.UnitTests.Services.WorkLogControllerTests
{
    public class WhenExportingAllData : Fixture
    {
        // Mapping of data types is tested elsewhere.
        // Here, let's just confirm that all records are mapped

        private const int Id = 123;
        private const int Id2 = 34234;

        [Fact]
        public void ConfirmMapsFirstRecord()
        {
            Assert.Equal(Id, RunPositiveTest().First().Id);
        }

        [Fact]
        public void ConfirmMapsSecondRecord()
        {
            Assert.Equal(Id2, RunPositiveTest().Skip(1).First().Id);
        }

        private List<WorkLogModel> RunPositiveTest()
        {
            // Arrange 
            var records = new List<WorkLogReportRecord>
                {
                    new WorkLogReportRecord
                    {
                        Id = Id,
                        WorkDate = "01/10/2016"
                    },

                    new WorkLogReportRecord
                    {
                        Id = Id2,
                        WorkDate = "01/10/2016"
                    }
                };

            MockDomainManager.Setup(x => x.GetReport())
                .Returns(records);

            // Act
            var result = BuildSystemWithSanitizer()
                .GetExport();

            var message = result.ExecuteAsync(new System.Threading.CancellationToken()).Result;

            Assert.Equal(HttpStatusCode.OK, message.StatusCode);

            return message.Content.ReadAsAsync<List<WorkLogModel>>().Result;
        }
    }
}
