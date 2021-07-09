using GreenUp.Core.Business.Locations.Models;
using GreenUp.Core.Business.Users.Models;
using System.ComponentModel.DataAnnotations;

namespace GreenUp.Web.Mvc.Models.Associations
{
    public class SignUpAssociationViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public int Siren { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string City { get; set; }
        public int ZipCode { get; set; }
    }
}
