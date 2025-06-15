using System;

namespace FlightNode.Common.Exceptions
{
    public class DatabaseExceptionContent
    {
        public int? Id { get; set; }
        public string Action { get; set; }
        public Type ModelType { get; set; }
        public string Description { get; set; }
    }
}
