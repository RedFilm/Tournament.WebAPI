using Microsoft.EntityFrameworkCore;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Enums.Statuses;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Models;

namespace Tournaments.Persistence.Repositories
{
	public class TeamJoinRequestRepository : ITeamJoinRequestRepository
	{
		private readonly TournamentDbContext _context;

		public TeamJoinRequestRepository(TournamentDbContext context)
		{
			_context = context;
		}

		public async Task<bool> AddJoinRequestAsync(TeamJoinRequest joinRequest)
		{
			await _context.TeamJoinRequests.AddAsync(joinRequest);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> UpdateRequestAsync(TeamJoinRequest request)
		{
			_context.TeamJoinRequests.Update(request);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<TeamJoinRequest?> GetRequestByIdAsync(long requestId)
		{
			return await _context.TeamJoinRequests.FirstOrDefaultAsync(t => t.Id == requestId);
		}

		public async Task<List<TeamJoinRequest>> GetAllAsync(PaginationModel model)
		{
			var skipCount = (model.PageNumber - 1) * model.PageSize;

			return await _context.TeamJoinRequests.Skip(skipCount).Take(model.PageSize).ToListAsync();
		}

		public async Task<int> GetTotalCountAsync()
		{
			return await _context.TeamJoinRequests.CountAsync();
		}

		public async Task<bool> AnyAsync(long senderId, long teamId)
		{
			return await _context.TeamJoinRequests.AnyAsync(
				r => r.SenderId == senderId &&
					 r.TeamId == teamId &&
					 r.Status == TeamJoinRequestStatus.Pending);
		}
	}
}
