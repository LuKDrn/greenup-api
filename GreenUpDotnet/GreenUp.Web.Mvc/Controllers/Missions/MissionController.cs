using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using Microsoft.Extensions.Configuration;

namespace GreenUp.Web.Mvc.Controllers.Missions
{
    public class MissionController : GreenUpControllerBase
    {
        public MissionController(GreenUpContext _context, IConfiguration _config) : base(_context, _config)
        {

        }
    }
}
