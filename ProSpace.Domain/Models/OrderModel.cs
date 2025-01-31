namespace ProSpace.Domain.Models
{
    /// <summary>
    /// Order model
    /// </summary>
    public class OrderModel
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public DateOnly OrderDate { get; }
        public DateOnly? ShipmentDate { get; }
        public int? OrderNumber {  get; }
        public string? Status { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerId"></param>
        /// <param name="orderDate"></param>
        /// <param name="shipmentDate"></param>
        /// <param name="orderNumber"></param>
        /// <param name="status"></param>
        public OrderModel(Guid id, Guid customerId, DateOnly orderDate, DateOnly? shipmentDate, int? orderNumber, string? status)
        {
            Id = id;
            CustomerId = customerId;
            OrderDate = orderDate;
            ShipmentDate = shipmentDate;
            OrderNumber = orderNumber;
            Status = status;
        }

        /// <summary>
        /// Create new Order model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerId"></param>
        /// <param name="orderDate"></param>
        /// <param name="shipmentDate"></param>
        /// <param name="orderNumber"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static OrderModel Create(Guid id, Guid customerId, DateOnly orderDate, DateOnly? shipmentDate, int? orderNumber, string? status)
            => new(id, customerId, orderDate, shipmentDate, orderNumber, status);
    }
}
