using ProSpace.Domain.Models;

namespace ProSpace.Domain.Interfaces.Repositories
{
    public interface ICustomersRepository : IBasicCRUD<CustomerModel, Guid>
    {
    }
}
