using FluentValidation;
using System;


namespace Xamarin_Validation.Validation
{
    public class ValidatorRegistry : IValidatorRegistry
    {
        public IValidator GetValidator<T>()
        {
            if (typeof(UserValidator) == typeof(T))
            {
                return new UserValidator();
            }
            throw new NotSupportedException(typeof(T).AssemblyQualifiedName);
        }
    }
}
