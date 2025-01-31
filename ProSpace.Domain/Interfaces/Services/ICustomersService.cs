using ProSpace.Domain.Models;

namespace ProSpace.Domain.Interfaces.Services
{
    public interface ICustomersService : IBasicCRUD<CustomerModel, Guid>
    {
    }
}
