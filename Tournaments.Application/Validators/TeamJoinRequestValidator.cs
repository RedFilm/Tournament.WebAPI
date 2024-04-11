using FluentValidation;
using Tournaments.Domain.Models.TeamJoinRequestModels;

namespace Tournaments.Application.Validators
{
	public class TeamJoinRequestValidator : AbstractValidator<TeamJoinRequestModel>
	{
		public TeamJoinRequestValidator()
		{
			RuleFor(t => t.SenderId).NotEmpty();
			RuleFor(t => t.ReceiverId).NotEmpty();
			RuleFor(t => t.TeamId).NotEmpty();
			RuleFor(t => t.Message).MaximumLength(128);
		}
	}
}
