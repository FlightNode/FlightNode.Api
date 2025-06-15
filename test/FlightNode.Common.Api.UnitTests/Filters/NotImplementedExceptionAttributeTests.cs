using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FlightNode.Common.Api.Filters;
using System.Web.Http.Filters;
using System.Net;
using System.Web.Http.Controllers;

namespace FligthNote.Common.Api.UnitTests.Filters
{
    public class NotImplementedExceptionAttributeTests
    {
        [Fact]
        public void RejectNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new NotImplementedExceptionAttribute().OnException(null));
        }

        [Fact]
        public void CreateNotImplementedResponse()
        {
            // Arrange
            var exception = new NotImplementedException();
            var context = new HttpActionContext();
            var actContext = new HttpActionExecutedContext(context, exception);
            var system = new NotImplementedExceptionAttribute();

            // Act
            system.OnException(actContext);

            // Assert
            Assert.Equal(HttpStatusCode.NotImplemented, actContext.Response.StatusCode);
        }

        [Fact]
        public void IgnoreOtherException()
        {
            // Arrange
            var exception = new InvalidCastException();
            var context = new HttpActionContext();
            var actContext = new HttpActionExecutedContext(context, exception);
            var system = new NotImplementedExceptionAttribute();

            // Act
            system.OnException(actContext);

            // Assert
            Assert.Null(actContext.Response);
        }

        [Fact]
        public void IgnoreNullException()
        {
            // Arrange
            NotImplementedException exception = null;
            var context = new HttpActionContext();
            var actContext = new HttpActionExecutedContext(context, exception);
            var system = new NotImplementedExceptionAttribute();

            // Act
            system.OnException(actContext);

            // Assert
            Assert.Null(actContext.Response);
        }
    }
}
