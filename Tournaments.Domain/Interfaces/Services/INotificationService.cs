namespace Tournaments.Domain.Interfaces.Services
{
	public interface INotificationService
	{
		/// <summary>
		/// Send notification to specified user.
		/// </summary>
		/// <param name="toUserId">Receiver id</param>
		/// <param name="message"></param>
		/// <returns></returns>
		Task SendNotificationAsync(long toUserId, string message);
	}
}
