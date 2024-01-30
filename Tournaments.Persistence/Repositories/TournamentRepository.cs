using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Models;
using Tournaments.Domain.ViewModels;

namespace Tournaments.Persistence.Repositories
{
	public class TournamentRepository : ITournamentRepository
	{
		private readonly TournamentDbContext _context;

		public TournamentRepository(TournamentDbContext context)
        {
            _context =	context;
        }

        public async Task<bool> AddTournamentAsync(TournamentViewModel vm)
		{
			if (vm is null)
				return false;

			var tournament = new Tournament()
			{
				OrganizerId = vm.OrganizerId,
				PrizePool = vm.PrizePool,
				MaxParticipantCount = vm.MaxParticipantCount,
				TournamentName = vm.TournamentName,
				GameName = vm.GameName,
				TournamentDescription = vm.TournamentDescription,
				RegistrationStartDate = vm.RegistrationStartDate,
				RegistrationEndDate = vm.RegistrationEndDate,
				TournamentStartDate = vm.TournamentStartDate,
				TournamentEndDate = vm.TournamentEndDate
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

		public async Task<bool> UpdateTournamentAsync(TournamentViewModel vm)
		{
			if (vm is null)
				return false;

			var tournament = new Tournament()
			{
				Id = vm.Id,
				OrganizerId = vm.OrganizerId,
				PrizePool = vm.PrizePool,
				MaxParticipantCount = vm.MaxParticipantCount,
				TournamentName = vm.TournamentName,
				GameName = vm.GameName,
				TournamentDescription = vm.TournamentDescription,
				RegistrationStartDate = vm.RegistrationStartDate,
				RegistrationEndDate = vm.RegistrationEndDate,
				TournamentStartDate = vm.TournamentStartDate,
				TournamentEndDate = vm.TournamentEndDate
			};

			_context.Tournaments.Update(tournament);
			var result = await _context.SaveChangesAsync();

			return result > 0;
		}
	}
}
