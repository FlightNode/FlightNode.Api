using System;

namespace FlightNode.Common.Exceptions
{
    public class DoesNotExistException : Exception
    {
        public DoesNotExistException(string message) : base(message)
        {

        }
    }
}
