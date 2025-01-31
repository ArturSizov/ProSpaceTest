using Microsoft.AspNetCore.Identity;

namespace ProSpace.DataAcsess.Entites.Users
{
    /// <summary>
    /// App user role
    /// </summary>
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public AppUser User { get; set; } = null!;
        public AppRole Role { get; set; } = null!;
    }
}
