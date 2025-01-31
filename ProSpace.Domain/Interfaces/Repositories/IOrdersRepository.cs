using ProSpace.Domain.Models;

namespace ProSpace.Domain.Interfaces.Repositories
{
    public interface IOrdersRepository : IBasicCRUD<OrderModel, Guid>
    {
    }
}
