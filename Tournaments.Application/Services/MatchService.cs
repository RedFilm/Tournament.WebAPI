using AutoMapper;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;

namespace Tournaments.Application.Services
{
	public class MatchService : IMatchService
	{
		private readonly IMatchRepository _matchRepository;
		private readonly IMapper _mapper;

		public MatchService(IMatchRepository matchRepository,
			IMapper mapper)
		{
			_matchRepository = matchRepository;
			_mapper = mapper;
		}

		public async Task<MatchModel> GetMatchByIdAsync(long matchId)
		{
			var match = await _matchRepository.GetMatchByIdAsync(matchId);

			if (match is null)
				throw new NotFoundException("Match doesn't exist");

			return _mapper.Map<MatchModel>(match);
		}

		public async Task<IEnumerable<MatchModel>> GetMatchesAsync(long stageId)
		{
			var teams = await _matchRepository.GetMatchesAsync(stageId);

			return _mapper.Map<IEnumerable<MatchModel>>(teams);
		}
	}
}
