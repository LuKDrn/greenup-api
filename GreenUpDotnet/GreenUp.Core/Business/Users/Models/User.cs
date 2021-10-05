using Abp.Domain.Entities;
using GreenUp.Core.Business.Adresses.Models;
using GreenUp.Core.Business.Inscriptions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Users.Models
{
    public class User : Entity<Guid>
    {
        public DateTime CreationTime { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Photo { get; set; }
        public int Points { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("AdressId")]
        public Adress Adress { get; set; }
        public int AdressId { get; set; }
        public ICollection<MissionUser> Missions { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
    }
}
