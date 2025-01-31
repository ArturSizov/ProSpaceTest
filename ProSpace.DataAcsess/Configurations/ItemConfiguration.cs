using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProSpace.DataAcsess.Entites.Supply;

namespace ProSpace.DataAcsess.Configurations
{
    /// <summary>
    /// Item configuration
    /// </summary>
    public class ItemConfiguration : IEntityTypeConfiguration<ItemEntity>
    {
        public void Configure(EntityTypeBuilder<ItemEntity> builder)
        {
            builder.HasKey(o => o.Id);
        }
    }

}
