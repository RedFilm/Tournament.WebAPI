using AutoMapper;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Mapping
{
	public class TournamentProfile : Profile
	{
        public TournamentProfile()
        {
            CreateMap<Tournament, TournamentModel>().ReverseMap();
        }
    }
}
