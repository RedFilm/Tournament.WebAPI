using Tournaments.Domain.Models.TournamentModels;
using Tournaments.Domain.Validators.TournamentModelValidators;

namespace Tournaments.Domain.Validators.TournamentValidators
{
	public class TournamentModelValidator : TournamentModelBaseValidator<TournamentModel>
    {
        public TournamentModelValidator() : base() { }
    }
}
