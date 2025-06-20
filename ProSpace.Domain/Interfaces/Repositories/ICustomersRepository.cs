using ProSpace.Domain.Models;

namespace ProSpace.Domain.Interfaces.Repositories
{
    public interface ICustomersRepository : IBasicCRUD<CustomerModel, Guid>
    {
        /// <summary>
        /// Returns the customer by code 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<CustomerModel?> GetByEmailAsync(string code);
    }
}
