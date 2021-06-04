using Abp.Domain.Entities;
using GreenUp.Core.Business.Associations.Models;
using GreenUp.Core.Business.Companies.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Users.Models;
using System.ComponentModel.DataAnnotations;

namespace GreenUp.Core.Business.Locations.Models
{
    public class Location : Entity
    {
        public Association Association { get; set; }
        public Company Company { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public Mission Mission { get; set; }
        public int? MissionId { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string City { get; set; }
        public int ZipCode { get; set; }
    }
}
