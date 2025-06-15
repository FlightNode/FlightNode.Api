using FlightNode.Common.Utility;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Domain.Services.Controllers;
using FlightNode.DataCollection.Domain.UnitTests.Services.Controller;
using Moq;
using System;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.WorkLogControllerTests
{
    public class Fixture : LoggingControllerBaseFixture<WorkLogsController, IWorkLogDomainManager>
    {
    }

    public class ArgumentTests : Fixture
    {
        [Fact]
        public void ConfirmConstructorHappyPath()
        {
            BuildSystemWithSanitizer();
        }

        [Fact]
        public void ConfirmConstructorRejectsNullFirstArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new WorkLogsController(null, new Sanitizer()));
        }

        [Fact]
        public void ConfirmConstructorRejectsNullSecondArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new WorkLogsController(Mock.Of<IWorkLogDomainManager>(), null));
        }
    }
}
