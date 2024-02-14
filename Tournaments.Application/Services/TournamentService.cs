using AutoMapper;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Mapping;
using Tournaments.Domain.Models;
using Tournaments.Domain.Statuses;

namespace Tournaments.Application.Services
{
	public class TournamentService : ITournamentService
	{
		private readonly IMapper _mapper;
		private readonly ITournamentRepository _tournamentRepository;

		public TournamentService(IMapper mapper, ITournamentRepository tournamentRepository)
        {
            _mapper = mapper;
			_tournamentRepository = tournamentRepository;
        }

        public async Task<bool> AddTournamentAsync(TournamentModel tournamentModel)
		{
			var tournament = _mapper.Map<Tournament>(tournamentModel);

			return await _tournamentRepository.AddTournamentAsync(tournament);	
		}

		public async Task<bool> DeleteTournamentAsync(long id)
		{
			var result = await _tournamentRepository.DeleteTournamentAsync(id);

			if (!result)
				throw new NoContentException("Tournament's already been removed");

			return result;
		}

		public async Task<IEnumerable<TeamModel>> GetTeamsAsync(long tournamentId)
		{
			if (await _tournamentRepository.GetTournamentByIdAsync(tournamentId) is null)
				throw new NotFoundException("Tournament with this id doesn't exist");

			var teams = await _tournamentRepository.GetTeamsAsync(tournamentId);

			return _mapper.Map<IEnumerable<TeamModel>>(teams);
		}

		public async Task<TournamentModel?> GetTournamentByIdAsync(long id)
		{
			var tournament = await _tournamentRepository.GetTournamentByIdAsync(id);

			if (tournament is null)
				throw new NotFoundException("Tournament doesn't exist");

			var tournamentModel = _mapper.Map<TournamentModel>(tournament);
			return tournamentModel;
		}

		public async Task<IEnumerable<TournamentModel>> GetTournamentsAsync()
		{
			var tournaments = await _tournamentRepository.GetTournamentsAsync();

			return _mapper.Map<IEnumerable<TournamentModel>>(tournaments);
		}

		public async Task<bool> UpdateTournamentAsync(TournamentModel tournamentModel)
		{
			if (await _tournamentRepository.GetTournamentByIdAsync(tournamentModel.Id) is null)
				throw new NotFoundException("Tournament doesn't exist");

			var tournament = _mapper.Map<Tournament>(tournamentModel);

			return await _tournamentRepository.UpdateTournamentAsync(tournament);
		}
	}
}
