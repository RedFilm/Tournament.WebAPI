using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
using Tournaments.Persistence.Repositories;

namespace Tournaments.Application.Services
{
	public class TeamService : ITeamService
	{
		private readonly ITeamRepository _teamRepository;
		private readonly ITournamentTeamRepository _tournamentTeamRepository;
		private readonly ITournamentRepository _tournamentRepository;
		private readonly ITeamUserRepository _teamUserRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;
		

		public TeamService(ITeamRepository teamRepository,
			ITournamentTeamRepository tournamentTeamRepository,
			ITournamentRepository tournamentRepository,
			ITeamUserRepository teamUserRepository,
			UserManager<AppUser> userManager,
			IMapper mapper)
        {
			_teamRepository = teamRepository;
			_tournamentTeamRepository = tournamentTeamRepository;
			_tournamentRepository = tournamentRepository;
			_teamUserRepository = teamUserRepository;
			_userManager = userManager;
			_mapper = mapper;	
		}

		public async Task<bool> CreateTeamAsync(TeamModel teamModel)
		{
			var team = _mapper.Map<Team>(teamModel);
			return await _teamRepository.AddTeamAsync(team);
		}

		public async Task<bool> DeleteTeamAsync(long id)
		{
			var result = await _teamRepository.DeleteTeamAsync(id);

			if (!result)
				throw new NoContentException("Team's already been removed");
			return result;
		}

		public async Task<TeamModel?> GetTeamByIdAsync(long id)
		{
			var team = await _teamRepository.GetTeamByIdAsync(id);

			if (team is null)
				throw new NotFoundException("Team doesn't exist");

			return _mapper.Map<TeamModel>(team);
		}

		public async Task<IEnumerable<TournamentModel>> GetTournamentsAsync(long teamId)
		{
			if (!await _teamRepository.AnyAsync(teamId))
				throw new NotFoundException("Team with this id doesn't exist");

			var teamTournaments = await _teamRepository.GetTournamentsAsync(teamId);

			return _mapper.Map<IEnumerable<TournamentModel>>(teamTournaments);
		}

		// TODO: Заменить AppUser на модель
		public async Task<IEnumerable<AppUser>> GetTeamPlayersAsync(long teamId)
		{
			if (!await _teamRepository.AnyAsync(teamId))
				throw new NotFoundException("Team doesn't exist");

			return await _teamRepository.GetTeamPlayersAsync(teamId);
		}

		public async Task<IEnumerable<TeamModel>> GetTeamsAsync()
		{
			var teams = await _teamRepository.GetTeamsAsync();

			return _mapper.Map<IEnumerable<TeamModel>>(teams);
		}

		public async Task<bool> RegisterTeamForTournamentAsync(long teamId, long tournamentId)
		{
			var team = await _teamRepository.GetTeamByIdAsync(teamId);
			if (team is null)
				throw new NotFoundException("Team doesn't exist");

			var tournament = await _tournamentRepository.GetTournamentByIdAsync(tournamentId);
			if (tournament is null)
				throw new NotFoundException("Tournament doesn't exist");
			// TODO: Сделать кастомное исключение
			if (await _tournamentTeamRepository.AnyAsync(tournamentId, teamId))
				throw new NotImplementedException("Team's already been registred");
				//throw new AlreadyExistsException("Team already registred");

			if (await _teamRepository.AddTeamToTournamentAsync(tournament, team!))
				return true;
			return false;
		}

		public async Task<bool> AddPlayerToTeamAsync(long teamId, long playerId)
		{
			var team = await _teamRepository.GetTeamByIdAsync(teamId);
			if (team is null)
				throw new NotFoundException("Team doesn't exist");

			var user = await _userManager.FindByIdAsync(playerId.ToString());
			if (user is null)
				throw new NotFoundException("User doesn't exist");
			// TODO: Сделать кастомное исключение
			if (await _teamUserRepository.AnyAsync(teamId, playerId))
				throw new NotImplementedException("Player's alredy in team");
				//throw new AlreadyExistsException("User already is in team");

			if (await _teamRepository.AddPlayerToTeamAsync(user, team!))
				return true;
			return false;
		}

		public async Task<bool> RemovePlayerFromTeamAsync(long teamId, long playerId)
		{
			if (!await _teamRepository.AnyAsync(teamId))
				throw new NotFoundException("Team doesn't exist");

			var player = await _userManager.FindByIdAsync(playerId.ToString());
			if (player is null)
				throw new NotFoundException("Player doesn't exist");

			if (!await _teamUserRepository.AnyAsync(teamId, playerId))
				throw new NoContentException("Player's already been removed");

			if (await _teamRepository.RemovePlayerFromTeamAsync(player, teamId))
				return true;
			return false;
		}

		public async Task<bool> UpdateTeamAsync(TeamModel teamModel)
		{
			if (!await _teamRepository.AnyAsync(teamModel.Id))
				throw new NotFoundException("Team doesn't exist");

			var team = _mapper.Map<Team>(teamModel);

			return await _teamRepository.UpdateTeamAsync(team);
		}
	}
}
