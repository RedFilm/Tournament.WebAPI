using FluentValidation;
using Tournaments.Domain.Models.BracketModels;

namespace Tournaments.Domain.Validators
{
	public class BracketUpdateModelValidator : AbstractValidator<BracketUpdateModel>
	{
		public BracketUpdateModelValidator()
		{
			RuleFor(bracket => bracket.TournamentId).NotEmpty();
			RuleFor(bracket => bracket.Results).NotNull();
			RuleFor(bracket => bracket.Results.Count).LessThanOrEqualTo(16);
		}
	}
}
