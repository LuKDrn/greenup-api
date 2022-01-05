using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Products.Models
{
    public class BasketProducts
    {
        public DateTime ProductAdded { get; set; }
        public int ProductQuantity { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("BasketId")]
        public Basket Basket { get; set; }
        public int BasketId { get; set; }
    }
}
