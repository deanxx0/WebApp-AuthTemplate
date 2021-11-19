using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_AuthTest.Authorization
{
    public class HRManagerProbationRequirment : IAuthorizationRequirement
    {
        public HRManagerProbationRequirment(int probationMonths)
        {
            ProbationMonths = probationMonths;
        }

        public int ProbationMonths { get; }
    }

    public class HRManagerProbationRequirmentHandler : AuthorizationHandler<HRManagerProbationRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerProbationRequirment requirement)
        {
            if (!context.User.HasClaim(x => x.Type == "Date"))
                return Task.CompletedTask;

            var empDate = DateTime.Parse(context.User.FindFirst(x => x.Type == "Date").Value);
            var period = DateTime.Now - empDate;
            if (period.Days > 30 * requirement.ProbationMonths)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
