using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Missions.Models
{
    public class MissionTask : Entity
    {
        [ForeignKey("MissionId")]
        public Mission Mission { get; set; }
        public int MissionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
