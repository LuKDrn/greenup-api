using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Users.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenUp.Core.Business.Participations.Models
{
    public class Participation
    {
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("MissionId")]
        public Mission Mission { get; set; }
        public int MissionId { get; set; }
        public DateTime DateInscription { get; set; }

    }
}
