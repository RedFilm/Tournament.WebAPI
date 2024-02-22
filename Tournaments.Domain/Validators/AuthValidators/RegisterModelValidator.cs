using FluentValidation;
using System.Text.RegularExpressions;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Validators.AuthValidators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(registerModel => registerModel.UserName).NotEmpty().MaximumLength(256);
            RuleFor(registerModel => registerModel.Email).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(registerModel => registerModel.Password).NotEmpty().MinimumLength(3);
            RuleFor(registerModel => registerModel.PhoneNumber).Length(11, 20)
                .Matches(new Regex(@"^(\+7|8|\+374|\+994|\+995|\+375|\+380|\+38|\+996|\+998|\+993){1}\d{10}$"));
        }
    }
}
