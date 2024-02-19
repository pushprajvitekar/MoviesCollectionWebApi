using System.Security.Claims;

namespace MovieCollectionWebApi.Extensions
{
    public static class AuthExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetClaimValue(ClaimTypes.NameIdentifier);
        }
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetClaimValue(ClaimTypes.Name);
        }
        private static string GetClaimValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var retValue = string.Empty;
            var claim = claimsPrincipal;
            if (claim != null)
            {
                var claimValue = Convert.ToString(claim.FindFirstValue(claimType));
                retValue = !string.IsNullOrEmpty(claimValue) ? claimValue : retValue;
            }
            return retValue;
        }
    }
}
