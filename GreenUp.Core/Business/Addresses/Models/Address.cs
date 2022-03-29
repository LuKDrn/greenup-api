using Abp.Domain.Entities;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Orders.Models;
using GreenUp.Core.Business.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Addresses.Models
{
    public class Address : Entity
    {
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Mission> Missions { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string City { get; set; }

        public int ZipCode { get; set; }
    }
}