using FlightNode.Common.BaseClasses;
using FlightNode.Common.Exceptions;
using System;
using Xunit;

namespace FlightNode.Common.UnitTests.Exceptions
{
    public class ServerExceptionTest
    {
        public class Entity : IEntity
        {
            public int Id { get; set; }
        }

        public class WrapAGeneralExceptionInServerExceptionWithNonNullId
        {
            const int id = 23423423;
            const string action = "Update";
            const string message = "this is a message";
            static Exception exception = new Exception(message);
            static Type type = typeof(Entity);

            private ServerException RunTheTest()
            {
                return ServerException.HandleException<Entity>(exception, action, id);
            }

            [Fact]
            public void ConfirmThatTheExceptionMessageIsStaticConstant()
            {
                Assert.Equal(ServerException.MESSAGE, RunTheTest().Message);
            }

            [Fact]
            public void ConfirmTheInnerException()
            {
                Assert.Same(exception, RunTheTest().InnerException);
            }

            [Fact]
            public void ConfirmTheContentDescriptionProperty()
            {
                Assert.Equal(message, RunTheTest().Content.Description);
            }

            [Fact]
            public void ConfirmTheContentIdProperty()
            {
                Assert.Equal(id, RunTheTest().Content.Id);
            }

            [Fact]
            public void ConfirmTheContentActionProperty()
            {
                Assert.Equal(action, RunTheTest().Content.Action);
            }

            [Fact]
            public void ConfirmTheContentModelTypeProperty()
            {
                Assert.Equal(type, RunTheTest().Content.ModelType);
            }
        }

        public class CreateExceptionForAFailedDatabaseUpdate
        {
            const string description = "Something bad happened";
            const int id = 23423423;
            const string action = "Update";
            static Type type = typeof(Entity);


            private static ServerException RunTheTest()
            {
                return ServerException.UpdateFailed<Entity>(description, id);
            }

            [Fact]
            public void ConfirmThatTheExceptionMessageIsStaticConstant()
            {
                Assert.Equal(ServerException.MESSAGE, RunTheTest().Message);
            }

            [Fact]
            public void ConfirmTheContentDescriptionProperty()
            {
                Assert.Equal(description, RunTheTest().Content.Description);
            }

            [Fact]
            public void ConfirmTheContentIdProperty()
            {
                Assert.Equal(id, RunTheTest().Content.Id);
            }

            [Fact]
            public void ConfirmTheContentActionProperty()
            {
                Assert.Equal(action, RunTheTest().Content.Action);
            }

            [Fact]
            public void ConfirmTheContentModelTypeProperty()
            {
                Assert.Equal(type, RunTheTest().Content.ModelType);
            }
        }
    }
}
