using Abp.Domain.Entities;
using GreenUp.Core.Business.Locations.Models;
using GreenUp.Core.Business.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenUp.Core.Business.Companies.Models
{
    public class Company : Entity<Guid>
    {
        [Required]
        public string Name { get; set; }
        public int Siren { get; set; }
        public Role Role { get; set; }
        public ICollection<Location> Adresses { get; set; } = new List<Location>();
        public ICollection<Reward> Rewards { get; set; } = new List<Reward>();
    }
}
