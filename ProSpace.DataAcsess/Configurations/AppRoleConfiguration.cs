using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSpace.DataAcsess.Entites.Users;

namespace ProSpace.DataAcsess.Configurations
{
    /// <summary>
    /// App role configuration
    /// </summary>
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasMany(ur => ur.UserRoles)
                   .WithOne(u => u.Role)
                   .HasForeignKey(u => u.RoleId)
                   .IsRequired();
        }
    }
}
