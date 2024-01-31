using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Tournaments.Domain.Entities;

namespace Tournaments.Persistence.Extensions
{
	public static class IdentityExtensions
	{
		public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
		{
			services.AddIdentity<AppUser, IdentityRole>(options =>
			{
				options.Password.RequiredLength = 3;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
			})
				.AddEntityFrameworkStores<TournamentDbContext>();

			return services;
		}
	}
}
