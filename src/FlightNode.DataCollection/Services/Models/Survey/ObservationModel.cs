namespace FlightNode.DataCollection.Services.Models.Survey
{
    public class ObservationModel
    {
        public int BirdSpeciesId { get; set; }

        public int Adults { get; set; }

        public int? Juveniles { get; set; }

        public int? PrimaryActivityId { get; set; }

        public int? SecondaryActivityId { get; set; }

        public int? HabitatId { get; set; }

        public int? FeedingId { get; set; }

        public int ObservationId { get; set; }

        public bool ChicksPresent { get; set; }

        public bool NestsPresent { get; set; }

        public bool FledglingsPresent { get; set; }
    }
}
