using FluentValidation;
using System.Text.RegularExpressions;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Validators
{
	public class RegisterModelValidator : AbstractValidator<RegisterModel>
	{
        public RegisterModelValidator()
        {
			RuleFor(registerModel => registerModel.UserName).NotEmpty().MaximumLength(256);
			RuleFor(registerModel => registerModel.Email).NotEmpty().EmailAddress().MaximumLength(256);
			RuleFor(registerModel => registerModel.Password).NotEmpty().MinimumLength(3);
			RuleFor(registerModel => registerModel.PhoneNumber).Length(11)
				.Matches(new Regex(@"(\+7|7|8)?[\s\-]?\(?[489][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}"));
		}
    }
}
