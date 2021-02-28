using AutoMapper;
using FluentValidation;
using FoodPal.Notifications.Application.Commands;
using FoodPal.Notifications.Application.Extensions;
using FoodPal.Notifications.Data.Abstractions;
using FoodPal.Notifications.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Notifications.Application.Handlers
{
    public class NotificationViewedHandler : IRequestHandler<NotificationViewedCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IValidator<NotificationViewedCommand> _validator;
        public NotificationViewedHandler(IUnitOfWork unitOfWork, IValidator<NotificationViewedCommand> validator)
        {
            this._unitOfWork = unitOfWork;
            this._notificationRepository = _unitOfWork.GetRepository<Notification>();

            this._validator = validator;
        }

        public async Task<bool> Handle(NotificationViewedCommand request, CancellationToken cancellationToken)
        {
            this._validator.ValidateAndThrowEx(request);

            var notificationModel = await _notificationRepository.FindByIdAsync(request.Id);

            if(notificationModel is null)
            {
                throw new ArgumentException($"No notification with id {request.Id} exists.");
            }

            notificationModel.Status = request.Status;
            _notificationRepository.Update(notificationModel);
            return await this._unitOfWork.SaveChangesAsnyc();
        }
    }
}
