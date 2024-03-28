using FluentValidation;
using Tournaments.Domain.Models.TournamentModels;

namespace Tournaments.Domain.Validators.TournamentModelValidators
{
	public class TournamentModelWithIdValidator : TournamentModelBaseValidator<TournamentWithIdModel>
	{
        public TournamentModelWithIdValidator()
        {
			RuleFor(tournament => tournament.Id).NotEmpty();
		}
    }
}
