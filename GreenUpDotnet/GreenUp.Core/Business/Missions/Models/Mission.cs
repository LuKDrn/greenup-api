using Abp.Domain.Entities;
using GreenUp.Core.Business.Associations.Models;
using GreenUp.Core.Business.Images.Models;
using GreenUp.Core.Business.Locations.Models;
using GreenUp.Core.Business.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Missions.Models
{
    public class Mission : Entity
    {
        public string Titre { get; set; }
        public string Description { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("AssociationId")]
        public Association Association { get; set; }
        public Guid AssociationId { get; set; }
        public int RewardValue { get; set; }
        public bool IsInGroup { get; set; }
        public int? Places { get; set; }
        public bool Available { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
