using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournaments.Application.Tournaments.Commands.DeleteTournament
{
	public class DeleteTournamentCommand : IRequest
	{
		public Guid Id { get; set; }
	}
}
