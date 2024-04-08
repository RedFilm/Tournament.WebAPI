using Tournaments.Domain.Models.TeamJoinRequestModels;

namespace Tournaments.Domain.Interfaces.Services
{
	public interface ITeamJoinRequestService
	{
		/// <summary>
		/// Sends join request to recipient.
		/// </summary>
		/// <param name="model">Model that contains sender id, inviter id, team id and message</param>
		/// <returns></returns>
		Task SendJoinRequestAsync(TeamJoinRequestModel model);
	}
}
