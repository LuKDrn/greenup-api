using Abp.Domain.Entities;
using GreenUp.Core.Business.Addresses.Models;
using GreenUp.Core.Business.Products.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Orders.Models
{
    public class Order : Entity<Guid>
    {
        public double Amount { get; set; }
        public Guid OrderNumber { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("StepId")]
        public Step Step { get; set; }
        [Required]
        public int StepId { get; set; }
        [ForeignKey("BasketId")]
        public Basket Basket { get; set; }
        [Required]
        public int BasketId { get; set; }
        [ForeignKey("DeliveryId")]
        public Address Delivery { get; set; }
        [Required]
        public int DeliveryId { get; set; }
        [ForeignKey("BillingId")]
        public Address Billing { get; set; }
        [Required]
        public int BillingId { get; set; }
    }
}
