using FoodPal.Notifications.Common.Enums;

namespace FoodPal.Notifications.Dto
{
    public class NotificationDto
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; } 
        public string CreateBy { get; set; }
        public string ModifiedBy { get; set; }
        public NotificationTypeEnum Type { get; set; }
        public string Info { get; set; }
    }
}
