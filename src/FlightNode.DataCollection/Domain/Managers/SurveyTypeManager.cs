using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;

namespace FlightNode.DataCollection.Domain.Managers
{
    public interface ISurveyTypeDomainManager : ICrudManager<SurveyType>
    {
    }

    public class SurveyTypeDomainManager : DomainManagerBase<SurveyType>, ISurveyTypeDomainManager
    {
        public SurveyTypeDomainManager(ISurveyTypePersistence surveyTypePersistence) : base(surveyTypePersistence)
        {
        }
    }
}
