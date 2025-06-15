namespace FlightNode.DataCollection.Services.Models
{
    /// <summary>
    /// Fully models a Location object.
    /// </summary>
    public class LocationModel
    {

        /// <summary>
        /// The unique identifier for the location.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Full latitude value.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Full longitude value.
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Short code for the location.
        /// </summary>
        public string SiteCode { get; set; }

        /// <summary>
        /// Name of the location.
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Location's nearest city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Location's county
        /// </summary>
        public string County { get; set; }
    }
}
