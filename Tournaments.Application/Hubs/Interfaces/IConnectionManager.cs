namespace Tournaments.Application.Hubs.Interfaces
{
	public interface IConnectionManager
	{
		IEnumerable<long> OnlineUsers { get; }
		void AddConnection(long userId, string connectionId);
		void RemoveConnection(string connectionId);
		public bool IsUserConnected(long userId);
		HashSet<string> GetConnections(long userId);
	}
}
