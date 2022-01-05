using Abp.Domain.Entities;
using GreenUp.Core.Business.Participations.Models;
using GreenUp.Core.Business.Missions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GreenUp.Core.Business.Products.Models;
using GreenUp.Core.Business.Orders.Models;
using GreenUp.Core.Business.Addresses.Models;

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
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public int Points { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
        public bool IsUser { get; set; }
        public bool IsAssociation { get; set; }
        public bool IsCompany { get; set; }
        public bool IsAdmin { get; set; }
        public string RnaNumber { get; set; }
        public string SiretNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public ICollection<Address> Addresses { get; set; }
        //The list of the participations at missions for an user
        public ICollection<Participation> Participations { get; set; }
        //The list of the orders passed by an user
        public ICollection<Order> Orders { get; set; }
        //The list of the product saved as favorite for an user
        public ICollection<Favorite> Favorites { get; set; }
        //The list of the missions as owner for an association
        public ICollection<Mission> Missions { get; set; }
        //The list of the products provide on the store by a company
        public ICollection<Product> Products { get; set; }
    }
}
