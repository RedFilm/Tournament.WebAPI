using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tournaments.Domain.Entities;
using Tournaments.Persistence.EntityTypeConfigurations;

namespace Tournaments.Persistence
{
	public class TournamentDbContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
	{
		public TournamentDbContext(DbContextOptions<TournamentDbContext> options)
			: base(options) { }

		public DbSet<Team> Teams { get; set; }
		public DbSet<Tournament> Tournaments { get; set; }
		public DbSet<TournamentTeam> TournamentTeams { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);

			base.OnModelCreating(builder);
		}
	}
}
