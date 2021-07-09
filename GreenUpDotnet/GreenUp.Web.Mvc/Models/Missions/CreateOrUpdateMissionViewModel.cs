using GreenUp.Core.Business.Associations.Models;
using GreenUp.Core.Business.Locations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Models.Missions
{
    public class CreateOrUpdateMissionViewModel
    {
        public int? Id { get; set; }
        public Association Association { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public Location Place { get; set; }
        public DateTime Date { get; set; }
        public int RewardValue { get; set; }
        public bool IsInGroup { get; set; }
        public int Availability { get; set; }
        public bool Available { get; set; }
    }
}
