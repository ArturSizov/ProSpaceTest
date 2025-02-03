using ProSpace.Domain.Interfaces.Repositories;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Interfaces.Validations;
using ProSpace.Domain.Models;

namespace ProSpace.Domain.Services
{
    public class OrdersService : IOrderService
    {
        /// <summary>
        /// Validation service
        /// </summary>
        private readonly IValidationProvider<OrderModel> _validation;

        /// <summary>
        /// Unit of Work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public OrdersService(IValidationProvider<OrderModel> validation, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateAsync(OrderModel order, CancellationToken cancellationToken = default)
        {
            await _validation.ValidateAsync(order);
            return await _unitOfWork.OrdersRepository.CreateAsync(order, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.OrdersRepository.DeleteAsync(id, cancellationToken);

        /// <inheritdoc/>
        public Task<OrderModel[]?> GetByCustomerCode(string customerCode)
            => _unitOfWork.OrdersRepository.GetByCustomerCode(customerCode);

        /// <inheritdoc/>
        public Task<OrderModel[]?> GetByCustomerId(Guid customerId)
            => _unitOfWork.OrdersRepository.GetByCustomerId(customerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<OrderModel?> GetByOrderNumber(int orderNumber)
            => _unitOfWork.OrdersRepository.GetByOrderNumber(orderNumber);

        /// <inheritdoc/>
        public Task<OrderModel[]?> ReadAllAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.OrdersRepository.ReadAllAsync(cancellationToken);

        /// <inheritdoc/>
        public Task<OrderModel?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.OrdersRepository.ReadAsync(id, cancellationToken);

        /// <inheritdoc/>
        public Task<OrderModel?> UpdateAsync(OrderModel entity, CancellationToken cancellationToken = default)
            => _unitOfWork.OrdersRepository.UpdateAsync(entity, cancellationToken);
    }
}
