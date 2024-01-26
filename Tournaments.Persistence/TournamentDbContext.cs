using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tournaments.Domain.Models;
using Tournaments.Persistence.EntityTypeConfigurations;

namespace Tournaments.Persistence
{
	public class TournamentDbContext : IdentityDbContext<AppUser>
	{
		public TournamentDbContext(DbContextOptions<TournamentDbContext> options)
			: base(options) { }

		public DbSet<Team> Teams { get; set; }
		public DbSet<Tournament> Tournaments { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{ 
			builder.ApplyConfiguration(new AppUserConfiguration());
			builder.ApplyConfiguration(new TeamConfiguration());
			builder.ApplyConfiguration(new TournamentTeamConfiguration());

			base.OnModelCreating(builder);
		}
	}
}
