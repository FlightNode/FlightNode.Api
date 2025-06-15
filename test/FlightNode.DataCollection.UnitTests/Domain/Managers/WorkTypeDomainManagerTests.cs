using FlightNode.Common.Exceptions;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using FlightNode.DataCollection.Domain.Managers;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Domain.Managers
{
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    public class WorkTypeDomainManagertests
    {
        public class Fixture : IDisposable
        {
            protected MockRepository MockRepository { get; set; }
            protected Mock<IWorkTypePersistence> WorkTypePersistenceMock { get; set; }

            public Fixture()
            {
                MockRepository = new MockRepository(MockBehavior.Loose);
                WorkTypePersistenceMock = MockRepository.Create<IWorkTypePersistence>();
            }

            protected WorkTypeDomainManager BuildSystem()
            {
                return new WorkTypeDomainManager(WorkTypePersistenceMock.Object);
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
                Assert.Throws<ArgumentNullException>(() => new WorkTypeDomainManager(null));
            }

            [Fact]
            public void ConfirmCreateDoesNotAcceptNullArgument()
            {
                // need to move this to another class, maybe in a renamed constructor class.
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
            protected FakeDbSet<WorkType> FakeSet = new FakeDbSet<WorkType>();

            protected void SetupWorkTypesCollection()
            {
                WorkTypePersistenceMock.SetupGet(x => x.Collection)
                    .Returns(FakeSet);
            }
        }

        public class Create : CreateFakeSet
        {
            private const int RecordCount = 1;
            private readonly WorkType _item = new WorkType
            {
                Description = "this is a valid description",
                Id = 1,
                WorkLogs = null
            };


            [Fact]
            public void ConfirmReturnsModifiedRecord()
            {

                // Arrange
                var id = 34234;
                SetupWorkTypesCollection();
                WorkTypePersistenceMock.Setup(x => x.SaveChanges())
                    .Callback(() => _item.Id = id)
                    .Returns(RecordCount);
                WorkTypePersistenceMock.Setup(x => x.Add(_item));

                // Act
                BuildSystem().Create(_item);

                // Assert
                Assert.Equal(id, _item.Id);
            }


            [Fact]
            public void ConfirmInputIsSavedToPersistenceLayer()
            {

                // Arrange
                var id = 34234;
                SetupWorkTypesCollection();
                WorkTypePersistenceMock.Setup(x => x.SaveChanges())
                    .Callback(() => _item.Id = id)
                    .Returns(RecordCount);
                WorkTypePersistenceMock.Setup(x => x.Add(_item));

                // Act
                BuildSystem().Create(_item);

                // Assert
                WorkTypePersistenceMock.Verify(x => x.SaveChanges(), Times.Once);
                WorkTypePersistenceMock.Verify(x => x.Add(_item), Times.Once);
            }


            public class Validation : CreateFakeSet
            {
                private WorkType _item = new WorkType
                {
                    Description = "this is a valid description",
                    Id = 1,
                    WorkLogs = null
                };

                private void RunNegativeTest(string memberName)
                {
                    try
                    {
                        BuildSystem().Create(_item);
                        throw new Exception("this should have failed");
                    }
                    catch (DomainValidationException de)
                    {
                        Assert.True(de.ValidationResults.Any(x => x.MemberNames.Any(y => y == memberName)));
                    }
                }


                [Fact]
                public void ConfirmDescriptionCannotBeNull()
                {
                    _item.Description = null;

                    RunNegativeTest("Description");
                }

                [Fact]
                public void ConfirmDescriptionIsRequired()
                {
                    _item.Description = string.Empty;

                    RunNegativeTest("Description");
                }

                [Fact]
                public void ConfirmDescriptionRejectsGreaterThan100()
                {
                    _item.Description = "a".PadLeft(101, '0');

                    RunNegativeTest("Description");
                }


            }
        }

        public class FindAll : CreateFakeSet
        {
            [Fact]
            public void ConfirmHappyPath()
            {
                // Arrange
                SetupWorkTypesCollection();
                var one = new WorkType();
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
                var id = 23423;
                SetupWorkTypesCollection();
                var one = new WorkType();
                var two = new WorkType { Id = id };
                FakeSet.List.AddRange(new List<WorkType>() { one, two });

                // Act
                var result = BuildSystem().FindById(id);

                // Assert
                Assert.Same(two, result);
            }
        }

        public class Update : CreateFakeSet
        {
            private const int RecordCount = 1;
            private readonly WorkType _item = new WorkType
            {
                Description = "this is a valid description",
                Id = 0,
                WorkLogs = null
            };


            
            [Fact]
            public void ConfirmHappyPath()
            {

                // Arrange
                SetupWorkTypesCollection();

                WorkTypePersistenceMock.Setup(x => x.Update(_item));
                WorkTypePersistenceMock.Setup(x => x.SaveChanges())
                    .Returns(RecordCount);

                // Act
                BuildSystem().Update(_item);

                // Assert
                WorkTypePersistenceMock.Verify(x => x.Update(_item), Times.Once);
                WorkTypePersistenceMock.Verify(x => x.SaveChanges(), Times.Once);
            }

            public class Validation : Update
            {

                private void RunNegativeTest(string memberName)
                {
                    try
                    {
                        BuildSystem().Update(_item);
                        throw new Exception("this should have failed");
                    }
                    catch (DomainValidationException de)
                    {
                        Assert.True(de.ValidationResults.Any(x => x.MemberNames.Any(y => y == memberName)));
                    }
                }

                [Fact]
                public void ConfirmDescriptionCannotBeNull()
                {
                    _item.Description = null;

                    RunNegativeTest("Description");
                }

                [Fact]
                public void ConfirmDescriptionIsRequired()
                {
                    _item.Description = string.Empty;

                    RunNegativeTest("Description");
                }

                [Fact]
                public void ConfirmDescriptionMaxLength100()
                {
                    _item.Description = "a".PadLeft(101, '0');

                    RunNegativeTest("Description");
                }

                [Fact]
                public void ConfirmDescriptionRejectsGreaterThan100()
                {
                    _item.Description = "a".PadLeft(101, '0');

                    RunNegativeTest("Description");
                }
            }
        }
    }
}
