using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightNode.DataCollection.Services.Models.Survey
{
    public class WaterbirdForagingModel : ISurveyModel
    {

        public Guid? SurveyIdentifier { get; set; }

        public bool Updating { get; set; }

        public bool FinishedEditing { get; set; }

        public int LocationId { get; set; }
        
        [RegularExpression("[0-9]{4}-[0-9]{2}-[0-9]{2}|[0-9]{1,2}/[0-9]{1,2}/[0-9]{4}")]
        public string StartDate { get; set; }

        public string StartTime { get; set; }

        public decimal? PrepTimeHours { get; set; }

        public string EndTime { get; set; }

        public int? SiteTypeId { get; set; }

        public string TimeLowTide { get; set; }

        public bool? WindDrivenTide { get; set; }

        public int? WaterHeightId { get; set; }

        public int? WeatherId { get; set; }

        public decimal? Temperature { get; set; }

        public int? WindSpeed { get; set; }

        public int? WindDirection { get; set; }

        public string Observers { get; set; }

        public int? VantagePointId { get; set; }

        public int? AccessPointId { get; set; }

        public string SurveyComments { get; set; }

        public string DisturbanceComments { get; set; }

        public List<DisturbanceModel> Disturbances { get; set; }

        public List<ObservationModel> Observations { get; set; }

        public int SurveyId { get; set; }
        public int SubmittedBy { get; set; }

        public WaterbirdForagingModel()
        {
            SurveyIdentifier = Guid.Empty;
            Observations = new List<ObservationModel>();
            Disturbances = new List<DisturbanceModel>();
        }

        public WaterbirdForagingModel Add(ObservationModel observation)
        {
            Observations.Add(observation);
            return this;
        }

        public WaterbirdForagingModel Add(DisturbanceModel disturbanceModel)
        {
            Disturbances.Add(disturbanceModel);
            return this;
        }
    }
}
