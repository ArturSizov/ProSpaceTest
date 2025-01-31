namespace ProSpace.Domain.Models
{
    /// <summary>
    /// Customer model
    /// </summary>
    public class CustomerModel
    {
        public Guid Id { get; }
        public string Name { get; } = null!;
        public string Code { get; } = null!;
        public string? Address { get; }
        public decimal? Discount { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="address"></param>
        /// <param name="discount"></param>
        public CustomerModel(Guid id, string name, string code, string? address, decimal? discount)
        {
            Id = id;
            Name = name;
            Code = code;
            Address = address;
            Discount = discount;
        }

        /// <summary>
        /// Create new Customer model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="address"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public static CustomerModel Create(Guid id, string name, string code, string? address, decimal? discount) 
            => new(id, name, code, address, discount); 
    }
}
