using FlightNode.Common.Exceptions;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using FlightNode.DataCollection.Domain.Managers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Domain.Managers
{
    public class LocationDomainManagerTests
    {
        public class Fixture : IDisposable
        {
            protected MockRepository MockRepository { get; set; }
            protected Mock<ILocationPersistence> LocationPersistenceMock { get; set; }

            public Fixture()
            {
                MockRepository = new MockRepository(MockBehavior.Loose);
                LocationPersistenceMock = MockRepository.Create<ILocationPersistence>();
            }

            protected LocationDomainManager BuildSystem()
            {
                return new LocationDomainManager(LocationPersistenceMock.Object);
            }

            public void Dispose()
            {
            }
        }

        public class ArgumentTests : Fixture
        {
            [Fact]
            public void ConfirmHappyPath()
            {
                BuildSystem();
            }

            [Fact]
            public void ConfirmThatNullArgumentIsNotAllowed()
            {
                Assert.Throws<ArgumentNullException>(() => new LocationDomainManager(null));
            }

            [Fact]
            public void ConfirmCreateDoesNotAcceptNullArgument()
            {
                Assert.Throws<ArgumentNullException>(() => BuildSystem().Create(null));
            }


            [Fact]
            public void ConfirmUpdateDoesNotAcceptNullArgument()
            {
                Assert.Throws<ArgumentNullException>(() => BuildSystem().Update(null));
            }
        }

        public class CreateFakeSet : Fixture
        {
            protected FakeDbSet<Location> FakeSet = new FakeDbSet<Location>();

            protected void SetupLocationsCollection()
            {
                LocationPersistenceMock.SetupGet(x => x.Collection)
                    .Returns(FakeSet);
            }

            protected Location BuildDefault()
            {
                return new Location
                {
                    SiteCode = "code1",
                    SiteName = "name1",
                    Latitude = 0m,
                    Longitude = 0m,
                    Id = 0,
                    WorkLogs = null,
                    County = "United States",
                    City = "Texas"
                };
            }
        }

        public class Create : CreateFakeSet
        {
            private const int RecordCount = 1;


            [Fact]
            public void ConfirmReturnsModifiedRecord()
            {

                // Arrange
                var item = BuildDefault();
                var id = 34234;
                SetupLocationsCollection();
                LocationPersistenceMock.Setup(x => x.SaveChanges())
                    .Callback(() => item.Id = id)
                    .Returns(RecordCount);

                LocationPersistenceMock.Setup(x => x.Add(item));

                // Act
                BuildSystem().Create(item);

                // Assert
                Assert.Equal(id, item.Id);
            }


            [Fact]
            public void ConfirmInputIsSavedToPersistenceLayer()
            {

                // Arrange
                var item = BuildDefault();
                var id = 34234;
                SetupLocationsCollection();
                LocationPersistenceMock.Setup(x => x.SaveChanges())
                    .Callback(() => item.Id = id)
                    .Returns(RecordCount);

                LocationPersistenceMock.Setup(x => x.Add(item));

                // Act
                BuildSystem().Create(item);

                // Assert
                LocationPersistenceMock.Verify(x => x.SaveChanges(), Times.Once);
                LocationPersistenceMock.Verify(x => x.Add(item), Times.Once);
            }


            public class Validation : CreateFakeSet
            {

                // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
                private void RunNegativeTest(Location item, string memberName)
                {
                    try
                    {
                        LocationPersistenceMock.Setup(x => x.Add(item));

                        BuildSystem().Create(item);
                        throw new Exception("this should have failed");
                    }
                    catch (DomainValidationException de)
                    {
                        Assert.True(de.ValidationResults.Any(x => x.MemberNames.Any(y => y == memberName)));
                    }
                }

                private void RunPositiveTest(Location item)
                {
                    SetupLocationsCollection();
                    LocationPersistenceMock.Setup(x => x.SaveChanges())
                        .Returns(1);
                    LocationPersistenceMock.Setup(x => x.Add(item));

                    BuildSystem().Create(item);
                }

                [Fact]
                public void ConfirmCityCannotBeNull()
                {
                    var item = BuildDefault();
                    item.City = null;

                    RunNegativeTest(item, "City");
                }

                [Fact]
                public void ConfirmCityCanBeBlank()
                {
                    var item = BuildDefault();
                    item.City = string.Empty;

                    RunPositiveTest(item);
                }

                [Fact]
                public void ConfirmCountyCannotBeNull()
                {
                    var item = BuildDefault();
                    item.County = null;

                    RunNegativeTest(item, "County");
                }

                [Fact]
                public void ConfirmCountyCanBeBlank()
                {
                    var item = BuildDefault();
                    item.County = string.Empty;

                    RunPositiveTest(item);
                }


                [Fact]
                public void ConfirmSiteCodeCannotBeNull()
                {
                    var item = BuildDefault();
                    item.SiteCode = null;

                    RunNegativeTest(item, "SiteCode");
                }

                [Fact]
                public void ConfirmSiteNameCannotBeNull()
                {
                    var item = BuildDefault();
                    item.SiteName = null;

                    RunNegativeTest(item, "SiteName");
                }
                
                [Fact]
                public void ConfirmSiteCodeIsRequired()
                {
                    var item = BuildDefault();
                    item.SiteCode = string.Empty;

                    RunNegativeTest(item, "SiteCode");
                }

                [Fact]
                public void ConfirmSiteNameIsRequired()
                {
                    var item = BuildDefault();
                    item.SiteName = string.Empty;

                    RunNegativeTest(item, "SiteName");
                }


         

                [Fact]
                public void ConfirmSiteCodeRejectsGreaterThan100()
                {
                    var item = BuildDefault();
                    item.SiteCode = "a".PadLeft(101, '0');

                    RunNegativeTest(item, "SiteCode");
                }

                [Fact]
                public void ConfirmSiteNameRejectsGreaterThan100()
                {
                    var item = BuildDefault();
                    item.SiteName = "a".PadLeft(101, '0');

                    RunNegativeTest(item, "SiteName");
                }

                [Fact]
                public void ConfirmLatitudeRejectsGreaterThan90()
                {
                    var item = BuildDefault();
                    item.Latitude = 90.1m;

                    RunNegativeTest(item, "Latitude");
                }

                [Fact]
                public void ConfirmLatitudeAccepts90()
                {
                    var item = BuildDefault();
                    item.Latitude = 90.0m;
                    RunPositiveTest(item);
                }

                [Fact]
                public void ConfirmLatitudeRejectsLessThanNeg90()
                {
                    var item = BuildDefault();
                    item.Latitude = -90.1m;

                    RunNegativeTest(item, "Latitude");
                }

                [Fact]
                public void ConfirmLatitudeAcceptsNeg90()
                {
                    var item = BuildDefault();
                    item.Latitude = -90.0m;
                    RunPositiveTest(item);
                }

                [Fact]
                public void ConfirmLongitudeAccepts180()
                {
                    var item = BuildDefault();
                    item.Longitude = 180.0m;
                    RunPositiveTest(item);
                }

                [Fact]
                public void ConfirmLongitudeRejectsGreaterThan180()
                {
                    var item = BuildDefault();
                    item.Longitude = 182.1m;

                    RunNegativeTest(item, "Longitude");
                }

                [Fact]
                public void ConfirmLongitudeAcceptsNeg180()
                {
                    var item = BuildDefault();
                    item.Longitude = -180.0m;
                    RunPositiveTest(item);
                }

                [Fact]
                public void ConfirmLongitudeRejectsGreaterThanNeg180()
                {
                    var item = BuildDefault();
                    item.Longitude = -180.1m;

                    RunNegativeTest(item, "Longitude");
                }


            }
        }

        public class FindAll : CreateFakeSet
        {
            [Fact]
            public void ConfirmHappyPath()
            {
                // Arrange
                SetupLocationsCollection();
                var one = new Location();
                FakeSet.List.Add(one);

                // Act
                var result = BuildSystem().FindAll();

                // Assert
                Assert.Collection(result, x => Assert.Same(one, x));
            }
        }

        public class FindById : CreateFakeSet
        {
            [Fact]
            public void ConfirmHappyPath()
            {
                // Arrange
                const int id = 23423;
                SetupLocationsCollection();
                var one = new Location();
                var two = new Location { Id = id };
                FakeSet.List.AddRange(new List<Location>() { one, two });

                // Act
                var result = BuildSystem().FindById(id);

                // Assert
                Assert.Same(two, result);
            }
        }

        public class Update : CreateFakeSet
        {
            private const int RecordCount = 1;


            [Fact]
            public void ConfirmHappyPath()
            {

                // Arrange
                var item = BuildDefault();
                SetupLocationsCollection();

                LocationPersistenceMock.Setup(x => x.Add(item));

                LocationPersistenceMock.Setup(x => x.SaveChanges())
                    .Returns(RecordCount);

                // Act
                var recordCount = BuildSystem().Update(item);

                // Assert
                Assert.Equal(RecordCount, recordCount);
            }

            public class Validation : Update
            {

                private void RunPositiveTest(Location item)
                {
                    SetupLocationsCollection();
                    
                    LocationPersistenceMock.Setup(x => x.SaveChanges())
                           .Returns(1);
                    LocationPersistenceMock.Setup(x => x.Add(item));

                    BuildSystem().Update(item);
                }

                // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
                private void RunNegativeTest(Location item, string memberName)
                {
                    try
                    {
                        LocationPersistenceMock.Setup(x => x.Add(item));
                        BuildSystem().Update(item);
                        throw new Exception("this should have failed");
                    }
                    catch (DomainValidationException de)
                    {
                        Assert.True(de.ValidationResults.Any(x => x.MemberNames.Any(y => y == memberName)));
                    }
                }
                

                [Fact]
                public void ConfirmSiteCodeCannotBeNull()
                {
                    var item = BuildDefault();
                    item.SiteCode = null;

                    RunNegativeTest(item, "SiteCode");
                }

                [Fact]
                public void ConfirmSiteNameCannotBeNull()
                {
                    var item = BuildDefault();
                    item.SiteName = null;

                    RunNegativeTest(item, "SiteName");
                }
                
                [Fact]
                public void ConfirmSiteCodeIsRequired()
                {
                    var item = BuildDefault();
                    item.SiteCode = string.Empty;

                    RunNegativeTest(item, "SiteCode");
                }

                [Fact]
                public void ConfirmSiteNameIsRequired()
                {
                    var item = BuildDefault();
                    item.SiteName = string.Empty;

                    RunNegativeTest(item, "SiteName");
                }
                

                [Fact]
                public void ConfirmSiteCodeRejectsGreaterThan100()
                {
                    var item = BuildDefault();
                    item.SiteCode = "a".PadLeft(101, '0');

                    RunNegativeTest(item, "SiteCode");
                }

                [Fact]
                public void ConfirmSiteNameRejectsGreaterThan100()
                {
                    var item = BuildDefault();
                    item.SiteName = "a".PadLeft(101, '0');

                    RunNegativeTest(item, "SiteName");
                }

                [Fact]
                public void ConfirmLatitudeRejectsGreaterThan90()
                {
                    var item = BuildDefault();
                    item.Latitude = 90.1m;

                    RunNegativeTest(item, "Latitude");
                }

                [Fact]
                public void ConfirmLatitudeAccepts90()
                {
                    var item = BuildDefault();
                    item.Latitude = 90.0m;
                    RunPositiveTest(item);
                }

                [Fact]
                public void ConfirmLatitudeRejectsLessThanNeg90()
                {
                    var item = BuildDefault();
                    item.Latitude = -90.1m;

                    RunNegativeTest(item, "Latitude");
                }

                [Fact]
                public void ConfirmLatitudeAcceptsNeg90()
                {
                    var item = BuildDefault();
                    item.Latitude = -90.0m;
                    RunPositiveTest(item);
                }

                [Fact]
                public void ConfirmLongitudeAccepts180()
                {
                    var item = BuildDefault();
                    item.Longitude = 180.0m;
                    RunPositiveTest(item);
                }

                [Fact]
                public void ConfirmLongitudeRejectsGreaterThan180()
                {
                    var item = BuildDefault();
                    item.Longitude = 180.1m;

                    RunNegativeTest(item, "Longitude");
                }
                [Fact]
                public void ConfirmLongitudeAcceptsNeg180()
                {
                    var item = BuildDefault();
                    item.Longitude = -180.0m;
                    RunPositiveTest(item);
                }

                [Fact]
                public void ConfirmLongitudeRejectsGreaterThanNeg180()
                {
                    var item = BuildDefault();
                    item.Longitude = -180.1m;

                    RunNegativeTest(item, "Longitude");
                }
            }
        }
    }
}
