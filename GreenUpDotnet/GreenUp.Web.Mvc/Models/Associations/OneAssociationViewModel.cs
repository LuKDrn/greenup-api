using GreenUp.Web.Mvc.Models.Missions;
using System;
using System.Collections.Generic;

namespace GreenUp.Web.Mvc.Models.Associations
{
    public class OneAssociationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Siren { get; set; }
        public string Logo { get; set; }
        public ICollection<OneMissionViewModel> Missions { get; set; } = new List<OneMissionViewModel>();
    }
}
