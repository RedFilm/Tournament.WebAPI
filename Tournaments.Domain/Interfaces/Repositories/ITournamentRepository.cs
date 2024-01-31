using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Interfaces.Repositories
{
    public interface ITournamentRepository
    {
        Task<Tournament?> GetTournamentAsync(int id);
        Task<IEnumerable<Tournament>> GetTournamentsAsync();

        Task<bool> AddTournamentAsync(TournamentModel tournament);
        Task<bool> UpdateTournamentAsync(TournamentModel tournament);
        Task<bool> DeleteTournamentAsync(int id);
    }
}
