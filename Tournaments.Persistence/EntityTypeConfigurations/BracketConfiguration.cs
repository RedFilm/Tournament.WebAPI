using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournaments.Domain.Entities;

namespace Tournaments.Persistence.EntityTypeConfigurations
{
	public class BracketConfiguration : IEntityTypeConfiguration<Bracket>
	{
		public void Configure(EntityTypeBuilder<Bracket> builder)
		{
			builder.HasOne(b => b.Tournament)
				.WithOne(t => t.Bracket)
				.HasForeignKey<Bracket>(b => b.TournamentId);
		}
	}
}
