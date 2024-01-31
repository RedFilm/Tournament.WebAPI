using AutoMapper;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Mapping
{
	public class UserProfile : Profile
	{
        public UserProfile()
        {
            CreateMap<RegisterModel, AppUser>();
        }
    }
}
