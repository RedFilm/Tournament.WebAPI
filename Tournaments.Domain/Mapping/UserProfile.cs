using AutoMapper;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;
using Tournaments.Domain.Models.AuthModels;

namespace Tournaments.Domain.Mapping
{
    public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<RegisterModel, AppUser>();
			CreateMap<AppUser, UserModel>();
		}
	}
}
