using Abp.Domain.Entities;
using GreenUp.Core.Business.Images.Models;
using GreenUp.Core.Business.Locations.Models;
using GreenUp.Core.Business.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Companies.Models
{
    public class Company : Entity<Guid>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public int Siren { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        [ForeignKey("LogoId")]
        public Image Logo { get; set; }
        public int LogoId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        [ForeignKey("AdressId")]
        public Location Adress { get; set; }
        public int AdressId { get; set; }
        public ICollection<Reward> Rewards { get; set; } = new List<Reward>();
    }
}
