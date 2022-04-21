using Microsoft.AspNetCore.Http;

namespace GreenUp.Web.Mvc.Models.Associations
{
    public class UpdateAssociationViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public IFormFile NewLogo { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Place { get; set; }
    }
}
