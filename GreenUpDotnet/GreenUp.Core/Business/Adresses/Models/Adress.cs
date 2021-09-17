using Abp.Domain.Entities;
using GreenUp.Core.Business.Associations.Models;
using GreenUp.Core.Business.Companies.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Users.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenUp.Core.Business.Adresses.Models
{
    public class Adress : Entity
    {
        public ICollection<Association> Associations { get; set; }
        public ICollection<Company> Companies { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Mission> Missions { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string City { get; set; }

        public int ZipCode { get; set; }
    }
}