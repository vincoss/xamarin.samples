using FluentValidation;
using System;


namespace Xamarin_Validation.Validation
{
    public class GuidValidator : AbstractValidator<ValidationString>
    {
        public GuidValidator(string propertyName)
        {
            RuleFor(x => x.Value).NotEmpty().OverridePropertyName(propertyName).WithMessage("Must not be empty string");
            RuleFor(x => x.Value).Must(CheckType).OverridePropertyName(propertyName).WithMessage("Not valid type");
        }

        public bool CheckType(string value)
        {
            Guid g;
            return Guid.TryParse(value, out g);
        }
    }
}
