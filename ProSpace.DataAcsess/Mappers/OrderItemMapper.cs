using ProSpace.Infrastructure.Entites.Supply;
using ProSpace.Domain.Models;

namespace ProSpace.Infrastructure.Mappers
{
    /// <summary>
    /// Order item mapper
    /// </summary>
    public static class OrderItemMapper
    {
        public static OrderItemEntity ToEntity(this OrderItemModel model) => new()
        {
           OrderId = model.OrderId,
           ItemId = model.ItemId,
           ItemsCount = model.ItemsCount,
           ItemPrice = model.ItemPrice
        };

        public static OrderItemModel ToModel(this OrderItemEntity entity)
                      => OrderItemModel.Create(entity.Id, entity.OrderId, entity.ItemId, entity.ItemsCount, entity.ItemPrice);
    }
}
