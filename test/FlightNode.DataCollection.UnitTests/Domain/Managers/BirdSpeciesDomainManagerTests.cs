using System;
using System.Linq;
using Xunit;
using Moq;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.Common.UnitTests;
using Ploeh.AutoFixture;
using System.Data.Entity.Infrastructure;
using FlightNode.Common.Exceptions;
using FlightNode.DataCollection.Infrastructure.Persistence;

namespace FlightNode.DataCollection.Domain.UnitTests.Domain.Managers
{
    public class BirdSpeciesDomainManagerTests
    {
        public class Fixture : IDisposable
        {
            protected MockRepository MockRepository { get; set; }
            protected Mock<IBirdSpeciesPersistence> BirdSpeciesPersistenceMock { get; set; }


            public Fixture()
            {
                MockRepository = new MockRepository(MockBehavior.Strict);
                BirdSpeciesPersistenceMock = MockRepository.Create<IBirdSpeciesPersistence>();
            }

            public void Dispose()
            {
                MockRepository.VerifyAll();
            }
        }

        public class GetBirdSpeciesBySurveyTypeId : Fixture
        {
            [Fact]
            public void ThereAreNoBirds()
            {
                // Arrange
                BirdSpeciesPersistenceMock.SetupGet(x => x.Collection)
                    .Returns(new FakeDbSet<BirdSpecies>());

                // Act
                var result = new BirdSpeciesDomainManager(BirdSpeciesPersistenceMock.Object)
                                        .GetBirdSpeciesBySurveyTypeId(0);

                // Assert
                Assert.Equal(0, result.Count());
            }

            [Fact]
            public void ThereIsOneMatch()
            {
                // Arrange
                const int surveyTypeId = 23;
                const string species = "Blue Jay";
                var bird = new BirdSpecies
                {
                    Species = species
                };
                bird.SurveyTypes.Add(new SurveyType { Id = surveyTypeId });

                var set = new FakeDbSet<BirdSpecies>();
                set.Add(bird);

                BirdSpeciesPersistenceMock.SetupGet(x => x.Collection)
                    .Returns(set);

                // Act
                var result = new BirdSpeciesDomainManager(BirdSpeciesPersistenceMock.Object)
                                        .GetBirdSpeciesBySurveyTypeId(surveyTypeId);

                // Assert
                Assert.Equal(species, result.First().Species);
            }


            [Fact]
            public void ThereIsABirdButNotWithThisSurvey()
            {
                // Arrange
                const int surveyTypeId = 23;
                const string species = "Blue Jay";
                var bird = new BirdSpecies
                {
                    Species = species
                };
                bird.SurveyTypes.Add(new SurveyType { Id = surveyTypeId + 100 });

                var set = new FakeDbSet<BirdSpecies>();
                set.Add(bird);

                BirdSpeciesPersistenceMock.SetupGet(x => x.Collection)
                    .Returns(set);

                // Act
                var result = new BirdSpeciesDomainManager(BirdSpeciesPersistenceMock.Object)
                                        .GetBirdSpeciesBySurveyTypeId(surveyTypeId);

                // Assert
                Assert.Equal(0, result.Count());
            }
        }

        public class AddSpeciesToSurvey : BaseTester
        {
            protected const int SpeciesId = 23;
            protected const int SurveyTypeId = 23423;
            protected const string Description = "Survey Type";
            protected Mock<IBirdSpeciesPersistence> PersistenceMock;
            protected BirdSpecies Bird = new BirdSpecies
            {
                Id = SpeciesId
            };

            public AddSpeciesToSurvey()
            {
                PersistenceMock = Fixture.Freeze<Mock<IBirdSpeciesPersistence>>();
            }

            protected void ArrangeToRetrieveSurveyType()
            {
                var dbSet = new FakeDbSet<SurveyType>();
                dbSet.Add(new SurveyType
                {
                    Id = SurveyTypeId,
                    Description = Description
                });

                PersistenceMock.SetupGet(x => x.SurveyTypes)
                    .Returns(dbSet);
            }

