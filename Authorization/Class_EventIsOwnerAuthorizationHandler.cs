using Synapse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Synapse.Areas.Identity.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Synapse.Authorization
{
    public class Class_EventIsOwnerAuthorizationHandler
                : AuthorizationHandler<OperationAuthorizationRequirement, Class_Event>
    {
        UserManager<SynapseUser> _userManager;

        public Class_EventIsOwnerAuthorizationHandler(UserManager<SynapseUser>
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Class_Event resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for CRUD permission, return.

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }
            // resource.Instructors.Contains(resource.Instructors.FirstOrDefault(x => x.userID == _userManager.GetUserId(context.User)))
            if (true)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}