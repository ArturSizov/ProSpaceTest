using FluentValidation;
using ProSpace.Domain.Interfaces.Validations;
using ProSpace.Domain.Models;

namespace ProSpace.Infrastructure.Validations.Services
{
    public class ItemValidationsService : IValidationProvider<ItemModel>
    {
        /// <summary>
        /// Item validator
        /// </summary>
        private readonly IValidator<ItemModel> _validator;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="validator"></param>
        public ItemValidationsService(IValidator<ItemModel> validator)
        {
            _validator = validator;
        }

        /// <inheritdoc/>
        public async Task<bool> ValidateAsync(ItemModel item)
        {
            var validate = await _validator.ValidateAsync(item ?? throw new ArgumentNullException(nameof(item)));

            if (!validate.IsValid)
            {
                string? errors = string.Empty;

                foreach (var error in validate.Errors)
                    errors = $"{errors}\n" + error;

                throw new Exception(errors);
            }

            return true;
        }
    }
}
