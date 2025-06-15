using System;
using System.Collections.Generic;

namespace FlightNode.DataCollection.Domain.Entities
{
    public interface ISurvey
    {

        Guid SurveyIdentifier { get; set; }

        int Id { get; set; }

        bool Completed { get; }

        int LocationId { get; set; }

        DateTime? StartDate { get; set; }

        DateTime? EndDate { get; set; }

        decimal? PrepTimeHours { get; set; }

        int? AssessmentId { get; set; }

        int? VantagePointId { get; set; }

        int? AccessPointId { get; set; }

        string GeneralComments { get; set; }

        string DisturbanceComments { get; set; }


        int SurveyTypeId { get; set; }


        int SubmittedBy { get; set; }

        int? WeatherId { get; set; }

        decimal? Temperature { get; set; }

        int? WindSpeed { get; set; }

        int? WindDirection { get; set; }

        bool? WindDrivenTide { get; set; }

        DateTime? TimeOfLowTide { get; set; }

        List<Observation> Observations { get; }

        List<Disturbance> Disturbances { get; }

        string Observers { get; set; }
        
        string LocationName { get; set; }

        int? WaterHeightId { get; set; }

        ISurvey Add(Observation item);

        ISurvey Add(Disturbance item);

    }
}
