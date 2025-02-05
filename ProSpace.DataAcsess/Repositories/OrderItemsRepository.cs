﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProSpace.Infrastructure.Entites.Supply;
using ProSpace.Infrastructure.Mappers;
using ProSpace.Domain.Interfaces.Repositories;
using ProSpace.Domain.Models;

namespace ProSpace.Infrastructure.Repositories
{
    /// <summary>
    /// Order items repository
    /// </summary>
    public class OrderItemsRepository : IOrderItemsRepository
    {
        /// <summary>
        /// Logger 
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Db context
        /// </summary>
        private readonly ProSpaceDbContext _dbContext;

        public OrderItemsRepository(ILogger<OrderItemsRepository> logger, ProSpaceDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateAsync(OrderItemModel entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var orderItem = entity.ToEntity();

                _ = await _dbContext.OrderItems.AddAsync(orderItem, cancellationToken);

                var saved = await _dbContext.SaveChangesAsync(cancellationToken);

                return saved > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create an order item");
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var found = await _dbContext.OrderItems.FindAsync([id], cancellationToken);
                if (found == null)
                {
                    _logger.LogWarning("Cannot find order item with id = {id}", id);
                    return false;
                }

                _ = _dbContext.OrderItems.Remove(found);

                var saved = await _dbContext.SaveChangesAsync(cancellationToken);

                return saved > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot delete order item");
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<OrderItemModel[]?> ReadAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var items = await _dbContext.OrderItems.ToArrayAsync();
                return items.Select(x => x.ToModel()).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot read all items");
                return null;
            }
        }

        /// <inheritdoc/>
        public async Task<OrderItemModel?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var found = await _dbContext.OrderItems.FindAsync([id], cancellationToken);
                if (found == null)
                {
                    _logger.LogWarning("Cannot find order item with id = {id}", id);
                    return null;
                }

                return found.ToModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot find order item");
                return null;
            }
        }

        /// <inheritdoc/>
        public async Task<OrderItemModel?> UpdateAsync(OrderItemModel entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var found = await _dbContext.OrderItems.FindAsync([entity.Id], cancellationToken);
                if (found == null)
                {
                    _logger.LogWarning("Cannot find order item with id = {id}", entity.Id);
                    return null;
                }

                found.OrderId = entity.OrderId;
                found.ItemId = entity.ItemId;
                found.ItemsCount = entity.ItemsCount;
                found.ItemPrice = entity.ItemPrice;

                _ = _dbContext.OrderItems.Update(found);

                var saved = await _dbContext.SaveChangesAsync(cancellationToken);
                if (saved > 0)
                    return found.ToModel();

                _logger.LogError("Cannot save the updated data");
                return null;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot update item");
                return null;
            }
        }

        public async Task AddItemAsync(Guid itemId, OrderItemEntity orderItemEntity)
        {
            var orderItem = await _dbContext.OrderItems.FirstOrDefaultAsync(o => o.ItemId == itemId);
        }
    }
}
