﻿using FlightNode.Common.Exceptions;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using System.Collections.Generic;
using System;

namespace FlightNode.DataCollection.Domain.Managers
{
    public interface IWorkLogDomainManager : ICrudManager<WorkLog>
    {
        IEnumerable<WorkLogReportRecord> GetReport();
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
                return _persistence as IWorkLogPersistence;
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

            // Do not allow the user to be changed
            var existing = this.FindById(input.Id);
            if (existing.UserId != input.UserId)
            {
                throw ServerException.UpdateFailed<WorkLog>("Changing the person on a work log entry is forbidden.", input.Id);
            }

            return base.Update(input);
        }
    }
}
