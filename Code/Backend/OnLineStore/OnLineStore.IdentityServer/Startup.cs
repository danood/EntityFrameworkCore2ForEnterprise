﻿using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnLineStore.IdentityServer.Models;
using OnLineStore.IdentityServer.Services;
using OnLineStore.IdentityServer.Validation;

namespace OnLineStore.IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /* Setting dependency injection */

            // Db Context
            services.AddDbContext<AuthDbContext>(options => options.UseInMemoryDatabase("Auth"));

            // Password validator and profile
            services
                .AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
                .AddTransient<IProfileService, ProfileService>();

            /* Identity Server */

            services
                .AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients());

            services
                .AddAuthentication()
                .AddIdentityServerAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            /* Init DbContext */
            var authDbContext = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetService<AuthDbContext>();

            authDbContext.SeedInMemory();

            app.UseIdentityServer();
        }
    }
}
