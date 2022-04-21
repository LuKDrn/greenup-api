using GreenUp.Web.Mvc.Models.Adresses;
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
        public OneAdressViewModel Address { get; set; }
        public string AssociationId { get; set; }
        public string AssociationLogo { get; set; }
        public string AssociationName { get; set; }
        public OneAdressViewModel AssociationAdress { get; set; }
        public int RewardValue { get; set; }
        public bool IsInGroup { get; set; }
        public int? NumberPlaces { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public int TotalParticipants { get; set; }
        public ICollection<OneParticipantViewModel> Participants { get; set; } = new List<OneParticipantViewModel>();

    }
}
