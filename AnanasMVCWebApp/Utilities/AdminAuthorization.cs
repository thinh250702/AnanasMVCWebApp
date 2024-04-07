using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AnanasMVCWebApp.Utilities {
    public class AdminAuthorization : AuthorizeAttribute, IAuthorizationFilter {
        public void OnAuthorization(AuthorizationFilterContext context) {
            if (!context.HttpContext.User.IsInRole(ApplicationRole.Admin)) {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
