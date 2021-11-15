using GreenUp.Web.Mvc.Models.Adresses;
using GreenUp.Web.Mvc.Models.Associations;
using System.Collections.Generic;

namespace GreenUp.Web.Mvc.Models.Missions
{
    public class OneMissionViewModel
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public OneAdressViewModel Adress { get; set; }
        public string AssociationId { get; set; }
        public OneAssociationViewModel Association { get; set; }
        public string Date { get; set; }
        public int RewardValue { get; set; }
        public bool IsInGroup { get; set; }
        public int? NumberPlaces { get; set; }
        public bool Available { get; set; }
        public ICollection<OneMissionUserViewModel> Users { get; set; } = new List<OneMissionUserViewModel>();
    }
}
