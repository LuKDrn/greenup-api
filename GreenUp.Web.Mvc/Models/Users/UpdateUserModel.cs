using Microsoft.AspNetCore.Http;

namespace GreenUp.Web.Mvc.Models.Users
{
    public class UpdateUserModel
    {
        public string Id { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public IFormFile NewPhoto { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Place { get; set; }
    }
}
