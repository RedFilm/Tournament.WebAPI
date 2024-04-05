using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournaments.Domain.Entities;

namespace Tournaments.Persistence.EntityTypeConfigurations
{
	public class TournamentTeamConfiguration : IEntityTypeConfiguration<TournamentTeam>
	{
		public void Configure(EntityTypeBuilder<TournamentTeam> builder)
		{
			builder.HasKey(t => new { t.TournamentId, t.TeamId, t.Status });

			builder.HasOne(t => t.Tournament)
				.WithMany(tr => tr.TournamentTeams)
				.HasForeignKey(t => t.TournamentId);
			builder.HasOne(t => t.Team)
				.WithMany(tm => tm.TournamentTeams)
				.HasForeignKey(tm => tm.TeamId);
		}
	}
}
