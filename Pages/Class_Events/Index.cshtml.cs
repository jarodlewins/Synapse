using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Synapse.Authorization;
using Microsoft.AspNetCore.Identity;
using Synapse.Models;
using Synapse.Data;
using Synapse.Areas.Identity.Data;

namespace Synapse.Pages.Class_Events
{
    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(
            SynapseContext context,
            IAuthorizationService authorizationService,
            UserManager<SynapseUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<Class_Event> Class_Event { get;set; }

        public async Task OnGetAsync()
        {
            var class_Events = from c in Context.Class_Event select c;

            var isAuthorized = User.IsInRole(Constants.Class_EventManagersRole) || User.IsInRole(Constants.Class_EventAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);
            Class_Event = await Context.Class_Event.ToListAsync();

            if (!isAuthorized)
            {
                class_Events = class_Events.Where(c => c.Status == ClassStatus.Approved
                                            || c.Instructors.Contains(c.Instructors.FirstOrDefault(x => x.userID == currentUserId)));
            }
            Class_Event = await class_Events.ToListAsync();
        }
    }
}
