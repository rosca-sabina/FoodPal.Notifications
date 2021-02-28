using FoodPal.Contracts;
using FoodPal.Notifications.Common.Enums;
using FoodPal.Notifications.Dto;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FoodPal.Notifications.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {  
        private readonly ILogger<NotificationController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public NotificationController(ILogger<NotificationController> logger, IPublishEndpoint publishEndpoint)
        {
            this._logger = logger;
            this._publishEndpoint = publishEndpoint;
        } 

        [HttpPost]
        public async Task<IActionResult> CreateNotification(NotificationDto notificationDto)
        { 
            await this._publishEndpoint.Publish<INewNotificationAdded>(notificationDto);

            return Accepted();
        }

        [HttpPatch]
        [Route("viewed/{id}")]
        public async Task<IActionResult> UpdateNotificationStatus(int id)
        {
            var notificationStatusDto = new NotificationStatusDto
            {
                Id = id,
                Status = NotificationStatusEnum.Viewed
            };
            await this._publishEndpoint.Publish<INotificationViewed>(notificationStatusDto);

            return Accepted();
        }
    }
}
