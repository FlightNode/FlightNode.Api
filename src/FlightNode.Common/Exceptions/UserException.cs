using System;
using System.Collections.Generic;

namespace FlightNode.Common.Exceptions
{
    /// <summary>
    /// An exception whose message is safe to show to the user. Typically indicates that that user
    /// has tried to enter invalid data into the system.
    /// </summary>
    public class UserException : Exception
    {
       
        public UserException(string message) : base(message)
        {
        }

        public static UserException FromMultipleMessages(IEnumerable<string> errors)
        {
            var message = string.Empty;
            foreach(var e in errors)
            {
                message += e + ", ";
            }

            message = message.Substring(0, message.Length - 2);

            return new UserException(message);
        }
    }

}