            protected void ArrangeToRetrieveSpecies()
            {
                var dbSet = new FakeDbSet<BirdSpecies>();
                dbSet.Add(Bird);

                PersistenceMock.SetupGet(x => x.Collection)
                    .Returns(dbSet);
            }

            protected void ExpectToSaveChanges()
            {
                PersistenceMock.Setup(x => x.SaveChanges())
                    .Returns(1);
            }

            protected void Act()
            {
                var system = Fixture.Create<BirdSpeciesDomainManager>();
                system.AddSpeciesToSurveyType(SpeciesId, SurveyTypeId);
            }

            public class HappyPath : AddSpeciesToSurvey
            {
                [Fact]
                public void SavesChangesToPersistenceLayer()
                {
                    ArrangeToRetrieveSpecies();
                    ArrangeToRetrieveSurveyType();
                    ExpectToSaveChanges();

                    Act();

                    PersistenceMock.Verify(x => x.SaveChanges());
                }

                [Fact]
                public void UpdateTheSpeciesObject()
                {
                    ArrangeToRetrieveSpecies();
                    ArrangeToRetrieveSurveyType();
                    ExpectToSaveChanges();

                    Act();

                    Assert.Equal(SurveyTypeId, Bird.SurveyTypes.First().Id);
                }


                [Fact]
                public void VerifyRetrievalOfSurveyType()
                {
                    ArrangeToRetrieveSpecies();
                    ArrangeToRetrieveSurveyType();
                    ExpectToSaveChanges();

                    Act();

                    PersistenceMock.VerifyGet(x => x.SurveyTypes);
                }

            }

            public class Negative : AddSpeciesToSurvey
            {
                [Fact]
                public void DuplicateKeyShouldNotThrowException()
                {
                    ArrangeToRetrieveSpecies();
                    ArrangeToRetrieveSurveyType();

                    PersistenceMock.Setup(x => x.SaveChanges())
                        .Throws(new DbUpdateException("An error occurred while saving entities that do not expose foreign key properties for their relationships. The EntityEntries property will return null because a single entity cannot be identified as the source of the exception. Handling of exceptions while saving can be made easier by exposing foreign key properties in your entity types. See the InnerException for details. ", new System.Data.Entity.Core.UpdateException("An error occurred while updating the entries. See the inner exception for details.", new Exception("Violation of PRIMARY KEY constraint 'PK_dbo.SurveyType_BirdSpecies'. Cannot insert duplicate key in object 'dbo.SurveyType_BirdSpecies'. The duplicate key value is (1, 2)"))));

                    Act();
                }

                [Fact]
                public void SpeciesDoesNotExistThrowsProperException()
                {
                    ArrangeThatTheSpeciesDoesNotExist();

                    Assert.Throws<DoesNotExistException>(() => Act());
                }

                private void ArrangeThatTheSpeciesDoesNotExist()
                {
                    PersistenceMock.SetupGet(x => x.Collection)
                        .Returns(new FakeDbSet<BirdSpecies>()); // Note that it is empty
                }

                [Fact]
                public void SpeciesDoesNotExistErrorContainsProperMessage()
                {
                    ArrangeThatTheSpeciesDoesNotExist();
                    try
                    {
                        Act();
                        throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        Assert.Equal("Species ID " + SpeciesId.ToString() + " not found.", ex.Message);
                    }
                }



                [Fact]
                public void SurveyTypeDoesNotExistThrowsProperException()
                {
                    ArrangeToRetrieveSpecies();
                    ArrangeThatTheSurveyTypeDoesNotExist();

                    Assert.Throws<DoesNotExistException>(() => Act());
                }

                [Fact]
                public void SurveyTypeDoesNotExistErrorContainsProperMessage()
                {
                    ArrangeToRetrieveSpecies();
                    ArrangeThatTheSurveyTypeDoesNotExist();

                    try
                    {
                        Act();
                        throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        Assert.Equal("Survey Type ID " + SurveyTypeId.ToString() + " not found.", ex.Message);
                    }
                }

                protected void ArrangeThatTheSurveyTypeDoesNotExist()
                {
                    PersistenceMock.SetupGet(x => x.SurveyTypes)
                        .Returns(new FakeDbSet<SurveyType>()); // Empty result
                }

