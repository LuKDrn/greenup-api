using Abp.Domain.Entities;
using GreenUp.Core.Business.Locations.Models;
using GreenUp.Core.Business.Missions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Url]
        public string Photo { get; set; }
        public int Points { get; set; }
        public Role Role { get; set; }
        public Location Adress { get; set; }
        public ICollection<Mission> Missions { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
