using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Synapse.Models;

namespace Synapse.Pages.Class_Events
{
    public class DetailsModel : PageModel
    {
        private readonly Synapse.Data.SynapseContext _context;

        public DetailsModel(Synapse.Data.SynapseContext context)
        {
            _context = context;
        }

        public Class_Event Class_Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Class_Event = await _context.Class_Event.FirstOrDefaultAsync(m => m.ID == id);

            if (Class_Event == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
