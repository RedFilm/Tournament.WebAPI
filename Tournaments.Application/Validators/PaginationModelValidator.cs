using FluentValidation;
using Tournaments.Domain.Models;

namespace Tournaments.Application.Validators
{
	public class PaginationModelValidator : AbstractValidator<PaginationModel>
	{
        public PaginationModelValidator()
        {
            RuleFor(model => model.PageNumber).GreaterThan(0);
            RuleFor(model => model.PageSize).GreaterThan(0).LessThan(50);
        }
    }
}
