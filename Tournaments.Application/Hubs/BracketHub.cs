using Microsoft.AspNetCore.SignalR;
using Tournaments.Application.Hubs.Interfaces;
using Tournaments.Domain.Exceptions;

namespace Tournaments.Application.Hubs
{
	public sealed class BracketHub : Hub
	{
		private static readonly Dictionary<string, string> connectionToBracketMap = new Dictionary<string, string>();
		private readonly IConnectionManager _connectionManager;

		public BracketHub(IConnectionManager connectionManager)
		{
			_connectionManager = connectionManager;
		}

		public override async Task OnConnectedAsync()
		{
			var claimId = Context.UserIdentifier;

			if (claimId is null)
				throw new BadRequestException("User identifier claim is missing");
			if (!long.TryParse(claimId, out long userId))
				throw new BadRequestException("Invalid format of user identifier claim.");

			_connectionManager.AddConnection(userId, Context.ConnectionId);
			await Clients.Caller.SendAsync("OnConnected", $"{Context.ConnectionId} has connected successfully");
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			var connectionId = Context.ConnectionId;
			var bracketId = connectionToBracketMap[connectionId];

			await UnsubscribeFromBracketUpdate(bracketId);
			_connectionManager.RemoveConnection(connectionId);
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

		public async Task UnsubscribeFromBracketUpdate(string bracketId)
		{
			connectionToBracketMap.Remove(Context.ConnectionId);
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"bracket-{bracketId}");
		}
	}
}
