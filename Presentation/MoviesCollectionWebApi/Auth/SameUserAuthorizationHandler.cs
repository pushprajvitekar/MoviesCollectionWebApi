using Microsoft.AspNetCore.Authorization;
using MovieCollectionWebApi.Extensions;

namespace MovieCollectionWebApi.Auth
{
    public static class PolicyName
    {
        public const string SameUserPolicy = "SameUserPolicy";
    }
    public class SameUserAuthorizationHandler : AuthorizationHandler<SameUserAuthorizationRequirement, int>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       SameUserAuthorizationRequirement requirement,
                                                       int resource)
        {
            if (context.User.IsInRole(Roles.Admin) || context.User.GetUserId() == resource)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
    public class SameUserAuthorizationRequirement : IAuthorizationRequirement
    {
    }
}
