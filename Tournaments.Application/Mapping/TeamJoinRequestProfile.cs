using AutoMapper;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Models.TeamJoinRequestModels;

namespace Tournaments.Application.Mapping
{
	public class TeamJoinRequestProfile : Profile
	{
		public TeamJoinRequestProfile()
		{
			CreateMap<TeamJoinRequestModel, TeamJoinRequest>();
			CreateMap<TeamJoinRequest, TeamJoinRequestWithIdModel>();
		}
	}
}
