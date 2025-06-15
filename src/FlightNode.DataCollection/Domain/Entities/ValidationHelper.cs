using FlightNode.Common.BaseClasses;
using FlightNode.Common.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightNode.DataCollection.Domain.Entities
{
    public static class ValidationHelper
    {
        public static void Validate<TEntity>(this TEntity input)
            where TEntity : IEntity
        {
            var context = new ValidationContext(input, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(input, context, results, validateAllProperties: true);
            if (!isValid)
            {
                throw DomainValidationException.Create(results);
            }
        }
}
}
