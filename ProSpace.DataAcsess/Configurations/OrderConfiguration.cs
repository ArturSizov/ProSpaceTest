using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProSpace.DataAcsess.Entites.Supply;

namespace ProSpace.DataAcsess.Configurations
{
    /// <summary>
    /// Order configuration
    /// </summary>
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.HasMany(o => o.OrderItems)
                   .WithOne(o => o.Order)
                   .HasForeignKey(o => o.OrderId);

            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId);
        }
    }
}
