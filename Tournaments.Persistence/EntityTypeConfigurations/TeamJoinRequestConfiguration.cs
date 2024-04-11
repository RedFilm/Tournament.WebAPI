using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournaments.Domain.Entities;

namespace Tournaments.Persistence.EntityTypeConfigurations
{
	public class TeamJoinRequestConfiguration : IEntityTypeConfiguration<TeamJoinRequest>
	{
		public void Configure(EntityTypeBuilder<TeamJoinRequest> builder)
		{
			builder.HasOne(t => t.Sender)
				.WithMany()
				.HasForeignKey(m => m.SenderId);

			builder.HasOne(m => m.Receiver)
				.WithMany()
				.HasForeignKey(m => m.ReceiverId);

			builder.HasOne(m => m.Team)
				.WithMany()
				.HasForeignKey(m => m.TeamId);
		}
	}
}
