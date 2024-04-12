using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournaments.Domain.Entities;

namespace Tournaments.Persistence.EntityTypeConfigurations
{
	public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
	{
		public void Configure(EntityTypeBuilder<UserNotification> builder)
		{
			builder.HasKey(u => new { u.NotificationId, u.UserId });

			builder.HasOne(u => u.Notification)
				.WithMany(n => n.UserNotifications)
				.HasForeignKey(t => t.NotificationId);
			builder.HasOne(t => t.User)
				.WithMany(tm => tm.UserNotifications)
				.HasForeignKey(tm => tm.UserId);
		}
	}
}
