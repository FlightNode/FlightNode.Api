using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using FlightNode.DataCollection.Domain.Managers;
using Moq;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Domain.Managers
{
    public class SurveyTypeDomainManagerTests
    {
        [Fact]
        public void ConstructorThrowsNoExceptions()
        {
            new SurveyTypeDomainManager(Mock.Of<ISurveyTypePersistence>());
        }
    }
}
