using Microsoft.EntityFrameworkCore;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Entities;

namespace Tournaments.Persistence.Repositories
{
	public class TournamentRepository : ITournamentRepository
	{
		private readonly TournamentDbContext _context;

		public TournamentRepository(TournamentDbContext context)
        {
            _context =	context;
        }

		/// <inheritdoc />
		public async Task<bool> AddTournamentAsync(Tournament tournament)
		{
			await _context.Tournaments.AddAsync(tournament);
			var result = await _context.SaveChangesAsync();

			return result > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteTournamentAsync(long id)
		{
			var tournament = await _context.Tournaments.FirstOrDefaultAsync(x => x.Id == id);

			if (tournament is null) 
				return false;

			_context.Tournaments.Remove(tournament);
			var result = await _context.SaveChangesAsync();

			return result > 0;
		}

		/// <inheritdoc />
		public async Task<Tournament?> GetTournamentAsync(long id)
		{
			var tournament = await _context.Tournaments.FirstOrDefaultAsync(x => x.Id == id);
			return tournament;
		}
		
		/// <inheritdoc />
		public async Task<IEnumerable<Tournament>> GetTournamentsAsync()
		{
			var tournaments = await _context.Tournaments.AsNoTracking().ToListAsync();
			return tournaments;
		}

		/// <inheritdoc />
		public async Task<bool> UpdateTournamentAsync(Tournament tournament)
		{
			_context.Tournaments.Update(tournament);
			var result = await _context.SaveChangesAsync();

			return result > 0;
		}
	}
}
