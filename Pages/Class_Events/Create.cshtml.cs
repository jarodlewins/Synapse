using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Synapse.Data;
using Synapse.Authorization;
using Synapse.Models;
using Synapse.Areas.Identity.Data;

namespace Synapse.Pages.Class_Events
{
    public class CreateModel : DI_BasePageModel
    {
        public CreateModel(SynapseContext context,
            IAuthorizationService authorizationService,
            UserManager<SynapseUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Class_Event Class_Event { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // requires using ContactManager.Authorization;
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                        User, Class_Event,
                                                        Class_EventOperations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Class_Event.Add(Class_Event);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
