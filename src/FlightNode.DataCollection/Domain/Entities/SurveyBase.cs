using FlightNode.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FlightNode.DataCollection.Domain.Entities
{
    public abstract class SurveyBase : IEntity
    {
        [Required]
        public Guid SurveyIdentifier { get; set; }

        public int Id { get; set; }

        [Required]
        public int LocationId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Range(0.001, 24.0)]
        public decimal? PrepTimeHours { get; set; }

        public int? AssessmentId { get; set; }

        public int? VantagePointId { get; set; }

        public int? AccessPointId { get; set; }

        [StringLength(500)]
        [MaxLength(500)]
        public string GeneralComments { get; set; }

        [StringLength(500)]
        [MaxLength(500)]
        public string DisturbanceComments { get; set; }

        [Required]
        public int SurveyTypeId { get; set; }

        [Required]
        public int SubmittedBy { get; set; }

        public int? WeatherId { get; set; }

        public decimal? Temperature { get; set; }

        public int? WindSpeed { get; set; }

        public int? WindDirection { get; set; }
        
        public bool? WindDrivenTide { get; set; }

        public int? WaterHeightId { get; set; }

        public DateTime? TimeOfLowTide { get; set; }

        [NotMapped] // Prefer to handle this relationship manually
        public List<Observation> Observations { get; private set; }

        [NotMapped] // Prefer to handle this relationship manually
        public List<Disturbance> Disturbances { get; private set; }

        [StringLength(1000)]
        public string Observers { get; set; }


        [NotMapped]
        public string LocationName { get; set; }

        public DateTime CreateDate { get; set; }

        public SurveyBase()
        {
            Observations = new List<Observation>();
            Disturbances = new List<Disturbance>();
            CreateDate = DateTime.Now;
        }
    }
}
