using FoodPal.Notifications.Common.Enums;

namespace FoodPal.Contracts
{
    public interface INotificationViewed
    {
        public int Id { get; set; }
        public NotificationStatusEnum Status { get; set; }
    }
}
