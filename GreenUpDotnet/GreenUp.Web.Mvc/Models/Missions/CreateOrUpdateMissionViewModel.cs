using System;
using System.ComponentModel.DataAnnotations;

namespace GreenUp.Web.Mvc.Models.Missions
{
    public class CreateOrUpdateMissionViewModel
    {
        public int? Id { get; set; }
        public Guid AssociationId { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public string Description { get; set; }
        public int? AdressId { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public int? ZipCode { get; set; }
        public DateTime Date { get; set; }
        public int RewardValue { get; set; }
        public bool IsInGroup { get; set; }
        public int NumberPlaces { get; set; }
        public bool Available { get; set; }
    }
}
