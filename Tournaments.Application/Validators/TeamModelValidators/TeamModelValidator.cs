using FluentValidation;
using Tournaments.Domain.Models.TeamModels;

namespace Tournaments.Domain.Validators.TeamModelValidators
{
    public class TeamModelValidator : TeamModelBaseValidator<TeamModel>
    {
        public TeamModelValidator() : base() { }
    }
}
