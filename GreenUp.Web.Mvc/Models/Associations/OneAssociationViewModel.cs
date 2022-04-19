using GreenUp.Web.Mvc.Models.Adresses;
using GreenUp.Web.Mvc.Models.Missions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GreenUp.Web.Mvc.Models.Associations
{
    public class OneAssociationViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public string RnaNumber { get; set; }
        public string Logo { get; set; }
        public IFormFile NewLogo { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
        public string WebsiteUrl { get; set; }
        public ICollection<OneAdressViewModel> Addresses { get; set; } = new List<OneAdressViewModel>();
        public ICollection<OneMissionViewModel> Missions { get; set; } = new List<OneMissionViewModel>();
    }
}
