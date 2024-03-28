using AutoMapper;
using Tournaments.Application.BracketGeneration;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
using Tournaments.Domain.Models.BracketModels;

namespace Tournaments.Application.Services
{
	public class BracketService : IBracketService
	{
		private readonly ITournamentRepository _tournamentRepository;
		private readonly BracketGenerator _bracketGenerator;
		private readonly IMapper _mapper;

		public BracketService(ITournamentRepository tournamentRepository,
			BracketGenerator bracketGenerator,
			IMapper mapper)
		{
			_tournamentRepository = tournamentRepository;
			_bracketGenerator = bracketGenerator;
			_mapper = mapper;
		}

		public async Task<BracketModel> GenerateNewBracketAsync(long tournamentId)
		{
			if (!await _tournamentRepository.AnyAsync(tournamentId))
				throw new NotFoundException("Tournament with this id doesn't exist");

			var teams = await _tournamentRepository.GetTeamsAsync(tournamentId);

			if (teams.Count() < 2 || teams.Count() > 32)
				throw new BadRequestException("The number of teams in the tournament should be in the range [2;32].");

			var bracket = _bracketGenerator.GenerateNewBracket(teams.ToList());

			await _tournamentRepository.AddBracketAsync(bracket, tournamentId);

			return _mapper.Map<BracketModel>(bracket);
		}

		public async Task<BracketModel> GetBracketAsync(long tournamentId)
		{
			var bracket = await _tournamentRepository.GetBracketAsync(tournamentId);

			if (bracket is null)
				throw new NotFoundException("There's no bracket yet");

			return _mapper.Map<BracketModel>(bracket);
		}

		public async Task<BracketModel> UpdateBracketAsync(IList<MatchResultModel> results, long tournamentId)
		{
			var tournament = await _tournamentRepository.GetTournamentByIdAsync(tournamentId);

			if (tournament is null)
				throw new NotFoundException("Tournament with this id doesn't exist");

			var bracket = await _tournamentRepository.GetBracketAsync(tournamentId);

			if (bracket is null)
				throw new NotFoundException("Tournament has no bracket yet. Generate a new one");

			var updatedBracket = _bracketGenerator.Update(bracket, results);

			tournament.Bracket = updatedBracket;

			await _tournamentRepository.UpdateTournamentAsync(tournament);

			return _mapper.Map<BracketModel>(bracket);
		}
	}
}
