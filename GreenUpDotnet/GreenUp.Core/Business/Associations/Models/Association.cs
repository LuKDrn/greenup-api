using Abp.Domain.Entities;
using GreenUp.Core.Business.Adresses.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Associations.Models
{
    public class Association : Entity<Guid>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string Siren { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public string Logo { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Mail { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        [ForeignKey("AdressId")]
        public Adress Adress { get; set; }
        public int AdressId { get; set; }
        public ICollection<Mission> Missions { get; set; } = new List<Mission>();
    }
}
