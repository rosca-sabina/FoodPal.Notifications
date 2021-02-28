using AutoMapper;
using FluentValidation;
using FoodPal.Notifications.Application.Commands;
using FoodPal.Notifications.Common.Enums;
using FoodPal.Notifications.Data.Abstractions;
using FoodPal.Notifications.Domain;
using FoodPal.Notifications.Dto.Intern;
using FoodPal.Notifications.Service;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Notifications.Application.Handlers
{
    public class RetrySendNotificationHandler : IRequestHandler<RetrySendNotificationCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<RetrySendNotificationCommand> _validator;
        private readonly INotificationService _notificationService;

        public RetrySendNotificationHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RetrySendNotificationCommand> validator, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationRepository = _unitOfWork.GetRepository<Notification>();
            _mapper = mapper;
            _validator = validator;
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(RetrySendNotificationCommand request, CancellationToken cancellationToken)
        {
            var notificationServiceDto = _mapper.Map<RetrySendNotificationCommand, NotificationServiceDto>(request);
            var sent = await this._notificationService.Send(request.Type, notificationServiceDto);

            var notificationModel = await _notificationRepository.FindByIdAsync(request.Id);
            notificationModel.Status = sent ? NotificationStatusEnum.Viewed : NotificationStatusEnum.Error;
            return await this._unitOfWork.SaveChangesAsnyc();
        }
    }
}
