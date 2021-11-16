using GreenUp.Core.Business.Adresses.Models;
using GreenUp.Core.Business.Associations.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Users.Models;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Models.Adresses;
using GreenUp.Web.Mvc.Models.Associations;
using GreenUp.Web.Mvc.Models.Missions;
using GreenUp.Web.Mvc.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Controllers.Missions
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : GreenUpControllerBase
    {
        public MissionsController(GreenUpContext _context, IConfiguration _config) : base(_context, _config)
        {

        }

        [AllowAnonymous]
        [HttpGet, Route("List")]
        public async Task<ActionResult<ICollection<OneMissionViewModel>>> ListMission(int numberOfItems)
        {
            ICollection<Mission> missions = await _context.Missions.Include(m => m.Location).Include(m => m.Association).AsNoTracking().Take(numberOfItems).ToListAsync();
            var model = new List<OneMissionViewModel>();
            foreach (var mission in missions)
            {
                model.Add(new OneMissionViewModel
                {
                    Id = mission.Id,
                    Titre = mission.Titre,
                    Description = mission.Description,
                    Date = mission.Date.ToString("dd/MM/yyyy HH:mm"),
                    Available = mission.Available,
                    IsInGroup = mission.IsInGroup,
                    RewardValue = mission.RewardValue,
                    NumberPlaces = mission.NumberPlaces,
                    Adress = new OneAdressViewModel
                    {
                        Id = mission.LocationId,
                        City = mission.Location.City,
                        Place = mission.Location.Place,
                        ZipCode = mission.Location.ZipCode
                    },
                    Association = new OneAssociationViewModel
                    {
                        Id = mission.AssociationId,
                        Name = mission.Association.Name,
                        Siren = mission.Association.Siren,
                        Logo = mission.Association.Logo
                    }
                });
            }
            return model;
        }


        [HttpGet, Route("GetOneAssociationMissions")]
        public async Task<ActionResult<ICollection<OneMissionViewModel>>> GetAssociationMissions(Guid associationId)
        {
            Association association = await GetOneAssociation(associationId, false)
                .Include(a => a.Missions).ThenInclude(m => m.Location)
                .Include(a => a.Missions).ThenInclude(m => m.Users)
                .FirstOrDefaultAsync();
            if(association != null)
            {
                var model = new List<OneMissionViewModel>();
                foreach (var mission in association.Missions)
                {
                    var usersModel = new List<OneMissionUserViewModel>();
                    if(mission.Users.Count > 0)
                    {
                        foreach (var user in mission.Users)
                        {
                            usersModel.Add(new OneMissionUserViewModel
                            {
                                Id = user.UserId,
                                FirstName = user.User.FirstName,
                                LastName = user.User.LastName,
                                Mail = user.User.Mail,
                                Adress = new OneAdressViewModel
                                {
                                    Id = user.User.AdressId,
                                    Place = user.User.Adress.Place,
                                    City = user.User.Adress.City,
                                    ZipCode = user.User.Adress.ZipCode
                                },
                                Photo = user.User.Photo,
                                DateInscription = user.DateInscription.ToString("dd/MM/yyyy HH:mm")
                            });
                        }
                    }
                    model.Add(new OneMissionViewModel
                    {
                        Id = mission.Id,
                        Titre = mission.Titre,
                        Description = mission.Description,
                        Date = mission.Date.ToString("dd/MM/yyyy HH:mm"),
                        Available = mission.Available,
                        IsInGroup = mission.IsInGroup,
                        NumberPlaces = mission.NumberPlaces,
                        RewardValue = mission.RewardValue,
                        Adress = new OneAdressViewModel()
                        {
                            Id = mission.LocationId,
                            Place = mission.Location.Place,
                            City = mission.Location.City,
                            ZipCode = mission.Location.ZipCode
                        },
                        Users = usersModel
                    });
                }
                return model;
            }
            else
            {
                return NotFound($"Aucun association n'a été trouvé");
            }
        }

        [AllowAnonymous]
        [HttpGet, Route("{id}")]
        public async Task<OneMissionViewModel> Details(int id)
        {
            Mission mission = await GetOneMission(id, true).FirstOrDefaultAsync();
            if(mission != null)
            {

                OneMissionViewModel model = new()
                {
                    Id = id,
                    AssociationId = mission.AssociationId.ToString(),
                    Titre = mission.Titre,
                    Description = mission.Description,
                    Date = mission.Date.ToString("dd/MM/yyyy HH:mm"),
                    IsInGroup = mission.IsInGroup,
                    NumberPlaces = mission.NumberPlaces,
                    Available = mission.Available,
                    RewardValue = mission.RewardValue,
                    Adress = new OneAdressViewModel()
                    {
                        Id = mission.LocationId,
                        Place = mission.Location.Place,
                        City = mission.Location.City,
                        ZipCode = mission.Location.ZipCode
                    },
                    Association = new OneAssociationViewModel()
                    {
                        Id = mission.AssociationId,
                        Name = mission.Association.Name,
                        Siren = mission.Association.Siren.ToString(),
                        Logo = mission.Association.Logo
                    },             
                };
                return model;
            }
            else
            {
                return null;
            }
        }

        [HttpPost, Route("Add")]
        public async Task<string> Add(CreateOrUpdateMissionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Association association = await _context.Associations.Include(a => a.Missions).FirstOrDefaultAsync(a => a.Id == model.AssociationId);
                Mission mission = new()
                {
                    Titre = model.Titre,
                    Description = model.Description,
                    Date = model.Date,
                    RewardValue = model.RewardValue,
                    Available = true,
                    NumberPlaces = model.NumberPlaces,
                    IsInGroup = model.IsInGroup,
                    Location = new Adress()
                    {
                        Place = model.Adress,
                        City = model.City,
                        ZipCode = (int)model.ZipCode,
                    },
                };
                association.Missions.Add(mission);
                await _context.SaveChangesAsync();
                return "La mission a bien été créée";
            }
            return "Informations saisies incorrectes";
        }

        [HttpPut, Route("Update")]
        public async Task<ActionResult<Mission>> Update(CreateOrUpdateMissionViewModel model)
        {
            Association association = await _context.Associations.Include(a => a.Missions).FirstOrDefaultAsync(a => a.Id == model.AssociationId);
            Mission mission = await _context.Missions.Include(m => m.Location).Include(m => m.Users).FirstOrDefaultAsync(m => m.Id == model.Id && m.AssociationId == model.AssociationId);
            if (mission != null && association.Missions.Select(m => m.Id).Contains((int)model.Id))
            {
                if (mission.Users.Count == 0)
                {
                    mission.Titre = model.Titre;
                    mission.Description = model.Description;
                    mission.Date = model.Date;
                    mission.RewardValue = model.RewardValue;
                    mission.Available = true;
                    mission.NumberPlaces = model.NumberPlaces;
                    mission.IsInGroup = model.IsInGroup;
                    Adress missionLocation = mission.Location;
                    missionLocation.Place = model.Adress;
                    missionLocation.City = model.City;
                    missionLocation.ZipCode = (int)model.ZipCode;
                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        mission,
                        success = $"Les informatinos de la mission de l'association {association.Name} ont été mises à jours"
                    });
                    
                }
                else
                {
                    return NotFound(new { error = $"Error : Les informations de cette mission ne sont plus modifiables." });
                }
            }
            else
            {
                return NotFound(new { error = $"Error : Aucune mission de l'association {association.Name} n'a été trouvé." });
            }
        }

        [HttpDelete, Route("Remove")]
        public async Task<string> Remove([FromQuery] int missionId, Guid associationId)
        {
            if (ModelState.IsValid)
            {
                Association association = await _context.Associations.Include(a => a.Missions).FirstOrDefaultAsync(a => a.Id == associationId);
                Mission mission = await _context.Missions.Include(m => m.Association).FirstOrDefaultAsync(m => m.Id == missionId && m.AssociationId == associationId);
                if (mission != null && association.Missions.Select(m => m.Id).Contains(missionId))
                {
                    association.Missions.Remove(mission);
                    _context.Missions.Remove(mission);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return $"Error : Aucune mission de l'association {association.Name} n'a été trouvé à supprimer.";
                }
            }
            return "Error : Les informations saisies ne permettent pas une suppression de mission.";
        }

    }
}
