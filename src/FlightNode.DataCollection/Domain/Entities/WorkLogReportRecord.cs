
namespace FlightNode.DataCollection.Domain.Entities
{

    public class WorkLogReportRecord
    {
        public int Id { get; set; }

        public string WorkDate { get; set; }

        public string Activity { get; set; }

        public string County { get; set; }

        public string SiteName { get; set; }

        public int NumberOfVolunteers { get; set; }

        public decimal WorkHours { get; set; }

        public decimal TravelTimeHours { get; set; }

        public string Volunteer { get; set; }

        public string TasksCompleted { get; set; }

        public int UserId { get; set; }
    }
}
