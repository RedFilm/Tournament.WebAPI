using MediatR;
using Microsoft.EntityFrameworkCore;
using Tournaments.Application.Interfaces;
using Tournaments.Domain;

namespace Tournaments.Application.Tournaments.Commands.CreateTournament
{
	public class CreateTournamentCommandHandler
		: IRequestHandler<CreateTournamentCommand, Guid>
	{
		private readonly ITournamentDbContext _dbContext;

		public CreateTournamentCommandHandler(ITournamentDbContext dbContext) =>
			_dbContext = dbContext;
		

		public async Task<Guid> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
		{
			var tournament = new Tournament
			{
				Id = Guid.NewGuid(),
				Name = request.Name,
				Description = request.Description,
				CreationDate = DateTime.UtcNow,
			};

			await _dbContext.Tournaments.AddAsync(tournament, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return tournament.Id;
		}
	}
}
