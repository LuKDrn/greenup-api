using Abp.Domain.Entities;
using GreenUp.Core.Business.Locations.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenUp.Core.Business.Associations.Models
{
    public class Association : Entity<Guid>
    {
        [Required]
        public string Name { get; set; }
        public int Siren { get; set; }
        public Role Role { get; set; }
        public ICollection<Location> Adresses { get; set; } = new List<Location>();
        public ICollection<Mission> Missions { get; set; } = new List<Mission>();
    }
}
