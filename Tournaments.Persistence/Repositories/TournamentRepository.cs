using Microsoft.EntityFrameworkCore;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;
using AutoMapper;

namespace Tournaments.Persistence.Repositories
{
	public class TournamentRepository : ITournamentRepository
	{
		private readonly TournamentDbContext _context;
		private readonly IMapper _mapper;

		public TournamentRepository(TournamentDbContext context, IMapper mapper)
        {
            _context =	context;
			_mapper = mapper;
        }

		/// <inheritdoc />
		public async Task<bool> AddTournamentAsync(TournamentModel model)
		{
			var tournament = _mapper.Map<Tournament>(model);			

			await _context.Tournaments.AddAsync(tournament);
			var result = await _context.SaveChangesAsync();

			return result > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteTournamentAsync(int id)
		{
			var tournament = await _context.Tournaments.FirstOrDefaultAsync(x => x.Id == id);

			if (tournament is null) 
				return false;

			_context.Tournaments.Remove(tournament);
			var result = await _context.SaveChangesAsync();

			return result > 0;
		}

		/// <inheritdoc />
		public async Task<Tournament?> GetTournamentAsync(int id)
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
		public async Task<bool> UpdateTournamentAsync(TournamentModel model)
		{
			var tournament = _mapper.Map<Tournament>(model);

			_context.Tournaments.Update(tournament);
			var result = await _context.SaveChangesAsync();

			return result > 0;
		}
	}
}
