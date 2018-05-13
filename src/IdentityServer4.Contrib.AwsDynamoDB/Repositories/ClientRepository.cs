/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Spudmash Media Pty Ltd. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Logging;
using Amazon.DynamoDBv2;
using IdentityServer4.Contrib.AwsDynamoDB.Models;
using IdentityServer4.Contrib.AwsDynamoDB.Models.Extensions;

namespace IdentityServer4.Contrib.AwsDynamoDB.Repositories
{
    public class ClientRepository : IClientStore
    {
        private readonly IAmazonDynamoDB client;
        private readonly ILogger<ClientRepository> logger;

        public ClientRepository(IAmazonDynamoDB client, ILogger<ClientRepository> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            Client response = null;
            try
            {
                using(var context = new DynamoDBContext(client)){
                    var batch = context.QueryAsync<ClientDynamoDB>(clientId);

                    var dataset = await batch.GetRemainingAsync();

                    response = dataset?.First().GetClient();
                }
            }
            catch(Exception ex){
                logger.LogError(default(EventId), ex, "ClientRepository.FindClientByIdAsync failed with clientId {clientId}", clientId);
                throw;
            }

            return response;
        }
    }
}
