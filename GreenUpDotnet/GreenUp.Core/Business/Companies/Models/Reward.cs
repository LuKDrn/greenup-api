using Abp.Domain.Entities;
using GreenUp.Core.Business.Users.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Companies.Models
{
    public class Reward : Entity
    {
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }
        public int ImageId { get; set; }
        [Required]
        public string Name { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}