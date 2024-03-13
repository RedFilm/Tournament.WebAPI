using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournaments.Domain.Entities;

namespace Tournaments.Persistence.EntityTypeConfigurations
{
	public class StageConfiguration : IEntityTypeConfiguration<Stage>
	{
		public void Configure(EntityTypeBuilder<Stage> builder)
		{
			builder.HasMany(s => s.Matches)
				.WithOne(m => m.Stage);
		}
	}
}
