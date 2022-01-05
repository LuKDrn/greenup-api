using GreenUp.Core.Business.Addresses.Models;
using GreenUp.Web.Mvc.Models.Associations;
using GreenUp.Web.Mvc.Models.Users;
using System;

namespace GreenUp.Web.Mvc.Models.Participations
{
    public class OneParticipantViewModel
    {
        public Guid UserId { get; set; }
        public OneUserViewModel User { get; set; }
        public int MissionId { get; set; }
        public OneAssociationViewModel Association { get; set; }
        public string DateInscription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public Address Adress { get; set; }
    }
}
