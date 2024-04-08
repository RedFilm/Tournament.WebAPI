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
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Subscribers");
		}

		public async Task SubscribeToBracketUpdate(long bracketId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, bracketId.ToString());
		}
	}
}
