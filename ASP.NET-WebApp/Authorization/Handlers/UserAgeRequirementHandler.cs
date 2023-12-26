using ASP.NET_Auth_under_the_hood_test.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ASP.NET_Auth_under_the_hood_test.Authorization.Handlers
{
    public class UserAgeRequirementHandler : AuthorizationHandler<UserAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "Age") || context == null)
                return Task.CompletedTask;

            var ageClaim = context.User.FindFirst("Age");

            if (ageClaim == null)
                return Task.CompletedTask;

            var ageValue = int.Parse(ageClaim.Value);
            if (ageValue >= requirement.AgeRequirement)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
