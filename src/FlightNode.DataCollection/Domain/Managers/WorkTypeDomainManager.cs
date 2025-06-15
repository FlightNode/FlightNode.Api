using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;

namespace FlightNode.DataCollection.Domain.Managers
{
    public interface IWorkTypeDomainManager : ICrudManager<WorkType>
    {
    }

    public class WorkTypeDomainManager : DomainManagerBase<WorkType>, IWorkTypeDomainManager
    {
        /// <summary>
        /// Returns the persistence layer as the specific type instead of generic type
        /// </summary>
        /// <remarks>
        /// This property is not in use at this time, and has been created just to illuatrate
        /// how to access the specific persistence layer when overriding the base class
        /// methods or adding methods not in the base class.
        /// </remarks>
        private IWorkTypePersistence LocationPersistence
        {
            get
            {
                return Persistence as IWorkTypePersistence;
            }
        }

        public WorkTypeDomainManager(IWorkTypePersistence persistence) : base(persistence)
        {
        }
    }
}
