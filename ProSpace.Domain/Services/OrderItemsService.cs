using ProSpace.Domain.Interfaces.Repositories;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Models;

namespace ProSpace.Domain.Services
{
    public class OrderItemsService : IOtderItemsService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public OrderItemsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public Task<bool> CreateAsync(OrderItemModel item, CancellationToken cancellationToken = default)
            => _unitOfWork.OrderItemsRepository.CreateAsync(item, cancellationToken);

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

        /// <inheritdoc/>
        public Task<OrderItemModel[]?> GetByFilterAsync(string code, string name, decimal price, string category, CancellationToken cancellationToken = default)
           => _unitOfWork.OrderItemsRepository.GetByFilterAsync(code, name, price, category);

        /// <inheritdoc/>
        public Task<OrderItemModel[]?> GetByPageAsync(int page, int pasgeSize)
          => _unitOfWork.OrderItemsRepository.GetByPageAsync(page, pasgeSize);
    }
}
