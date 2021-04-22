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
    public class EditModel : DI_BasePageModel
    {
        public EditModel(
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

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Class_Event, Class_EventOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch Contact from DB to get OwnerID.
            var class_event = await Context
                .Class_Event.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (class_event == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, class_event,
                                                     Class_EventOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Class_Event.Instructors = class_event.Instructors;

            Context.Attach(Class_Event).State = EntityState.Modified;

            if (Class_Event.Status == ClassStatus.Approved)
            {
                // If the contact is updated after approval, 
                // and the user cannot approve,
                // set the status back to submitted so the update can be
                // checked and approved.
                var canApprove = await AuthorizationService.AuthorizeAsync(User,
                                        Class_Event,
                                        Class_EventOperations.Approve);

                if (!canApprove.Succeeded)
                {
                    Class_Event.Status = ClassStatus.Submitted;
                }
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}