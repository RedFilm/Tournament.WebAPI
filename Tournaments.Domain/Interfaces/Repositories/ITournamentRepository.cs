using Tournaments.Domain.Models;
using Tournaments.Domain.ViewModels;

namespace Tournaments.Domain.Interfaces.Repositories
{
    public interface ITournamentRepository
    {
        Tournament GetTournament(int id);
        Task<IEnumerable<Tournament>> GetTournamentsAsync();

        Task<bool> AddTournamentAsync(TournamentViewModel tournament);
        Task<bool> UpdateTournamentAsync(TournamentViewModel tournament);
        Task<bool> DeleteTournamentAsync(int id);
    }
}
