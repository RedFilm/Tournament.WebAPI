using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournaments.Domain.Models;

namespace Tournaments.Persistence.EntityTypeConfigurations
{
	public class TeamConfiguration : IEntityTypeConfiguration<Team>
	{
		public void Configure(EntityTypeBuilder<Team> builder)
		{
			builder.HasMany(t => t.Players)
				.WithMany(u => u.Teams)
				.UsingEntity(table => table.ToTable("PlayerTeam"));
		}
	}
}
