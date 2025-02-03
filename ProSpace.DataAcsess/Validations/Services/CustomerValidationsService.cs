using FluentValidation;
using ProSpace.Domain.Interfaces.Validations;
using ProSpace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSpace.Infrastructure.Validations.Services
{
    public class CustomerValidationsService : IValidationProvider<CustomerModel>
    {
        /// <summary>
        /// Customer validator
        /// </summary>
        private readonly IValidator<CustomerModel> _validator;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="validator"></param>
        public CustomerValidationsService(IValidator<CustomerModel> validator)
        {
           _validator = validator;
        }

        /// <inheritdoc/>
        public async Task ValidateAsync(CustomerModel customer)
        {
            var validate = await _validator.ValidateAsync(customer ?? throw new ArgumentNullException(nameof(customer)));

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
