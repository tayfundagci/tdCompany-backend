using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieApp.Entities;
using MovieApp.Message.Response;

namespace MovieApp
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<UserRole> _userRoles;

        public AuthorizeAttribute(params UserRole[] userRoles)
        {
            _userRoles = userRoles ?? new UserRole[] { };
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;
            if (context != null && context.HttpContext != null)
            {
                var user = context.HttpContext.Items["User"] as User;
                if (user == null || _userRoles.Any() && !_userRoles.Contains(user.Role))
                {
                    context.Result = new JsonResult(new CommonResponse() { Code = 401 , Message = "Unauthorized!"}) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}
