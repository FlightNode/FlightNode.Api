using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightNode.DataCollection.Domain.UnitTests.Services.Models
{
    public static class ModelValidator
    {
        public static bool Validate<TModel>(TModel model)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(model, context, validationResults, true);
        }
    }
}
