using ProSpace.Domain.Interfaces.Repositories;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Models;

namespace ProSpace.Domain.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ItemsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public Task<bool> CreateAsync(ItemModel item, CancellationToken cancellationToken = default)
            => _unitOfWork.ItemsRepository.CreateAsync(item, cancellationToken);

        /// <inheritdoc/>
        public Task<ItemModel?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.ItemsRepository.ReadAsync(id, cancellationToken);

        /// <inheritdoc/>
        public Task<ItemModel?> UpdateAsync(ItemModel entity, CancellationToken cancellationToken = default)
            => _unitOfWork.ItemsRepository.UpdateAsync(entity, cancellationToken);

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.ItemsRepository.DeleteAsync(id, cancellationToken);

        /// <inheritdoc/>
        public Task<ItemModel[]?> ReadAllAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.ItemsRepository.ReadAllAsync(cancellationToken);
    }
}
