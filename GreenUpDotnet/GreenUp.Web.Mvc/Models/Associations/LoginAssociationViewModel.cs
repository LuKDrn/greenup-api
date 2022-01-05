using System.ComponentModel.DataAnnotations;

namespace GreenUp.Web.Mvc.Models.Associations
{
    public class LoginAssociationViewModel
    {
        [Required]
        public string RnaNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
