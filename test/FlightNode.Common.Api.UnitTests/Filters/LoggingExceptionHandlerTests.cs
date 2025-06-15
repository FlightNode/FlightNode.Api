using FlightNode.Common.Api.Filters;
using log4net;
using Moq;
using System;
using System.Web.Http.ExceptionHandling;
using Xunit;

namespace FligthNote.Common.Api.UnitTests.Filters
{
    public class LoggingExceptionHandlerTests
    {
        public class Logger
        {

            [Fact]
            public void GetSet()
            {
                var loggerMock = new Mock<ILog>();

                var system = new LoggingExceptionHandler();
                system.Logger = loggerMock.Object;

                Assert.Same(loggerMock.Object, system.Logger);
            }

            [Fact]
            public void GetDefault()
            {
                var system = new LoggingExceptionHandler();
                var logger = system.Logger;

                Assert.NotNull(logger);
            }
        }

        public class Handle
        {
            [Fact]
            public void RejectsNullArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new LoggingExceptionHandler().Handle(null));
            }

            [Fact]
            public void WritesExceptionToLog()
            {
                // Arrange
                var exception = new InvalidCastException();
                var loggerMock = new Mock<ILog>();
                loggerMock.Setup(x => x.Error(It.IsAny<object>()));

                var system = new LoggingExceptionHandler();
                system.Logger = loggerMock.Object;

                var catchBlock = new ExceptionContextCatchBlock("a", true, true);
                var excepContext = new ExceptionContext(exception, catchBlock)
                {
                    Request = new System.Net.Http.HttpRequestMessage()
                };
                var handlerContext = new ExceptionHandlerContext(excepContext);

                // Act
                system.Handle(handlerContext);

                // Assert
                loggerMock.Verify(x => x.Error(exception));
            }
        }

    }
}
