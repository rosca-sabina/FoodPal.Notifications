using FoodPal.Notifications.Common.Enums;

namespace FoodPal.Notifications.Dto
{
    public class NotificationStatusDto
    {
        public int Id { get; set; }
        public NotificationStatusEnum Status { get; set; }
    }
}
