using FlightNode.Api.DependencyResolution;
using FligthNode.Common.Api.Controllers;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FligthNote.Common.Api.UnitTests
{
    public class UnityDependencyScopeTests
    {
        // Test-specific Subclass for accessing protected property
        public class UnityDependencyScopeTss : UnityDependencyScope
        {
            public UnityDependencyScopeTss(IUnityContainer container) : base(container) { }

            public new IUnityContainer Container { get { return base.Container; } }
        }

        public abstract class Fixture : IDisposable
        {
            protected MockRepository MockRepository { get; set; }

            protected Mock<IUnityContainer> MockContainer { get; set; }

            protected Fixture()
            {
                MockRepository = new MockRepository(MockBehavior.Strict);
                MockContainer = MockRepository.Create<IUnityContainer>();
            }


            protected UnityDependencyScopeTss BuildSystem()
            {
                return new UnityDependencyScopeTss(MockContainer.Object);
            }

            public void Dispose()
            {
                MockContainer.VerifyAll();
            }

        }

        public class ConstructorBehavior : Fixture
        {
            [Fact]
            public void ConfirmWithvalidArgument()
            {
                var system = BuildSystem();
                Assert.Same(MockContainer.Object, system.Container);
            }

            [Fact]
            public void ConfirmThatNullArgumentIsNotAllowed()
            {
                Assert.Throws<ArgumentNullException>(() => new UnityDependencyScope(null));
            }
        }

        public class GetServiceBehavior : Fixture
        {
            [Fact]
            public void ConfirmThatNullArgumentReturnsNull()
            {
                Assert.Null(BuildSystem().GetService(null));
            }

            [Fact]
            public void ConfirmControllerResolution()
            {
                MockContainer.Setup(x => x.Resolve(It.Is<Type>(y => y == typeof(LoggingController)),
                                                    It.Is<string>(y => y == null),
                                                    It.Is<ResolverOverride[]>(y => y.Length == 0))
                    )
                    .Returns(this);


                var result = BuildSystem().GetService(typeof(LoggingController));

                Assert.Same(this, result);
            }

            [Fact]
            public void ConfirmControllerResolutionDoesNotCatchException()
            {
                MockContainer.Setup(x => x.Resolve(It.Is<Type>(y => y == typeof(LoggingController)),
                                                   It.Is<string>(y => y == null),
                                                   It.IsAny<ResolverOverride[]>())
                   )
                   .Throws(new InvalidOperationException());

                Assert.Throws<InvalidOperationException>(() =>
                    {
                        BuildSystem().GetService(typeof(LoggingController));
                    });
            }


            [Fact]
            public void ConfirmNonControllerResolution()
            {
                MockContainer.Setup(x => x.Resolve(It.Is<Type>(y => y == typeof(DependencyAttribute)),
                                                    It.Is<string>(y => y == null),
                                                    It.Is<ResolverOverride[]>(y => y.Length == 0))
                    )
                    .Returns(this);


                var result = BuildSystem().GetService(typeof(DependencyAttribute));

                Assert.Same(this, result);
            }

            [Fact]
            public void ConfirmNonControllerResolutionCatchesException()
            {
                // Ridiculous amount of work to create a ResolutionFailedException from mocks... 
                // therefore, just generate a real one.

                ResolutionFailedException exception = null;
                try
                {
                    new UnityContainer().Resolve<DependencyAttribute>();
                }
                catch (ResolutionFailedException res)
                {
                    exception = res;
                }
                

                MockContainer.Setup(x => x.Resolve(It.Is<Type>(y => y == typeof(DependencyAttribute)),
                                                   It.Is<string>(y => y == null),
                                                   It.Is<ResolverOverride[]>(y => y.Length == 0))
                   )
                   .Throws(exception);

                Assert.Null(BuildSystem().GetService(typeof(DependencyAttribute)));
            }
        }



        public class GetServicesBehavior : Fixture
        {
            [Fact]
            public void ConfirmResolution()
            {

                MockContainer.Setup(x => x.ResolveAll(It.Is<Type>(y => y == typeof(DependencyAttribute)),
                                                    It.Is<ResolverOverride[]>(y => y.Length == 0))
                    )
                    .Returns(new List<object>() { this });


                var result = BuildSystem().GetServices(typeof(DependencyAttribute));

                Assert.Same(this, result.First());
            }

            [Fact]
            public void ConfirmNullAccepted()
            {

                MockContainer.Setup(x => x.ResolveAll(It.Is<Type>(y => y == null),
                                                    It.Is<ResolverOverride[]>(y => y.Length == 0))
                    )
                    .Returns(new List<object>() { this });


                var result = BuildSystem().GetServices(null);

                Assert.Same(this, result.First());
            }
        }

        public class DisposeBehavior : Fixture
        {
            [Fact]
            public void ConfirmDisposalOfContainer()
            {
                MockContainer.Setup(x => x.Dispose());

                BuildSystem().Dispose();
            }
        }
    }
}
