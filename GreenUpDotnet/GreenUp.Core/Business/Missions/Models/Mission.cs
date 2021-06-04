using Abp.Domain.Entities;
using GreenUp.Core.Business.Associations.Models;
using GreenUp.Core.Business.Locations.Models;
using GreenUp.Core.Business.Users.Models;
using System;
using System.Collections.Generic;

namespace GreenUp.Core.Business.Missions.Models
{
    public class Mission : Entity
    {
        public Association Association { get; set; }
        public Location Place { get; set; }
        public DateTime Date { get; set; }
        public int RewardValue { get; set; }
        public bool IsInGroup { get; set; }
        public int Availability { get; set; }
        public bool Available { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
