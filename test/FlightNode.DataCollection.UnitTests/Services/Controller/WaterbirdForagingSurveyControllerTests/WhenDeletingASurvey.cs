using System;
using Xunit;
using Moq;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;
using FlightNode.DataCollection.Domain.Entities;
using FluentAssertions;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Controller.WaterbirdForagingSurveyControllerTests
{
    public class WhenDeletingASurvey 
    {
        public class GivenTheSurveyExists : Fixture
        {

            private IHttpActionResult RunTest()
            {
                // Arrange
                MockDomainManager.Setup(x => x.FindBySurveyId(Identifier, SurveyType.Foraging))
                    .Returns(CreateDefaultEntity());

                MockDomainManager.Setup(x => x.Delete(Identifier))
                    .Returns(true);

                // Act
                return BuildSystem().Delete(Identifier);
            }

            [Fact]
            public void ThenDeleteIt()
            {
                RunTest();

                MockDomainManager.Verify(x => x.Delete(Identifier), Times.Once());
            }

            [Fact]
            public void ThenReturn204NoContent()
            {
                var result = RunTest() as StatusCodeResult;

                result.Should().NotBeNull();
                // ReSharper disable once PossibleNullReferenceException
                Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
            }
        }

        public class GivenTheSurveyDoesNotExist : Fixture
        {
            [Fact]
            public void ThenReturn404NotFound()
            {
                // Arrange
                MockDomainManager.Setup(x => x.FindBySurveyId(Identifier, SurveyType.Foraging))
                    .Returns(null as ISurvey);

                // Act
                var result = BuildSystem().Delete(Identifier);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }

        public class GivenSomethingBadHappens : Fixture
        {
            [Fact]
            public void ThenLetTheExceptionBubbleThrough()
            {

                // Arrange
                MockDomainManager.Setup(x => x.FindBySurveyId(Identifier, SurveyType.Foraging))
                    .Throws<InvalidOperationException>();

                // Act
                Func<IHttpActionResult> act = () => BuildSystem().Delete(Identifier);

                // Assert
                Assert.Throws<InvalidOperationException>(act);
            }
        }

    }
}
