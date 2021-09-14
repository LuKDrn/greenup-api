using System.ComponentModel.DataAnnotations;

namespace GreenUp.Web.Mvc.Models.Users
{
    public class LoginUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
