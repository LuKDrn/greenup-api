using Abp.Domain.Entities;
using GreenUp.Core.Business.Adresses.Models;
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
        public string Siren { get; set; }
        public string Logo { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        [ForeignKey("AdressId")]
        public Adress Adress { get; set; }
        public int AdressId { get; set; }
        public ICollection<Reward> Rewards { get; set; } = new List<Reward>();
    }
}
