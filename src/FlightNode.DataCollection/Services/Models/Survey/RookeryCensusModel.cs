using System;
using System.Collections.Generic;

namespace FlightNode.DataCollection.Services.Models.Survey
{
    public class RookeryCensusModel : ISurveyModel
    {
        public Guid? SurveyIdentifier { get; set; }

        public bool Updating { get; set; }

        public bool FinishedEditing { get; set; }

        public int LocationId { get; set; }

        public string StartDate { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
        public decimal? PrepTimeHours { get; set; }
        public int? SiteTypeId { get; set; }
        
        public string Observers { get; set; }

        public int? VantagePointId { get; set; }

        public int? AccessPointId { get; set; }

        public string SurveyComments { get; set; }

        public string DisturbanceComments { get; set; }

        public List<DisturbanceModel> Disturbances { get; set; }

        public List<ObservationModel> Observations { get; set; }

        public int SurveyId { get; set; }

        public int SubmittedBy { get; set; }


        public RookeryCensusModel()
        {
            SurveyIdentifier = Guid.Empty;
            Observations = new List<ObservationModel>();
            Disturbances = new List<DisturbanceModel>();
        }

        public RookeryCensusModel Add(ObservationModel observation)
        {
            Observations.Add(observation);
            return this;
        }

        public RookeryCensusModel Add(DisturbanceModel disturbanceModel)
        {
            Disturbances.Add(disturbanceModel);
            return this;
        }
    }
}
