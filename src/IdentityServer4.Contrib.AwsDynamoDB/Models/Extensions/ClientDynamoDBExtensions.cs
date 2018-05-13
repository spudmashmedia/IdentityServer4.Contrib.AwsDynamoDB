/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Spudmash Media Pty Ltd. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;
using Newtonsoft.Json;

namespace IdentityServer4.Contrib.AwsDynamoDB.Models.Extensions
{
    public static class ClientDynamoDBExtensions
    {
        public static Client GetClient(this ClientDynamoDB cd){
            if (cd == null) return null;

            return JsonConvert.DeserializeObject<Client>(cd.JsonString);
        }

        public static IEnumerable<Client> GetClients(this IEnumerable<ClientDynamoDB> cd){
            if (cd == null) return null;

            return cd.Select(item => item.GetClient());
        }

        public static ClientDynamoDB GetClientDynamoDB(this Client cd)
        {
            if (cd == null || string.IsNullOrEmpty(cd.ClientId)) return null;

            return new ClientDynamoDB
            {
                ClientId = cd.ClientId,
                JsonString = JsonConvert.SerializeObject(cd)
            };
        }

        public static IEnumerable<ClientDynamoDB> ClientDynamoDBs(this IEnumerable<Client> cd)
        {
            if (cd == null || cd.Count() == 0) return null;

            return cd.Select(item => item.GetClientDynamoDB());
        }
    }
}
