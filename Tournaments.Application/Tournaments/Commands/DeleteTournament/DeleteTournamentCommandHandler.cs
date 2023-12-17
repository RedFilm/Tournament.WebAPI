using MediatR;
using Tournaments.Application.Common.Exceptions;
using Tournaments.Application.Interfaces;
using Tournaments.Domain;

namespace Tournaments.Application.Tournaments.Commands.DeleteTournament
{
	public class DeleteTournamentCommandHandler
		: IRequestHandler<DeleteTournamentCommand>
	{
		private readonly ITournamentDbContext _tournamentDbContext;

        public DeleteTournamentCommandHandler(ITournamentDbContext tournamentDbContext) =>
            _tournamentDbContext = tournamentDbContext;

		public async Task Handle(DeleteTournamentCommand request, CancellationToken cancellationToken)
		{
			var tournament = await _tournamentDbContext.Tournaments
				.FindAsync(new object[] { request.Id }, cancellationToken);

			if (tournament == null)
				throw new NotFoundException(nameof(Tournament), request.Id);

			_tournamentDbContext.Tournaments.Remove(tournament);
			await _tournamentDbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
