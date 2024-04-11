using Microsoft.AspNetCore.SignalR;

namespace Tournaments.Application.Hubs
{
	public sealed class NotificationHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			await Clients.Caller.SendAsync("OnConnected", $"{Context.ConnectionId} has connected successfully");
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			await base.OnDisconnectedAsync(exception);
		}

		public async Task Subscribe(string userId)
		{
			var connectionId = Context.ConnectionId;

			await Groups.AddToGroupAsync(connectionId, $"user-{userId}");
			await Clients.Caller.SendAsync("OnSubscribed", $"{connectionId} subscribed");
		}

		public async Task Unsubscribe(string userId)
		{
			var connectionId = Context.ConnectionId;

			await Groups.RemoveFromGroupAsync(connectionId, $"user-{userId}");
			await Clients.Caller.SendAsync("OnUnsubscribed", $"{connectionId} unsubscribed");
		}
	}
}
