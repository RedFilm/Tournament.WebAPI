using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Domain;

namespace Tournaments.Application.Tournaments.Queries.GetTournamentList
{
	public class TournamentsListVm
	{
        public List<Tournament> Tournaments { get; set; }
    }
}
