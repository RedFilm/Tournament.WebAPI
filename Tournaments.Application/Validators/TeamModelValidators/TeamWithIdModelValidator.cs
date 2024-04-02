using FluentValidation;
using Tournaments.Domain.Models.TeamModels;

namespace Tournaments.Domain.Validators.TeamModelValidators
{
	public class TeamWithIdModelValidator : TeamModelBaseValidator<TeamWithIdModel>
	{
        public TeamWithIdModelValidator()
        {
			RuleFor(team => team.Id).NotEmpty();
		}
    }
}
