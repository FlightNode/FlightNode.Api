using FlightNode.DataCollection.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightNode.DataCollection.Domain.Interfaces.Persistence
{
    public interface IEnumRepository
    {
        Task<IReadOnlyCollection<Weather>> GetWeather();
        Task<IReadOnlyCollection<WaterHeight>> GetWaterHeights();
        Task<IReadOnlyCollection<DisturbanceType>> GetDisturbanceTypes();
        Task<IReadOnlyCollection<HabitatType>> GetHabitatTypes();
        Task<IReadOnlyCollection<FeedingSuccessRate>> GetFeedingSuccessRates();
        Task<IReadOnlyCollection<SurveyActivity>> GetSurveyActivities();
        Task<IReadOnlyCollection<SiteAssessment>> GetSiteAssessments();
        Task<IReadOnlyCollection<VantagePoint>> GetVantagePoints();
        Task<IReadOnlyCollection<AccessPoint>> GetAccessPoints();
        Task<IReadOnlyCollection<WindSpeed>> GetWindSpeeds();
        Task<IReadOnlyCollection<WindDirection>> GetWindDirections();
    }
}
