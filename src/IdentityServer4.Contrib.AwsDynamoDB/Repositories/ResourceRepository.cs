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
    /// <summary>
    /// Resource repository.
    /// </summary>
    public class ResourceRepository : IResourceStore
    {
        private readonly IAmazonDynamoDB client;
        private readonly ILogger<ResourceRepository> logger;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:IdentityServer4.Contrib.AwsDynamoDB.Repositories.ResourceRepository"/> class.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="logger">Logger.</param>
        public ResourceRepository(IAmazonDynamoDB client, ILogger<ResourceRepository> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        /// <summary>
        /// Finds the API resource async.
        /// </summary>
        /// <returns>The API resource async.</returns>
        /// <param name="name">Name.</param>
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the API resources by scope async.
        /// </summary>
        /// <returns>The API resources by scope async.</returns>
        /// <param name="scopeNames">Scope names.</param>
        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the identity resources by scope async.
        /// </summary>
        /// <returns>The identity resources by scope async.</returns>
        /// <param name="scopeNames">Scope names.</param>
        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all resources async.
        /// </summary>
        /// <returns>The all resources async.</returns>
        public Task<Resources> GetAllResourcesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
