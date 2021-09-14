﻿using Abp.AspNetCore.Mvc.Controllers;
using GreenUp.Core.Business.Images.Models;
using GreenUp.Core.Business.Users.Models;
using GreenUp.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<Image> UploadImage(IFormFile image)
        {
            Image img = new Image()
            {
                ImageTitle = image.FileName
            };

            MemoryStream ms = new MemoryStream();
            image.CopyTo(ms);
            img.ImageData = ms.ToArray();

            ms.Close();
            ms.Dispose();

            await _context.Images.AddAsync(img);
            await _context.SaveChangesAsync();
            return img;
        }
    }
}