using AutoMapper;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Application.Mapping
{
	public class NotificationProfile : Profile
	{
		public NotificationProfile()
		{
			CreateMap<Notification, NotificationModel>()
				.ForMember(dest => dest.IsRead,
					opt => opt.MapFrom(src => src.UserNotifications.Any(un => un.IsRead)))
				.ForMember(dest => dest.ReceiverId,
					opt => opt.MapFrom(src => src.UserNotifications.FirstOrDefault()!.UserId));
		}
	}
}
