using FlightNode.DataCollection.Services.Models.Survey;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Models.Survey
{
    public class WaterbirdForagingModelTests
    {
        [Fact]
        public void AcceptsStandardAmericanDateFormat()
        {
            // Arrange
            var model = new WaterbirdForagingModel
            {
                StartDate = "05/06/2007"
            };

            // Act
            var isValid = ModelValidator.Validate(model);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void AcceptsYearFirstDateFormat()
        {
            // Arrange
            var model = new WaterbirdForagingModel
            {
                StartDate = "2007-05-06"
            };

            // Act
            var isValid = ModelValidator.Validate(model);

            // Assert
            Assert.True(isValid);
        }
    }
}
