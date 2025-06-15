using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Results;
using FlightNode.Common.Api.Models;
using FlightNode.Common.Exceptions;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Domain.Services.Controllers;
using FlightNode.DataCollection.Services.Models;
using FlightNode.DataCollection.Services.Models.WorkLog;
using Moq;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Controller
{
    public class WorkTypeControllerTests
    {
        public class Fixture : LoggingControllerBaseFixture<WorkTypesController, IWorkTypeDomainManager>
        {
        }


        public class ArgumentTests : Fixture
        {
            [Fact]
            public void ConfirmConstructorHappyPath()
            {
                BuildSystem();
            }

            [Fact]
            public void ConfirmConstructorRejectsNullArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new WorkTypesController(null));
            }
        }

        public class Get : Fixture
        {
            private int _id = 123;
            private string _description = "somewhere";

            [Fact]
            public void ConfirmGetMapsDescription()
            {
                Assert.Equal(_description, RunPositiveTest().Description);
            }


            [Fact]
            public void ConfirmGetMapsId()
            {
                Assert.Equal(_id, RunPositiveTest().Id);
            }
            

            private WorkTypeModel RunPositiveTest()
            {
                // Arrange 
                var record = new WorkType
                {
                    Description = _description,
                    Id = _id,
                    WorkLogs = null
                };

                MockDomainManager.Setup(x => x.FindById(It.Is<int>(y => y == _id)))
                    .Returns(record);


                // Act
                var result = BuildSystem().Get(_id);

                var message = result.ExecuteAsync(new System.Threading.CancellationToken()).Result;

                Assert.Equal(HttpStatusCode.OK, message.StatusCode);

                return message.Content.ReadAsAsync<WorkTypeModel>().Result;
            }
            
        }


        public class GetAll : Fixture
        {
            private int _id = 123;
            private string _description = "somewhere";

            [Fact]
            public void ConfirmGetMapsDescription()
            {
                Assert.Equal(_description, RunPositiveTest().First().Description);
            }


            [Fact]
            public void ConfirmGetMapsId()
            {
                Assert.Equal(_id, RunPositiveTest().First().Id);
            }
            
            private List<WorkTypeModel> RunPositiveTest()
            {
                // Arrange 
                var records = new List<WorkType>
                {
                    new WorkType
                    {
                        Description = _description,
                        Id = _id,
                        WorkLogs = null
                    }
                };

                MockDomainManager.Setup(x => x.FindAll())
                    .Returns(records);

                // Act
                var result = BuildSystem().Get();

                var message = result.ExecuteAsync(new System.Threading.CancellationToken()).Result;

                Assert.Equal(HttpStatusCode.OK, message.StatusCode);

                return message.Content.ReadAsAsync<List<WorkTypeModel>>().Result;
            }
            
        }


        public class Post : Fixture
        {
            private int _id = 123;
            private string _description = "somewhere";

            [Fact]
            public void ConfirmMapsDescription()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<WorkType>(y => y.Description == _description)));
            }
            
            [Fact]
            public void ConfirmUpdatesLocationHeader()
            {
                var expected = "http://some/where/" + _id.ToString();

                Assert.Equal(expected, RunPositiveTest().Headers.Location.ToString());
            }

            [Fact]
            public void ConfirmReturnsCreatedStatus()
            {
                Assert.Equal(HttpStatusCode.Created, RunPositiveTest().StatusCode);
            }

            private HttpResponseMessage RunPositiveTest()
            {
                // Arrange 
                var record = new WorkTypeModel
                {
                    Description = _description,
                };

                MockDomainManager.Setup(x => x.Create(It.IsAny<WorkType>()))
                .Returns((WorkType actual) =>
                {
                    // inject an ID value so we can confirm that it is passed in the response
                    actual.Id = _id;
                    return actual;
                });


                // Act
                var result = BuildSystem().Post(record);

                var message = result.ExecuteAsync(new System.Threading.CancellationToken()).Result;

                return message;

            }

            public class ExceptionHandling : Fixture
            {

                protected HttpResponseMessage RunTest(Exception ex)
                {
                    MockDomainManager.Setup(x => x.Create(It.IsAny<WorkType>()))
                        .Throws(ex);

                    return BuildSystem().Post(new WorkTypeModel()).ExecuteAsync(new System.Threading.CancellationToken()).Result;
                }

                [Fact]
                public void ConfirmDoesNotAcceptNull()
                {
                    Assert.Throws<ArgumentNullException>(() => BuildSystem().Post(null));
                }
            }

          
        }


        public class Put : Fixture
        {
            private int _id = 123;
            private string _description = "somewhere";

            [Fact]
            public void ConfirmMapsDescription()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Update(It.Is<WorkType>(y => y.Description == _description)));
            }
            

            [Fact]
            public void ConfirmMapsWorkTypeId()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Update(It.Is<WorkType>(y => y.Id == _id)));
            }

            [Fact]
            public void ConfirmReturnsNoContenttatus()
            {
                Assert.Equal(HttpStatusCode.NoContent, RunPositiveTest().StatusCode);
            }


            private HttpResponseMessage RunPositiveTest()
            {
                // Arrange 
                var record = new WorkTypeModel
                {
                    Description = _description,
                    Id = _id
                };

                MockDomainManager.Setup(x => x.Update(It.IsAny<WorkType>()))
                    .Returns(1);


                // Act
                var result = BuildSystem().Put(record);

                var message = result.ExecuteAsync(new System.Threading.CancellationToken()).Result;

                return message;

            }

            public class ExceptionHandling : Fixture
            {

                private HttpResponseMessage RunTest(Exception ex)
                {
                    MockDomainManager.Setup(x => x.Update(It.IsAny<WorkType>()))
                        .Throws(ex);

                    return BuildSystem().Put(new WorkTypeModel()).ExecuteAsync(new System.Threading.CancellationToken()).Result;
                }

                [Fact]
                public void ConfirmDoesNotAcceptNull()
                {
                    Assert.Throws<ArgumentNullException>(() => BuildSystem().Put(null));
                }
             

                [Fact]
                public void ConfirmHandlingOfUserError()
                {
                    ExpectToLogToDebug();

                    var e = new UserException("asdf");
                    Assert.Equal(HttpStatusCode.BadRequest, RunTest(e).StatusCode);
                }
            }



            public class ValidationExceptionHandling : Fixture
            {

                private const string Message1 = "asdfasdfa";
                private const string Field1 = "fieeeeeld";
                private const string Message2 = "as8df7a89psdfp";
                private const string Field2 = "sdk;kl;hl;";


                [Fact]
                public void ConfirmSendsField1InModelState()
                {
                    Assert.True(
                        RunTest().ModelState
                            .FirstOrDefault(x => x.Key == Field1)
                            .Value
                            .Errors
                            .Any(x => x.ErrorMessage == Message1)
                    );
                }

                [Fact]
                public void ConfirmSendsField2InModelState()
                {
                    Assert.True(
                        RunTest().ModelState
                            .FirstOrDefault(x => x.Key == Field2)
                            .Value
                            .Errors
                            .Any(x => x.ErrorMessage == Message2)
                    );
                }

                private InvalidModelStateResult RunTest()
                {
                    var list = new List<ValidationResult>
                    {
                        new ValidationResult(Message1, new [] { Field1 }),
                        new ValidationResult(Message2, new [] { Field2 })
                    };

                    var e = DomainValidationException.Create(list);

                    MockDomainManager.Setup(x => x.Update(It.IsAny<WorkType>()))
                                           .Throws(e);

                    ExpectToLogToDebug();

                    return BuildSystem().Put(new WorkTypeModel()) as InvalidModelStateResult;
                }
            }
        }


        public class GetSimpleList : Fixture
        {
            private const int Id1 = 123;
            private const int Id2 = 52334;
            private const string Description1 = "somewhere";
            private const string Description2 = "else";

            [Fact]
            public void ConfirmReturnsOkStatus()
            {
                Assert.Equal(HttpStatusCode.OK, RunPositiveTest().StatusCode);
            }

            [Fact]
            public void ConfirmReturnsFirstItem()
            {
                Assert.Equal(Description1, GetTestResults().FirstOrDefault(x => x.Id == Id1).Value);
            }

            [Fact]
            public void ConfirmReturnsSecondItem()
            {
                Assert.Equal(Description2, GetTestResults().FirstOrDefault(x => x.Id == Id2).Value);
            }

            private List<SimpleListItem> GetTestResults()
            {

                return RunPositiveTest().Content.ReadAsAsync<List<SimpleListItem>>().Result;
            }

            private HttpResponseMessage RunPositiveTest()
            {
                // Arrange 
                var records = new List<WorkType>
                {
                    new WorkType
                    {
                        Description = Description1,
                        Id = Id1
                    },
                    new WorkType
                    {
                        Description = Description2,
                        Id = Id2
                    }
                };

                MockDomainManager.Setup(x => x.FindAll())
                    .Returns(records);

                // Act
                var result = BuildSystem().GetSimpleList();

                var message = result.ExecuteAsync(new System.Threading.CancellationToken()).Result;

                return message;
            }

            public class ExceptionHandling : Fixture
            {

                private HttpResponseMessage RunTest(Exception ex)
                {
                    MockDomainManager.Setup(x => x.FindAll())
                        .Throws(ex);

                    return BuildSystem().GetSimpleList().ExecuteAsync(new System.Threading.CancellationToken()).Result;
                }
               
            }
        }
    }
}
