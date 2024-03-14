using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
using Tournaments.Domain.Models.TeamModels;
using Tournaments.Domain.Models.TournamentModels;

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

		public async Task<TeamModel> GetTeamByIdAsync(long id)
		{
			var team = await _teamRepository.GetTeamByIdAsync(id);

			if (team is null)
				throw new NotFoundException("Team doesn't exist");

			return _mapper.Map<TeamModel>(team);
		}

		public async Task<IEnumerable<TournamentWithIdModel>> GetTournamentsAsync(long teamId)
		{
			if (!await _teamRepository.AnyAsync(teamId))
				throw new NotFoundException("Team with this id doesn't exist");

			var teamTournaments = await _teamRepository.GetTournamentsAsync(teamId);

			return _mapper.Map<IEnumerable<TournamentWithIdModel>>(teamTournaments);
		}

		public async Task<IEnumerable<UserModel>> GetTeamPlayersAsync(long teamId)
		{
			if (!await _teamRepository.AnyAsync(teamId))
				throw new NotFoundException("Team doesn't exist");

			var appUsers = await _teamRepository.GetTeamPlayersAsync(teamId);

			return _mapper.Map<IEnumerable<UserModel>>(appUsers);
		}

		public async Task<IEnumerable<TeamWithIdModel>> GetTeamsAsync()
		{
			var teams = await _teamRepository.GetTeamsAsync();

			return _mapper.Map<IEnumerable<TeamWithIdModel>>(teams);
		}

		// TODO: Вынести некоторые проверки в отдельные методы\классы
		public async Task<bool> RegisterTeamForTournamentAsync(RegisterForTournamentModel model)
		{
			var team = await _teamRepository.GetTeamByIdAsync(model.TeamId);
			if (team is null)
				throw new NotFoundException("Team doesn't exist");

			var tournament = await _tournamentRepository.GetTournamentByIdAsync(model.TournamentId);
			if (tournament is null)
				throw new NotFoundException("Tournament doesn't exist");

			if (team.OwnerId != model.OwnerId)
				throw new BadRequestException("You must be the creator of the team to register for tournament");

			if (await _tournamentTeamRepository.AnyAsync(model.TournamentId, model.TeamId))
				throw new AlreadyExistsException("Team's already been registered");

			if (tournament.RegistrationEndDate < DateTime.UtcNow)
				throw new AlreadyExistsException("Registration is over");

			return await _teamRepository.AddTeamToTournamentAsync(tournament, team);
		}

		public async Task<bool> AddPlayerToTeamAsync(TeamMemberUpdateModel model)
		{
			var team = await _teamRepository.GetTeamByIdAsync(model.TeamId);
			if (team is null)
				throw new NotFoundException("Team doesn't exist");

			var user = await _userManager.FindByIdAsync(model.PlayerId.ToString());
			if (user is null)
				throw new NotFoundException("User doesn't exist");

			if (team.OwnerId != model.ExecutorId)
				throw new BadRequestException("You must be the captain of the team to invite players");

			if (await _teamUserRepository.AnyAsync(model.TeamId, model.PlayerId))
				throw new AlreadyExistsException("Player's already in team");

			return await _teamRepository.AddPlayerToTeamAsync(user, team);
		}

		public async Task<bool> RemovePlayerFromTeamAsync(TeamMemberUpdateModel model)
		{
			var team = await _teamRepository.GetTeamByIdAsync(model.TeamId);
			if (team is null)
				throw new NotFoundException("Team doesn't exist");

			var player = await _userManager.FindByIdAsync(model.PlayerId.ToString());
			if (player is null)
				throw new NotFoundException("Player doesn't exist");

			if (team.OwnerId != model.ExecutorId)
				throw new BadRequestException("You must be the captain of the team to remove players");

			if (!await _teamUserRepository.AnyAsync(model.TeamId, model.PlayerId))
				throw new NoContentException("Player's already been removed");

			return await _teamRepository.RemovePlayerFromTeamAsync(player, model.TeamId);
		}

		public async Task UpdateTeamAsync(TeamWithIdModel teamModel)
		{
			var team = _mapper.Map<Team>(teamModel);

			if (!await _teamRepository.UpdateTeamAsync(team))
				throw new BadRequestException("Couldn't save changes or team doesn't exist");
		}
	}
}
