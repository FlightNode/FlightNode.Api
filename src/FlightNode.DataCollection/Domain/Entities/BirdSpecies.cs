using FlightNode.Common.BaseClasses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FlightNode.DataCollection.Domain.Entities
{
    public class BirdSpecies : IEntity
    {
        public int Id { get; set; }

        [StringLength(4)]
        [Required]
        public string CommonAlphaCode { get; set; }

        [StringLength(50)]
        [Required]
        public string CommonName { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string Order { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string Family { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        public string SubFamily { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string Genus { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string Species { get; set; }

        [JsonIgnore]
        public virtual ICollection<SurveyType> SurveyTypes { get; private set; }

        public ICollection<string> SurveyTypeNames { get; set; }

        public BirdSpecies()
        {
            SurveyTypes = new HashSet<SurveyType>();
        }

        public BirdSpecies WithFlatSurveyTypeNames()
        {
            // TODO: consider transmitting *ALL* survey type names, with
            // status applied. Will help avoid hard-coding in the UI. But time
            // is too tight right now for such nice things.

            SurveyTypeNames = new List<string>(SurveyTypes.Select(x => x.Description));

            return this;
        }

        public override bool Equals(object obj)
        {
            var other = obj as BirdSpecies;
            if (other == null)
            {
                return false;
            }

            return other.CommonName == this.CommonName;
        }

        public override int GetHashCode()
        {
            return (this.CommonName ?? string.Empty).GetHashCode();
        }
    }
}
