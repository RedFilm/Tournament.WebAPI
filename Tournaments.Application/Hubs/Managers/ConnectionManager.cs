using System.Collections.Concurrent;
using Tournaments.Application.Hubs.Interfaces;

namespace Tournaments.Application.Hubs.Managers
{
	public class ConnectionManager : IConnectionManager
	{
		private static ConcurrentDictionary<long, HashSet<string>> userConnectionsMap = new ConcurrentDictionary<long, HashSet<string>>();

		public IEnumerable<long> OnlineUsers { get; } = userConnectionsMap.Keys;

		public void AddConnection(long userId, string connectionId)
		{
			if (!userConnectionsMap.ContainsKey(userId))
				userConnectionsMap[userId] = new HashSet<string>();

			userConnectionsMap[userId].Add(connectionId);
		}

		public void RemoveConnection(string connectionId)
		{
			foreach (var userId in userConnectionsMap.Keys)
			{
				if (userConnectionsMap[userId].Contains(connectionId))
					userConnectionsMap[userId].Remove(connectionId);
			}
		}

		public bool IsUserConnected(long userId)
		{
			return userConnectionsMap.ContainsKey(userId);
		}

		public HashSet<string> GetConnections(long userId)
		{
			var connections = userConnectionsMap[userId] ??= new HashSet<string>();
			return connections;
		}
	}
}
