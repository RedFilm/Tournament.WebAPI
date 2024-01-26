using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Tournaments.Domain.Models;

namespace Tournaments.Persistence.Extensions
{
	public static class IdentityExtensions
	{
		public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
		{
			services.AddIdentity<AppUser, IdentityRole>(options =>
			{
				options.Password.RequireDigit = true;
			})
				.AddEntityFrameworkStores<TournamentDbContext>();

			return services;
		}
	}
}
