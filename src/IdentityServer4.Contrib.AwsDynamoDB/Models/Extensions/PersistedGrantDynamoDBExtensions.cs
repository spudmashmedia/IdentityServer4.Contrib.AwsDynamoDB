/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Spudmash Media Pty Ltd. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using IdentityServer4.Models;
using Amazon.Util;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer4.Contrib.AwsDynamoDB.Models.Extensions
{
    /// <summary>
    /// Persisted grant dynamo DBE xtensions.
    /// </summary>
    public static class PersistedGrantDynamoDBExtensions
    {
        /// <summary>
        /// Gets the persisted grant.
        /// </summary>
        /// <returns>The persisted grant.</returns>
        /// <param name="pgd">Pgd.</param>
        public static PersistedGrant GetPersistedGrant(this PersistedGrantDynamoDB pgd)
        {
            if (pgd == null) return null;

            return new PersistedGrant
            {
                Key = pgd.Key,
                ClientId = pgd.ClientId,
                SubjectId = pgd.SubjectId,
                Type = pgd.Type,
                CreationTime = AWSSDKUtils.ConvertFromUnixEpochSeconds(int.Parse(pgd.CreationTime)),
                Expiration = AWSSDKUtils.ConvertFromUnixEpochSeconds(int.Parse(pgd.Expiration)),
                Data = pgd.Data
            };
        }

        /// <summary>
        /// Gets the persisted grants.
        /// </summary>
        /// <returns>The persisted grants.</returns>
        /// <param name="pgd">Pgd.</param>
        public static IEnumerable<PersistedGrant> GetPersistedGrants(this IEnumerable<PersistedGrantDynamoDB> pgd)
        {
            if (pgd == null) return null;

            return pgd.Select(item => item.GetPersistedGrant());
        }

        /// <summary>
        /// Gets the persisted grant dynamo db.
        /// </summary>
        /// <returns>The persisted grant dynamo db.</returns>
        /// <param name="pg">Pg.</param>
        public static PersistedGrantDynamoDB GetPersistedGrantDynamoDB(this PersistedGrant pg)
        {
            if (pg == null) return null;

            return new PersistedGrantDynamoDB
            {
                Key = pg.Key,
                ClientId = pg.ClientId,
                SubjectId = pg.SubjectId,
                Type = pg.Type,
                CreationTime = AWSSDKUtils.ConvertToUnixEpochSecondsString(pg.CreationTime),
                Expiration = AWSSDKUtils.ConvertToUnixEpochSecondsString(pg.Expiration),
                Data = pg.Data
            };
        }

        /// <summary>
        /// Gets the persisted grant dynamo DB.
        /// </summary>
        /// <returns>The persisted grant dynamo DB.</returns>
        /// <param name="pg">Pg.</param>
        public static IEnumerable<PersistedGrantDynamoDB> GetPersistedGrantDynamoDBs(this IEnumerable<PersistedGrant> pg)
        {
            if (pg == null) return null;

            return pg.Select(item => item.GetPersistedGrantDynamoDB());
        }
    }
}
