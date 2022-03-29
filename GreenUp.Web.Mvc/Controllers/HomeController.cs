using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace GreenUp.Web.Mvc.Controllers
{
    [AllowAnonymous]
    public class HomeController : GreenUpControllerBase
    {
        public HomeController(GreenUpContext _context, IConfiguration _config)
            : base(_context, _config)
        {
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ErrorWithMessage(string type, int status, string debug)
        {
            ErrorViewModel model = new()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Status = status,
                Type = type,
                Message = debug,
            };
            return View("Error", model);
        }

        public IActionResult PageNotFound()
        {
            ErrorViewModel model = new()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };
            return View(model);
        }
    }
}
