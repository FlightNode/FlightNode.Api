using FlightNode.Api.DependencyResolution;
using FligthNode.Common.Api.Controllers;
using Microsoft.Practices.Unity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FligthNote.Common.Api.UnitTests.DependencyResolution
{
    public class UnityDependencyResolverTests
    {
        public abstract class Fixture : IDisposable
        {
            protected MockRepository MockRepository { get; set; }

            protected Mock<IUnityContainer> MockContainer { get; set; }

            protected Fixture()
            {
                MockRepository = new MockRepository(MockBehavior.Strict);
                MockContainer = MockRepository.Create<IUnityContainer>();
            }


            protected UnityDependencyResolver BuildSystem()
            {
                return new UnityDependencyResolver(MockContainer.Object);
            }

            public void Dispose()
            {
                MockContainer.VerifyAll();
            }

        }

        public class ConstructorBehavior : Fixture
        {
            [Fact]
            public void ConfirmWithValidArgument()
            {
                var system = BuildSystem();
            }

            [Fact]
            public void ConfirmThatNullArgumentIsNotAllowed()
            {
                Assert.Throws<ArgumentNullException>(() => new UnityDependencyResolver(null));
            }
        }

        public class BeginScopeBehavior : Fixture
        {
            [Fact]
            public void ConfirmScopeCreation()
            {
                MockContainer.Setup(x => x.CreateChildContainer())
                    .Returns(MockContainer.Object);

                Assert.NotNull(BuildSystem().BeginScope());
            }
        }
    }
}
