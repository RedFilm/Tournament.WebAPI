using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tournaments.Persistence.Extensions
{
	public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<TournamentDbContext>(options => 
            { 
                options.UseNpgsql(connectionString);
                options.EnableSensitiveDataLogging(); 
            });
            return services;
        }
    }
}