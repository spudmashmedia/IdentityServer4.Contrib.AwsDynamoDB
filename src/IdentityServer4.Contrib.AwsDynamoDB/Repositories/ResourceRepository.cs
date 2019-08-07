/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Spudmash Media Pty Ltd. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using IdentityServer4.Contrib.AwsDynamoDB.Models;
using IdentityServer4.Contrib.AwsDynamoDB.Models.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;

namespace IdentityServer4.Contrib.AwsDynamoDB.Repositories
{
    /// <summary>
    /// Resource repository.
    /// </summary>
    public class ResourceRepository : IResourceStore
    {
        private readonly IAmazonDynamoDB client;
        private readonly DynamoDBContextConfig ddbConfig;
        private readonly ILogger<ResourceRepository> logger;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:IdentityServer4.Contrib.AwsDynamoDB.Repositories.ResourceRepository"/> class.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="logger">Logger.</param>
        public ResourceRepository(IAmazonDynamoDB client, DynamoDBContextConfig ddbConfig, ILogger<ResourceRepository> logger)
        {
            this.client = client;
            this.ddbConfig = ddbConfig;
            this.logger = logger;
        }

        /// <summary>
        /// Finds the API resource async.
        /// </summary>
        /// <returns>The API resource async.</returns>
        /// <param name="name">Name.</param>
        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) { return null; }

            ApiResource response = null;

            try
            {
                using(var context = new DynamoDBContext(client, ddbConfig))
                {
                    var dataset = await context.QueryAsync<ApiResourceDynamoDB>(name).GetRemainingAsync();
                    response = dataset?.First()?.GetApiResource();
                }
            }
            catch(Exception ex)
            {
                logger.LogError(default(EventId), ex, "ResourceRepository.FindApiResourceAsync failed with name {name}", name);
                await Task.FromException(ex);
                return null;
            }

