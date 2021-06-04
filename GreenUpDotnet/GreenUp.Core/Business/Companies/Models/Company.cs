using Abp.Domain.Entities;
using GreenUp.Core.Business.Locations.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenUp.Core.Business.Companies.Models
{
    public class Company : Entity
    {
        [Required]
        public string Name { get; set; }
        public int Siren { get; set; }
        public ICollection<Location> Adresses { get; set; } = new List<Location>();
        public ICollection<Reward> Rewards { get; set; } = new List<Reward>();
    }
}
