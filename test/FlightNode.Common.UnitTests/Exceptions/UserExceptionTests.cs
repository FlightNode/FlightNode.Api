using FlightNode.Common.Exceptions;
using System.Collections.Generic;
using Xunit;

namespace FlightNode.Common.UnitTests.Exceptions
{
    public class UserExceptionTests
    {
        [Fact]
        public void ConfirmTheBasicConstructor()
        {
            var message = "asdfasdf";
            var actual = new UserException(message);
            Assert.Equal(message, actual.Message);
        }
        
        [Fact]
        public void ConfirmMessageFromListWithOneError()
        {
            var errors = new List<string>() { "one" };
            var expected = "one";

            var actual = UserException.FromMultipleMessages(errors);

            Assert.Equal(expected, actual.Message);
        }

        [Fact]
        public void ConfirmMessageFromListWithTwoErrors()
        {
            var errors = new List<string>() { "one", "two" };
            var expected = "one, two";

            var actual = UserException.FromMultipleMessages(errors);

            Assert.Equal(expected, actual.Message);
        }
    }
}
