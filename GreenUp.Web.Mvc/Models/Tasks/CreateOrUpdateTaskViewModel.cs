using GreenUp.Core.Business.Missions.Models;

namespace GreenUp.Web.Mvc.Models.Tasks
{
    public class CreateOrUpdateTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MissionId { get; set; }
    }
}
