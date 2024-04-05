using AutoMapper;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Application.Mapping
{
	public class MatchProfile : Profile
	{
		public MatchProfile()
		{
			CreateMap<Match, MatchModel>();
		}
	}
}
