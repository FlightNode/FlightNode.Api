using FligthNode.Common.Api.Controllers;
using Moq;
using System.Net;
using System.Web.Http;
using Xunit;

namespace FligthNote.Common.Api.UnitTests.Controllers
{
    public  class LoggingControllerTests
    {
        public class LoggingControllerTss : LoggingController
        {
            public new IHttpActionResult MethodNotAllowed()
            {
                return base.MethodNotAllowed();
            }
        }

        [Fact]
        public void GetDefaultLogger()
        {
            var system = Mock.Of<LoggingController>();
            var result = system.Logger;
            
            Assert.NotNull(result);
        }

        [Fact]
        public void MethodNotAllowed()
        {
            var system = new LoggingControllerTss();
            system.Request = new System.Net.Http.HttpRequestMessage();
            var result = system.MethodNotAllowed()
                .ExecuteAsync(new System.Threading.CancellationToken())
                .Result;

            Assert.Equal(HttpStatusCode.MethodNotAllowed, result.StatusCode);
        }

    }
}
