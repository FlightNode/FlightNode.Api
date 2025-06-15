using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using FlightNode.DataCollection.Domain.Managers;
using Moq;
using System;
using Xunit;

namespace FlightNode.DataCollection.Domain.UnitTests.Domain.Managers.WaterBirdForaging
{
    public class WhenExportingResults 
    {
        public class Fixture : IDisposable
        {
            protected MockRepository MockRepository { get; set; }
            protected Mock<ISurveyPersistence> SurveyPersistenceMock { get; set; }
            protected SurveyManager Manager { get; set; }

            public Fixture()
            {
                MockRepository = new MockRepository(MockBehavior.Strict);
                SurveyPersistenceMock = MockRepository.Create<ISurveyPersistence>();

                Manager = new SurveyManager(SurveyPersistenceMock.Object);
            }

            public void Dispose()
            {
                MockRepository.VerifyAll();
            }

            [Fact]
            public void ThenGetAllResults()
            {
                SurveyPersistenceMock.SetupGet(x => x.ForagingSurveyExport)
                    .Returns(FakeDbSet<ForagingSurveyExportItem>.Create(
                        new ForagingSurveyExportItem(),
                        new ForagingSurveyExportItem()
                        ));

                var result = Manager.ExportAllForagingSurveys();

                Assert.Equal(2, result.Count);
            }
        }       

    }
}
