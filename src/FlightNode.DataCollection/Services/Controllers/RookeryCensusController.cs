using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Services.Models.Survey;
using System;
using System.Web.Http;

namespace FlightNode.DataCollection.Services.Controllers
{
    /// <summary>
    /// API Controller for submitting Rookery Censuses.
    /// </summary>
    public class RookeryCensusController : SurveyControllerBase<RookeryCensusModel>
    {


        /// <summary>
        /// Creates a new instance of <see cref="RookeryCensusController"/>.
        /// </summary>
        /// <param name="domainManager">An instance of <see cref="IWorkLogDomainManager"/></param>
        public RookeryCensusController(ISurveyManager domainManager) : base(domainManager)
        {
        }

        /// <summary>
        /// Retrieves the requested Rookery Censuses data.
        /// </summary>
        /// <param name="surveyIdentifier">
        /// Unique identifier for the survey resource to retrieve.
        /// </param>
        /// <returns>
        /// 200 with the survey data
        /// 400 if not found
        /// </returns>
        [HttpGet]
        [Authorize]
        [Route("api/v1/RookeryCensus/{surveyIdentifier:Guid}")]
        public IHttpActionResult Get(Guid surveyIdentifier)
        {
            return GetSurveyById(surveyIdentifier, SurveyType.Rookery);
        }

        /// <summary>
        /// Retrieves a list of all Rookery Census information, including both pending and completed surveys.
        /// </summary>
        /// <returns>
        /// 200 with a list of <see cref="Domain.Entities.SurveyListItem"/>
        /// </returns>
        /// <example>
        /// GET api/v1/RookeryCensus/
        /// GET api/v1/RookeryCensus/?userId={userId}
        /// </example>
        [HttpGet]
        [Authorize]
        public IHttpActionResult Get([FromUri]int? userId = null)
        {
            return GetSurveysByType(SurveyType.Rookery, userId);
        }

        /// <summary>
        /// Retrieves all completed surveys for data export.
        /// </summary>
        /// <returns>
        /// 200 with a list of <see cref="RookeryCensusExportItem"/>
        /// </returns>
        [HttpGet]
        [Authorize]
        [Route("api/v1/RookeryCensus/export")]
        public IHttpActionResult Export()
        {
            return Ok(_domainManager.ExportAllRookeryCensuses());
        }

        /// <summary>
        /// Creates a new Rookery Census record
        /// </summary>
        /// <param name="input">An instance of <see cref="RookeryCensusModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody]RookeryCensusModel input)
        {
            return Create(input);
        }

        /// <summary>
        /// Updates an existing new Rookery Census record
        /// </summary>
        /// <param name="surveyIdentifier"></param>
        /// <param name="input">An instance of <see cref="RookeryCensusModel"/></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/RookeryCensus/{surveyIdentifier:Guid}")]
        [Authorize]
        public IHttpActionResult Put(Guid surveyIdentifier, [FromBody]RookeryCensusModel input)
        {
            return Update(surveyIdentifier, input);
        }



        protected override RookeryCensusModel Map(ISurvey input)
        {
            var entity = new RookeryCensusModel
            {
                AccessPointId = input.AccessPointId,
                SiteTypeId = input.AssessmentId,
                DisturbanceComments = input.DisturbanceComments,
                SurveyComments = input.GeneralComments,
                LocationId = input.LocationId,
                SurveyIdentifier = input.SurveyIdentifier,
                VantagePointId = input.VantagePointId,
                SurveyId = input.Id,
                Observers = input.Observers,
                StartDate = input.StartDate.HasValue ? input.StartDate.Value.ToShortDateString() : string.Empty,
                StartTime = input.StartDate.HasValue ? input.StartDate.Value.ToShortTimeString() : string.Empty,
                EndTime = input.EndDate.HasValue ? input.EndDate.Value.ToShortTimeString() : string.Empty,
                Updating = input.Completed,
                PrepTimeHours = input.PrepTimeHours,
                SubmittedBy = input.SubmittedBy
            };

            foreach (var o in input.Observations)
            {
                entity.Add(new ObservationModel
                {
                    Adults = o.Bin3,
                    NestsPresent = o.NestPresent,
                    FledglingsPresent = o.FledglingPresent,
                    ChicksPresent = o.ChicksPresent,
                    BirdSpeciesId = o.BirdSpeciesId,
                    ObservationId = o.Id
                });
            }

            foreach (var d in input.Disturbances)
            {
                entity.Add(Map(d));
            }

            return entity;
        }

        protected override SurveyPending MapToPendingSurvey(RookeryCensusModel input, Guid identifier)
        {

            var entity = new SurveyPending();
            MapRookeryCensusIntoSurvey(entity, input, identifier);
            MapObservationsIntoSurvey(entity, input, identifier);
            MapDisturbancesIntoSurvey(entity, input, identifier);

            return entity;
        }

        protected override SurveyCompleted MapToCompletedSurvey(RookeryCensusModel input, Guid identifier)
        {

            var entity = new SurveyCompleted();
            MapRookeryCensusIntoSurvey(entity, input, identifier);
            MapObservationsIntoSurvey(entity, input, identifier);
            MapDisturbancesIntoSurvey(entity, input, identifier);

            return entity;
        }

        private void MapRookeryCensusIntoSurvey(ISurvey survey, RookeryCensusModel input, Guid identifier)
        {
            survey.AccessPointId = input.AccessPointId;
            survey.AssessmentId = input.SiteTypeId;
            survey.DisturbanceComments = input.DisturbanceComments;
            survey.GeneralComments = input.SurveyComments;
            survey.LocationId = input.LocationId;
            survey.SurveyIdentifier = identifier;
            survey.SurveyTypeId = SurveyType.Rookery;
            survey.VantagePointId = input.VantagePointId;
            survey.SubmittedBy = this.LookupUserId();
            survey.Observers = input.Observers;
            survey.Id = input.SurveyId;
            survey.StartDate = ParseDateTime(input.StartDate, input.StartTime);
            survey.EndDate = ParseDateTime(input.StartDate, input.EndTime);
            survey.PrepTimeHours = input.PrepTimeHours;
            survey.SubmittedBy = input.SubmittedBy;
        }

        private void MapObservationsIntoSurvey(ISurvey survey, RookeryCensusModel input, Guid identifier)
        {
            foreach (var o in input.Observations)
            {
                survey.Add(new Observation
                {
                    Bin3 = o.Adults,
                    NestPresent = o.NestsPresent,
                    FledglingPresent = o.FledglingsPresent,
                    ChicksPresent = o.ChicksPresent,
                    BirdSpeciesId = o.BirdSpeciesId,
                    SurveyIdentifier = identifier,
                    Id = o.ObservationId
                });
            }
        }

        private void MapDisturbancesIntoSurvey(ISurvey survey, RookeryCensusModel input, Guid identifier)
        {
            foreach (var d in input.Disturbances)
            {
                survey.Add(Map(d, identifier));
            }
        }
    }
}
