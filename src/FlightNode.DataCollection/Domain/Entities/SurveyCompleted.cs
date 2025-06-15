using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightNode.DataCollection.Domain.Entities
{
    public class SurveyCompleted : SurveyBase, ISurvey
    {
        public bool Completed { get { return true; } }

        public SurveyCompleted Add(Observation item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Observations.Add(item);
            return this;
        }
        
        public SurveyCompleted Add(Disturbance item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Disturbances.Add(item);
            return this;
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
