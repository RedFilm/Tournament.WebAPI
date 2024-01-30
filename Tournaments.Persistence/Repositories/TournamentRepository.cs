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
		private TournamentDbContext _context;

		public TournamentRepository(TournamentDbContext context)
        {
            _context =	context;
        }

        public async Task<bool> AddTournamentAsync(TournamentViewModel vm)
		{
			if (vm == null)
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

			_context.Tournaments.Add(tournament);
			var result = await _context.SaveChangesAsync();

			if(result > 0)
				return true;
			return false;
		}

		public async Task<bool> DeleteTournamentAsync(int id)
		{
			var tournament = _context.Tournaments.FirstOrDefault(x => x.Id == id);

			if (tournament == null) 
				return false;

			_context.Tournaments.Remove(tournament);
			var result = await _context.SaveChangesAsync(); 

			if (result > 0)
				return true;
			return false;
		}

		public Tournament GetTournament(int id)
		{
			var tournament = _context.Tournaments.FirstOrDefault(x => x.Id == id);

			return tournament;
		}

		public async Task<IEnumerable<Tournament>> GetTournamentsAsync()
		{
			var tournaments = _context.Tournaments.AsNoTracking().ToList();

			return tournaments;
		}

		public async Task<bool> UpdateTournamentAsync(TournamentViewModel vm)
		{
			if (vm == null)
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

			if (result > 0)
				return true;
			return false;
		}
	}
}
