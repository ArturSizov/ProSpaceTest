using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSpace.DataAcsess.Entites.Users;

namespace ProSpace.DataAcsess.Configurations
{
    /// <summary>
    /// User configuration
    /// </summary>
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(ur => ur.UserRoles)
                   .WithOne(u => u.User)
                   .HasForeignKey(u => u.UserId)
                   .IsRequired();

            builder.HasOne(x => x.Customer)
                   .WithOne(u => u.AppUser)
                   .HasForeignKey<AppUser>(c => c.CustomerId)
                   .IsRequired(false);
        }
    }
}
