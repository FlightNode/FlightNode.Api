using FlightNode.Common.BaseClasses;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightNode.DataCollection.Domain.Entities
{
    public class Disturbance : IEntity
    {
        public int Id { get; set; }

        public int DisturbanceTypeId { get; set; }

        [ForeignKey("DisturbanceTypeId")]
        public DisturbanceType DisturbanceType { get; set; }
        
        public int Quantity { get; set; }

        public int DurationMinutes { get; set; }

        [MaxLength(150)]
        [StringLength(150)]
        public string Result { get; set; }

        public Guid SurveyIdentifier { get; set; }
    }
}