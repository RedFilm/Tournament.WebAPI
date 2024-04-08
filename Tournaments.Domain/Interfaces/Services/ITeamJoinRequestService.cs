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
		Task<TeamJoinRequestModel> GetRequestByIdAsync(long requestId);

		/// <summary>
		/// Asynchronously gets all join requests.
		/// </summary>
		/// <param name="model">Model that contains page number and size</param>
		/// <returns>Page model that contains list of join requests, total count, page number and size</returns>
		Task<PageModel<TeamJoinRequestWithIdModel>> GetAllAsync(PaginationModel model);
	}
}
