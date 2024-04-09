using Microsoft.AspNetCore.SignalR;

namespace Tournaments.Application.Hubs
{
	public class NotificationHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			await Clients.Caller.SendAsync("OnConnected", $"{Context.ConnectionId} has connected successfully");
		}
	}
}
