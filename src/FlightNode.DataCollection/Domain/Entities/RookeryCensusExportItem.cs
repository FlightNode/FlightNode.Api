using System;
using FlightNode.Common.BaseClasses;

namespace FlightNode.DataCollection.Domain.Entities
{
    /// <summary>
    /// Models the results of running the view "WaterbirdForagingExport"
    /// </summary>
    public class RookeryCensusExportItem : IEntity
    {
        /// <summary>
        /// Gets or sets the observation id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Survey Identifier 
        /// </summary>
        public Guid SurveyIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the Site Code
        /// </summary>
        public string SiteCode { get; set; }

        /// <summary>
        /// Gets or sets the Site Name
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the County
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the Longitude
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the Latitude
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the Site Assessment
        /// </summary>
        public string Assessment { get; set; }

        /// <summary>
        /// Gets or sets the Start Date
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets the End Date
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// Gets or sets the Prep Time
        /// </summary>
        public string PrepTimeHours { get; set; }

        /// <summary>
        /// Gets or sets the Vantage Point
        /// </summary>
        public string VantagePoint { get; set; }

        /// <summary>
        /// Gets or sets the Access Point
        /// </summary>
        public string AccessPoint { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who submitted the survey
        /// </summary>
        public string SubmittedBy { get; set; }

        /// <summary>
        /// Gets or sets the list of observers
        /// </summary>
        public string Observers { get; set; }

        /// <summary>
        /// Gets or sets general comments about the survey
        /// </summary>
        public string GeneralComments { get; set; }

        /// <summary>
        /// Gets or sets a bird's Genus.
        /// </summary>
        public string Genus { get; set; }

        /// <summary>
        /// Gets or sets a bird's Species
        /// </summary>
        public string Species { get; set; }

        /// <summary>
        /// Gets or sets a bird's common alpha code
        /// </summary>
        public string CommonAlphaCode { get; set; }

        /// <summary>
        /// Gets or sets a bird's commonly-used name
        /// </summary>
        public string CommonName { get; set; }

        /// <summary>
        /// Gets or sets the number of adults observed
        /// </summary>
        public string NumberOfAdults { get; set; }

        /// <summary>
        /// Gets or sets the number of juveniles observed
        /// </summary>
        public string NestsPresent { get; set; }

        /// <summary>
        /// Gets or sets the primary activity observed
        /// </summary>
        public string ChicksPresent { get; set; }

        /// <summary>
        /// Gets or sets the secondary activity observed
        /// </summary>
        public string FledglingsPresent { get; set; }
        
        /// <summary>
        /// Gets or sets the disturbance comments
        /// </summary>
        public string DisturbanceComments { get; set; }

        /// <summary>
        /// Gets or sets the number of kayakers
        /// </summary>
        public string KayakerQuantity { get; set; }

        /// <summary>
        /// Gets or sets the duration in minutes of kayaker disturbance
        /// </summary>
        public string KayakerDurationMinutes { get; set; }

        /// <summary>
        /// Gets or sets the result of kayaker disturbance
        /// </summary>
        public string KayakResult { get; set; }

        /// <summary>
        /// Gets or sets the number of fishermen wading 
        /// </summary>
        public string FishermenWadingQuantity { get; set; }

        /// <summary>
        /// Gets or sets the duration of fishermen wading
        /// </summary>
        public string FishermenWadingDurationMinutes { get; set; }

        /// <summary>
        /// Gets or sets the result of fishermen wading
        /// </summary>
        public string FishermenWadingResult { get; set; }

        /// <summary>
        /// Gets or sets the quantity of stationary boats
        /// </summary>
        public string StationaryBoatsQuantity { get; set; }

        /// <summary>
        /// Gets or sets the duration in minutes of stationary boat disturbances
        /// </summary>
        public string StationaryBoatsDurationMinutes { get; set; }

        /// <summary>
        /// Gets or sets the result of stationary boat disturbances
        /// </summary>
        public string StationaryBoatsResult { get; set; }

        /// <summary>
        /// Gets or sets the quantity of moving boats
        /// </summary>
        public string MovingBoatsQuantity { get; set; }

        /// <summary>
        /// Gets or sets the duration in minutes of moving boats disturbances
        /// </summary>
        public string MovingBoatsDurationMinutes { get; set; }

        /// <summary>
        /// Gets or sets the result of moving boats disturbances
        /// </summary>
        public string MovingBoatsResult { get; set; }

        /// <summary>
        /// Gets or set the number of personal watercraft
        /// </summary>
        public string PersonalWatercraftQuantity { get; set; }

        /// <summary>
        /// Gets or sets the duration in minutes of personal watercraft disturbances
        /// </summary>
        public string PersonalWatercraftDurationMinutes { get; set; }

        /// <summary>
        /// Gets or sets the number of humans
        /// </summary>
        public string HumansQuantity { get; set; }

        /// <summary>
        /// Gets or sets the duration in minutes of human disturbances
        /// </summary>
        public string HumansMinutes { get; set; }

        /// <summary>
        /// Gets or sets the result of human disturbances
        /// </summary>
        public string HumansResult { get; set; }

        /// <summary>
        /// Gets or sets the quantity of noise disturbances
        /// </summary>
        public string NoiseQuantity { get; set; }

        /// <summary>
        /// Gets or sets the duration in minutes of noise disturbances
        /// </summary>
        public string NoiseMinutes { get; set; }

        /// <summary>
        /// Gets or sets the result of noise disturbances
        /// </summary>
        public string NoiseResult { get; set; }

        /// <summary>
        /// Gets or sets the quantity of other disturbances
        /// </summary>
        public string OtherDisturbanceQuantity { get; set; }

        /// <summary>
        /// Gets or sets the duration in minutes of other disturbances
        /// </summary>
        public string OtherDisturbanceMinutes { get; set; }

        /// <summary>
        /// Gets or sets the result of other disturbances
        /// </summary>
        public string OtherDisturbanceResult { get; set; }
    }
}
