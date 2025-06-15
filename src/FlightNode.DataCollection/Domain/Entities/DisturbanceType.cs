using System.Collections.Generic;

namespace FlightNode.DataCollection.Domain.Entities
{
    public class DisturbanceType : EnumBase
    {

        public virtual ICollection<Disturbance> Disturbances { get; set; }
    }
}