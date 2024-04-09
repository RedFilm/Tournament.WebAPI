using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Tournaments.Application.Hubs.Interfaces;
using Tournaments.Domain.Entities;

namespace Tournaments.Application.Hubs
{
	public sealed class BracketHub : Hub
	{
		private readonly Dictionary<string, string> connectionToBracketMap = new Dictionary<string, string>();

		public override async Task OnConnectedAsync()
		{
			await Clients.Caller.SendAsync("OnConnected", $"{Context.ConnectionId} has connected successfully");
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			var bracketId = connectionToBracketMap[Context.ConnectionId];
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"bracket-{bracketId}");
		}

		public async Task SubscribeToBracketUpdate(string bracketId)
		{
			var connectionId = Context.ConnectionId;

			if (!connectionToBracketMap.ContainsKey(connectionId))
				connectionToBracketMap.Add(connectionId, bracketId);
			else
				connectionToBracketMap[connectionId] = bracketId;

			await Groups.AddToGroupAsync(connectionId, $"bracket-{bracketId}");
		}

		public async Task UnsubscribeFromBracketUpdate(long bracketId)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"bracket-{bracketId}");
		}
	}
}
