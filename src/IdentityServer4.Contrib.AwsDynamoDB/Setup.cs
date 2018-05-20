/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Spudmash Media Pty Ltd. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2;
using IdentityServer4.Contrib.AwsDynamoDB.Repositories;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4.Contrib.AwsDynamoDB
{
    /// <summary>
    /// Use this extension method to setup 
    /// 
    /// public void ConfigureServices(IServiceCollection services)
    /// {
    ///     services.AddMvc();
    ///
    ///     services.SetupIs4DynamoDB(Configuration);
    ///   ...
    /// 
    /// </summary>
    public static class Setup
    {
        public static IServiceCollection AddIs4DynamoDB(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddDefaultAWSOptions(configuration.GetAWSOptions());
            collection.AddAWSService<IAmazonDynamoDB>();
            collection.AddTransient<IClientStore, ClientRepository>();
            collection.AddTransient<IResourceStore, ResourceRepository>();
            collection.AddTransient<IPersistedGrantStore, PersistedGrantRepository>();

            return collection;
        }

        public static void SeedDynamoDB(
            this IServiceCollection collection,
            IConfiguration configuration,
            IEnumerable<Client> clientItems,
            IEnumerable<ApiResource> apiResourceItems,
            IEnumerable<IdentityResource> identityResourceItems
        )
        {
            var serviceProvider = collection.BuildServiceProvider();

            var clientRepo = serviceProvider.GetService<ClientRepository>();
            var resourceRepo = serviceProvider.GetService<ResourceRepository>();

            if(clientItems.Any()){
                clientItems.ToList().ForEach(async item => await clientRepo.StoreClientAsync(item));
            }

            if(apiResourceItems.Any()){
                apiResourceItems.ToList().ForEach(async item => await resourceRepo.StoreApiResource(item));
            }

            if (apiResourceItems.Any())
            {
                identityResourceItems.ToList().ForEach(async item => await resourceRepo.StoreIdentityResource(item));
            }
        }
    }
}
