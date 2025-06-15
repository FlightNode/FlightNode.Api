using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.ComponentModel.DataAnnotations;
using FlightNode.Common.Exceptions;

namespace FlightNode.Common.UnitTests.Exceptions
{
    public class DomainValidationExceptionTests
    {
        public class Constructor
        {
            [Fact]
            public void ConfirmHappyPath()
            {
                var list = new List<ValidationResult>();

                var system = DomainValidationException.Create(list);

                Assert.Same(list, system.ValidationResults);
                Assert.Equal(DomainValidationException.MESSAGE, system.Message);
            }

            [Fact]
            public void ConfirmDoesNotAcceptNullArgument()
            {
                Assert.Throws<ArgumentNullException>(() => DomainValidationException.Create(null));
            }
        }
    }
}
