using ProSpace.Domain.Interfaces.Repositories;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Models;

namespace ProSpace.Domain.Services
{
    public class OrdersService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public OrdersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public Task<bool> CreateAsync(OrderModel order, CancellationToken cancellationToken = default)
            => _unitOfWork.OrdersRepository.CreateAsync(order, cancellationToken);

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.OrdersRepository.DeleteAsync(id, cancellationToken);

        /// <inheritdoc/>
        public Task<OrderModel[]?> GetByFilterAsync(string code, string name, decimal price, string category, CancellationToken cancellationToken = default)
            => _unitOfWork.OrdersRepository.GetByFilterAsync(code, name, price, category);

        /// <inheritdoc/>
        public Task<OrderModel[]?> GetByPageAsync(int page, int pasgeSize)
            => _unitOfWork.OrdersRepository.GetByPageAsync(page, pasgeSize);

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
