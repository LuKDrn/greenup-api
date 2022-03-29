using GreenUp.Core.Business.Addresses.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Tags.Models;
using GreenUp.Core.Business.Users.Models;
using GreenUp.Web.Mvc.Models.Participations;
using System.Collections.Generic;

namespace GreenUp.Web.Mvc.Models.Missions
{
    public class OneMissionViewModel
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Creation { get; set; }
        public string Edit { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public Address Address { get; set; }
        public string AssociationId { get; set; }
        public User Association { get; set; }
        public int RewardValue { get; set; }
        public bool IsInGroup { get; set; }
        public int? NumberPlaces { get; set; }
        public Status Status { get; set; }
        public ICollection<MissionTask> Tasks { get; set; } = new List<MissionTask>();
        public ICollection<OneParticipantViewModel> Participants { get; set; } = new List<OneParticipantViewModel>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    }
}
