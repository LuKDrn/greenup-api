using System.ComponentModel.DataAnnotations;

namespace GreenUp.Web.Mvc.Models.Associations
{
    public class LoginAssociationViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Siren { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
