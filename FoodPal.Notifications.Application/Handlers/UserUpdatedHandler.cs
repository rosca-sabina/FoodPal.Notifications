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
    public class UserUpdatedHandler : IRequestHandler<UserUpdatedCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UserUpdatedCommand> _validator;

        public UserUpdatedHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UserUpdatedCommand> validator)
        {
            this._unitOfWork = unitOfWork;
            this._userRepository = _unitOfWork.GetRepository<User>();
            this._mapper = mapper;
            this._validator = validator;
        }
        public async Task<bool> Handle(UserUpdatedCommand request, CancellationToken cancellationToken)
        {
            this._validator.ValidateAndThrowEx(request);

            var userModel = this._mapper.Map<User>(request);

            if(await _userRepository.FindByIdAsync(userModel.Id) is null)
            {
                throw new ArgumentException($"No user with id {userModel.Id} exists.");
            }

            // save to db
            this._userRepository.Update(userModel);
            return await this._unitOfWork.SaveChangesAsnyc();
        }
    }
}
