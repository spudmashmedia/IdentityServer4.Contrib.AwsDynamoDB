/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Spudmash Media Pty Ltd. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Amazon.DynamoDBv2.DataModel;

namespace IdentityServer4.Contrib.AwsDynamoDB.Models
{
    [DynamoDBTable("PersistedGrant")]
    public class PersistedGrantDynamoDB
    {
        [DynamoDBHashKey]
        public string Key { get; set; }

        [DynamoDBProperty]
        public string ClientId { get; set; }

        [DynamoDBProperty]
        public string SubjectId { get; set; }

        [DynamoDBProperty]
        public string Type { get; set; }

        /// <summary>
        /// Use AWSSDKUtils.ConvertToUnixEpochSecondsString() to convert 
        /// DateTime to UnixEpoch in seconds represented as a string
        /// </summary>
        /// <value>The creation time.</value>
        [DynamoDBProperty]
        public string CreationTime { get; set; }

        /// <summary>
        /// Use AWSSDKUtils.ConvertToUnixEpochSecondsString() to convert 
        /// DateTime to UnixEpoch in seconds represented as a string
        /// </summary>
        /// <value>The expiration.</value>
        [DynamoDBProperty]
        public string Expiration { get; set; }

        [DynamoDBProperty]
        public string Data { get; set; }
    }
}
