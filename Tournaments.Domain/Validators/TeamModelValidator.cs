using FluentValidation;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Validators
{
	public class TeamModelValidator : AbstractValidator<TeamModel>
	{
        public TeamModelValidator()
        {
			RuleFor(team => team.Id).NotEmpty();
			RuleFor(team => team.OwnerId).NotEmpty();
			RuleFor(team => team.TeamName).NotEmpty().MaximumLength(256);
		}
	}
}
