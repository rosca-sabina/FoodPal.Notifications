using FoodPal.Notifications.Common.Enums;
using MediatR;

namespace FoodPal.Notifications.Application.Commands
{
    public class RetrySendNotificationCommand: IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public NotificationTypeEnum Type { get; set; }
    }
}
