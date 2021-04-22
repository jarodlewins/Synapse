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
    public class DeleteModel : DI_BasePageModel
    {
        public DeleteModel(
            SynapseContext context,
            IAuthorizationService authorizationService,
            UserManager<SynapseUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Class_Event Class_Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Class_Event = await Context.Class_Event.FirstOrDefaultAsync(
                                                 m => m.ID == id);

            if (Class_Event == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Class_Event,
                                                     Class_EventOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var contact = await Context
                .Class_Event.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (contact == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, contact,
                                                     Class_EventOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Class_Event.Remove(contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
