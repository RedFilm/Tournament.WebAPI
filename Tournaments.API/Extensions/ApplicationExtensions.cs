using Tournaments.Application.BracketGeneration;
using Tournaments.Application.Services;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Persistence.Repositories;

namespace Tournaments.API.Extensions
{
    public static class ApplicationExtensions
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddScoped<ITournamentRepository, TournamentRepository>();
			services.AddScoped<ITeamRepository, TeamRepository>();
			services.AddScoped<ITeamUserRepository, TeamUserRepository>();
			services.AddScoped<ITournamentTeamRepository, TournamentTeamRepository>();

			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<ITournamentService, TournamentService>();
			services.AddScoped<ITeamService, TeamService>();

			services.AddScoped<BracketGenerator>();

			return services;
		}
	}
}
