using AutoMapper;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Mapping
{
	public class TeamProfile : Profile
	{
        public TeamProfile()
        {
            CreateMap<TeamModel, Team>().ReverseMap();
        }
    }
}
