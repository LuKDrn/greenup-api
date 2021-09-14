using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using Microsoft.Extensions.Configuration;

namespace GreenUp.Web.Mvc.Controllers.Companies
{
    public class CompaniesController : GreenUpControllerBase
    {
        public CompaniesController(GreenUpContext _context, IConfiguration _config) : base(_context, _config)
        {

        }
    }
}
