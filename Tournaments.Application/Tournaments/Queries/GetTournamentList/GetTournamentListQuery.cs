using MediatR;

namespace Tournaments.Application.Tournaments.Queries.GetTournamentList
{
	public class GetTournamentListQuery : IRequest<TournamentsListVm>
	{
        public Guid UserId { get; set; }
    }
}
