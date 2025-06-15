using System;
using Moq;
using log4net;
using FligthNode.Common.Api.Controllers;
using System.Net.Http;
using System.Web.Http;
using FlightNode.Common.Utility;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Controller
{
    public abstract class LoggingControllerBaseFixture<TController, TManager> : IDisposable
        where TController : LoggingController
        where TManager : class
    {
        protected MockRepository MockRepository = new MockRepository(MockBehavior.Strict);
        protected Mock<TManager> MockDomainManager;
        protected Mock<ILog> MockLogger;
        protected Mock<ISanitizer> MockSanitizer;

        protected const string Url = "http://some/where/";

        public LoggingControllerBaseFixture()
        {
            MockDomainManager = MockRepository.Create<TManager>();
            MockLogger = MockRepository.Create<ILog>();
            MockSanitizer = MockRepository.Create<ISanitizer>();
        }

        protected TController BuildSystem()
        {
            var controller = Activator.CreateInstance(typeof(TController), MockDomainManager.Object) as TController;

            controller.Logger = MockLogger.Object;

            controller.Request = new HttpRequestMessage();
            controller.Request.RequestUri = new Uri(Url);

            controller.Configuration = new HttpConfiguration();

            return controller;
        }


        protected TController BuildSystemWithSanitizer()
        {
            var controller = Activator.CreateInstance(typeof(TController), MockDomainManager.Object, MockSanitizer.Object) as TController;

            controller.Logger = MockLogger.Object;

            controller.Request = new HttpRequestMessage();
            controller.Request.RequestUri = new Uri(Url);

            controller.Configuration = new HttpConfiguration();

            return controller;
        }

        public virtual void Dispose()
        {
            MockRepository.VerifyAll();
        }

        
        protected void ExpectToLogToDebug()
        {
            MockLogger.Setup(x => x.Debug(It.IsAny<Exception>()));
        }


        protected static HttpResponseMessage ExecuteHttpAction(IHttpActionResult result)
        {
            return result.ExecuteAsync(new System.Threading.CancellationToken()).Result;
        }



        protected static TModel ReadResult<TModel>(HttpResponseMessage message)
        {
            return message.Content.ReadAsAsync<TModel>().Result;
        }
    }
}
