import boto3
import json

# Get the service resource
ddb = boto3.client('dynamodb')

print('\n\n\n *** Delete IdentityServer4.Contrib.AwsDynamoDB Environment ***\n\n')

ddb.delete_table(TableName='Client')
print('-removed Client table')

ddb.delete_table(TableName='ApiResource')
print('-removed ApiResource table')

ddb.delete_table(TableName='IdentityResource')
print('-removed IdentityResource table')

ddb.delete_table(TableName='PersistedGrant')
print('-removed PersistedGrant table')

print ('\n\n **** Process completed ***\n\n')