using ProSpace.Domain.Models;

namespace ProSpace.Domain.Interfaces.Services
{
    public interface IOrderService : IBasicCRUD<OrderModel, Guid>
    {
        /// <summary>
        /// Receives orders by customer ID
        /// </summary>
        /// <param name="custonerId"></param>
        /// <returns></returns>
        Task<OrderModel[]?> GetByCustomerId(Guid customerId);

        /// <summary>
        /// Receives orders by customer code
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        Task<OrderModel[]?> GetByCustomerCode(string customerCode);

        /// <summary>
        /// Receives an order by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        Task<OrderModel?> GetByOrderNumber(int orderNumber);
    }
}
