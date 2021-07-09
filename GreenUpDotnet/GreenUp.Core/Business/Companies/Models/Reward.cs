using Abp.Domain.Entities;
using GreenUp.Core.Business.Images.Models;
using GreenUp.Core.Business.Users.Models;
using System.ComponentModel.DataAnnotations;

namespace GreenUp.Core.Business.Companies.Models
{
    public class Reward : Entity
    {
        public Company Company { get; set; }
        public Image Image { get; set; }
        [Required]
        public string Name { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; }
        public User User { get; set; }
    }
}