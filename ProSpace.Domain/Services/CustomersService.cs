using ProSpace.Domain.Interfaces.Repositories;
using ProSpace.Domain.Interfaces.Services;
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
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public CustomersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public Task<bool> CreateAsync(CustomerModel customer, CancellationToken cancellationToken = default)
            => _unitOfWork.CustomersRepository.CreateAsync(customer, cancellationToken);

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.CustomersRepository.DeleteAsync(id, cancellationToken);

        /// <inheritdoc/>
        public Task<CustomerModel[]?> GetByFilterAsync(string code, string name, decimal price, string category, CancellationToken cancellationToken = default)
            => _unitOfWork.CustomersRepository.GetByFilterAsync(code, name, price, category);

        /// <inheritdoc/>
        public Task<CustomerModel[]?> GetByPageAsync(int page, int pasgeSize)
            => _unitOfWork.CustomersRepository.GetByPageAsync(page, pasgeSize);

        /// <inheritdoc/>
        public Task<CustomerModel[]?> ReadAllAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.CustomersRepository.ReadAllAsync(cancellationToken);

        /// <inheritdoc/>
        public Task<CustomerModel?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.CustomersRepository.ReadAsync(id, cancellationToken);

        /// <inheritdoc/>
        public Task<CustomerModel?> UpdateAsync(CustomerModel entity, CancellationToken cancellationToken = default)
            => _unitOfWork.CustomersRepository.UpdateAsync(entity, cancellationToken);
    }
}
