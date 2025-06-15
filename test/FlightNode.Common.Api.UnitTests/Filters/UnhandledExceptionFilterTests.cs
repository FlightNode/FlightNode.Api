using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FlightNode.Common.Api.Filters;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using log4net;

namespace FligthNote.Common.Api.UnitTests.Filters
{
    public class UnhandledExceptionFilterTests
    {
        [Fact]
        public void RejectNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new UnhandledExceptionFilterAttribute().OnException(null));
        }

        Uri uri = new Uri("http://localhost");
        Exception exception = new InvalidOperationException();


        [Fact]
        public void WriteExceptionToLog()
        {
            // Arrange
            var actContext = CreateActionExecutedContext();
            var system = new UnhandledExceptionFilterAttribute();
            var loggerMock = CreateAndInjectLogger(system);

            // Act
            system.OnException(actContext);

            // Assert
            loggerMock.Verify(x => x.Error(It.IsAny<object>(), exception));
        }

        private static Mock<ILog> CreateAndInjectLogger(UnhandledExceptionFilterAttribute system)
        {
            var loggerMock = new Mock<ILog>();
            loggerMock.Setup(x => x.Error(It.IsAny<object>(), It.IsAny<Exception>()));

            system.Logger = loggerMock.Object;
            return loggerMock;
        }

        private HttpActionExecutedContext CreateActionExecutedContext()
        {
            var controllerContext = new HttpControllerContext();
            controllerContext.Request = new System.Net.Http.HttpRequestMessage { RequestUri = uri };
            var context = new HttpActionContext(controllerContext, Mock.Of<HttpActionDescriptor>());
            var actContext = new HttpActionExecutedContext(context, exception);
            return actContext;
        }

        [Fact]
        public void WriteRequestUriToLog()
        {
            // Arrange
            var actContext = CreateActionExecutedContext();
            var system = new UnhandledExceptionFilterAttribute();
            var loggerMock = CreateAndInjectLogger(system);

            // Act
            system.OnException(actContext);

            // Assert
            loggerMock.Verify(x => x.Error(uri, It.IsAny<Exception>()));
        }
    }
}
