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
        [Required]
        public string RnaNumber { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        [Url]
        public string WebsiteUrl { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string City { get; set; }
        public int ZipCode { get; set; }
    }
}
