using AutoMapper;
using FoodPal.Notifications.Application.Commands;
using FoodPal.Notifications.Common.Enums;
using FoodPal.Notifications.Data.Abstractions;
using FoodPal.Notifications.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Notifications.Processor
{
    public class NotificationRetryHostedService : IHostedService, IDisposable
    {
        private Timer _timer;

        private readonly ILogger<NotificationRetryHostedService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        public NotificationRetryHostedService(ILogger<NotificationRetryHostedService> logger, IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _notificationRepository = _unitOfWork.GetRepository<Notification>();
            _mapper = mapper;
            _mediator = mediator;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(NotificationRetryHostedService)} started.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(15));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(NotificationRetryHostedService)} stopped.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            _logger.LogInformation($"{nameof(NotificationRetryHostedService)} is working.");

            try
            {
                var failedNotifications = await _notificationRepository
                    .Find(x => x.Status == NotificationStatusEnum.Error, new List<string> { "User" })
                    .ToListAsync();

                foreach (Notification n in failedNotifications)
                {
                    var retrySendNotificationCommand = _mapper.Map<Notification, RetrySendNotificationCommand>(n);
                    await _mediator.Send(retrySendNotificationCommand);
                }
            }
            catch(Exception ex)
            {
                _logger.LogDebug(ex, ex.Message, null);
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
