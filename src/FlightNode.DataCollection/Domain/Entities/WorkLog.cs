using FlightNode.Common.BaseClasses;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightNode.DataCollection.Domain.Entities
{
    public class WorkLog : IEntity
    {
        [Required]
        public DateTime WorkDate { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public int WorkTypeId { get; set; }

        [Required]
        [Range(0.001, 24.0)]
        public decimal WorkHours { get; set; }

        [Required]
        [Range(0.001, 24.0)]
        public decimal TravelTimeHours { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int LocationId { get; set; }

        public int NumberOfVolunteers { get; set; }

        public string TasksCompleted { get; set; }
    
    }

    /// <summary>
    /// Used to map additional information when retrieving a <see cref="WorkLog"/> record, but never with inserts/updates.
    /// </summary>
    [NotMapped]
    public class WorkLogWithVolunteerName : WorkLog
    {
        public string VolunteerName { get; set; }

    }
}


