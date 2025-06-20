using ProSpace.Domain.Models;

namespace ProSpace.Domain.Interfaces.Services
{
    public interface ICustomersService : IBasicCRUD<CustomerModel, Guid>
    {
        /// <summary>
        /// Returns the customer by code 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<CustomerModel?> GetByEmailAsync(string email);
    }
}
