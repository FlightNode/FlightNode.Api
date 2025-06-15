using System;

namespace FlightNode.DataCollection.Domain.Entities
{
    public class SurveyPending : SurveyBase, ISurvey
    {
        public bool Completed {  get { return false; } }

        public SurveyPending Add(Observation item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Observations.Add(item);
            return this;
        }

        public SurveyPending Add(Disturbance item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Disturbances.Add(item);
            return this;
        }

        public SurveyCompleted ToSurveyCompleted()
        {
            var completed = new SurveyCompleted
            {
                AccessPointId = this.AccessPointId,
                AssessmentId = this.AssessmentId,
                DisturbanceComments = this.DisturbanceComments,
                EndDate = this.EndDate.Value,
                GeneralComments = this.GeneralComments,
                LocationId = this.LocationId,
                StartDate = this.StartDate.Value,
                Temperature = this.Temperature,
                SubmittedBy = this.SubmittedBy,
                SurveyIdentifier = this.SurveyIdentifier,
                SurveyTypeId = this.SurveyTypeId,
                WindDrivenTide = this.WindDrivenTide,
                TimeOfLowTide = this.TimeOfLowTide,
                VantagePointId = this.VantagePointId,
                WeatherId = this.WeatherId,
                WindSpeed = this.WindSpeed,
                WaterHeightId = this.WaterHeightId,
                WindDirection = this.WindDirection,
                PrepTimeHours = this.PrepTimeHours
            };

            completed.Observations.AddRange(this.Observations);
            completed.Disturbances.AddRange(this.Disturbances);
            completed.Observers = this.Observers;

            return completed;
        }


        ISurvey ISurvey.Add(Observation item)
        {
            return Add(item);
        }

        ISurvey ISurvey.Add(Disturbance item)
        {
            return Add(item);
        }
    }
}
