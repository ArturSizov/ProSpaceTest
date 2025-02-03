using FluentValidation;
using ProSpace.Domain.Models;

namespace ProSpace.Infrastructure.Validations
{
    public class OrderValidations : AbstractValidator<OrderModel>
    {
        public OrderValidations()
        {
            RuleFor(x => x.CustomerId)
               .NotEmpty();

            RuleFor(x => x.OrderDate)
               .NotEmpty();
        }
    }
}
