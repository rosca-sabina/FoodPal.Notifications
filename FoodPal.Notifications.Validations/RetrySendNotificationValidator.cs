using FluentValidation;
using FoodPal.Notifications.Application.Commands;

namespace FoodPal.Notifications.Validations
{
    public class RetrySendNotificationValidator: InternalValidator<RetrySendNotificationCommand>
    {
        public RetrySendNotificationValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.Message)
                .NotEmpty();

            RuleFor(x => x.Type)
                .NotEmpty()
                .IsInEnum();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.PhoneNo)
                .NotEmpty();
        }
    }
}
