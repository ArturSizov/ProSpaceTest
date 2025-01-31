using ProSpace.Domain.Models;

namespace ProSpace.Domain.Interfaces.Repositories
{
    public interface IItemsRepository : IBasicCRUD<ItemModel, Guid>
    {
    }
}
