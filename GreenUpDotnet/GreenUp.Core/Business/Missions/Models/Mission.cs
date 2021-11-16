using Abp.Domain.Entities;
using GreenUp.Core.Business.Adresses.Models;
using GreenUp.Core.Business.Associations.Models;
using GreenUp.Core.Business.Inscriptions.Models;
using GreenUp.Core.Business.Tags.Models;
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
        public Adress Location { get; set; }
        public int LocationId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("AssociationId")]
        public Association Association { get; set; }
        public Guid AssociationId { get; set; }
        public int RewardValue { get; set; }
        public bool IsInGroup { get; set; }
        public int? NumberPlaces { get; set; }
        public bool Available { get; set; }
        public ICollection<MissionUser> Users { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
