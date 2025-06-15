using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Services.Controllers;
using System.Collections.Generic;
using System.Web.Http.Results;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Controller.WaterbirdForagingSurveyControllerTests
{
    public class WhenGettingTheListOfForagingSurveys
    {
        public class Fixture : LoggingControllerBaseFixture<WaterbirdForagingSurveyController, ISurveyManager>
        {
            protected WaterbirdForagingSurveyController CreateController()
            {
                return new WaterbirdForagingSurveyController(MockDomainManager.Object);
            }
        }

        public class GivenThereAreTwoResults : Fixture
        {
            [Fact]
            public void ThenReturnOkWithListOfForagingListItem()
            {
                ArrangeTest();

                var result = RunTest();

                Assert.IsType<OkNegotiatedContentResult<IReadOnlyList<SurveyListItem>>>(result);
            }

            [Fact]
            public void ThenReturnTheListItem()
            {
                ArrangeTest();

                var result = RunTest();

                var ok = result as OkNegotiatedContentResult<IReadOnlyList<SurveyListItem>>;

                Assert.Equal(2, ok.Content.Count);
            }

            private System.Web.Http.IHttpActionResult RunTest()
            {
                return CreateController().Get();
            }

            private void ArrangeTest()
            {
                var list = new List<SurveyListItem>
                {
                    new SurveyListItem(),
                    new SurveyListItem()
                };

                MockDomainManager.Setup(mgr => mgr.GetSurveyListByTypeAndUser(SurveyType.Foraging, null))
                    .Returns(list);
            }
        }

        public class GivenThereAreNoResults : Fixture
        {
            [Fact]
            public void ThenReturnOkWithListOfForagingListItem()
            {
                ArrangeTest();

                var result = RunTest();

                Assert.IsType<OkNegotiatedContentResult<IReadOnlyList<SurveyListItem>>>(result);
            }

            [Fact]
            public void ThenReturnTheListItem()
            {
                ArrangeTest();

                var result = RunTest();

                var ok = result as OkNegotiatedContentResult<IReadOnlyList<SurveyListItem>>;

                Assert.Equal(0, ok.Content.Count);
            }

            private System.Web.Http.IHttpActionResult RunTest()
            {
                return CreateController().Get();
            }

            private void ArrangeTest()
            {
                var list = new List<SurveyListItem>();

                MockDomainManager.Setup(mgr => mgr.GetSurveyListByTypeAndUser(SurveyType.Foraging, null))
                    .Returns(list);
            }
        }
        
    }
}
