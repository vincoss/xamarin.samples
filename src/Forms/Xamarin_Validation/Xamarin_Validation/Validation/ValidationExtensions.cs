using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_Validation.Validators;

namespace Xamarin_Validation.Validation
{
    public static class ValidationExtensions
    {
        public static void ValidateToModel(this IValidator validator, object value, ModelStateDictionary model)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var result = validator.Validate(value);

            foreach(var error in result.Errors)
            {
                model.AddError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}