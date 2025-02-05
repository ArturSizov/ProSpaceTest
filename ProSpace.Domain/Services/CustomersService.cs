using ProSpace.Domain.Interfaces.Repositories;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Interfaces.Validations;
using ProSpace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSpace.Domain.Services
{
    public class CustomersService : ICustomersService
    {
        /// <summary>
        /// Validation service
        /// </summary>
        private readonly IValidationProvider<CustomerModel> _validation;

        /// <summary>
        /// Unit of Work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public CustomersService(IValidationProvider<CustomerModel> validation, IUnitOfWork unitOfWork)
        {
            _validation = validation;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateAsync(CustomerModel customer, CancellationToken cancellationToken = default)
        {
            if (!await _validation.ValidateAsync(customer))
                return false;

            return await _unitOfWork.CustomersRepository.CreateAsync(customer, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.CustomersRepository.DeleteAsync(id, cancellationToken);

        public Task<CustomerModel?> GetByCodeAsync(string code)
            => _unitOfWork.CustomersRepository.GetByCodeAsync(code);


        /// <inheritdoc/>
        public Task<CustomerModel[]?> ReadAllAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.CustomersRepository.ReadAllAsync(cancellationToken);

        /// <inheritdoc/>
        public Task<CustomerModel?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.CustomersRepository.ReadAsync(id, cancellationToken);

        /// <inheritdoc/>
        public async Task<CustomerModel?> UpdateAsync(CustomerModel customer, CancellationToken cancellationToken = default)
        {
            if (!await _validation.ValidateAsync(customer))
                return null;

            return await _unitOfWork.CustomersRepository.UpdateAsync(customer, cancellationToken);
        }
    }
}
