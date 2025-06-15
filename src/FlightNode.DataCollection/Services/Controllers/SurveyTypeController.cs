using FlightNode.Common.Api.Models;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FligthNode.Common.Api.Controllers;
using Flurl;
using System;
using System.Linq;
using System.Web.Http;

namespace FlightNode.DataCollection.Domain.Services.Controllers
{
    public class SurveyTypesController : LoggingController
    {

        private readonly ISurveyTypeDomainManager _domainManager;

        /// <summary>
        /// Creates a new instance of <see cref="LocationsController"/>.
        /// </summary>
        /// <param name="domainManager">An instance of <see cref="ISurveyTypeDomainManager"/></param>
        public SurveyTypesController(ISurveyTypeDomainManager domainManager)
        {
            if (domainManager == null)
            {
                throw new ArgumentNullException(nameof(domainManager));
            }

            _domainManager = domainManager;
        }

        /// <summary>
        /// Retrieves all survey type representations.
        /// </summary>
        /// <returns>Action result containing an enumeration of survey types</returns>
        /// <example>
        /// GET: /api/v1/surveytypes
        /// </example>
        [Authorize]
        public IHttpActionResult Get()
        {
            return WrapWithTryCatch(() =>
            {
                var items = _domainManager.FindAll();

                var models = items.Select(x => new SimpleListItem
                {
                    Value = x.Description,
                    Id = x.Id
                });

                return Ok(models);
            });
        }

        /// <summary>
        /// Retrieves a specific survey type representation.
        /// </summary>
        /// <param name="id">Unique identifier for the survey type resource</param>
        /// <returns>Action result containing a representation of the requested survey types</returns>
        /// <example>
        /// GET: /api/v1/surveytypes/123
        /// </example>
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            return WrapWithTryCatch(() =>
            {
                var x = _domainManager.FindById(id);

                var model = new SimpleListItem
                {
                    Value = x.Description,
                    Id = x.Id
                };

                return Ok(model);
            });
        }

        /// <summary>
        /// Creates a new survey type resource.
        /// </summary>
        /// <param name="input">Complete parameters of the survey type resource</param>
        /// <returns>Action result containing the new resource's permanent URL</returns>
        /// <example>
        /// POST: /api/v1/surveytypes
        /// {
        ///   "value": "some survey type"
        /// }
        /// </example>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]SimpleListItem input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            return WrapWithTryCatch(() =>
            {
                var surveyType = new SurveyType
                {
                    Description = input.Value
                };

                surveyType = _domainManager.Create(surveyType);

                var locationHeader = this.Request
                    .RequestUri
                    .ToString()
                    .AppendPathSegment(surveyType.Id.ToString());

                return Created(locationHeader, surveyType);
            });
        }

        /// <summary>
        /// Updates an existing survey type resource.
        /// </summary>
        /// <param name="input">Complete parameters of the survey type resource</param>
        /// <returns>Action result with status code 204 "no content"</returns>
        /// <example>
        /// PUT: /api/v1/surveytypes/123
        /// {
        ///   "value": "some survey type"
        ///   "id": 3
        /// }
        /// </example>
        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]SimpleListItem input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            return WrapWithTryCatch(() =>
            {
                var surveyType = new SurveyType
                {
                    Description = input.Value,
                    Id = input.Id
                };

                _domainManager.Update(surveyType);                

                return NoContent();
            });
        }

    }
}
