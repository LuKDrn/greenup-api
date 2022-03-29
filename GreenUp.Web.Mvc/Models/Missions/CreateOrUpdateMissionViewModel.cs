using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Tags.Models;
using GreenUp.Web.Mvc.Models.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenUp.Web.Mvc.Models.Missions
{
    public class CreateOrUpdateMissionViewModel
    {
        public int? Id { get; set; }
        [Required]
        public Guid AssociationId { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public string Description { get; set; }
        public int? AdressId { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public int? ZipCode { get; set; }
        public DateTime Creation { get; set; }
        public DateTime Edit { get; set; }
        public DateTime DateDebutMission { get; set; }
        public DateTime DateFinMission { get; set; }
        public int PointMission { get; set; }
        public bool IsGroup { get; set; }
        public int NombrePlace { get; set; }
        public ICollection<CreateOrUpdateTaskViewModel> Tasks { get; set; } = new List<CreateOrUpdateTaskViewModel>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<string> SelectedTags  { get; set; } = new List<string>();
        public ICollection<string> NewTags { get; set; } = new List<string>();
        public ICollection<Status> Statuses { get; set; } = new List<Status>();
        public int SelectedStatus { get; set; } 
    }
}
