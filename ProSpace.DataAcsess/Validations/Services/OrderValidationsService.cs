using FluentValidation;
using ProSpace.Domain.Interfaces.Validations;
using ProSpace.Domain.Models;

namespace ProSpace.Infrastructure.Validations.Services
{
    public class OrderValidationsService : IValidationProvider<OrderModel>
    {
        /// <summary>
        /// Order validator
        /// </summary>
        private readonly IValidator<OrderModel> _validator;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="validator"></param>
        public OrderValidationsService(IValidator<OrderModel> validator)
        {
            _validator = validator;
        }

        /// <inheritdoc/>
        public async Task ValidateAsync(OrderModel order)
        {
            var validate = await _validator.ValidateAsync(order ?? throw new ArgumentNullException(nameof(order)));

            if (!validate.IsValid)
            {
                string? errors = string.Empty;

                foreach (var error in validate.Errors)
                    errors = $"{errors}\n" + error;

                throw new Exception(errors);
            }
        }
    }
}
