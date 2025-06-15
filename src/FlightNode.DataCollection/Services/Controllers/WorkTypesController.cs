using FlightNode.Common.Api.Models;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Services.Models.WorkLog;
using FligthNode.Common.Api.Controllers;
using Flurl;
using System;
using System.Linq;
using System.Web.Http;

namespace FlightNode.DataCollection.Domain.Services.Controllers
{
    public class WorkTypesController : LoggingController
    {

        private readonly IWorkTypeDomainManager _domainManager;

        /// <summary>
        /// Creates a new instance of <see cref="LocationsController"/>.
        /// </summary>
        /// <param name="domainManager">An instance of <see cref="IWorkTypeDomainManager"/></param>
        public WorkTypesController(IWorkTypeDomainManager domainManager)
        {
            if (domainManager == null)
            {
                throw new ArgumentNullException(nameof(domainManager));
            }

            _domainManager = domainManager;
        }

        /// <summary>
        /// Retrieves all Work Type representations.
        /// </summary>
        /// <returns>Action result containing an enumeration of work types</returns>
        /// <example>
        /// GET: /api/v1/worktypes
        /// </example>
        [Authorize]
        public IHttpActionResult Get()
        {
                var locations = _domainManager.FindAll();

                var models = locations.Select(x => new WorkTypeModel
                {
                    Description = x.Description,
                    Id = x.Id
                });

                return Ok(models);
        }

        /// <summary>
        /// Retrieves a specific work type representation.
        /// </summary>
        /// <param name="id">Unique identifier for the work type resource</param>
        /// <returns>Action result containing a representation of the requested work types</returns>
        /// <example>
        /// GET: /api/v1/worktypes/123
        /// </example>
        [Authorize]
        public IHttpActionResult Get(int id)
        {
                var x = _domainManager.FindById(id);

                var model = new WorkTypeModel
                {
                    Description = x.Description,
                    Id = x.Id
                };

                return Ok(model);
        }

        /// <summary>
        /// Creates a new work type resource.
        /// </summary>
        /// <param name="input">Complete parameters of the work type resource</param>
        /// <returns>Action result containing the new resource's permanent URL</returns>
        /// <example>
        /// POST: /api/v1/worktypes
        /// {
        ///   "description": "some location"
        /// }
        /// </example>
        [Authorize(Roles = "Administrator, Coordinator")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]WorkTypeModel input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

                var workType = new WorkType
                {
                    Description = input.Description
                };

                workType = _domainManager.Create(workType);

                var locationHeader = this.Request
                    .RequestUri
                    .ToString()
                    .AppendPathSegment(workType.Id.ToString());

                return Created(locationHeader, workType);
        }

        /// <summary>
        /// Updates an existing work type resource.
        /// </summary>
        /// <param name="input">Complete parameters of the work type resource</param>
        /// <returns>Action result with status code 204 "no content"</returns>
        /// <example>
        /// PUT: /api/v1/worktypes/123
        /// {
        ///   "description": "some location"
        ///   "id": 3
        /// }
        /// </example>
        [Authorize(Roles = "Administrator, Coordinator")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]WorkTypeModel input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            return WrapWithTryCatch(() =>
            {
                var location = new WorkType
                {
                    Description = input.Description,
                    Id = input.Id
                };

                _domainManager.Update(location);                

                return NoContent();
            });
        }

        /// <summary>
        /// Retrieves a simplified list representation of all work type resources.
        /// </summary>
        /// <returns>Action result containing an enumeration of <see cref="SimpleListItem"/></returns>
        /// <example>
        /// GET: /api/v1/worktypes/simple
        /// </example>
        [Authorize]
        [Route("api/v1/worktypes/simple")]
        public IHttpActionResult GetSimpleList()
        {
            return WrapWithTryCatch(() =>
            {
                var worktTypes = _domainManager.FindAll();

                var models = worktTypes.Select(x => new SimpleListItem
                {
                    Value = x.Description,
                    Id = x.Id
                });

                return Ok(models);
            });
        }

    }
}
