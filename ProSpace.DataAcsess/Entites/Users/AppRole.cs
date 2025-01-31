using Microsoft.AspNetCore.Identity;

namespace ProSpace.DataAcsess.Entites.Users
{
    /// <summary>
    /// App role
    /// </summary>
    public class AppRole : IdentityRole<Guid>
    {
        /// <summary>
        /// User roles collection
        /// </summary>
        public ICollection<AppUserRole> UserRoles { get; set; } = [];
    }
}
