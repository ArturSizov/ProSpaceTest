namespace ProSpace.Domain.Models
{
    /// <summary>
    /// Product/Item
    /// </summary>
    public class ItemModel
    {
        public Guid Id { get; }
        public string Code { get; } = null!;
        public string Name { get; } = null!;
        public decimal Price { get; }
        public string? Category { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="category"></param>
        public ItemModel(Guid id, string code, string name, decimal price, string? category)
        {
            Id = id;
            Code = code;
            Name = name;
            Price = price;
            Category = category;
        }

        /// <summary>
        /// Cteate item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public static ItemModel Create(Guid id, string code, string name, decimal price, string? category) 
            => new(id, code, name, price, category);
    }
}
