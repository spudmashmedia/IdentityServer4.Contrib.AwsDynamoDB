/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Spudmash Media Pty Ltd. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Amazon.DynamoDBv2.DataModel;

namespace IdentityServer4.Contrib.AwsDynamoDB.Models
{
    [DynamoDBTable("Is4Client")]
    public class ClientDynamoDB
    {
        [DynamoDBHashKey]
        public string ClientId { get; set; }

        [DynamoDBProperty]
        public string JsonString { get; set; }
    }
}
