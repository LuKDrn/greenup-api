using GreenUp.Core.Business.Addresses.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Participations.Models;
using GreenUp.Core.Business.Tags.Models;
using GreenUp.Core.Business.Users.Models;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Models.Adresses;
using GreenUp.Web.Mvc.Models.Missions;
using GreenUp.Web.Mvc.Models.Participations;
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
        public async Task<ActionResult<ICollection<OneMissionViewModel>>> ListMission(string numberOfItems)
        {
            ICollection<Mission> missions = new List<Mission>();
            if (numberOfItems != null)
            {
                missions = await _context.Missions.Include(m => m.Status).Include(m => m.Location).Include(m => m.Association.Addresses).AsNoTracking().Take(int.Parse(numberOfItems)).OrderBy(m => m.Creation).ToListAsync();
            } else
            {
                missions = await _context.Missions.Include(m => m.Status).Include(m => m.Location).Include(m => m.Association.Addresses).AsNoTracking().OrderBy(m => m.Creation).ToListAsync();
            }
            var model = new List<OneMissionViewModel>();
            foreach (var mission in missions)
            {
                model.Add(new OneMissionViewModel
                {
                    Id = mission.Id,
                    Titre = mission.Title,
                    Description = mission.Description,
                    Creation = mission.Creation.ToString("dd/MM/yyyy HH:mm"),
                    Edit = mission.Edit.ToString("dd/MM/yyyy HH:mm"),
                    Start = mission.Start.ToString("dd/MM/yyyy HH:mm"),
                    End = mission.End.ToString("dd/MM/yyyy HH:mm"),
                    IsInGroup = mission.IsInGroup,
                    RewardValue = mission.RewardValue,
                    NumberPlaces = mission.NumberPlaces,
                    Address = new OneAdressViewModel
                    {
                        Id = mission.LocationId,
                        City = mission.Location.City,
                        Place = mission.Location.Place,
                        ZipCode = mission.Location.ZipCode
                    },
                    AssociationId = mission.AssociationId.ToString(),
                    AssociationName = mission.Association.LastName,
                    AssociationLogo = mission.Association.Photo,  
                    AssociationAdress = mission.Association.Addresses.Select(a => new OneAdressViewModel
                    {
                        Id = a.Id,
                        UserId = a.UserId.ToString(),
                        City = a.City,
                        Place = a.Place,
                        ZipCode = a.ZipCode
                    }).FirstOrDefault(),
                });
            }
            return model;
        }


        [HttpGet, Route("GetOneAssociationMissions")]
        public async Task<ActionResult<ICollection<OneMissionViewModel>>> GetAssociationMissions(Guid associationId)
        {
            User association = await GetOneAssociation(associationId, false)
                .Include(a => a.Missions).ThenInclude(m => m.Status)
                .Include(a => a.Missions).ThenInclude(m => m.Tasks)
                .Include(a => a.Missions).ThenInclude(m => m.Location)
                .Include(a => a.Missions).ThenInclude(m => m.Participants)
                .FirstOrDefaultAsync();
            if (association != null)
            {
                var model = new List<OneMissionViewModel>();
                foreach (var mission in association.Missions)
                {
                    var participants = new List<OneParticipantViewModel>();
                    foreach (var participation in mission.Participants)
                    {
                        participants.Add(ConvertParticipantToViewModel(participation));
                    }
                    model.Add(ConvertMissionToViewModel(mission, participants));
                    };
                return model;
            }
            return NotFound($"Aucun association n'a été trouvé");
        }

        [AllowAnonymous]
        [HttpGet, Route("{id}")]
        public async Task<OneMissionViewModel> Details(int id)
        {
            Mission mission = await GetOneMission(id, true).FirstOrDefaultAsync();
            if (mission != null)
            {
                OneMissionViewModel model = new()
                {
                    Id = mission.Id,
                    Titre = mission.Title,
                    Description = mission.Description,
                    Creation = mission.Creation.ToString("dd/MM/yyyy HH:mm"),
                    Edit = mission.Edit.ToString("dd/MM/yyyy HH:mm"),
                    Start = mission.Start.ToString("dd/MM/yyyy HH:mm"),
                    End = mission.End.ToString("dd/MM/yyyy HH:mm"),
                    Address = new OneAdressViewModel
                    {
                        Id = mission.LocationId,
                        City = mission.Location.City,
                        Place = mission.Location.Place,
                        ZipCode = mission.Location.ZipCode
                    },
                    AssociationId = mission.AssociationId.ToString(),
                    RewardValue = mission.RewardValue,
                    IsInGroup = mission.IsInGroup,
                    NumberPlaces = mission.NumberPlaces,
                    TotalParticipants = mission.Participants.Count,
                    Tasks = mission.Tasks,
                    Status = mission.Status,
                    AssociationName = mission.Association.FirstName,
                    AssociationAdress = mission.Association.Addresses.Select(a => new OneAdressViewModel
                    {
                        Id = a.Id,
                        UserId = a.UserId.ToString(),
                        City = a.City,
                        Place = a.Place,
                        ZipCode = a.ZipCode
                    }).FirstOrDefault(),
                };
                return model;
            }
            return null;
        }

        [HttpPost, Route("Add")]
        public async Task<string> Add(CreateOrUpdateMissionViewModel model)
        {
            if (ModelState.IsValid)
            {
                User association = await GetOneAssociation(model.AssociationId, false).Include(a => a.Missions).FirstOrDefaultAsync();
                Mission mission = new()
                {
                    Title = model.Titre,
                    Description = model.Description,
                    Creation = DateTime.UtcNow,
                    Start = model.DateDebutMission,
                    End = model.DateFinMission,
                    RewardValue = model.PointMission,
                    NumberPlaces = model.NombrePlace,
                    IsInGroup = model.IsGroup,
                    Location = new Address()
                    {
                        UserId = model.AssociationId,
                        Place = model.Adress,
                        City = model.City,
                        ZipCode = (int)model.ZipCode,
                    },
                    Status = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == 1)
                };
                foreach (var task in model.Tasks)
                {
                    mission.Tasks.Add(new MissionTask()
                    {
                        Title = task.Title,
                        Description = task.Description,
                    });
                };
                foreach (var tag in model.SelectedTags.Concat(model.NewTags))
                {
                    var tagAlreadyExists = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tag);
                    if (tagAlreadyExists == null)
                    {
                        Tag newTag = new()
                        {
                            Name = tag
                        };
                        mission.Tags.Add(newTag);
                    }
                    else
                    {
                        mission.Tags.Add(tagAlreadyExists);
                    }
                }
                association.Missions.Add(mission);
                await _context.SaveChangesAsync();
                return "La mission a bien été créée";
            }
            return "Informations saisies incorrectes";
        }

        [HttpPut, Route("Update")]
        public async Task<ActionResult<Mission>> Update(CreateOrUpdateMissionViewModel model)
        {
            User association = await GetOneAssociation(model.AssociationId, false).Include(a => a.Missions).FirstOrDefaultAsync();
            Mission mission = await GetOneMission((int)model.Id, true).FirstOrDefaultAsync();
            if (mission != null && association.Missions.Select(m => m.Id).Contains((int)model.Id))
            {
                if (mission.Participants.Count == 0)
                {
                    mission.Title = model.Titre;
                    mission.Description = model.Description;
                    mission.Start = model.DateDebutMission;
                    mission.End = model.DateFinMission;
                    mission.RewardValue = model.PointMission;
                    mission.NumberPlaces = model.NombrePlace;
                    mission.IsInGroup = model.IsGroup;
                    mission.StatusId = model.SelectedStatus;
                    Address missionLocation = mission.Location;
                    missionLocation.Place = model.Adress;
                    missionLocation.City = model.City;
                    missionLocation.ZipCode = (int)model.ZipCode;
                    mission.Edit = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        mission,
                        success = $"Les informatinos de la mission de l'association {association.LastName} ont été mises à jours"
                    });
                }
                return NotFound(new { error = $"Error : Les informations de cette mission ne sont plus modifiables." });
            }
            else
            {
                return NotFound(new { error = $"Error : Aucune mission de l'association {association.LastName} n'a été trouvé." });
            }
        }

        [HttpDelete, Route("Remove")]
        public async Task<string> Remove([FromQuery] int missionId, Guid associationId)
        {
            if (ModelState.IsValid)
            {
                User association = await GetOneAssociation(associationId, false).Include(a => a.Missions).FirstOrDefaultAsync();
                Mission mission = await GetOneMission(missionId, true).FirstOrDefaultAsync();
                if (mission != null && association.Missions.Select(m => m.Id).Contains(missionId))
                {
                    if(mission.StatusId <= 2) { 
                        association.Missions.Remove(mission);
                        _context.Missions.Remove(mission);
                    }
                    else
                    {
                        mission.StatusId = 5;
                    }
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return $"Error : Aucune mission de l'association {association.LastName} n'a été trouvé à supprimer.";
                }
            }
            return "Error : Les informations saisies ne permettent pas une suppression de mission.";
        }


        private static OneParticipantViewModel ConvertParticipantToViewModel(Participation user)
        {
            return new OneParticipantViewModel
            {
                UserId = user.UserId,
                FirstName = user.User.FirstName,
                LastName = user.User.LastName,
                Mail = user.User.Mail,
                DateInscription = user.DateInscription.ToString("dd/MM/yyyy HH:mm"),
                Photo = user.User.Photo,
            };
        }
        private static OneMissionViewModel ConvertMissionToViewModel(Mission mission, ICollection<OneParticipantViewModel> participants)
        {
            return new OneMissionViewModel()
            {
                Id = mission.Id,
                Titre = mission.Title,
                Description = mission.Description,
                Creation = mission.Creation.ToString("dd/MM/yyyy HH:mm"),
                Edit = mission.Edit.ToString("dd/MM/yyyy HH:mm"),
                Start = mission.Start.ToString("dd/MM/yyyy HH:mm"),
                End = mission.End.ToString("dd/MM/yyyy HH:mm"),
                IsInGroup = mission.IsInGroup,
                NumberPlaces = mission.NumberPlaces,
                RewardValue = mission.RewardValue,
                AssociationId = mission.AssociationId.ToString(),
                AssociationName = mission.Association.LastName,
                AssociationLogo = mission.Association.Photo,
                AssociationAdress = mission.Association.Addresses.Select(a => new OneAdressViewModel
                {
                    Id = a.Id,
                    UserId = a.UserId.ToString(),
                    City = a.City,
                    Place = a.Place,
                    ZipCode = a.ZipCode
                }).FirstOrDefault(),
                Status = new Status()
                {
                    Id = mission.StatusId,
                    Value = mission.Status.Value
                },
                Address = new OneAdressViewModel()
                {
                    Id = mission.LocationId,
                    Place = mission.Location.Place,
                    City = mission.Location.City,
                    ZipCode = mission.Location.ZipCode
                },
                Tasks = mission.Tasks,
                Participants = participants
            };
        }
    }
}
