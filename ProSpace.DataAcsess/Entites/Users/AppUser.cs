using Microsoft.AspNetCore.Identity;
using ProSpace.DataAcsess.Entites.Supply;

namespace ProSpace.DataAcsess.Entites.Users
{
    public class AppUser : IdentityUser<Guid>
    {
        public Guid CustomerId { get; set; }

        public CustomerEntity Customer { get; set; } = null!;

        public ICollection<AppUserRole> UserRoles { get; set; } = [];
    }
}
