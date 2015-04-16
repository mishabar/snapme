using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;

namespace SNAPME.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string ImageUrl { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("smn:image_url", this.ImageUrl ?? "/Content/Images/default-avatar.png"));
            userIdentity.AddClaim(new Claim("urn:iisnap:name", this.Claims.Find(c => c.Type == "urn:iisnap:name").Value));
            if (this.Roles.Contains("Seller"))
            {
                userIdentity.AddClaim(new Claim("urn:iisnap:seller_id", this.Claims.Find(c => c.Type == "urn:iisnap:seller_id").Value));
            }
            // Add custom user claims here
            return userIdentity;
        }
    }
}