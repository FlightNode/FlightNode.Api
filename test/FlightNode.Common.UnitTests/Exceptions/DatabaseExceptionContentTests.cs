using FlightNode.Common.Exceptions;
using Xunit;

namespace FlightNode.Common.UnitTests.Exceptions
{
    public class DatabaseExceptionContentTests
    {
        [Fact]
        public void ConfirmGetAndSetForId()
        {
            var expected = 2234;

            var system = new DatabaseExceptionContent();

            system.Id = expected;

            var actual = system.Id;

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void ConfirmGetAndSetForIdAsNull()
        {
            var system = new DatabaseExceptionContent();

            system.Id = null;

            Assert.Null(system.Id);
        }

        [Fact]
        public void ConfirmGetAndSetForAction()
        {
            var expected = "2234";

            var system = new DatabaseExceptionContent();

            system.Action = expected;

            var actual = system.Action;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConfirmGetAndSetForModelType()
        {
            var expected = this.GetType();

            var system = new DatabaseExceptionContent();

            system.ModelType = expected;

            var actual = system.ModelType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConfirmGetAndSetForDescription()
        {
            var expected = "2234";

            var system = new DatabaseExceptionContent();

            system.Description = expected;

            var actual = system.Description;

            Assert.Equal(expected, actual);
        }
    }
}
