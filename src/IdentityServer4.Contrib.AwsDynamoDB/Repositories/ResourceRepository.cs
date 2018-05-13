/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Spudmash Media Pty Ltd. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;

namespace IdentityServer4.Contrib.AwsDynamoDB.Repositories
{
    public class ResourceRepository : IResourceStore
    {
        private readonly IAmazonDynamoDB client;
        private readonly ILogger<ResourceRepository> logger;

        public ResourceRepository(IAmazonDynamoDB client, ILogger<ResourceRepository> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
