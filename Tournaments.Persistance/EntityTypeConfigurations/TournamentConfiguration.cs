using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tournaments.Domain;

namespace Tournaments.Persistance.EntityTypeConfigurations
{
	class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
	{
		public void Configure(EntityTypeBuilder<Tournament> builder)
		{
			builder.HasKey(tournament => tournament.Id);
			builder.HasIndex(tournament => tournament.Id).IsUnique();
			builder.Property(tournament => tournament.Name).HasMaxLength(250);
			builder.Property(tournament => tournament.Description).HasMaxLength(1000);
		}
	}
}