                [Fact]
                public void RethrowOtherExceptions()
                {
                    ArrangeToRetrieveSpecies();

                    PersistenceMock.SetupGet(x => x.SurveyTypes)
                        .Throws(new DbUpdateException("not a duplicate key exception"));

                    Assert.Throws<DbUpdateException>(() => Act());

                }
            }

        }
        
        public class RemoveSpeciesFromSurvey : BaseTester
        {
            protected const int SpeciesId = 23;
            protected const int SurveyTypeId = 23423;
            protected const string Description = "Survey Type";
            protected Mock<IBirdSpeciesPersistence> PersistenceMock { get; set; }
            protected BirdSpecies Bird = new BirdSpecies
            {
                Id = SpeciesId
            };
            protected Mock<IDbEntityEntryDecorator> EntryDecoratorMock { get; set; }
            protected Mock<IDbCollectionEntryDecorator> CollectionMock { get; set; }

            public RemoveSpeciesFromSurvey()
            {
                PersistenceMock = Fixture.Freeze<Mock<IBirdSpeciesPersistence>>();
            }
            

            protected void ArrangeToRetrieveSpecies()
            {
                var dbSet = new FakeDbSet<BirdSpecies>();
                dbSet.Add(Bird);

                PersistenceMock.SetupGet(x => x.Collection)
                    .Returns(dbSet);
            }

            protected void ExpectToSaveChanges()
            {
                PersistenceMock.Setup(x => x.SaveChanges())
                    .Returns(1);
            }

            protected void Act()
            {
                var system = Fixture.Create<BirdSpeciesDomainManager>();
                system.RemoveSpeciesFromSurveyType(SpeciesId, SurveyTypeId);
            }

            protected void ArrangeToRetrieveAssociatedSurveyTypes()
            {
                EntryDecoratorMock = Fixture.Freeze<Mock<IDbEntityEntryDecorator>>();
                PersistenceMock.Setup(x => x.Entry(It.IsAny<object>()))
                    .Returns(EntryDecoratorMock.Object);

                CollectionMock = Fixture.Freeze<Mock<IDbCollectionEntryDecorator>>();

                EntryDecoratorMock.Setup(x => x.Collection("SurveyTypes"))
                    .Returns(CollectionMock.Object);

                // mimics what the real EF call would do
                CollectionMock.Setup(x => x.Load())
                    .Callback(() =>
                    {
                        Bird.SurveyTypes.Add(new SurveyType
                        {
                            Id = SurveyTypeId,
                            Description = Description
                        });
                    });
            }

            protected void ArrangeThatTheSpeciesDoesNotBelongToTheSurveyType()
            {

                EntryDecoratorMock = Fixture.Freeze<Mock<IDbEntityEntryDecorator>>();
                PersistenceMock.Setup(x => x.Entry(It.IsAny<object>()))
                    .Returns(EntryDecoratorMock.Object);

                CollectionMock = Fixture.Freeze<Mock<IDbCollectionEntryDecorator>>();

                EntryDecoratorMock.Setup(x => x.Collection("SurveyTypes"))
                    .Returns(CollectionMock.Object);

                // mimics what the real EF call would do
                CollectionMock.Setup(x => x.Load())
                    .Callback(() =>
                    {
                        Bird.SurveyTypes.Add(new SurveyType
                        {
                            Id = SurveyTypeId + 100, // SOME OTHER SURVEY TYPE!
                            Description = Description
                        });
                    });
            }

            public class HappyPath : RemoveSpeciesFromSurvey
            {
                [Fact]
                public void SavesChangesToPersistenceLayer()
                {
                    ArrangeToRetrieveSpecies();
                    ArrangeToRetrieveAssociatedSurveyTypes();
                    ExpectToSaveChanges();

                    Act();

                    PersistenceMock.Verify(x => x.SaveChanges());
                }

                [Fact]
                public void UpdateTheSpeciesObject()
                {
                    ArrangeToRetrieveSpecies();
                    ArrangeToRetrieveAssociatedSurveyTypes();
                    ExpectToSaveChanges();

                    Act();

                    Assert.Empty(Bird.SurveyTypes);
                }

            }

