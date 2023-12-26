using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_Auth_under_the_hood_test.Authorization.Requirements
{
    public class UserAgeRequirement : IAuthorizationRequirement
    {
        public int AgeRequirement { get; set; }

        public UserAgeRequirement(int ageRequirement)
        {
            AgeRequirement = ageRequirement;
        }
    }
}
