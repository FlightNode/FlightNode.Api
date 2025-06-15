using FlightNode.Common.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace FlightNode.DataCollection.Domain.Entities
{
    public class EnumBase : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
