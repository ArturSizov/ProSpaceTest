namespace ProSpace.Domain.Models
{
    /// <summary>
    /// Order item model
    /// </summary>
    public class OrderItemModel
    {
        public Guid Id { get;  }
        public Guid OrderId { get;  }
        public Guid ItemId { get; }
        public  int ItemsCount { get; }
        public  decimal ItemPrice { get;  }

        public OrderItemModel(Guid id, Guid orderId, Guid itemId, int itemsCount, decimal itemPrice)
        {
            Id = id;
            OrderId = orderId;
            ItemId = itemId;
            ItemsCount = itemsCount;
            ItemPrice = itemPrice;
        }

        /// <summary>
        /// Create Order item model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderId"></param>
        /// <param name="itemId"></param>
        /// <param name="itemsCount"></param>
        /// <param name="itemPrice"></param>
        /// <returns></returns>
        public static OrderItemModel Create(Guid id, Guid orderId, Guid itemId, int itemsCount, decimal itemPrice) 
            => new (id, orderId, itemId, itemsCount, itemPrice);
    }
}
