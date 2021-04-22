using Synapse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synapse.Areas.Identity.Data;

namespace Synapse.Pages.Class_Events
{
    public class DI_BasePageModel : PageModel
    {
        protected SynapseContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<SynapseUser> UserManager { get; }

        

        public DI_BasePageModel(SynapseContext context, IAuthorizationService authorizationService, UserManager<SynapseUser> userManager)
        {
            Context = context;
            AuthorizationService = authorizationService;
            UserManager = userManager;
        }
    }
}
