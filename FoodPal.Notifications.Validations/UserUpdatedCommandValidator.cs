using FoodPal.Notifications.Application.Commands;
using FluentValidation;

namespace FoodPal.Notifications.Validations
{
    public class UserUpdatedCommandValidator: InternalValidator<UserUpdatedCommand>
    {
        public UserUpdatedCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.PhoneNo)
                .NotEmpty();
        }
    }
}
