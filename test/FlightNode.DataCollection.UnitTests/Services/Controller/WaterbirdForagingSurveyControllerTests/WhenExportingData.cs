using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Services.Controllers;
using System.Collections.Generic;
using System.Web.Http.Results;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Controller.WaterbirdForagingSurveyControllerTests
{
    public class WhenExportingData
    {
        public class Fixture : LoggingControllerBaseFixture<WaterbirdForagingSurveyController, ISurveyManager>
        {
            protected WaterbirdForagingSurveyController CreateController()
            {
                return new WaterbirdForagingSurveyController(MockDomainManager.Object);
            }
        }

        public class GivenThereAreResults : Fixture
        {
            [Fact]
            public void ThenReturnOk()
            {
                MockDomainManager.Setup(x => x.ExportAllForagingSurveys())
                    .Returns(new List<ForagingSurveyExportItem>()
                    {
                        new ForagingSurveyExportItem(),
                        new ForagingSurveyExportItem()
                    });


                var result = CreateController().Export();

                Assert.IsType<OkNegotiatedContentResult<IReadOnlyList<ForagingSurveyExportItem>>>(result);
            }

            [Fact]
            public void ThenReturnListOfQueryResults()
            {
                MockDomainManager.Setup(x => x.ExportAllForagingSurveys())
                    .Returns(new List<ForagingSurveyExportItem>()
                    {
                        new ForagingSurveyExportItem(),
                        new ForagingSurveyExportItem()
                    });


                var result = CreateController().Export();

                var ok = result as OkNegotiatedContentResult < IReadOnlyList < ForagingSurveyExportItem >>;

                Assert.Equal(2, ok.Content.Count);
            }
        }

        public class GivenThereAreNoResults : Fixture
        {
            [Fact]
            public void ThenReturnOk()
            {
                MockDomainManager.Setup(x => x.ExportAllForagingSurveys())
                    .Returns(new List<ForagingSurveyExportItem>());


                var result = CreateController().Export();

                Assert.IsType<OkNegotiatedContentResult<IReadOnlyList<ForagingSurveyExportItem>>>(result);
            }

            [Fact]
            public void ThenReturnListOfQueryResults()
            {
                MockDomainManager.Setup(x => x.ExportAllForagingSurveys())
                    .Returns(new List<ForagingSurveyExportItem>());


                var result = CreateController().Export();

                var ok = result as OkNegotiatedContentResult<IReadOnlyList<ForagingSurveyExportItem>>;

                Assert.Equal(0, ok.Content.Count);
            }
        }
    }
}
