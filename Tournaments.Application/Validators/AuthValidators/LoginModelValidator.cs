using FluentValidation;
using Tournaments.Domain.Models.AuthModels;

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
