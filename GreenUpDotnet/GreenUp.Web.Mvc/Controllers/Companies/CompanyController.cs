using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using Microsoft.Extensions.Configuration;

namespace GreenUp.Web.Mvc.Controllers.Companies
{
    public class CompanyController : GreenUpControllerBase
    {
        public CompanyController(GreenUpContext _context, IConfiguration _config) : base(_context, _config)
        {

        }
    }
}
