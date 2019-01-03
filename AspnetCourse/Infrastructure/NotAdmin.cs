using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCourse.Infrastructure
{
    public class NotAdminRequirement : IAuthorizationRequirement
    {
        
    }

    public class NotAdminHandler : AuthorizationHandler<NotAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NotAdminRequirement requirement)
        {
            if (context.User.Identity != null
                && context.User.Identity.Name != null
                && context.User.Identity.Name != "admin")
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
