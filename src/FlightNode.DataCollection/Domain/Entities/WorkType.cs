using System.Collections.Generic;

namespace FlightNode.DataCollection.Domain.Entities
{
    public class WorkType :EnumBase
    {
        public virtual List<WorkLog> WorkLogs { get; set; }
        
    }
}
