using Abp.AspNetCore.Mvc.Controllers;
using GreenUp.EntityFrameworkCore.Data;
using Microsoft.Extensions.Configuration;

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
    }
}
