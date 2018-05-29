/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Spudmash Media Pty Ltd. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace IdentityServer4.Contrib.AwsDynamoDB.Models
{
    [DynamoDBTable("ApiResource")]
    public class ApiResourceDynamoDB
    {
        [DynamoDBHashKey]
        public string Name { get; set; }

        [DynamoDBProperty]
        public List<string> ScopeNames { get; set; }

        /// <summary>
        /// Gets or sets the json serialized ApiResource object
        /// </summary>
        /// <value>The json string.</value>
        [DynamoDBProperty]
        public string JsonString { get; set; }
    }
}
