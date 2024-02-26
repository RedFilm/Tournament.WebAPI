using FluentValidation;
using Tournaments.Domain.Models.TournamentModels;

namespace Tournaments.Domain.Validators.TournamentModelValidators
{
	public class TournamentModelBaseValidator<T> : AbstractValidator<T> where T : TournamentModel
	{
        public TournamentModelBaseValidator()
        {
			RuleFor(tournament => tournament.OrganizerId).NotEmpty();
			RuleFor(tournament => tournament.MaxParticipantCount).NotEmpty().GreaterThanOrEqualTo(0);
			RuleFor(tournament => tournament.TournamentName).NotEmpty().MaximumLength(30);
			RuleFor(tournament => tournament.GameName).NotEmpty().MaximumLength(30);
			RuleFor(tournament => tournament.TournamentDescription).NotEmpty().MaximumLength(512);

			RuleFor(tournament => tournament.RegistrationStartDate).NotEmpty()
				.LessThan(tournament => tournament.RegistrationEndDate);

			RuleFor(tournament => tournament.RegistrationEndDate).NotEmpty()
				.GreaterThan(tournament => tournament.RegistrationStartDate);

			RuleFor(tournament => tournament.TournamentStartDate).NotEmpty()
				.GreaterThan(tournament => tournament.RegistrationEndDate);

			RuleFor(tournament => tournament.TournamentEndDate).NotEmpty()
				.GreaterThan(tournament => tournament.TournamentStartDate);
		}
    }
}
