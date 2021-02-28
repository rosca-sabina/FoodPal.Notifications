using AutoMapper;
using FoodPal.Contracts;
using FoodPal.Notifications.Application.Commands;
using FoodPal.Notifications.Common.Exceptions;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPal.Notifications.Messages
{
    public class NotificationViewedConsumer : IConsumer<INotificationViewed>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UserUpdatedConsumer> _logger;

        public NotificationViewedConsumer(IMediator mediator, IMapper mapper, ILogger<UserUpdatedConsumer> logger)
        {
            this._mediator = mediator;
            this._mapper = mapper;
            this._logger = logger;
        }
        public async Task Consume(ConsumeContext<INotificationViewed> context)
        {
            try
            {
                var message = context.Message;

                var command = this._mapper.Map<NotificationViewedCommand>(message);

                // TODO: refactor this 
                await this._mediator.Send(command);
            }
            catch (ValidationsException e)
            {
                var errors = e.Errors.Aggregate((curr, next) => $"{curr}; {next}");
                this._logger.LogError(e, errors);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, $"Something went wrong in {nameof(UserUpdatedConsumer)}.");
            }
        }
    }
}
