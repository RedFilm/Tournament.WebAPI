namespace Tournaments.Application.Hubs.Interfaces
{
	public interface IConnectionManager
	{
		IEnumerable<string> OnlineUsers { get; }
		void AddConnection(string username, string connectionId);
		void RemoveConnection(string connectionId);
		HashSet<string> GetConnections(string username);
	}
}
