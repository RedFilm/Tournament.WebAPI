using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Models;
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
		Task CreateJoinRequestAsync(TeamJoinRequestModel model);

		/// <summary>
		/// Asynchronously gets join request by id.
		/// </summary>
		/// <param name="requestId">Join request id</param>
		/// <returns>Join request model which has this id</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<TeamJoinRequestWithIdModel> GetRequestByIdAsync(long requestId);

		/// <summary>
		/// Asynchronously gets all join requests.
		/// </summary>
		/// <param name="model">Model that contains page number and size</param>
		/// <returns>Page model that contains list of join requests, total count, page number and size</returns>
		Task<PageModel<TeamJoinRequestWithIdModel>> GetAllAsync(PaginationModel model);

		/// <summary>
		/// Asynchronously accept team join request.
		/// </summary>
		/// <param name="requestId">Team join request id</param>
		/// <param name="model">Team join request model</param>
		/// <returns></returns>
		Task AcceptRequestAsync(long requestId, TeamJoinRequestModel model);

		/// <summary>
		/// Asynchronously reject team join request.
		/// </summary>
		/// <param name="requestId">Team join request id</param>
		/// <param name="model">Team join request model</param>
		/// <returns></returns>
		Task RejectRequestAsync(long requestId, TeamJoinRequestModel model);
	}
}