            public class Negative : RemoveSpeciesFromSurvey
            {
                [Fact]
                public void SpeciesDoesNotExistThrowsProperException()
                {
                    ArrangeThatTheSpeciesDoesNotExist();
                    
                    Assert.Throws<DoesNotExistException>(() => Act());
                }

                private void ArrangeThatTheSpeciesDoesNotExist()
                {
                    PersistenceMock.SetupGet(x => x.Collection)
                        .Returns(new FakeDbSet<BirdSpecies>()); // Note that it is empty
                }

                [Fact]
                public void SpeciesDoesNotExistErrorContainsProperMessage()
                {
                    ArrangeThatTheSpeciesDoesNotExist();
                    try
                    {
                        Act();
                        throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        Assert.Equal("Species ID " + SpeciesId.ToString() + " not found.", ex.Message);
                    }
                }



                [Fact]
                public void SpeciesIsNotAssignedToSurveyDoesNotThrowException()
                {
                    ArrangeToRetrieveSpecies();
                    ArrangeThatTheSpeciesDoesNotBelongToTheSurveyType();

                    Act();
                }

                [Fact]
                public void RethrowOtherExceptions()
                {
                    PersistenceMock.SetupGet(x => x.Collection)
                        .Throws(new DbUpdateException("not a duplicate key exception"));

                    Assert.Throws<DbUpdateException>(() => Act());

                }
            }

        }

        public class FindById : BaseTester
        {
            const int Id = 23423;
            const int SurveyId = 233;
            const string SurveyName = "Point Count";

            [Fact]
            public void SpeciesExists()
            {
                ArrangeForReturnOfOneBird();

                var result = Act();

                Assert.Equal(Id, result.Id);
            }

            [Fact]
            public void LazyLoadsSurveyTypes()
            {
                var fakeSet = ArrangeForReturnOfOneBird();

                Act();

                _collectionMock.Verify(x => x.Load());
            }

            [Fact]
            public void FlattensSurveyTypeNames()
            {
                ArrangeForReturnOfOneBird();

                var result = Act();

                Assert.Equal(result.SurveyTypeNames.First(), SurveyName);
            }

            [Fact]
            public void SpeciesDoesNotExistCreatesDoesNotExistExeption()
            {
                ArrangeForAnAlternateDataset();

                Assert.Throws<DoesNotExistException>(() => Act());
            }


            private BirdSpecies Act()
            {
                var system = Fixture.Create<BirdSpeciesDomainManager>();
                var result = system.FindById(Id);
                return result;
            }

            private FakeDbSet<BirdSpecies> ArrangeForReturnOfOneBird()
            {
                var bird = new BirdSpecies { Id = Id };

                var fakeSet = new FakeDbSet<BirdSpecies>();
                fakeSet.Add(bird);

                var persistenceMock = Fixture.Freeze<Mock<IBirdSpeciesPersistence>>();
                persistenceMock.SetupGet(x => x.Collection)
                    .Returns(fakeSet);

                var surveyType = new SurveyType { Id = SurveyId, Description = SurveyName };
                bird.SurveyTypes.Add(surveyType);

                var entityMock = Fixture.Freeze<Mock<IDbEntityEntryDecorator>>();
                _collectionMock = Fixture.Freeze<Mock<IDbCollectionEntryDecorator>>();
                _collectionMock.Setup(x => x.Load());

                entityMock.Setup(x => x.Collection(It.IsAny<string>()))
                    .Returns(_collectionMock.Object);

                persistenceMock.Setup(x => x.Entry(It.IsAny<object>()))
                    .Returns(entityMock.Object);
                
                return fakeSet;
            }

            Mock<IDbCollectionEntryDecorator> _collectionMock;

            private void ArrangeForAnAlternateDataset()
            {
                var fakeSet = new FakeDbSet<BirdSpecies>();
                fakeSet.Add(new BirdSpecies { Id = Id + 100});

                var persistenceMock = Fixture.Freeze<Mock<IBirdSpeciesPersistence>>();
                persistenceMock.SetupGet(x => x.Collection)
                    .Returns(fakeSet);
            }
        }
    }
}
