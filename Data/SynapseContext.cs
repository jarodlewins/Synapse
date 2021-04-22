using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Synapse.Models;

namespace Synapse.Data
{
    public class SynapseContext : IdentityDbContext
    {
        public SynapseContext(DbContextOptions<SynapseContext> options)
            : base(options)
        {
        }

        public DbSet<Class_Event> Class_Event { get; set; }
    }
}
