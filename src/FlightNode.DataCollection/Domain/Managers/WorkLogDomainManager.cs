using FlightNode.Common.Exceptions;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightNode.DataCollection.Domain.Managers
{
    public interface IWorkLogDomainManager : ICrudManager<WorkLog>
    {
        IEnumerable<WorkLogReportRecord> GetReport();
        IEnumerable<WorkLogReportRecord> GetForUser(int userId);
        new WorkLogWithVolunteerName FindById(int id);
    }

    public class WorkLogDomainManager : DomainManagerBase<WorkLog>, IWorkLogDomainManager
    {
        /// <summary>
        /// Returns the persistence layer as the specific type instead of generic type
        /// </summary>
        /// <remarks>
        /// This property is not in use at this time, and has been created just to illuatrate
        /// how to access the specific persistence layer when overriding the base class
        /// methods or adding methods not in the base class.
        /// </remarks>
        private IWorkLogPersistence WorkLogPersistence
        {
            get
            {
                return Persistence as IWorkLogPersistence;
            }
        }

        public WorkLogDomainManager(IWorkLogPersistence persistence) : base(persistence)
        {
        }

        public IEnumerable<WorkLogReportRecord> GetReport()
        {
            return WorkLogPersistence.GetWorkLogReportRecords();
        }

        public override int Update(WorkLog input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            input.Validate();

            return base.Update(input);
        }

        public new WorkLogWithVolunteerName FindById(int id)
        {
            var record = WorkLogPersistence.Collection
                .Join(WorkLogPersistence.Users,
                    workLog => workLog.UserId,
                    user => user.Id,
                    (workLog, user) => new { workLog, user}
                )
                .Select(x => new WorkLogWithVolunteerName
                {
                    Id = x.workLog.Id,
                    LocationId = x.workLog.LocationId,
                    NumberOfVolunteers = x.workLog.NumberOfVolunteers,
                    TasksCompleted = x.workLog.TasksCompleted,
                    TravelTimeHours = x.workLog.TravelTimeHours,
                    UserId = x.workLog.UserId,
                    VolunteerName = x.user.GivenName + " " + x.user.FamilyName,
                    WorkDate = x.workLog.WorkDate,
                    WorkHours = x.workLog.WorkHours,
                    WorkTypeId = x.workLog.WorkTypeId
                })
                .FirstOrDefault(wl => wl.Id == id);

            if (record == null)
            {
                throw new DoesNotExistException("Activity Log ID " + id);
            }

            return record;
        }

        private static WorkLog MapInputToExisting(WorkLog input, WorkLog existing)
        {
            existing.LocationId = input.LocationId;
            existing.TravelTimeHours = input.TravelTimeHours;
            existing.WorkDate = input.WorkDate;
            existing.WorkHours = input.WorkHours;
            existing.WorkTypeId = input.WorkTypeId;
            existing.NumberOfVolunteers = input.NumberOfVolunteers;
            existing.TasksCompleted = input.TasksCompleted;

            return existing;
        }

        private static void DoNotAllowUserToBeChanged(WorkLog input, WorkLog existing)
        {
            if (existing.UserId != input.UserId)
            {
                throw ServerException.UpdateFailed<WorkLog>("Changing the person on a work log entry is forbidden.", input.Id);
            }
        }

        public IEnumerable<WorkLogReportRecord> GetForUser(int userId)
        {
            return WorkLogPersistence.GetWorkLogReportRecords(userId);
        }

    }
}
