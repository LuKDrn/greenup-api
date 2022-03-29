using GreenUp.Core.Business.Users.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Products.Models
{
    public class Favorite
    {
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
