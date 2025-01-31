using ProSpace.Domain.Models;

namespace ProSpace.Domain.Interfaces.Services
{
    public interface IOrderService : IBasicCRUD<OrderModel, Guid>
    {
    }
}
