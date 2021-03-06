using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synapse.Areas.Identity.Data;
using Synapse.Data;

[assembly: HostingStartup(typeof(Synapse.Areas.Identity.IdentityHostingStartup))]
namespace Synapse.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddDefaultIdentity<SynapseUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<IdentityContext>();
            });
        }
    }
}