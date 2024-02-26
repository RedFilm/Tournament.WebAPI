using FluentValidation;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Validators.AuthValidators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(loginModel => loginModel.UserName).NotEmpty().MaximumLength(256);
            RuleFor(loginModel => loginModel.Password).MinimumLength(3);
        }
    }
}
