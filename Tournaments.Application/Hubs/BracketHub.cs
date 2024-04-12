using Microsoft.AspNetCore.SignalR;

namespace Tournaments.Application.Hubs
{
	public sealed class BracketHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			await Clients.Caller.SendAsync("OnConnected", $"{Context.ConnectionId} has connected successfully");
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			await base.OnDisconnectedAsync(exception);
		}

		public async Task Subscribe(string bracketId)
		{
			var connectionId = Context.ConnectionId;

			await Groups.AddToGroupAsync(connectionId, $"bracket-{bracketId}");
			await Clients.Caller.SendAsync("OnSubscribed", $"{connectionId} subscribed");
		}

		public async Task Unsubscribe(string bracketId)
		{
			var connectionId = Context.ConnectionId;

			await Groups.RemoveFromGroupAsync(connectionId, $"bracket-{bracketId}");
			await Clients.Caller.SendAsync("OnUnsubscribed", $"{connectionId} unsubscribed");
		}
	}
}
