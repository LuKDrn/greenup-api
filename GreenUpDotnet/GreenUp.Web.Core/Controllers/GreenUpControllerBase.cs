using Abp.AspNetCore.Mvc.Controllers;
using GreenUp.Core.Business.Users.Models;
using GreenUp.EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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

        public IQueryable<User> GetUser(Guid id, bool allInclude)
        {
            if (allInclude)
            {
                return _context.Users
                    .Include(u => u.Role)
                    .Include(u => u.Adress)
                    .Include(u => u.Missions)
                        .ThenInclude(m => m.Association)
                    .AsSplitQuery()
                    .Where(u => u.Id == id);
            }
            return _context.Users
                .Where(u => u.Id == id);
        }
    }
}
