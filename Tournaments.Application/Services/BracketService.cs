using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Tournaments.Application.BracketGeneration;
using Tournaments.Application.Hubs;
using Tournaments.Domain.Entities;
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
		private readonly ITournamentTeamRepository _tournamentTeamRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly BracketGenerator _bracketGenerator;
		private readonly IHubContext<BracketHub> _hubContext;
		private readonly IMapper _mapper;

		public BracketService(ITournamentRepository tournamentRepository,
			ITournamentTeamRepository tournamentTeamRepository,
			UserManager<AppUser> userManager,
			BracketGenerator bracketGenerator,
			IHubContext<BracketHub> hubContext,
			IMapper mapper)
		{
			_tournamentRepository = tournamentRepository;
			_tournamentTeamRepository = tournamentTeamRepository;
			_userManager = userManager;
			_bracketGenerator = bracketGenerator;
			_hubContext = hubContext;
			_mapper = mapper;
		}

		public async Task<BracketModel> GenerateNewBracketAsync(long tournamentId, bool shuffleTeams)
		{
			if (!await _tournamentRepository.AnyAsync(tournamentId))
				throw new NotFoundException("Tournament with this id doesn't exist");

			var tournament = await _tournamentRepository.GetTournamentByIdAsync(tournamentId);

			var teams = await _tournamentRepository.GetTeamsAsync(tournamentId);

			if (teams.Count < 2 || teams.Count > 32)
				throw new BadRequestException("The number of teams in the tournament should be in the range [2;32].");

			var bracket = _bracketGenerator.GenerateNewBracket(teams.ToList(), shuffleTeams);

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

		public async Task<BracketModel> UpdateBracketAsync(IEnumerable<MatchResultModel> results, long tournamentId)
		{
			var tournament = await _tournamentRepository.GetTournamentByIdAsync(tournamentId);

			if (tournament is null)
				throw new NotFoundException("Tournament with this id doesn't exist");

			var bracket = await _tournamentRepository.GetBracketAsync(tournamentId);

			if (bracket is null)
				throw new NotFoundException("Tournament has no bracket yet. Generate a new one");

			var updatedBracket = SetResults(bracket, results);

			if (tournament.IsOfficial)
			{
				foreach (var result in results)
					await SetRating(result.WinnerId, result.StageNumber, tournamentId);
			}

			tournament.Bracket = updatedBracket;

			await _tournamentRepository.UpdateTournamentAsync(tournament);

			var bracketModel = _mapper.Map<BracketModel>(bracket);

			await _hubContext.Clients.Group($"bracket-{bracket.Id}").SendAsync("OnBracketUpdate", bracketModel);

			return bracketModel;
		}

		private Bracket SetResults(Bracket bracket, IEnumerable<MatchResultModel> matchesResult)
		{
			foreach (var result in matchesResult)
			{
				var currentStageMatch = bracket.Stages.SelectMany(s => s.Matches)
					.FirstOrDefault(m => m.MatchId == result.MatchId);

				if (currentStageMatch is null)
					continue;

				currentStageMatch.WinnerId = result.WinnerId;
				var nextStageMatchNumber = currentStageMatch.Identifier / 2;

				var nextStageMatch = bracket.Stages.Where(s => s.StageNumber == result.StageNumber - 1)
					.SelectMany(s => s.Matches)
					.FirstOrDefault(m => m.Identifier == nextStageMatchNumber);

				if (nextStageMatch is null)
					continue;

				if (nextStageMatch.Team1Id is null)
					nextStageMatch.Team1Id = result.WinnerId;
				else nextStageMatch.Team2Id ??= result.WinnerId;
			}

			return bracket;
		}

		private async Task SetRating(long winnerTeamId, int stageNumber, long tournamentId)
		{
			var playerIds = await _tournamentTeamRepository
				.GetTournamentTeamPlayerIdsAsync(tournamentId, winnerTeamId);

			foreach (var playerId in playerIds)
			{
				var user = await _userManager.FindByIdAsync(playerId.ToString());
				if (user is not null)
				{
					user.Rating += (100 - stageNumber) * 0.2d;
					await _userManager.UpdateAsync(user);
				}
			}
		}
	}
}
