using FluentValidation;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Validators
{
	public class TournamentModelValidator : AbstractValidator<TournamentModel>
	{
        public TournamentModelValidator()
        {
			RuleFor(tournament => tournament.OrganizerId).NotEmpty();
			RuleFor(tournament => tournament.PrizePool).NotEmpty().GreaterThanOrEqualTo(0);
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
