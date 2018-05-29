/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Spudmash Media Pty Ltd. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Amazon.DynamoDBv2.DataModel;

namespace IdentityServer4.Contrib.AwsDynamoDB.Models
{
    [DynamoDBTable("IdentityResource")]
    public class IdentityResourceDynamoDB
    {
        [DynamoDBHashKey]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the json serialized IdentityResource object.
        /// </summary>
        /// <value>The json string.</value>
        [DynamoDBProperty]
        public string JsonString { get; set; }
    }
}
