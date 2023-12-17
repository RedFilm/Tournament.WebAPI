using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Application.Interfaces;

namespace Tournaments.Persistance
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistence(this IServiceCollection services, 
			IConfiguration config)
		{
			var connectionString = config["SqlServer"];

			services.AddDbContext<TournamentsDbContext>(opt =>
			{
				opt.UseSqlServer(connectionString);
			});
			services.AddScoped<ITournamentDbContext, TournamentsDbContext>();

			return services;
		}
	}
}
