using ProSpace.Domain.Models;

namespace ProSpace.Domain.Interfaces.Services
{
    public interface IOtderItemsService : IBasicCRUD<OrderItemModel, Guid>
    {
        /// <summary>
        /// Returns a list of order items by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OrderItemModel[]?> GetOrderItemsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    }
}
