using MediatR;
using Microsoft.EntityFrameworkCore;
using Tournaments.Application.Interfaces;

namespace Tournaments.Application.Tournaments.Queries.GetTournamentList
{
	public class GetTournamentsListQueryHandler
		: IRequestHandler<GetTournamentListQuery, TournamentsListVm>
	{
		private readonly ITournamentDbContext _tournamentDbContext;

        public GetTournamentsListQueryHandler(ITournamentDbContext tournamentDbContext) =>
			_tournamentDbContext = tournamentDbContext;

        public async Task<TournamentsListVm> Handle(GetTournamentListQuery request, CancellationToken cancellationToken)
		{
			var tournamentsQuery = await _tournamentDbContext.Tournaments.ToListAsync(cancellationToken);

			return new TournamentsListVm { Tournaments = tournamentsQuery };
		}
	}
}
