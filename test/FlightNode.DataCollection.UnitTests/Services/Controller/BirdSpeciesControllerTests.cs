using FlightNode.Common.UnitTests;
using FlightNode.DataCollection.Domain.Services.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;
using Moq;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Domain.Entities;
using Xunit;
using System.Net.Http;
using System.Net;
using log4net;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Controller
{
    public class BirdSpeciesControllerTests : BaseTester
    {
        public static class Constants
        {
            public const int SpeciesId = 123;
            public const string CommonAlphaCode = "AMRO";
            public const string CommonName = "American Robin";
            public const string Order = "Passeriformes";
            public const string Family = "Turdidae";
            public const string SubFamily = "(none)";
            public const string Genus = "Turdus";
            public const string Species = "migratorius";
            public const int SurveTypeId = 3423;
        }

        private void ArrangeForDebugLogging()
        {
            Fixture.Freeze<Mock<ILog>>()
                .Setup(x => x.Debug(It.IsAny<string>()));
        }

        protected BirdSpeciesController CreateSystem()
        {
            return Fixture.Create<BirdSpeciesController>();
        }

        public class Constructor: BirdSpeciesControllerTests
        {
            [Fact]
            public void HappyPathThrowsNoExceptions()
            {
                Fixture.Create<BirdSpeciesController>();
            }

            [Fact]
            public void RejectsNullArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new BirdSpeciesController(null));
            }
        }

        public class Get : BirdSpeciesControllerTests
        {
            public class Counts : Get
            {
                [Fact]
                public void ThereIsOneSpecies()
                {
                    ArrangeToReturnOneSpecies();

                    var response = Act();
                    Assert.Equal(1, response.Count());
                }

                [Fact]
                public void ThereAreNoSpecies()
                {
                    ArrangeToReturnEmptyList();

                    var response = Act();

                    Assert.Empty(response);
                }
            }

            public class MapsField : Get
            {
                [Fact]
                public void CommonAlphaCode()
                {
                    ArrangeToReturnOneSpecies();

                    var response = Act();

                    Assert.Equal(Constants.CommonAlphaCode, response.First().CommonAlphaCode);
                }

                [Fact]
                public void CommonName()
                {
                    ArrangeToReturnOneSpecies();

                    var response = Act();

                    Assert.Equal(Constants.CommonName, response.First().CommonName);
                }

                [Fact]
                public void Family()
                {
                    ArrangeToReturnOneSpecies();

                    var response = Act();

                    Assert.Equal(Constants.Family, response.First().Family);
                }

                [Fact]
                public void Genus()
                {
                    ArrangeToReturnOneSpecies();

                    var response = Act();

                    Assert.Equal(Constants.Genus, response.First().Genus);
                }

                [Fact]
                public void Id()
                {
                    ArrangeToReturnOneSpecies();

                    var response = Act();

                    Assert.Equal(Constants.SpeciesId, response.First().Id);
                }

                [Fact]
                public void Order()
                {
                    ArrangeToReturnOneSpecies();

                    var response = Act();

                    Assert.Equal(Constants.Order, response.First().Order);
                }

                [Fact]
                public void Species()
                {
                    ArrangeToReturnOneSpecies();

                    var response = Act();

                    Assert.Equal(Constants.Species, response.First().Species);
                }

                [Fact]
                public void SubFamily()
                {
                    ArrangeToReturnOneSpecies();

                    var response = Act();

                    Assert.Equal(Constants.SubFamily, response.First().SubFamily);
                }
            }

            private List<BirdSpecies> Act()
            {
                return ReadResult<List<BirdSpecies>>(ExecuteHttpAction(CreateSystem().Get(new BirdQuery())));
            }

            private void ArrangeToReturnOneSpecies()
            {
                var domainManagerMock = Fixture.Freeze<Mock<IBirdSpeciesDomainManager>>();
                domainManagerMock.Setup(x => x.FindAll())
                    .Returns(new[] {  new BirdSpecies
                    {
                        CommonAlphaCode = Constants.CommonAlphaCode,
                        CommonName = Constants.CommonName,
                        Family = Constants.Family,
                        Genus = Constants.Genus,
                        Id = Constants.SpeciesId,
                        Order = Constants.Order,
                        Species = Constants.Species,
                        SubFamily = Constants.SubFamily
                    }});
            }

            private void ArrangeToReturnEmptyList()
            {
                var domainManagerMock = Fixture.Freeze<Mock<IBirdSpeciesDomainManager>>();
                domainManagerMock.Setup(x => x.FindAll())
                    .Returns(new List<BirdSpecies>());
            }
        }

        public class PostSurveyType : BirdSpeciesControllerTests
        {
            [Fact]
            public void HappyPathReturnsNoContent()
            {
                ArrangeDomainCallToAddSpeciesToSurveyType();

                var response = Act();

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }

            [Fact]
            public void HappyPathPassesCorrectSurveyType()
            {
                var domainManagerMock = ArrangeDomainCallToAddSpeciesToSurveyType();

                Act();

                domainManagerMock.Verify(x => x.AddSpeciesToSurveyType(It.IsAny<int>(), Constants.SurveTypeId));
            }

            [Fact]
            public void HappyPathPassesCorrectSpecies()
            {
                var domainManagerMock = ArrangeDomainCallToAddSpeciesToSurveyType();

                Act();

                domainManagerMock.Verify(x => x.AddSpeciesToSurveyType(Constants.SpeciesId, It.IsAny<int>()));
            }
            
            private Mock<IBirdSpeciesDomainManager> ArrangeDomainCallToAddSpeciesToSurveyType()
            {
                var domainManagerMock = Fixture.Freeze<Mock<IBirdSpeciesDomainManager>>();
                domainManagerMock.Setup(x => x.AddSpeciesToSurveyType(It.IsAny<int>(), It.IsAny<int>()));

                return domainManagerMock;
            }

            private HttpResponseMessage Act()
            {
                return ExecuteHttpAction(CreateSystem().PostSurveyType(Constants.SpeciesId, Constants.SurveTypeId));
            }
        }


        public class DeleteSurveyType : BirdSpeciesControllerTests
        {
            [Fact]
            public void HappyPathReturnsNoContent()
            {
                ArrangeDomainCallToRemoveSpeciesFromSurveyType();

                var response = Act();

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }

            [Fact]
            public void HappyPathPassesCorrectSurveyType()
            {
                var domainManagerMock = ArrangeDomainCallToRemoveSpeciesFromSurveyType();

                Act();

                domainManagerMock.Verify(x => x.RemoveSpeciesFromSurveyType(It.IsAny<int>(), Constants.SurveTypeId));
            }

            [Fact]
            public void HappyPathPassesCorrectSpecies()
            {
                var domainManagerMock = ArrangeDomainCallToRemoveSpeciesFromSurveyType();

                Act();

                domainManagerMock.Verify(x => x.RemoveSpeciesFromSurveyType(Constants.SpeciesId, It.IsAny<int>()));
            }

     

            private Mock<IBirdSpeciesDomainManager> ArrangeDomainCallToRemoveSpeciesFromSurveyType()
            {
                var domainManagerMock = Fixture.Freeze<Mock<IBirdSpeciesDomainManager>>();
                domainManagerMock.Setup(x => x.RemoveSpeciesFromSurveyType(It.IsAny<int>(), It.IsAny<int>()));

                return domainManagerMock;
            }

            private HttpResponseMessage Act()
            {
                return ExecuteHttpAction(CreateSystem().DeleteSurveyType(Constants.SpeciesId, Constants.SurveTypeId));
            }
        }
    }
}
