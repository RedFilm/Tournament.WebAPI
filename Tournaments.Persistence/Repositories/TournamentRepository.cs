using Microsoft.EntityFrameworkCore;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Persistence.Repositories
{
	public class TournamentRepository : ITournamentRepository
	{
		private readonly TournamentDbContext _context;

		public TournamentRepository(TournamentDbContext context)
        {
            _context =	context;
        }

        public async Task<bool> AddTournamentAsync(TournamentModel model)
		{
			if (model is null)
				return false;

			var tournament = new Tournament()
			{
				OrganizerId = model.OrganizerId,
				PrizePool = model.PrizePool,
				MaxParticipantCount = model.MaxParticipantCount,
				TournamentName = model.TournamentName,
				GameName = model.GameName,
				TournamentDescription = model.TournamentDescription,
				RegistrationStartDate = model.RegistrationStartDate,
				RegistrationEndDate = model.RegistrationEndDate,
				TournamentStartDate = model.TournamentStartDate,
				TournamentEndDate = model.TournamentEndDate
			};			

			await _context.Tournaments.AddAsync(tournament);
			var result = await _context.SaveChangesAsync();

			return result > 0;
		}

		public async Task<bool> DeleteTournamentAsync(int id)
		{
			var tournament = await _context.Tournaments.FirstOrDefaultAsync(x => x.Id == id);

			if (tournament is null) 
				return false;

			_context.Tournaments.Remove(tournament);
			var result = await _context.SaveChangesAsync();

			return result > 0;
		}

		public async Task<Tournament?> GetTournamentAsync(int id)
		{
			var tournament = await _context.Tournaments.FirstOrDefaultAsync(x => x.Id == id);

			return tournament;
		}

		public async Task<IEnumerable<Tournament>> GetTournamentsAsync()
		{
			var tournaments = await _context.Tournaments.AsNoTracking().ToListAsync();

			return tournaments;
		}

		public async Task<bool> UpdateTournamentAsync(TournamentModel model)
		{
			if (model is null)
				return false;

			var tournament = new Tournament()
			{
				Id = model.Id,
				OrganizerId = model.OrganizerId,
				PrizePool = model.PrizePool,
				MaxParticipantCount = model.MaxParticipantCount,
				TournamentName = model.TournamentName,
				GameName = model.GameName,
				TournamentDescription = model.TournamentDescription,
				RegistrationStartDate = model.RegistrationStartDate,
				RegistrationEndDate = model.RegistrationEndDate,
				TournamentStartDate = model.TournamentStartDate,
				TournamentEndDate = model.TournamentEndDate
			};

			_context.Tournaments.Update(tournament);
			var result = await _context.SaveChangesAsync();

			return result > 0;
		}
	}
}
