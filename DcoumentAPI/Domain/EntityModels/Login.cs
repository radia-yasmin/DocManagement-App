using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DcoumentAPI.Domain.EntityModels
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
