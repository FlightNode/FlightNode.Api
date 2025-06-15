using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightNode.Common.Exceptions
{
    public class DomainValidationException : Exception
    {
        public const string MESSAGE = "A validation error has occurred.";
 
        public IEnumerable<ValidationResult> ValidationResults
        {
            get; private set;
        }

        private DomainValidationException() : base(MESSAGE)
        {
        }


        public static DomainValidationException Create(IEnumerable<ValidationResult> results)
        {
            if (results == null)
            {
                throw new ArgumentNullException("reults");
            }

            var e = new DomainValidationException();
            e.ValidationResults = results;

            return e;
        }
    }
}
