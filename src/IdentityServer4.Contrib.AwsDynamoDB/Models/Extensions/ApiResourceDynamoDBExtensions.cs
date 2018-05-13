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
    /// <summary>
    /// API resource dynamo DBE xtensions.
    /// </summary>
    public static class ApiResourceDynamoDBExtensions
    {
        /// <summary>
        /// Gets the API resource.
        /// </summary>
        /// <returns>The API resource.</returns>
        /// <param name="apd">Apd.</param>
        public static ApiResource GetApiResource(this ApiResourceDynamoDB apd)
        {
            if (apd == null) return null;

            return JsonConvert.DeserializeObject<ApiResource>(apd.JsonString);
        }

        /// <summary>
        /// Gets the API resources.
        /// </summary>
        /// <returns>The API resources.</returns>
        /// <param name="apd">Apd.</param>
        public static IEnumerable<ApiResource> GetApiResources(this IEnumerable<ApiResourceDynamoDB> apd)
        {
            if (apd == null) return null;

            return apd.Select(item => item.GetApiResource());
        }

        /// <summary>
        /// Gets the API resource dynamo db.
        /// </summary>
        /// <returns>The API resource dynamo db.</returns>
        /// <param name="ap">Ap.</param>
        public static ApiResourceDynamoDB GetApiResourceDynamoDB(this ApiResource ap)
        {
            if (ap == null) return null;

            return new ApiResourceDynamoDB
            {
                Name = ap.Name,
                ScopeNames = ap.Scopes.Select(x => x.Name),
                JsonString = JsonConvert.SerializeObject(ap)
            };
        }

        /// <summary>
        /// Gets the API resource dynamo DB.
        /// </summary>
        /// <returns>The API resource dynamo DB.</returns>
        /// <param name="ap">Ap.</param>
        public static IEnumerable<ApiResourceDynamoDB> GetApiResourceDynamoDBs(this IEnumerable<ApiResource> ap)
        {
            if (ap == null) return null;

            return ap.Select(item => item.GetApiResourceDynamoDB());
        }
    }
}
