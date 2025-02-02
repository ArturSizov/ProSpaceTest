using ProSpace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSpace.Domain.Interfaces
{
    public interface IBasicCRUD<TItem, TKey> where TItem : class
    {
        /// <summary>
        /// Create item
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(TItem entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Read one item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TItem?> ReadAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TItem?> UpdateAsync(TItem entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete one item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Read all items
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TItem[]?> ReadAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Search by fields
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="category"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
    }
}

