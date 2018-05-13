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
using IdentityServer4.Contrib.AwsDynamoDB.Models;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;

namespace IdentityServer4.Contrib.AwsDynamoDB.Repositories
{
    public class PersistedGrantRepository : IPersistedGrantStore
    {
        private readonly IAmazonDynamoDB client;
        private readonly ILogger<PersistedGrantRepository> logger;

        public PersistedGrantRepository(IAmazonDynamoDB client, ILogger<PersistedGrantRepository> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            IEnumerable<PersistedGrant> result = null;

            try
            {
                using (var context = new DynamoDBContext(client))
                {
                    var obj = await context.QueryAsync<PersistedGrantDynamoDB>(subjectId).GetRemainingAsync();

                    result = obj.Select(item => item.GetPersistedGrant());
                }
            }
            catch (Exception ex)
            {
                logger.LogError(default(EventId), ex, "PersistedGrantRepository.GetAsync failed with subjectId {subjectId}", subjectId);
                await Task.FromException(ex);
            }

            await Task.FromResult(result);
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            PersistedGrant result = null;

            try{
                using(var context = new DynamoDBContext(client)){
                    var obj = await context.QueryAsync<PersistedGrantDynamoDB>(key).GetRemainingAsync();

                    result = obj?.First().GetPersistedGrant();
                }
            }
            catch(Exception ex){
                logger.LogError(default(EventId), ex, "PersistedGrantRepository.GetAsync failed with key {key}", key);
                await Task.FromException(ex);
            }

            await Task.FromResult(result);
        }

        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                using(var context = new DynamoDBContext(client)){
                    // find object first
                    var batch = await context.QueryAsync<PersistedGrantDynamoDB>(key).GetRemainingAsync();
                    var item = batch?.First();

                    // check if object exists
                    if(item == null){
                        await Task.FromResult(0);
                        return;
                    }

                    // delete object
                    await context.DeleteAsync(item);
                }    
            }
            catch(Exception ex){
                logger.LogError(default(EventId), ex, "PersistedGrantRepository.RemoveAsync failed with key {key}", key);
                await Task.FromException(ex);
                return;
            }

            await Task.CompletedTask;
        }

        public async Task StoreAsync(PersistedGrant grant)
        {
            try
            {
                using (var context = new DynamoDBContext(client))
                {
                    await context.SaveAsync<PersistedGrantDynamoDB>(grant.GetPersistedGrantDynamoDB());
                }
            }
            catch(Exception ex){
                logger.LogError(default(EventId), ex, "PersistedGrantRepository.StoreAsync failed with PersistedGrant {grant}", grant);
                await Task.FromException(ex);
                return;
            }

            await Task.CompletedTask;
        }
    }
}
