using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProiectMedii.Data;

[assembly: HostingStartup(typeof(ProiectMedii.Areas.Identity.IdentityHostingStartup))]
namespace ProiectMedii.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<LibraryIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("LibraryIdentityContextConnection")));

            });
        }
    }
}