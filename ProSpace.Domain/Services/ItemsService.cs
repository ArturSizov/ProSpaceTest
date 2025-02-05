using ProSpace.Domain.Interfaces.Repositories;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Interfaces.Validations;
using ProSpace.Domain.Models;

namespace ProSpace.Domain.Services
{
    public class ItemsService : IItemsService
    {
        /// <summary>
        /// Validation service
        /// </summary>
        private readonly IValidationProvider<ItemModel> _validation;

        /// <summary>
        /// Unit of Work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ItemsService(IValidationProvider<ItemModel> validation, IUnitOfWork unitOfWork)
        {
            _validation = validation;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateAsync(ItemModel item, CancellationToken cancellationToken = default)
        {
            if (!await _validation.ValidateAsync(item))
                return false;

            return await _unitOfWork.ItemsRepository.CreateAsync(item, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ItemModel?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.ItemsRepository.ReadAsync(id, cancellationToken);

        /// <inheritdoc/>
        public async Task<ItemModel?> UpdateAsync(ItemModel model, CancellationToken cancellationToken = default)
        {
            if (!await _validation.ValidateAsync(model))
                return null;

            return await _unitOfWork.ItemsRepository.UpdateAsync(model, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            => _unitOfWork.ItemsRepository.DeleteAsync(id, cancellationToken);

        /// <inheritdoc/>
        public Task<ItemModel[]?> ReadAllAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.ItemsRepository.ReadAllAsync(cancellationToken);
    }
}
