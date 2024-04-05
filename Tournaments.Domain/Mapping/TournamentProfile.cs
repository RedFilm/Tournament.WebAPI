using AutoMapper;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Models.TournamentModels;

namespace Tournaments.Domain.Mapping
{
    public class TournamentProfile : Profile
	{
        public TournamentProfile()
        {
            CreateMap<Tournament, TournamentWithIdModel>().ReverseMap();
        }
    }
}
