using FluentValidation;
using System;


namespace Xamarin_Validation.Validation
{
    public interface IValidatorRegistry
    {
        IValidator GetValidator<T>();
    }
}
