using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Services.Controllers;
using System.Collections.Generic;
using System.Web.Http.Results;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Controller.RookeryCensusControllerTests
{
    public class WhenExportingData
    {
        public class Fixture : LoggingControllerBaseFixture<RookeryCensusController, ISurveyManager>
        {
            protected RookeryCensusController CreateController()
            {
                return new RookeryCensusController(MockDomainManager.Object);
            }
        }

        public class GivenThereAreResults : Fixture
        {
            [Fact]
            public void ThenReturnOk()
            {
                MockDomainManager.Setup(x => x.ExportAllRookeryCensuses())
                    .Returns(new List<RookeryCensusExportItem>()
                    {
                        new RookeryCensusExportItem(),
                        new RookeryCensusExportItem()
                    });


                var result = CreateController().Export();

                Assert.IsType<OkNegotiatedContentResult<IReadOnlyList<RookeryCensusExportItem>>>(result);
            }

            [Fact]
            public void ThenReturnListOfQueryResults()
            {
                MockDomainManager.Setup(x => x.ExportAllRookeryCensuses())
                    .Returns(new List<RookeryCensusExportItem>()
                    {
                        new RookeryCensusExportItem(),
                        new RookeryCensusExportItem()
                    });


                var result = CreateController().Export();

                var ok = result as OkNegotiatedContentResult < IReadOnlyList < RookeryCensusExportItem >>;

                Assert.Equal(2, ok.Content.Count);
            }
        }

        public class GivenThereAreNoResults : Fixture
        {
            [Fact]
            public void ThenReturnOk()
            {
                MockDomainManager.Setup(x => x.ExportAllRookeryCensuses())
                    .Returns(new List<RookeryCensusExportItem>());


                var result = CreateController().Export();

                Assert.IsType<OkNegotiatedContentResult<IReadOnlyList<RookeryCensusExportItem>>>(result);
            }

            [Fact]
            public void ThenReturnListOfQueryResults()
            {
                MockDomainManager.Setup(x => x.ExportAllRookeryCensuses())
                    .Returns(new List<RookeryCensusExportItem>());


                var result = CreateController().Export();

                var ok = result as OkNegotiatedContentResult<IReadOnlyList<RookeryCensusExportItem>>;

                Assert.Equal(0, ok.Content.Count);
            }
        }
    }
}
