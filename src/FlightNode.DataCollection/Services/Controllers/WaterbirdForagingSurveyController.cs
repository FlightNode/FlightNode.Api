using FlightNode.Common.Api;
using FlightNode.Common.Exceptions;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Services.Models.Survey;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Http;

namespace FlightNode.DataCollection.Services.Controllers
{
    /// <summary>
    /// API Controller for submitting Waterbird Foraging surveys.
    /// </summary>
    public class WaterbirdForagingSurveyController : SurveyControllerBase<WaterbirdForagingModel>
    {
        private const string IdentifierRoute = "api/v1/waterbirdforagingsurvey/{surveyIdentifier:Guid}";

        /// <summary>
        /// Creates a new instance of <see cref="WaterbirdForagingSurveyController"/>.
        /// </summary>
        /// <param name="domainManager">An instance of <see cref="IWorkLogDomainManager"/></param>
        public WaterbirdForagingSurveyController(ISurveyManager domainManager) : base(domainManager)
        {
        }

        /// <summary>
        /// Retrieves the requested Waterbird Foraging Survey data.
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
        [Route(IdentifierRoute)]
        public IHttpActionResult Get(Guid surveyIdentifier)
        {
            return GetSurveyById(surveyIdentifier, SurveyType.Foraging);
        }

        /// <summary>
        /// Retrieves a list of all Waterbird Foraging information, including both pending and completed surveys.
        /// </summary>
        /// <returns>
        /// 200 with a list of <see cref="Domain.Entities.SurveyListItem"/>
        /// </returns>
        /// <example>
        /// GET api/v1/WaterbirdForagingSurvey/
        /// GET api/v1/WaterbirdForagingSurvey/?userId={userId}
        /// </example>
        [HttpGet]
        [Authorize]
        public IHttpActionResult Get([FromUri]int? userId = null)
        {
            return GetSurveysByType(SurveyType.Foraging, userId);
        }

        /// <summary>
        /// Retrieves all completed surveys for data export.
        /// </summary>
        /// <returns>
        /// 200 with a list of <see cref="ForagingSurveyExportItem"/>
        /// </returns>
        [HttpGet]
        [Authorize]
        [Route("api/v1/waterbirdforagingsurvey/export")]
        public IHttpActionResult Export()
        {
            return Ok(_domainManager.ExportAllForagingSurveys());
        }

        /// <summary>
        /// Creates a new waterbird foraging survey record
        /// </summary>
        /// <param name="input">An instance of <see cref="WaterbirdForagingModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody]WaterbirdForagingModel input)
        {
            return Create(input);
        }

        /// <summary>
        /// Updates an existing new waterbird foraging survey record
        /// </summary>
        /// <param name="surveyIdentifier"></param>
        /// <param name="input">An instance of <see cref="WaterbirdForagingModel"/></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/waterbirdforagingsurvey/{surveyIdentifier:Guid}")]
        [Authorize]
        public IHttpActionResult Put(Guid surveyIdentifier, [FromBody]WaterbirdForagingModel input)
        {
            Validate(input);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Update(surveyIdentifier, input);
        }



        /// <summary>
        /// Deletes a survey.
        /// </summary>
        /// <param name="surveyIdentifier">Survey identifier</param>
        /// <returns>
        /// 204 if successful
        /// 404 if not found
        /// 500 if an exception occurs
        /// </returns>
        [HttpDelete]
        [Authorize]
        [Route(IdentifierRoute)]
        public IHttpActionResult Delete([FromUri] Guid surveyIdentifier)
        {
            // TODO: add protection so that malicious user can't delete
            // anything but own records, unless an administrator

            if (_domainManager.FindBySurveyId(surveyIdentifier, SurveyType.Foraging) != null)
            {
                if (_domainManager.Delete(surveyIdentifier))
                {
                    return NoContent();
                }
            }

            return NotFound();
        }

