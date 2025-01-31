using ProSpace.Domain.Models;

namespace ProSpace.Domain.Interfaces.Repositories
{
    public interface IOrderItemsRepository : IBasicCRUD<OrderItemModel, Guid>
    {
    }
}
