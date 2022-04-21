using GreenUp.Core.Business.Addresses.Models;
using GreenUp.Core.Business.Orders.Models;
using GreenUp.Core.Business.Participations.Models;
using GreenUp.Core.Business.Products.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GreenUp.Web.Mvc.Models.Users
{
    public class OneUserViewModel
    {
        public string Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public IFormFile NewPhoto { get; set; }
        public int Points { get; set; }
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
        public Basket Basket { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Place { get; set; }
        public ICollection<Address> Adresses { get; set; } = new List<Address>();
        public ICollection<Participation> Participations { get; set; } = new List<Participation>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