        protected override WaterbirdForagingModel Map(ISurvey input)
        {
            var entity = new WaterbirdForagingModel
            {
                AccessPointId = input.AccessPointId,
                SiteTypeId = input.AssessmentId,
                DisturbanceComments = input.DisturbanceComments,
                SurveyComments = input.GeneralComments,
                LocationId = input.LocationId,
                Temperature = input.Temperature,
                SurveyIdentifier = input.SurveyIdentifier,
                WindDrivenTide = input.WindDrivenTide,
                TimeLowTide = input.TimeOfLowTide.HasValue ? input.TimeOfLowTide.Value.ToShortTimeString() : string.Empty,
                VantagePointId = input.VantagePointId,
                WeatherId = input.WeatherId,
                WindSpeed = input.WindSpeed,
                SurveyId = input.Id,
                Observers = input.Observers,
                WaterHeightId = input.WaterHeightId,
                StartDate = input.StartDate.HasValue ? input.StartDate.Value.ToShortDateString() : string.Empty,
                StartTime = input.StartDate.HasValue ? input.StartDate.Value.ToShortTimeString() : string.Empty,
                EndTime = input.EndDate.HasValue ? input.EndDate.Value.ToShortTimeString() : string.Empty,
                Updating = input.Completed,
                WindDirection = input.WindDirection,
                SubmittedBy = input.SubmittedBy,
                PrepTimeHours = input.PrepTimeHours
            };

            foreach (var o in input.Observations)
            {
                entity.Add(new ObservationModel
                {
                    Adults = o.Bin1,
                    Juveniles = o.Bin2,
                    BirdSpeciesId = o.BirdSpeciesId,
                    FeedingId = o.FeedingSuccessRate,
                    HabitatId = o.HabitatTypeId,
                    PrimaryActivityId = o.PrimaryActivityId,
                    SecondaryActivityId = o.SecondaryActivityId,
                    ObservationId = o.Id
                });
            }

            foreach (var d in input.Disturbances)
            {
                entity.Add(Map(d));
            }

            return entity;
        }

        protected override SurveyPending MapToPendingSurvey(WaterbirdForagingModel input, Guid identifier)
        {

            var entity = new SurveyPending();
            MapForagingInputIntoSurvey(entity, input, identifier);
            MapObservationsIntoSurvey(entity, input, identifier);
            MapDisturbancesIntoSurvey(entity, input, identifier);

            return entity;
        }

        protected override SurveyCompleted MapToCompletedSurvey(WaterbirdForagingModel input, Guid identifier)
        {

            var entity = new SurveyCompleted();
            MapForagingInputIntoSurvey(entity, input, identifier);
            MapObservationsIntoSurvey(entity, input, identifier);
            MapDisturbancesIntoSurvey(entity, input, identifier);

            return entity;
        }

        private void MapForagingInputIntoSurvey(ISurvey survey, WaterbirdForagingModel input, Guid identifier)
        {

            // This function exposes an architectural problem: by focusing 
            // validation in the businesslogic layer, we've neglected to 
            // validate input data transfer objects that must be translated 
            // into business objects. Thus we do not get any validation of the
            // formatting on a date string, for example.

            // Added validation to the startdate field through model attribute.
            // TODO: time validation, maybe some other fields.

            survey.AccessPointId = input.AccessPointId;
            survey.AssessmentId = input.SiteTypeId;
            survey.DisturbanceComments = input.DisturbanceComments;
            survey.GeneralComments = input.SurveyComments;
            survey.LocationId = input.LocationId;
            survey.Temperature = input.Temperature;
            survey.SurveyIdentifier = identifier;
            survey.WindDrivenTide = input.WindDrivenTide;
            survey.SurveyTypeId = SurveyType.Foraging;
            survey.VantagePointId = input.VantagePointId;
            survey.WeatherId = input.WeatherId;
            survey.WindSpeed = input.WindSpeed;
            survey.SubmittedBy = input.SubmittedBy;
            survey.Observers = input.Observers;
            survey.Id = input.SurveyId;
            survey.WaterHeightId = input.WaterHeightId;
            survey.StartDate = ParseDateTime(input.StartDate, input.StartTime);
            survey.EndDate = ParseDateTime(input.StartDate, input.EndTime);
            survey.WindDirection = input.WindDirection;
            survey.PrepTimeHours = input.PrepTimeHours;

            var tempDate = DateTime.MaxValue;
            if (DateTime.TryParse(input.TimeLowTide, out tempDate))
            {
                survey.TimeOfLowTide = tempDate;
            }
            else
            {
                survey.TimeOfLowTide = null;
            }
        }

        private void MapObservationsIntoSurvey(ISurvey survey, WaterbirdForagingModel input, Guid identifier)
        {
            foreach (var o in input.Observations)
            {
                survey.Add(new Observation
                {
                    Bin1 = o.Adults,
                    Bin2 = o.Juveniles ?? 0,
                    BirdSpeciesId = o.BirdSpeciesId,
                    FeedingSuccessRate = o.FeedingId,
                    HabitatTypeId = o.HabitatId,
                    PrimaryActivityId = o.PrimaryActivityId,
                    SecondaryActivityId = o.SecondaryActivityId,
                    SurveyIdentifier = identifier,
                    Id = o.ObservationId
                });
            }
        }

        private void MapDisturbancesIntoSurvey(ISurvey survey, WaterbirdForagingModel input, Guid identifier)
        {
            foreach (var d in input.Disturbances)
            {
                survey.Add(Map(d, identifier));
            }
        }
    }
}
