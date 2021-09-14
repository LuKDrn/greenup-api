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
    public class MissionController : GreenUpControllerBase
    {
        public MissionController(GreenUpContext _context, IConfiguration _config) : base(_context, _config)
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
                Location place = new Location();
                if (model.Place != null)
                {
                    place.Adress = model.Place.Adress;
                    place.City = model.Place.City;
                    place.ZipCode = model.Place.ZipCode;
                    await _context.Locations.AddAsync(place);
                }
                else
                {
                    place = await _context.Locations.Include(x => x.Missions).FirstOrDefaultAsync();
                }
                Mission mission = new Mission()
                {
                    Titre = model.Titre,
                    Description = model.Description,
                    Date = model.Date,
                    RewardValue = model.RewardValue,
                    Available = true,
                    Availability = model.Availability,
                    IsInGroup = model.IsInGroup,
                    Place = place,
                };
                place.Missions.Add(mission);
                await _context.SaveChangesAsync();
                return mission;
            }
            return Ok(new { error = "Informations saisies incorrectes" });
        }
    }
}
