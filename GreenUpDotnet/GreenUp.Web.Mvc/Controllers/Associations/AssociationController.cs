using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using Microsoft.Extensions.Configuration;

namespace GreenUp.Web.Mvc.Controllers.Associations
{
    public class AssociationController : GreenUpControllerBase
    {
        public AssociationController(GreenUpContext _context, IConfiguration _config) : base(_context, _config)
        {

        }
    }
}
