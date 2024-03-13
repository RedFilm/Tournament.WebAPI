using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournaments.Domain.Entities;

namespace Tournaments.Persistence.EntityTypeConfigurations
{
	public class MatchConfiguration : IEntityTypeConfiguration<Match>
	{
		public void Configure(EntityTypeBuilder<Match> builder)
		{
			builder.HasOne(m => m.Team1)
				.WithMany()
				.HasForeignKey(m => m.Team1Id);

			builder.HasOne(m => m.Team2)
				.WithMany()
				.HasForeignKey(m => m.Team2Id);

			builder.HasOne(m => m.Winner)
				.WithMany()
				.HasForeignKey(m => m.WinnerId);
		}
	}
}
