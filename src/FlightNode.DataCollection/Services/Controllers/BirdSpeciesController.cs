using FlightNode.Common.Api.Models;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FligthNode.Common.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FlightNode.DataCollection.Domain.Services.Controllers
{
    public class BirdQuery
    {
        public int SurveyTypeId { get; set; }
    }

    [RoutePrefix("api/v1")]
    public class BirdSpeciesController : LoggingController
    {

        private readonly IBirdSpeciesDomainManager _domainManager;

        /// <summary>
        /// Creates a new instance of <see cref="LocationsController"/>.
        /// </summary>
        /// <param name="domainManager">An instance of <see cref="IBirdSpeciesDomainManager"/></param>
        public BirdSpeciesController(IBirdSpeciesDomainManager domainManager)
        {
            if (domainManager == null)
            {
                throw new ArgumentNullException(nameof(domainManager));
            }

            _domainManager = domainManager;
        }

        /// <summary>
        /// Retrieves all bird species representations.
        /// </summary>
        /// <returns>Action result containing an enumeration of work types</returns>
        /// <example>
        /// GET: /api/v1/birdspecies
        /// </example>
        [Authorize]
        public IHttpActionResult Get([FromUri]BirdQuery query)
        {
            IEnumerable<BirdSpecies> birds = null;
            if (query.SurveyTypeId > 0)
            {
                birds = _domainManager.GetBirdSpeciesBySurveyTypeId(query.SurveyTypeId);
            }
            else
            {
                birds = _domainManager.FindAll();
            }

            return base.Ok(birds);
        }

        /// <summary>
        /// Retrieves a specific bird species representation.
        /// </summary>
        /// <param name="id">Unique identifier for the work type resource</param>
        /// <returns>Action result containing a representation of the requested bird species</returns>
        /// <example>
        /// GET: /api/v1/birdspecies/123
        /// </example>
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var x = _domainManager.FindById(id);

            return Ok(x);
        }

        /// <summary>
        /// Creates a new bird species resource.
        /// </summary>
        /// <param name="input">Complete parameters of the bird species resource</param>
        /// <returns>Action result containing the new resource's permanent URL</returns>
        /// <example>
        /// POST: /api/v1/birdspecies
        /// {
        ///   "description": "some location"
        /// }
        /// </example>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]BirdSpecies input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            input = _domainManager.Create(input);

            return Created(input, input.Id.ToString());
        }

        /// <summary>
        /// Updates an existing bird species resource.
        /// </summary>
        /// <param name="input">Complete parameters of the bird species resource</param>
        /// <returns>Action result with status code 204 "no content"</returns>
        /// <example>
        /// PUT: /api/v1/birdspecies/123
        /// {
        ///   "description": "some location"
        ///   "id": 3
        /// }
        /// </example>
        [Authorize(Roles = "Administrator,Coordinator")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]BirdSpecies input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            // TODO: PUT requests should return the modified object

            _domainManager.Update(input);

            return NoContent();
        }

        /// <summary>
        /// Retrieves a simplified list representation of all bird species resources.
        /// </summary>
        /// <returns>Action result containing an enumeration of <see cref="SimpleListItem"/></returns>
        /// <example>
        /// GET: /api/v1/birdspecies/simple
        /// </example>
        [Authorize]
        [Route("birdspecies/simple")]
        public IHttpActionResult GetSimpleList()
        {
            var worktTypes = _domainManager.FindAll();

            var models = worktTypes.Select(x => new SimpleListItem
            {
                Value = x.CommonName + " | <i>" + x.Species + "</i>",
                Id = x.Id
            });

            return Ok(models);
        }

        /// <summary>
        /// Adds the given species as a default in the selected survey type.
        /// </summary>
        /// <param name="speciesId">Bird species Id</param>
        /// <param name="surveyTypeId">Survey type ID</param>
        /// <returns>NoContent (201)</returns>
        /// <example>
        /// POST: /api/v1/birdspecies/1/surveytype/2
        /// </example>
        [Authorize(Roles = "Administrator, Coordinator")]
        [Route("birdspecies/{speciesId:int}/surveytype/{surveyTypeId:int}")]
        [HttpPost]
        public IHttpActionResult PostSurveyType(int speciesId, int surveyTypeId)
        {
            _domainManager.AddSpeciesToSurveyType(speciesId, surveyTypeId);

            return NoContent();
        }


        /// <summary>
        /// Removes the given species as a default in the selected survey type.
        /// </summary>
        /// <param name="speciesId">Bird species Id</param>
        /// <param name="surveyTypeId">Survey type ID</param>
        /// <returns>OK (201)</returns>
        /// <example>
        /// POST: /api/v1/birdspecies/1/surveytype/2
        /// </example>
        [Authorize(Roles = "Administrator, Coordinator")]
        [Route("birdspecies/{speciesId:int}/surveytype/{surveyTypeId:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteSurveyType(int speciesId, int surveyTypeId)
        {
            _domainManager.RemoveSpeciesFromSurveyType(speciesId, surveyTypeId);

            return NoContent();
        }
    }
}
