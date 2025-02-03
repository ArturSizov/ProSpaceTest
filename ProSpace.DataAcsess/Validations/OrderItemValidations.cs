using FluentValidation;
using ProSpace.Domain.Models;

namespace ProSpace.Infrastructure.Validations
{
    public class OrderItemValidations : AbstractValidator<OrderItemModel>
    {
        public OrderItemValidations()
        {
            RuleFor(x => x.OrderId)
               .NotEmpty();

            RuleFor(x => x.ItemId)
               .NotEmpty();

            RuleFor(x => x.ItemsCount)
               .NotEmpty();

            RuleFor(x => x.ItemPrice)
               .NotEmpty();
        }
    }
}
