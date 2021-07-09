using GreenUp.Core.Business.Images.Models;
using GreenUp.Core.Business.Locations.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Users.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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
        public Image Photo { get; set; }
        public IFormFile NewPhoto { get; set; }
        public int Points { get; set; }
        public string Role { get; set; }
        public Location Adress { get; set; }
        public ICollection<Mission> Missions = new List<Mission>();
    }
}
