using Abp.Domain.Entities;
using GreenUp.Core.Business.Missions.Models;
using System.Collections.Generic;

namespace GreenUp.Core.Business.Tags.Models
{
    public class Tag : Entity
    {
        public string Name { get; set; }
        public ICollection<Mission> Missions { get; set; }
    }
}
