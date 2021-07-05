using GreenUp.Core.Business.Locations.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Users.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GreenUp.Web.Mvc.Models.Auth
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public int Points { get; set; }
        public Role Role { get; set; }
        public Location Adress { get; set; }
        public ICollection<Mission> Missions { get; set; } = new List<Mission>();
    }
}
