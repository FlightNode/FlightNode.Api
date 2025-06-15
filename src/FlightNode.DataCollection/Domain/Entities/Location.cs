using FlightNode.Common.BaseClasses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightNode.DataCollection.Domain.Entities
{
    public class Location : IEntity
    {
        [Required]
        [MaxLength(100)] // required for Entity Framework
        [StringLength(100)] // required for validator
        public string SiteName { get; set; }

        [Required]
        [Range(-90.0d, 90.0d)]
        public decimal Latitude { get; set; }

        [Required]
        [Range(-180.0d, 180.0d)]
        public decimal Longitude { get; set; }

        public virtual List<WorkLog> WorkLogs { get; set; }

        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [StringLength(10)]
        public string SiteCode { get; set; }


        [Required(AllowEmptyStrings = true)]
        [MaxLength(100)]
        [StringLength(100)]
        public string County { get; set; }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(100)]
        [StringLength(100)]
        public string City { get; set; }
    }
}
