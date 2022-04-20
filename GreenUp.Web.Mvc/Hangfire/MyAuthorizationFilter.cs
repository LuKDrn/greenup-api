using Hangfire.Dashboard;

namespace GreenUp.Web.Mvc.Hangfire
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all users to see the Dashboard (potentially dangerous).
            return true;
        }
    }
}
