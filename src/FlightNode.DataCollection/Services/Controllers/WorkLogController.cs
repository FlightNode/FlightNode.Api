using FlightNode.Common.Utility;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Managers;
using FlightNode.DataCollection.Services.Models.WorkLog;
using FligthNode.Common.Api.Controllers;
using Flurl;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace FlightNode.DataCollection.Domain.Services.Controllers
{
    public class WorkLogsController : LoggingController
    {

        private readonly IWorkLogDomainManager _domainManager;
        private readonly ISanitizer _sanitizer;

        /// <summary>
        /// Creates a new instance of <see cref="LocationsController"/>.
        /// </summary>
        /// <param name="domainManager">An instance of <see cref="IWorkLogDomainManager"/></param>
        public WorkLogsController(IWorkLogDomainManager domainManager, ISanitizer sanitizer)
        {
            if (domainManager == null)
            {
                throw new ArgumentNullException(nameof(domainManager));
            }
            if (sanitizer == null)
            {
                throw new ArgumentNullException(nameof(sanitizer));
            }

            _domainManager = domainManager;
            _sanitizer = sanitizer;
        }

        /// <summary>
        /// Retrieves all Work Type representations.
        /// </summary>
        /// <returns>Action result containing an enumeration of work types</returns>
        /// <example>
        /// GET: /api/v1/worklogs
        /// </example>
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult Get()
        {
            var locations = _domainManager.FindAll();

            var models = locations.Select(Map);

            return Ok(models);
        }

        /// <summary>
        /// Retrieves a specific work type representation.
        /// </summary>
        /// <param name="id">Unique identifier for the work type resource</param>
        /// <returns>Action result containing a representation of the requested work types</returns>
        /// <example>
        /// GET: /api/v1/worklogs/123
        /// </example>
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var record = _domainManager.FindById(id);

            if (record == null)
            {
                return NotFound();
            }

            var model = MapWithVolunteerName(record);

            return Ok(model);
        }


        /// <summary>
        /// Retrieves all of the work log entries only for the logged-in user.
        /// </summary>
        /// <returns>Action result containing information about the user's work log entries</returns>
        /// <example>
        /// GET: /api/v1/worklogs/my
        /// </example>
        [Authorize]
        [Route("api/v1/worklogs/my")]
        [HttpGet]
        public IHttpActionResult GetMyLogs()
        {
            int userId = RetrieveCurrentUserId();

            var data = _domainManager.GetForUser(userId)
                                .ToList();
            return Ok(data);
        }

        private int RetrieveCurrentUserId()
        {
            return User.Identity.GetUserId<int>();
        }

        /// <summary>
        /// Retrieves all work log entries for all users.
        /// </summary>
        /// <returns>Action result containing information about all work log entries</returns>
        /// <example>
        /// GET: /api/v1/worklogs/my
        /// </example>
        [Authorize(Roles = "Administrator")]
        [Route("api/v1/worklogs/export")]
        public IHttpActionResult GetExport()
        {
            var data = _domainManager.GetReport()
                                .ToList();

            return Ok(data);
        }

        /// <summary>
        /// Creates a new work type resource.
        /// </summary>
        /// <param name="input">Complete parameters of the work type resource</param>
        /// <returns>Action result containing the new resource's permanent URL</returns>
        /// <example>
        /// POST: /api/v1/worklogs
        /// {
        ///   "locationdId": 1
        ///   "travelTimeHours": 1.53,
        ///   "userId": 43,
        ///   "workDate": "2015-12-07 13:43", 
        ///   "WorkHours": 4.2,
        ///   "workTypeId": 3,
        ///   "numberOfVolunteers": 1,
        ///   "tasksCompleted": "describes what was actually accomplished."
        /// }
        /// </example>
        /// <remarks>
        /// The work date does not need to be in UTC. It is assumed to be local to the 
        /// location. In any statistical calculations, it is the relative time of day
        /// that matters, not the absolute (UTC) time.
        /// </remarks>
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post([FromBody]WorkLogModel input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            input.Sanitize(_sanitizer);

            var WorkLog = Map(input);

            WorkLog = _domainManager.Create(WorkLog);

            var locationHeader = this.Request
                .RequestUri
                .ToString()
                .AppendPathSegment(WorkLog.Id.ToString());

            return Created(locationHeader, WorkLog);
        }

        /// <summary>
        /// Updates an existing work type resource.
        /// </summary>
        /// <param name="input">Complete parameters of the work type resource</param>
        /// <returns>Action result with status code 204 "no content"</returns>
        /// <example>
        /// PUT: /api/v1/worklogs/123
        /// {
        ///   "locationdId": 1
        ///   "id": 3,
        ///   "travelTimeHours": 1.53,
        ///   "userId": 43,
        ///   "workDate": "2015-12-07 13:43", 
        ///   "WorkHours": 4.2,
        ///   "workTypeId": 3,
        ///   "id": 46646,
        ///   "numberOfVolunteers": 1,
        ///   "tasksCompleted": "describes what was actually accomplished."
        /// }
        /// </example>
        /// <remarks>
        /// The work date does not need to be in UTC. It is assumed to be local to the 
        /// location. In any statistical calculations, it is the relative time of day
        /// that matters, not the absolute (UTC) time.
        /// </remarks>
        [Authorize]
        [HttpPut]
        public IHttpActionResult Put([FromBody]WorkLogModel input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            input.Sanitize(_sanitizer);

            var location = Map(input);

            _domainManager.Update(location);

            return NoContent();
        }

        private static WorkLogModel MapWithVolunteerName(WorkLogWithVolunteerName workLogEntry)
        {
            var model = Map(workLogEntry);
            model.VolunteerName = workLogEntry.VolunteerName;
            return model;
        }

        private static WorkLogModel Map(WorkLog workLogEntry)
        {
            return new WorkLogModel
            {
                LocationId = workLogEntry.LocationId,
                TravelTimeHours = workLogEntry.TravelTimeHours,
                UserId = workLogEntry.UserId,
                WorkDate = workLogEntry.WorkDate,
                WorkHours = workLogEntry.WorkHours,
                WorkTypeId = workLogEntry.WorkTypeId,
                Id = workLogEntry.Id,
                TasksCompleted = workLogEntry.TasksCompleted,
                NumberOfVolunteers = workLogEntry.NumberOfVolunteers
            };
        }
        private static WorkLog Map(WorkLogModel x)
        {
            return new WorkLog
            {
                LocationId = x.LocationId,
                TravelTimeHours = x.TravelTimeHours,
                UserId = x.UserId,
                WorkDate = x.WorkDate,
                WorkHours = x.WorkHours,
                WorkTypeId = x.WorkTypeId,
                Id = x.Id,
                TasksCompleted = x.TasksCompleted,
                NumberOfVolunteers = x.NumberOfVolunteers
            };
        }
    }
}
