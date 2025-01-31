using ProSpace.DataAcsess.Entites.Supply;
using ProSpace.Domain.Models;

namespace ProSpace.DataAcsess.Mappers
{
    /// <summary>
    /// Item mapper
    /// </summary>
    public static class ItemMapper
    {
        public static ItemEntity ToEntity(this ItemModel model) => new()
        {
            Name = model.Name,
            Code = model.Code,
            Price = model.Price,
            Category = model.Category
        };

        public static ItemModel ToModel(this ItemEntity entity)
            => new(entity.Id, entity.Code, entity.Name, entity.Price, entity.Category);
    }
}
