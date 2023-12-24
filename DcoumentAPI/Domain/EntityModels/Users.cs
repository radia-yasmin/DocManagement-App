using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DcoumentAPI.Domain.EntityModels
{
    public class Users: IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
