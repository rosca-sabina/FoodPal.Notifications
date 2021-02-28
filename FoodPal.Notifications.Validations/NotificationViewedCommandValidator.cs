using FluentValidation;
using FoodPal.Notifications.Application.Commands;

namespace FoodPal.Notifications.Validations
{
    public class NotificationViewedCommandValidator: InternalValidator<NotificationViewedCommand>
    {
        public NotificationViewedCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Status)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
