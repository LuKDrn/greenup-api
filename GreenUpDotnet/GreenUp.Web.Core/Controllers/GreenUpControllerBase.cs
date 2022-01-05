using Abp.AspNetCore.Mvc.Controllers;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Users.Models;
using GreenUp.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace GreenUp.Web.Core.Controllers
{
    public class GreenUpControllerBase : AbpController
    {
        protected readonly GreenUpContext _context;
        protected readonly IConfiguration _config;
        public GreenUpControllerBase(GreenUpContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<Mission> GetOneMission(int id, bool allIncluded)
        {
            if (allIncluded)
            {
                return _context.Missions
                    .Include(m => m.Status)
                    .Include(m => m.Location)
                    .Include(m => m.Participants)
                        .ThenInclude(u => u.User)
                    .Include(m => m.Association.Addresses)
                    .Include(m => m.Tasks)
                    .Include(m => m.Tags)
                    .Where(m => m.Id == id);
            }
            return _context.Missions
                .Where(m => m.Id == id);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<User> GetUser(Guid id, bool allInclude)
        {
            if (allInclude)
            {
                return _context.Users
                    .Include(u => u.Addresses)
                    .Include(u => u.Participations)
                        .ThenInclude(p => p.Mission.Location)
                    .Include(u => u.Participations)
                        .ThenInclude(p => p.Mission.Association)
                    .Include(u => u.Favorites)
                    .Include(u => u.Orders)
                    .Where(u => u.IsUser && u.Id == id);
            }
            return _context.Users
                .Where(u => u.IsUser && u.Id == id);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<User> GetOneAssociation(Guid id, bool allIncluded)
        {
            if (allIncluded)
            {
                return _context.Users
                    .Include(u => u.Addresses)
                    .Include(u => u.Missions)
                        .ThenInclude(m => m.Status)
                    .Include(u => u.Missions)
                        .ThenInclude(u => u.Participants)
                    .Include(u => u.Missions)
                        .ThenInclude(u => u.Location)
                    .Where(u => u.IsAssociation && u.Id == id);
            }
            return _context.Users
                .Where(u => u.IsAssociation && u.Id == id);
        }

        [HttpPost]
        public static string UploadImage(IFormFile image)
        {
            string img = "/Images/default-profile-picture-avatar-png-green.png";
            if (image != null)
            {              
                MemoryStream ms = new();
                image.CopyTo(ms);
                var imageBytes = ms.ToArray();
                img = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
                ms.Close();
                ms.Dispose();
            }
            return img;
        }
    }
}