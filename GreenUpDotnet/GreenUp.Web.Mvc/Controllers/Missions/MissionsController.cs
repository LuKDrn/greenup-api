using GreenUp.Core.Business.Associations.Models;
using GreenUp.Core.Business.Locations.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Models.Missions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Controllers.Missions
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : GreenUpControllerBase
    {
        public MissionsController(GreenUpContext _context, IConfiguration _config) : base(_context, _config)
        {

        }

        [HttpGet]
        public async Task<ICollection<Mission>> Missions()
        {
            ICollection<Mission> model = await _context.Missions.Include(m => m.Association).Include(m => m.Location).ToListAsync();
            return model;
        }

        [HttpPost, Route("Add")]
        public async Task<ActionResult<Mission>> Add([FromBody] CreateOrUpdateMissionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Association association = await _context.Associations.Include(a => a.Missions).FirstOrDefaultAsync(a => a.Id == model.AssociationId);
                Mission mission = new Mission()
                {
                    Titre = model.Titre,
                    Description = model.Description,
                    Date = model.Date,
                    RewardValue = model.RewardValue,
                    Available = true,
                    Places = model.Places,
                    IsInGroup = model.IsInGroup,
                    Location = new Location()
                    {
                        Adress = model.Adress,
                        City = model.City,
                        ZipCode = (int)model.ZipCode,
                    },
                };
                association.Missions.Add(mission);
                await _context.SaveChangesAsync();
                return mission;
            }
            return Ok(new { error = "Informations saisies incorrectes" });
        }
    }
}
