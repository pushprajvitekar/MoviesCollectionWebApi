using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieCollectionWebApi.Extensions
{
    public static class AuthExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var id = -1;
            var claim = claimsPrincipal;
            if (claim != null)
            {
                var claimid = Convert.ToInt32(claim.FindFirstValue(ClaimTypes.NameIdentifier));
                id = claimid > 0 ? claimid : id;
            }
            return id;
        }

        //public static Task<IActionResult> Authorize(this IAuthorizationService authorizationService,ClaimsPrincipal principal, IAuthorizationRequirement requirement, object? resource, )
        //{
            
        //}
    }
}
