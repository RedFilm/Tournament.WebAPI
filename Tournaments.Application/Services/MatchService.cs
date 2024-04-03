using AutoMapper;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;

namespace Tournaments.Application.Services
{
	internal class MatchService : IMatchService
	{
		private readonly IMatchRepository _matchRepository;
		private readonly IMapper _mapper;

		public MatchService(IMatchRepository matchRepository,
			IMapper mapper)
		{
			_matchRepository = matchRepository;
			_mapper = mapper;
		}

		public Task<MatchModel> GetMatchByIdAsync(long matchId)
		{
			var match = await _matchRepository.GetTeamByIdAsync(matchId);

			if (match is null)
				throw new NotFoundException("Match doesn't exist");

			return _mapper.Map<MatchModel>(match);
		}
	}
}
