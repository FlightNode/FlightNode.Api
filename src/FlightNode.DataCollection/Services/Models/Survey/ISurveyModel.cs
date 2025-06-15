using System;

namespace FlightNode.DataCollection.Services.Models.Survey
{
    public interface ISurveyModel
    {
        bool Updating { get; set; }
        bool FinishedEditing { get; set; }
        Guid? SurveyIdentifier { get; set; }
    }
}
