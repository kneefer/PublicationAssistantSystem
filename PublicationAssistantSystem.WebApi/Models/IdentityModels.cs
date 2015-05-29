using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PublicationAssistantSystem.WebApi.Models
{
    /// <summary>
    /// Specifies application user and login type.
    /// </summary>
    /// <remarks> http://go.microsoft.com/fwlink/?LinkID=317594 </remarks>
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    /// <summary>
    /// Specifies application database context.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        /// <summary>
        /// Creates database context.
        /// </summary>
        /// <returns> Db context. </returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}