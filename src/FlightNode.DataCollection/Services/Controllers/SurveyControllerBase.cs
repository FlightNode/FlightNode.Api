using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Services.Models.Survey;
using FligthNode.Common.Api.Controllers;
using Microsoft.AspNet.Identity;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace FlightNode.DataCollection.Services.Controllers
{
    /// <summary>
    /// API Controller for submitting Waterbird Foraging surveys.
    /// </summary>
    public abstract class SurveyControllerBase<TSurveyModel> : LoggingController
        where TSurveyModel : ISurveyModel
    {

        protected const string COMPLETE = "Complete";
        protected const string PENDING = "Pending";
        protected const string MISSING = "missing";

        protected readonly ISurveyManager _domainManager;

        /// <summary>
        /// Creates a new instance of <see cref="SurveyControllerBase"/>.
        /// </summary>
        /// <param name="domainManager">An instance of <see cref="IWorkLogDomainManager"/></param>
        protected SurveyControllerBase(ISurveyManager domainManager)
        {
            if (domainManager == null)
            {
                throw new ArgumentNullException(nameof(domainManager));
            }

            _domainManager = domainManager;
        }

        /// <summary>
        /// Retrieves the requested Waterbird Foraging Survey data.
        /// </summary>
        /// <param name="surveyIdentifier">
        /// Unique identifier for the survey resource to retrieve.
        /// </param>
        /// <param name="surveyTypeId">Survey Type Id</param>
        /// <returns>
        /// 200 with the survey data
        /// 400 if not found
        /// </returns>
        protected IHttpActionResult GetSurveyById(Guid surveyIdentifier, int surveyTypeId)
        {
            var result = _domainManager.FindBySurveyId(surveyIdentifier, surveyTypeId);

            if (result == null)
            {
                return NotFound();
            }

            var model = Map(result);

            return Ok(model);
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
        protected IHttpActionResult GetSurveysByType(int surveyTypeId, int? userId = null)
        {
            return Ok(_domainManager.GetSurveyListByTypeAndUser(surveyTypeId, userId));
        }

        /// <summary>
        /// Creates a new waterbird foraging survey record
        /// </summary>
        /// <returns></returns>
        protected IHttpActionResult Create(TSurveyModel input)
        {
            if (input == null)
            {
                return BadRequest("null input");
            }


            var identifier = _domainManager.NewIdentifier();

            var entity = MapToPendingSurvey(input, identifier);

            entity = _domainManager.Create(entity);

            var result = Map(entity);

            return Created(result, identifier.ToString());
        }



        /// <summary>
        /// Updates an existing new waterbird foraging survey record
        /// </summary>
        /// <param name="surveyIdentifier"></param>
        /// <returns></returns>
        protected IHttpActionResult Update(Guid surveyIdentifier, TSurveyModel input)
        {

            if (input == null)
            {
                return BadRequest("null input");
            }

            if (surveyIdentifier == Guid.Empty)
            {
                return BadRequest("Invalid Survey Identifier");
            }

            TSurveyModel result;

            if (input.Updating)
            {
                var entity = MapToCompletedSurvey(input, surveyIdentifier);
                result = Map(_domainManager.Update(entity));
            }
            else
            {
                var entity = MapToPendingSurvey(input, surveyIdentifier);

                if (input.FinishedEditing)
                {
                    result = Map(_domainManager.Finish(entity));
                }
                else
                {
                    result = Map(_domainManager.Update(entity));
                }
            }

            return Ok(result);
        }

        protected abstract SurveyPending MapToPendingSurvey(TSurveyModel input, Guid identifier);

        protected abstract SurveyCompleted MapToCompletedSurvey(TSurveyModel input, Guid identifier);

        protected abstract TSurveyModel Map(ISurvey input);

        protected DisturbanceModel Map(Disturbance input)
        {
            return new DisturbanceModel
            {
                DisturbanceTypeId = input.DisturbanceTypeId,
                DurationMinutes = input.DurationMinutes,
                Quantity = input.Quantity,
                Behavior = input.Result,
                DisturbanceId = input.Id
            };
        }

        protected Disturbance Map(DisturbanceModel input, Guid surveyIdentifier)
        {
            return new Disturbance
            {
                DisturbanceTypeId = input.DisturbanceTypeId,
                DurationMinutes = input.DurationMinutes,
                Quantity = input.Quantity,
                Result = input.Behavior,
                Id = input.DisturbanceId,
                SurveyIdentifier = surveyIdentifier
            };
        }

        protected DateTime? ParseDateTime(string date, string time)
        {
            date = date ?? string.Empty;
            time = time ?? string.Empty;

            var dateOnly = date.Contains("T") ? date.Split('T')[0] : date;
            var timeOnly = time.Contains("T") ? time.Split('T')[1] : time;

            string combined;
            if (timeOnly.Contains("M"))
            {
                combined = dateOnly + " " + timeOnly;
            }
            else
            {
                combined = dateOnly + "T" + timeOnly;
            }

            combined = Regex.Replace(combined,"([0-9]{1,2})/([0-9]{1,2})/([0-9]{4})", "$3-$1-$2");


            DateTime dateTime;
            if (DateTime.TryParse(combined, out dateTime))
            {
                return dateTime;
            }

            return null;
        }

        protected int RetrieveCurrentUserId()
        {
            return User.Identity.GetUserId<int>();
        }
    }
}
