using GreenUp.Core.Business.Associations.Models;
using GreenUp.Core.Business.Users.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Companies.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace GreenUp.Core.Business.Images.Models
{
    public class Image : Entity
    {
        [Required]
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public ICollection<Association> Association { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Company> Companies { get; set; }
        public ICollection<Mission> Missions { get; set; }
    }
}
