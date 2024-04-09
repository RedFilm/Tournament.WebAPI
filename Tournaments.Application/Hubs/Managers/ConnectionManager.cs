using System.Collections.Concurrent;
using Tournaments.Application.Hubs.Interfaces;

namespace Tournaments.Application.Hubs.Managers
{
	public class ConnectionManager : IConnectionManager
	{
		private static ConcurrentDictionary<string, HashSet<string>> userConnectionsMap = new ConcurrentDictionary<string, HashSet<string>>();

		public IEnumerable<string> OnlineUsers { get; } = userConnectionsMap.Keys;

		public void AddConnection(string username, string connectionId)
		{
			if (!userConnectionsMap.ContainsKey(username))
				userConnectionsMap[username] = new HashSet<string>();

			userConnectionsMap[username].Add(connectionId);
		}

		public void RemoveConnection(string connectionId)
		{
			foreach (var username in userConnectionsMap.Keys)
			{
				if (userConnectionsMap[username].Contains(connectionId))
					userConnectionsMap[username].Remove(connectionId);
			}
		}

		public HashSet<string> GetConnections(string username)
		{
			var connections = userConnectionsMap[username] ??= new HashSet<string>();
			return connections;
		}
	}
}
