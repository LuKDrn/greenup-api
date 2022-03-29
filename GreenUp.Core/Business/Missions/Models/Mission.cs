using Abp.Domain.Entities;
using GreenUp.Core.Business.Addresses.Models;
using GreenUp.Core.Business.Participations.Models;
using GreenUp.Core.Business.Tags.Models;
using GreenUp.Core.Business.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Missions.Models
{
    public class Mission : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("LocationId")]
        public Address Location { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public DateTime Creation { get; set; }
        public DateTime Edit { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public int RewardValue { get; set; }
        public bool IsInGroup { get; set; }
        public int? NumberPlaces { get; set; }
        //The association that add this mission
        [ForeignKey("AssociationId")]
        public User Association { get; set; }
        public Guid AssociationId { get; set; }
        public ICollection<Participation> Participants { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<MissionTask> Tasks { get; set; }
    }
}
