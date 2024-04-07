using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;

namespace AnanasMVCWebApp.Utilities {
    public class CustomAuthorization : AuthorizeAttribute, IAuthorizationFilter {
        private string _role;
        public CustomAuthorization(string role) {
            _role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context) {
            if (_role == ApplicationRole.Admin) {
                if (!context.HttpContext.User.IsInRole(ApplicationRole.Admin)) {
                    context.Result = new UnauthorizedResult();
                }
            } else if(_role == ApplicationRole.Customer) {
                if (!context.HttpContext.User.IsInRole(ApplicationRole.Customer)) {
                    if (context.HttpContext.User.IsInRole(ApplicationRole.Admin)) {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                            { "controller", "Home" },
                            { "action", "Index" },
                            { "area", "Admin" }
                        });
                    }
                }
            }
        }
    }
}
