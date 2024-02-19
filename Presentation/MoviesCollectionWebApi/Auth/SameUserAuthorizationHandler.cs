using Microsoft.AspNetCore.Authorization;
using MovieCollectionWebApi.Extensions;

namespace MovieCollectionWebApi.Auth
{
    public static class PolicyName
    {
        public const string SameUserPolicy = "SameUserPolicy";
    }
    public class SameUserAuthorizationHandler : AuthorizationHandler<SameUserAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       SameUserAuthorizationRequirement requirement,
                                                       string resource)
        {
            if (context.User.IsInRole(Roles.Admin) || context.User.GetUserName() == resource)
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
