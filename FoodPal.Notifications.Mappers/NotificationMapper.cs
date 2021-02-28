using FoodPal.Contracts;
using FoodPal.Notifications.Application.Commands;
using FoodPal.Notifications.Dto.Intern;
using FoodPal.Notifications.Processor.Commands;

namespace FoodPal.Notifications.Mappers
{
    public class NotificationMapper : InternalProfile
    {
        public NotificationMapper()
        {
            this.CreateMap<INewNotificationAdded, NewNotificationAddedCommand>();
            this.CreateMap<NewNotificationAddedCommand, Domain.Notification>();

            this.CreateMap<INotificationViewed, NotificationViewedCommand>();

            /*this.CreateMap<Domain.Notification, NotificationServiceDto>()
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(source => source.User.PhoneNo))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(source => source.User.Email))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(source => source.Title))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(source => source.Message));*/

            this.CreateMap<Domain.Notification, RetrySendNotificationCommand>()
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(source => source.User.PhoneNo))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(source => source.User.Email));

            this.CreateMap<RetrySendNotificationCommand, NotificationServiceDto>()
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(source => source.Title))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(source => source.Message));
        }
    }
}
