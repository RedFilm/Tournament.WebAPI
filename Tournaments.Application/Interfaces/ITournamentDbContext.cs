using Microsoft.EntityFrameworkCore;
using Tournaments.Domain;

namespace Tournaments.Application.Interfaces
{
	public interface ITournamentDbContext
	{
		DbSet<Tournament> Tournaments { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
