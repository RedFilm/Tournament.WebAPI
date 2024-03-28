﻿using AutoMapper;
using Tournaments.Application.BracketGeneration;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models.BracketModels;
using Tournaments.Domain.Models.TeamModels;
using Tournaments.Domain.Models.TournamentModels;

namespace Tournaments.Application.Services
{
    public class TournamentService : ITournamentService
	{
		private readonly IMapper _mapper;
		private readonly ITournamentRepository _tournamentRepository;
		private readonly BracketGenerator _bracketGenerator;

		public TournamentService(IMapper mapper,
			ITournamentRepository tournamentRepository,
			BracketGenerator bracketGenerator)
		{
			_mapper = mapper;
			_tournamentRepository = tournamentRepository;
			_bracketGenerator = bracketGenerator;
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

		public async Task<IEnumerable<TeamWithIdModel>> GetTeamsAsync(long tournamentId)
		{
			if (!await _tournamentRepository.AnyAsync(tournamentId))
				throw new NotFoundException("Tournament with this id doesn't exist");

			var teams = await _tournamentRepository.GetTeamsAsync(tournamentId);

			return _mapper.Map<IEnumerable<TeamWithIdModel>>(teams);
		}

		public async Task<TournamentModel> GetTournamentByIdAsync(long id)
		{
			var tournament = await _tournamentRepository.GetTournamentByIdAsync(id);

			if (tournament is null)
				throw new NotFoundException("Tournament doesn't exist");

			var tournamentModel = _mapper.Map<TournamentModel>(tournament);
			return tournamentModel;
		}

		public async Task<IEnumerable<TournamentWithIdModel>> GetTournamentsAsync()
		{
			var tournaments = await _tournamentRepository.GetTournamentsAsync();

			return _mapper.Map<IEnumerable<TournamentWithIdModel>>(tournaments);
		}

		public async Task UpdateTournamentAsync(TournamentModel tournamentModel, long tournamentId)
		{
			var tournament = _mapper.Map<Tournament>(tournamentModel);
			tournament.Id = tournamentId;

			if (!await _tournamentRepository.UpdateTournamentAsync(tournament))
				throw new NotFoundException("Couldn't save changes or tournament doesn't exist");
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

		public async Task<BracketModel> UpdateBracketAsync(BracketUpdateModel model)
		{
			var tournament = await _tournamentRepository.GetTournamentByIdAsync(model.TournamentId);

			if (tournament is null)
				throw new NotFoundException("Tournament with this id doesn't exist");

			var bracket = await _tournamentRepository.GetBracketAsync(model.TournamentId);

			if (bracket is null)
				throw new NotFoundException("Tournament has no bracket yet. Generate a new one");

			var updatedBracket = _bracketGenerator.Update(bracket, model.Results);

			tournament.Bracket = updatedBracket;

			await _tournamentRepository.UpdateTournamentAsync(tournament);

			return _mapper.Map<BracketModel>(bracket);
		}
	}
}
