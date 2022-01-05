using Abp.Domain.Entities;
using GreenUp.Core.Business.Orders.Models;
using GreenUp.Core.Business.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Products.Models
{
    public class Product : Entity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        [ForeignKey("CompanyId")]
        public User Company { get; set; }
        public Guid CompanyId { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<BasketProducts> BasketProducts { get; set; }
    }
}
