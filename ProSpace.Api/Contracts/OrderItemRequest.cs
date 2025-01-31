using ProSpace.DataAcsess.Entites.Supply;

namespace ProSpace.Api.Contracts
{
    /// <summary>
    /// Order item request
    /// </summary>
    public class OrderItemRequest
    {
        public Guid OrderId { get; set; }
        public Guid ItemId { get; set; }
        public int ItemsCount { get; set; }
        public decimal ItemPrice { get; set; }
    }
}
