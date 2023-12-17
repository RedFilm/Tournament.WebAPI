using MediatR;

namespace Tournaments.Application.Tournaments.Commands.CreateTournament
{
	public class CreateTournamentCommand : IRequest<Guid>
	{
        public string Name { get; set; }
		public string Description { get; set; }
    }
}
