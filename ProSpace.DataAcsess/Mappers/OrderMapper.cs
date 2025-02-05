using ProSpace.Infrastructure.Entites.Supply;
using ProSpace.Domain.Models;

namespace ProSpace.Infrastructure.Mappers
{
    /// <summary>
    /// Order mapper
    /// </summary>
    public static class OrderMapper
    {
        public static OrderEntity ToEntity(this OrderModel model) => new()
        {
            Id = model.Id,
            CustomerId = model.CustomerId,
            OrderDate = model.OrderDate,
            ShipmentDate = model.ShipmentDate,
            OrderNumber = model.OrderNumber,
            Status = model.Status,
        };

        public static OrderModel ToModel(this OrderEntity entity)
                      => new(entity.Id, entity.CustomerId, entity.OrderDate, entity.ShipmentDate, entity.OrderNumber, entity.Status);
    }
}
