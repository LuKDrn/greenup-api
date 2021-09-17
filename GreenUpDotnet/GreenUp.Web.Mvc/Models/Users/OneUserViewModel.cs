using GreenUp.Core.Business.Adresses.Models;
using GreenUp.Core.Business.Missions.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GreenUp.Web.Mvc.Models.Users
{
    public class OneUserViewModel
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public IFormFile NewPhoto { get; set; }
        public int Points { get; set; }
        public string Role { get; set; }
        public Adress Adress { get; set; }
        public ICollection<Mission> Missions = new List<Mission>();
    }
}
