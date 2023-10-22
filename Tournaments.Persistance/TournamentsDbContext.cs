using Microsoft.EntityFrameworkCore;
using Tournaments.Application.Interfaces;
using Tournaments.Domain;
using Tournaments.Persistance.EntityTypeConfigurations;

namespace Tournaments.Persistance
{
	public class TournamentsDbContext : DbContext, ITournamentDbContext
	{
		public DbSet<Tournament> Tournaments { get; set; }

		public TournamentsDbContext(DbContextOptions<TournamentsDbContext> options) 
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new TournamentConfiguration());
			base.OnModelCreating(modelBuilder);
		}

	}
}
