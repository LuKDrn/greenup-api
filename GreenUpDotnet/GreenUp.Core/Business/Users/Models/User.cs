using Abp.Domain.Entities;
using GreenUp.Core.Business.Locations.Models;
using GreenUp.Core.Business.Missions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Users.Models
{
    public class User : Entity<Guid>
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [ForeignKey("PhotoId")]
        public Image Photo { get; set; }
        public int PhotoId { get; set; }
        public int Points { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("AdressId")]
        public Location Adress { get; set; }
        public int AdressId { get; set; }
        public ICollection<Mission> Missions { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
