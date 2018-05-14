/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Spudmash Media Pty Ltd. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Stores;
using IdentityServer4.Contrib.AwsDynamoDB.Repositories;
using Amazon.DynamoDBv2;
using Host.Configuration;

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

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>();

            //inmemory
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryClients(Clients.Get())
                    .AddInMemoryApiResources(Resources.GetApiResources())
                    .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                    .AddTestUsers(TestUsers.Users);
            

            /*
            //dynamodb
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryClients(Clients.Get())
                    .AddInMemoryApiResources(Resources.GetApiResources())
                    .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                    .AddTestUsers(TestUsers.Users);
            services.AddTransient<IPersistedGrantStore, PersistedGrantRepository>();
            */     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();


            app.UseIdentityServer();
        }

    }
}
