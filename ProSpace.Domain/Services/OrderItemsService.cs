using ProSpace.Domain.Interfaces.Repositories;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Interfaces.Validations;
using ProSpace.Domain.Models;

namespace ProSpace.Domain.Services
{
    public class OrderItemsService : IOtderItemsService
    {
        /// <summary>
        /// Validation service
        /// </summary>
        private readonly IValidationProvider<OrderItemModel> _validation;

        /// <summary>
        /// Unit of Work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public OrderItemsService(IValidationProvider<OrderItemModel> validation, IUnitOfWork unitOfWork)
        {
            _validation = validation;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateAsync(OrderItemModel orderItem, CancellationToken cancellationToken = default)
        {
            await _validation.ValidateAsync(orderItem);
            return await _unitOfWork.OrderItemsRepository.CreateAsync(orderItem, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<OrderItemModel?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.OrderItemsRepository.ReadAsync(id, cancellationToken);

        /// <inheritdoc/>
        public Task<OrderItemModel?> UpdateAsync(OrderItemModel entity, CancellationToken cancellationToken = default)
            => _unitOfWork.OrderItemsRepository.UpdateAsync(entity, cancellationToken);

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.OrderItemsRepository.DeleteAsync(id, cancellationToken);

        /// <inheritdoc/>
        public Task<OrderItemModel[]?> ReadAllAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.OrderItemsRepository.ReadAllAsync(cancellationToken);
    }
}
