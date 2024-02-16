using FluentValidation;
using Tournaments.Domain.Models.TeamModels;

namespace Tournaments.Domain.Validators.TeamModelValidators
{
	public class TeamModelBaseValidator<T> : AbstractValidator<T> where T : TeamModel
	{
        public TeamModelBaseValidator()
        {
			RuleFor(team => team.OwnerId).NotEmpty();
			RuleFor(team => team.TeamName).NotEmpty().MaximumLength(256);
		}
    }
}
