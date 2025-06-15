using FlightNode.DataCollection.Domain.Entities;

namespace FlightNode.DataCollection.Domain.Interfaces.Persistence
{
    public interface IBirdSpeciesPersistence : IPersistenceBase<BirdSpecies>
    {
        ICrudSet<SurveyType> SurveyTypes { get;}
    }
}