            return response;
        }

        /// <summary>
        /// ----------------------------------------------------
        /// ***HACK***
        /// Review DynamoDB Scan condition!!!
        /// ----------------------------------------------------
        /// DYNAMO CAN WITH ARRAY CONDITIONS DOES NOT
        /// 
        ///     IEnumerable<ScanCondition> conditions = scopeNames.Select(sn => new ScanCondition(nameof(sn), ScanOperator.Equal, sn));
        /// 
        ///     var batch = context.ScanAsync<ApiResourceDynamoDB>(conditions, new DynamoDBOperationConfig
        //            {
        //                ConditionalOperator = ConditionalOperatorValues.Or
        //            });
        /// 
        /// ----------------------------------------------------
        /// Finds the API resources by scope async.
        /// 
        /// var apis =
        ///     from api in _context.ApiResources
        ///     where api.Scopes.Where(x => names.Contains(x.Name)).Any()
        ///             select api;
        /// 
        /// see https://github.com/diogodamiani/IdentityServer4.Contrib.MongoDB/blob/6f13fc65c9f06d9d24f8df7cad18fa2a1257072b/src/IdentityServer4.MongoDB/Stores/ResourceStore.cs#L55
        /// </summary>
        /// <returns>The API resources by scope async.</returns>
        /// <param name="scopeNames">Scope names.</param>
        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) return null;

            List<ApiResource> response = new List<ApiResource>();

            try
            {
                using (var context = new DynamoDBContext(client, ddbConfig))
                {
                    foreach (var sn in scopeNames)
                    {
                        var condition = new ScanCondition(nameof(ApiResourceDynamoDB.ScopeNames), ScanOperator.Contains, sn);
                        var batch = context.ScanAsync<ApiResourceDynamoDB>(new List<ScanCondition>{condition});
                        while(!batch.IsDone)
                        {
                            var dataset = await batch.GetNextSetAsync();
                            if(dataset.Any()){
                              var resources = dataset.Select (items => items.GetApiResource ())?.Distinct ();
                                foreach (var i in resources) {
                                    if (!response.Exists(x => x.Name == i.Name)) { response.Add (i); }
                                }                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(default(EventId), ex, "ResourceRepository.FindApiResourcesByScopeAsync failed");
                await Task.FromException(ex);
                return null;
            }

            return response;
        }


        /// <summary>
        /// ----------------------------------------------------
        /// ***HACK***
        /// Review DynamoDB Scan condition!!!
        /// ----------------------------------------------------
        /// DYNAMO CAN WITH ARRAY CONDITIONS DOES NOT
        /// 
        ///     IEnumerable<ScanCondition> conditions = scopeNames.Select(sn => new ScanCondition(nameof(sn), ScanOperator.Equal, sn));
        /// 
        ///     var batch = context.ScanAsync<IdentityResourceDynamoDB>(conditions, new DynamoDBOperationConfig
        //            {
        //                ConditionalOperator = ConditionalOperatorValues.Or
        //            });
        /// 
        /// ----------------------------------------------------
        /// Finds the identity resources by scope async.
        /// 
        /// var resources =
        ///      from identityResource in _context.IdentityResources
        ///      where scopes.Contains(identityResource.Name)
        ///      select identityResource;
        /// 
        /// see https://github.com/diogodamiani/IdentityServer4.Contrib.MongoDB/blob/6f13fc65c9f06d9d24f8df7cad18fa2a1257072b/src/IdentityServer4.MongoDB/Stores/ResourceStore.cs#L72
        /// 
        /// </summary>
        /// <returns>The identity resources by scope async.</returns>
        /// <param name="scopeNames">Scope names.</param>
        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) return null;


            List<IdentityResource> response = new List<IdentityResource>();

            try
            {
                using (var context = new DynamoDBContext(client, ddbConfig))
                {
                    foreach(var sn in scopeNames){
                        var dataset = await context.QueryAsync<IdentityResourceDynamoDB>(sn).GetRemainingAsync();   
                        if(dataset.Any()){
                            response.AddRange(dataset.Select(item => item.GetIdentityResource()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(default(EventId), ex, "ResourceRepository.FindIdentityResourcesByScopeAsync failed");
                await Task.FromException(ex);
                return null;
            }

            return response;
        }

        /// <summary>
        /// Gets all resources async.
        /// </summary>
        /// <returns>The all resources async.</returns>
        public async Task<Resources> GetAllResourcesAsync()
        {
            var response = new Resources();
            response.ApiResources = await GetAllApiResources();
            response.IdentityResources = await GetAllIdentityResources();

            return response;
        }

        /// <summary>
        /// Gets all identity resources.
        /// </summary>
        /// <returns>The all identity resources.</returns>
        private async Task<ICollection<IdentityResource>> GetAllIdentityResources()
        {
            List<IdentityResource> response = null;

            try
            {
                using (var context = new DynamoDBContext(client, ddbConfig))
                {
                    var batch = context.ScanAsync<IdentityResourceDynamoDB>(null);
                    while(!batch.IsDone)
                    {
                        var dataset = await batch.GetNextSetAsync();

                        if (dataset.Any())
                        {
                            response = dataset.Select(item => item.GetIdentityResource()).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(default(EventId), ex, "ResourceRepository.GetAllIdentityResources failed");
                await Task.FromException(ex);
                return null;
            }

            return response;
        }

        /// <summary>
        /// Gets all API resources.
        /// </summary>
        /// <returns>The all API resources.</returns>
        private async Task<ICollection<ApiResource>> GetAllApiResources()
        {
            List<ApiResource> response = null;

            try
            {
                using (var context = new DynamoDBContext(client, ddbConfig))
                {
                    var dataset = await context.ScanAsync<ApiResourceDynamoDB>(null).GetRemainingAsync();
                    if (dataset.Any())
                    {
                        response = dataset.Select(item => item.GetApiResource()).ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError(default(EventId), ex, "ResourceRepository.GetAllApiResources failed");
                await Task.FromException(ex);
                return null;
            }

            return response;
        }

        /// <summary>
        /// Stores the API resource.
        /// </summary>
        /// <returns>The API resource.</returns>
        /// <param name="item">Item.</param>
        public async Task StoreApiResource(ApiResource item){
            try
            {
                using (var context = new DynamoDBContext(client, ddbConfig))
                {
                    await context.SaveAsync(item.GetApiResourceDynamoDB());
                }
            }
            catch (Exception ex)
            {
                logger.LogError(default(EventId), ex, "ResourceRepository.StoreApiResource failed with ApiResource {item}", item);
                throw;
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Stores the identity resource.
        /// </summary>
        /// <returns>The identity resource.</returns>
        /// <param name="item">Item.</param>
        public async Task StoreIdentityResource(IdentityResource item)
        {
            try
            {
                using (var context = new DynamoDBContext(client, ddbConfig))
                {
                    await context.SaveAsync(item.GetIdentityResourceDynamoDB());
                }
            }
            catch (Exception ex)
            {
                logger.LogError(default(EventId), ex, "ResourceRepository.StoreIdentityResource failed with IdentityResource {item}", item);
                throw;
            }

            await Task.CompletedTask;
        }
    }
}
