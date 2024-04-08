using Tournaments.Domain.Interfaces.Services;

namespace Tournaments.Application.Services
{
	public class PushNotificationService : INotificationService
	{
		public PushNotificationService()
		{

		}
		public Task SendNotificationAsync(long toUserId, string message)
		{
			throw new NotImplementedException();
		}
	}
}
