using FlightNode.Common.Utility;
using System;

namespace FlightNode.DataCollection.Services.Models.WorkLog
{
    public class WorkLogModel
    {
        public DateTime WorkDate { get; set; }

        public int Id { get; set; }

        public int WorkTypeId { get; set; }

        public decimal WorkHours { get; set; }

        public decimal TravelTimeHours { get; set; }

        public int UserId { get; set; }
        
        public int LocationId { get; set; }

        public int NumberOfVolunteers { get; set; }

        public string TasksCompleted { get; set; }

        public string VolunteerName { get; set; }


        public void Sanitize(ISanitizer sanitizer)
        {
            TasksCompleted = sanitizer.RemoveAllHtml(TasksCompleted);
        }
    }
}
