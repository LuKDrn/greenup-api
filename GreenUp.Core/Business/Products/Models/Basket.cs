using Abp.Domain.Entities;
using GreenUp.Core.Business.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Products.Models
{
    public class Basket : Entity
    {
        public double Amount { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }
        public ICollection<BasketProducts> Products { get; set; }
    }
}
