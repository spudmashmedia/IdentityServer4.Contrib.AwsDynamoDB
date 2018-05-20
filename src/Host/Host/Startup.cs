/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Spudmash Media Pty Ltd. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Host.Configuration;
using IdentityServer4.Contrib.AwsDynamoDB;
using Microsoft.Extensions.Logging;
using IdentityServer4.Contrib.AwsDynamoDB.Repositories;

namespace Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIs4DynamoDB(Configuration);

            ////inmemory
            //services.AddIdentityServer()
            //.AddDeveloperSigningCredential()
            //.AddInMemoryClients(Clients.Get())
            //.AddInMemoryApiResources(Resources.GetApiResources())
            //.AddInMemoryIdentityResources(Resources.GetIdentityResources())
            //.AddTestUsers(TestUsers.Users);

            //dynamodb
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddTestUsers(TestUsers.Users)
                    .AddClientStore<ClientRepository>()
                    .AddResourceStore<ResourceRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseMvc();

            app.UseIdentityServer();
        }
    }
}
