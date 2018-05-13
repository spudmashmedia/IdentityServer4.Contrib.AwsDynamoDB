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
    /// Identity resource dynamo DBE xtensions.
    /// </summary>
    public static class IdentityResourceDynamoDBExtensions
    {
        /// <summary>
        /// Gets the identity resource.
        /// </summary>
        /// <returns>The identity resource.</returns>
        /// <param name="ird">Ird.</param>
        public static IdentityResource GetIdentityResource(this IdentityResourceDynamoDB ird)
        {
            if (ird == null) return null;

            return JsonConvert.DeserializeObject<IdentityResource>(ird.JsonString);
        }

        /// <summary>
        /// Gets the identity resources.
        /// </summary>
        /// <returns>The identity resources.</returns>
        /// <param name="ird">Ird.</param>
        public static IEnumerable<IdentityResource> GetIdentityResources(this IEnumerable<IdentityResourceDynamoDB> ird)
        {
            if (ird == null) return null;

            return ird.Select(item => item.GetIdentityResource());
        }

        /// <summary>
        /// Gets the identity resource dynamo db.
        /// </summary>
        /// <returns>The identity resource dynamo db.</returns>
        /// <param name="ir">Ir.</param>
        public static IdentityResourceDynamoDB GetIdentityResourceDynamoDB(this IdentityResource ir)
        {
            if (ir == null) return null;

            return new IdentityResourceDynamoDB
            {
                Name = ir.Name,
                JsonString = JsonConvert.SerializeObject(ir)
            };
        }

        /// <summary>
        /// Gets the identity resource dynamo DB.
        /// </summary>
        /// <returns>The identity resource dynamo DB.</returns>
        /// <param name="ir">Ir.</param>
        public static IEnumerable<IdentityResourceDynamoDB> GetIdentityResourceDynamoDBs(this IEnumerable<IdentityResource> ir)
        {
            if (ir == null) return null;

            return ir.Select(item => item.GetIdentityResourceDynamoDB());
        }
    }
}
