using FlightNode.Common.Api.Models;
using FlightNode.Common.Exceptions;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Domain.Services.Controllers;
using FlightNode.DataCollection.Services.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Results;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Controller
{
    public class LocationsControllerTests
    {
        public class Fixture : LoggingControllerBaseFixture<LocationsController, ILocationDomainManager>
        {
            protected const int Id = 123;
            protected const decimal Longitude = 89.3m;
            protected const decimal Latitude = -34.0m;
            protected const string SiteCode = "code1";
            protected const string SiteName = "name1";
            protected const string City = "city";
            protected const string County = "county";
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
                Assert.Throws<ArgumentNullException>(() => new LocationsController(null));
            }
        }

        public class Get : Fixture
        {


            [Fact]
            public void ConfirmGetMapsCity()
            {
                Assert.Equal(City, RunPositiveTest().City);
            }

            [Fact]
            public void ConfirmGetMapsCounty()
            {
                Assert.Equal(County, RunPositiveTest().County);
            }

            [Fact]
            public void ConfirmGetMapsSiteCode()
            {
                Assert.Equal(SiteCode, RunPositiveTest().SiteCode);
            }

            [Fact]
            public void ConfirmGetMapsSiteName()
            {
                Assert.Equal(SiteName, RunPositiveTest().SiteName);
            }

            [Fact]
            public void ConfirmGetMapsId()
            {
                Assert.Equal(Id, RunPositiveTest().Id);
            }

            [Fact]
            public void ConfirmGetMapsLongitude()
            {
                Assert.Equal(Longitude, RunPositiveTest().Longitude);
            }

            [Fact]
            public void ConfirmMapsLatitude()
            {
                Assert.Equal(Latitude, RunPositiveTest().Latitude);
            }


            private LocationModel RunPositiveTest()
            {
                // Arrange 
                var record = new Location
                {
                    SiteCode = SiteCode,
                    SiteName = SiteName,
                    Latitude = Latitude,
                    Longitude = Longitude,
                    Id = Id,
                    WorkLogs = null,
                    City = City,
                    County = County
                };

                MockDomainManager.Setup(x => x.FindById(It.Is<int>(y => y == Id)))
                    .Returns(record);


                // Act
                var result = BuildSystem().Get(Id);

                HttpResponseMessage message = ExecuteHttpAction(result);

                Assert.Equal(HttpStatusCode.OK, message.StatusCode);

                return ReadResult<LocationModel>(message);
            }

            public class ExceptionHandling : Fixture
            {

                private HttpResponseMessage RunTest(Exception ex)
                {
                    MockDomainManager.Setup(x => x.FindAll())
                        .Throws(ex);


                    return BuildSystem().Get().ExecuteAsync(new System.Threading.CancellationToken()).Result;
                }


                [Fact]
                public void ConfirmHandlingOfUserError()
                {
                    ExpectToLogToDebug();

                    var e = new UserException("asdf");
                    Assert.Equal(HttpStatusCode.BadRequest, RunTest(e).StatusCode);
                }
            }
        }


        public class GetAll : Fixture
        {

            [Fact]
            public void ConfirmGetMapsCity()
            {
                Assert.Equal(City, RunPositiveTest().First().City);
            }

            [Fact]
            public void ConfirmGetMapsCounty()
            {
                Assert.Equal(County, RunPositiveTest().First().County);
            }

            [Fact]
            public void ConfirmGetMapsSiteCode()
            {
                Assert.Equal(SiteCode, RunPositiveTest().First().SiteCode);
            }

            [Fact]
            public void ConfirmGetMapsSiteName()
            {
                Assert.Equal(SiteName, RunPositiveTest().First().SiteName);
            }

            [Fact]
            public void ConfirmGetMapsId()
            {
                Assert.Equal(Id, RunPositiveTest().First().Id);
            }

            [Fact]
            public void ConfirmGetMapsLongitude()
            {
                Assert.Equal(Longitude, RunPositiveTest().First().Longitude);
            }

            [Fact]
            public void ConfirmMapsLatitude()
            {
                Assert.Equal(Latitude, RunPositiveTest().First().Latitude);
            }


            private List<LocationModel> RunPositiveTest()
            {
                // Arrange 
                var records = new List<Location>
                {
                    new Location
                    {
                        SiteCode=SiteCode,
                        SiteName=SiteName,
                        Latitude = Latitude,
                        Longitude= Longitude,
                        Id = Id,
                        WorkLogs = null,
                        City = City,
                        County = County
                    }
                };

                MockDomainManager.Setup(x => x.FindAll())
                    .Returns(records);

                // Act
                var result = BuildSystem().Get();

                var message = result.ExecuteAsync(new System.Threading.CancellationToken()).Result;

                Assert.Equal(HttpStatusCode.OK, message.StatusCode);

                return message.Content.ReadAsAsync<List<LocationModel>>().Result;
            }

            public class ExceptionHandling : Fixture
            {

                private HttpResponseMessage RunTest(Exception ex)
                {
                    MockDomainManager.Setup(x => x.FindAll())
                        .Throws(ex);

                    return BuildSystem().Get().ExecuteAsync(new System.Threading.CancellationToken()).Result;
                }

                [Fact]
                public void ConfirmHandlingOfUserError()
                {
                    ExpectToLogToDebug();

                    var e = new UserException("asdf");
                    Assert.Equal(HttpStatusCode.BadRequest, RunTest(e).StatusCode);
                }
            }
        }


        public class Post : Fixture
        {
            [Fact]
            public void ConfirmMapsCity()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<Location>(y => y.City  == City)));
            }

            [Fact]
            public void ConfirmMapCounty()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<Location>(y => y.County == County)));
            }


            [Fact]
            public void ConfirmMapsSiteCode()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<Location>(y => y.SiteCode == SiteCode)));
            }

            [Fact]
            public void ConfirmMapSiteName()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<Location>(y => y.SiteName == SiteName)));
            }

            [Fact]
            public void ConfirmMapsLongitude()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<Location>(y => y.Longitude == Longitude)));
            }

            [Fact]
            public void ConfirmMapsLatitude()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Create(It.Is<Location>(y => y.Latitude == Latitude)));
            }

            [Fact]
            public void ConfirmUpdatesLocationHeader()
            {
                var expected = "http://some/where/" + Id.ToString();

                Assert.Equal(expected, RunPositiveTest().Headers.Location.ToString());
            }

            [Fact]
            public void ConfirmReturnsCreatedStatus()
            {
                Assert.Equal(HttpStatusCode.Created, RunPositiveTest().StatusCode);
            }

            private LocationModel GetTestResult()
            {
                return RunPositiveTest().Content.ReadAsAsync<LocationModel>().Result;
            }

            private HttpResponseMessage RunPositiveTest()
            {
                // Arrange 
                var record = new LocationModel
                {
                    SiteCode = SiteCode,
                    SiteName = SiteName,
                    Latitude = Latitude,
                    Longitude = Longitude,
                    City = City,
                    County = County
                };

                MockDomainManager.Setup(x => x.Create(It.IsAny<Location>()))
                .Returns((Location actual) =>
                {
                    // inject an ID value so we can confirm that it is passed in the response
                    actual.Id = Id;
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
                    MockDomainManager.Setup(x => x.Create(It.IsAny<Location>()))
                        .Throws(ex);

                    return BuildSystem().Post(new LocationModel()).ExecuteAsync(new System.Threading.CancellationToken()).Result;
                }

                [Fact]
                public void ConfirmDoesNotAcceptNull()
                {
                    Assert.Throws<ArgumentNullException>(() => BuildSystem().Post(null));
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

                    MockDomainManager.Setup(x => x.Create(It.IsAny<Location>()))
                                           .Throws(e);

                    ExpectToLogToDebug();

                    return BuildSystem().Post(new LocationModel()) as InvalidModelStateResult;
                }
            }
        }


        public class Put : Fixture
        {

            [Fact]
            public void ConfirmMapsCity()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Update(It.Is<Location>(y => y.City == City)));
            }

            [Fact]
            public void ConfirmMapCounty()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Update(It.Is<Location>(y => y.County == County)));
            }

            [Fact]
            public void ConfirmMapsSiteCode()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Update(It.Is<Location>(y => y.SiteCode == SiteCode)));
            }

            [Fact]
            public void ConfirmMapsSiteName()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Update(It.Is<Location>(y => y.SiteName == SiteName)));
            }

            [Fact]
            public void ConfirmMapsLongitude()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Update(It.Is<Location>(y => y.Longitude == Longitude)));
            }

            [Fact]
            public void ConfirmMapsLatitude()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Update(It.Is<Location>(y => y.Latitude == Latitude)));
            }

            [Fact]
            public void ConfirmMapsLocationId()
            {
                RunPositiveTest();
                MockDomainManager.Verify(x => x.Update(It.Is<Location>(y => y.Id == Id)));
            }

            [Fact]
            public void ConfirmReturnsNoContenttatus()
            {
                Assert.Equal(HttpStatusCode.NoContent, RunPositiveTest().StatusCode);
            }

            private LocationModel GetTestResult()
            {
                return RunPositiveTest().Content.ReadAsAsync<LocationModel>().Result;
            }

            private HttpResponseMessage RunPositiveTest()
            {
                // Arrange 
                var record = new LocationModel
                {
                    SiteCode = SiteCode,
                    SiteName = SiteName,
                    Latitude = Latitude,
                    Longitude = Longitude,
                    Id = Id,
                    City = City,
                    County = County
                };

                MockDomainManager.Setup(x => x.Update(It.IsAny<Location>()))
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
                    MockDomainManager.Setup(x => x.Update(It.IsAny<Location>()))
                        .Throws(ex);

                    return BuildSystem().Put(new LocationModel()).ExecuteAsync(new System.Threading.CancellationToken()).Result;
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

                    MockDomainManager.Setup(x => x.Update(It.IsAny<Location>()))
                                           .Throws(e);

                    ExpectToLogToDebug();

                    return BuildSystem().Put(new LocationModel()) as InvalidModelStateResult;
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
                var records = new List<Location>
                {
                    new Location
                    {
                        SiteName = Description1,
                        Id = Id1
                    },
                    new Location
                    {
                        SiteName = Description2,
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
