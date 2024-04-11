using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace Tournaments.Application.Hubs
{
	public sealed class BracketHub : Hub
	{
		private static readonly ConcurrentDictionary<string, string> connectionToBracketMap =
			new ConcurrentDictionary<string, string>();

		public override async Task OnConnectedAsync()
		{
			await Clients.Caller.SendAsync("OnConnected", $"{Context.ConnectionId} has connected successfully");
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			var connectionId = Context.ConnectionId;
			if (connectionToBracketMap.TryGetValue(connectionId, out var bracketId))
				await Unsubscribe(bracketId);
		}

		public async Task Subscribe(string bracketId)
		{
			var connectionId = Context.ConnectionId;
			connectionToBracketMap.AddOrUpdate(connectionId, bracketId, (_, _) => bracketId);

			await Groups.AddToGroupAsync(connectionId, $"bracket-{bracketId}");
			await Clients.Caller.SendAsync("OnSubscribed", $"{connectionId} subscribed");
		}

		public async Task Unsubscribe(string bracketId)
		{
			var connectionId = Context.ConnectionId;

			connectionToBracketMap.TryRemove(connectionId, out _);
			await Groups.RemoveFromGroupAsync(connectionId, $"bracket-{bracketId}");
			await Clients.Caller.SendAsync("OnUnsubscribed", $"{connectionId} unsubscribed");
		}
	}
}
