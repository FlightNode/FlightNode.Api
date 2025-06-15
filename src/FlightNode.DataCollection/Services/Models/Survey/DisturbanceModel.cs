namespace FlightNode.DataCollection.Services.Models.Survey
{
    public class DisturbanceModel
    {
        public int DisturbanceId { get; set; }

        public int Quantity { get; set; }

        public int DurationMinutes { get; set; }

        public string Behavior { get; set; }

        public int DisturbanceTypeId { get; set; }
    }
}
