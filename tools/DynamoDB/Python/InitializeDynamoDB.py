import boto3
import json

# Get the service resource
ddb = boto3.client('dynamodb')

print('\n\n\n *** Initialize IdentityServer4.Contrib.AwsDynamoDB Environment ***\n\n')

# Create Client Table
try:
    client_response = ddb.create_table(
        TableName='Client',
        KeySchema=[
            {
                'AttributeName': 'ClientId',
                'KeyType': 'HASH'
            }
        ],
        GlobalSecondaryIndexes=[
            {
                'IndexName': 'JsonString-Index',
                'KeySchema': [
                    {
                        'AttributeName': 'JsonString',
                        'KeyType' : 'HASH'
                    }
                ],
                'Projection': {
                    'ProjectionType': 'ALL'
                },
                'ProvisionedThroughput': {
                    'ReadCapacityUnits': 5,
                    'WriteCapacityUnits': 5
                }
            }
        ],
        AttributeDefinitions=[
            {
                'AttributeName': 'ClientId',
                'AttributeType': 'S'
            },
            {
                'AttributeName': 'JsonString',
                'AttributeType': 'S'
            }
        ],
        ProvisionedThroughput={
            'ReadCapacityUnits': 5,
            'WriteCapacityUnits': 5
        }
    )
    print("+Client table created")
except:
    print("-Client table already exists")

# Create ApiResource Table
try:
    client_response = ddb.create_table(
        TableName='ApiResource',
        KeySchema=[
            {
                'AttributeName': 'Name',
                'KeyType': 'HASH'
            }
        ],
        GlobalSecondaryIndexes=[
            {
                'IndexName': 'JsonString-Index',
                'KeySchema': [
                    {
                        'AttributeName': 'JsonString',
                        'KeyType' : 'HASH'
                    }
                ],
                'Projection': {
                    'ProjectionType': 'ALL'
                },
                'ProvisionedThroughput': {
                    'ReadCapacityUnits': 5,
                    'WriteCapacityUnits': 5
                }
            }
        ],
        AttributeDefinitions=[
            {
                'AttributeName': 'Name',
                'AttributeType': 'S'
            },
            {
                'AttributeName': 'JsonString',
                'AttributeType': 'S'
            }
        ],
        ProvisionedThroughput={
            'ReadCapacityUnits': 5,
            'WriteCapacityUnits': 5
        }
    )
    print("+ApiResource table created")
except:
    print("-ApiResource table already exists")

# Create IdentityResource Table
try:
    client_response = ddb.create_table(
        TableName='IdentityResource',
        KeySchema=[
            {
                'AttributeName': 'Name',
                'KeyType': 'HASH'
            }
        ],
        GlobalSecondaryIndexes=[
            {
                'IndexName': 'JsonString-Index',
                'KeySchema': [
                    {
                        'AttributeName': 'JsonString',
                        'KeyType' : 'HASH'
                    }
                ],
                'Projection': {
                    'ProjectionType': 'ALL'
                },
                'ProvisionedThroughput': {
                    'ReadCapacityUnits': 5,
                    'WriteCapacityUnits': 5
                }
            }
        ],
        AttributeDefinitions=[
            {
                'AttributeName': 'Name',
                'AttributeType': 'S'
            },
            {
                'AttributeName': 'JsonString',
                'AttributeType': 'S'
            }
        ],
        ProvisionedThroughput={
            'ReadCapacityUnits': 5,
            'WriteCapacityUnits': 5
        }
    )
    print('+IdentityResource table created')
except:
    print('-IdentityResource table already exists')

# Create PersistedGrant Table
try:
    client_response = ddb.create_table(
        TableName='PersistedGrant',
        KeySchema=[
            {
                'AttributeName': 'Key',
                'KeyType': 'HASH'
            }
        ],
        GlobalSecondaryIndexes=[
            {
                'IndexName': 'ClientId-Index',
                'KeySchema': [
                    {
                        'AttributeName': 'ClientId',
                        'KeyType' : 'HASH'
                    }
                ],
                'Projection': {
                    'ProjectionType': 'ALL'
                },
                'ProvisionedThroughput': {
                    'ReadCapacityUnits': 5,
                    'WriteCapacityUnits': 5
                }
            },
            {
                'IndexName': 'SubjectId-Index',
                'KeySchema': [
                    {
                        'AttributeName': 'SubjectId',
                        'KeyType' : 'HASH'
                    }
                ],
                'Projection': {
                    'ProjectionType': 'ALL'
                },
                'ProvisionedThroughput': {
                    'ReadCapacityUnits': 5,
                    'WriteCapacityUnits': 5
                }
            },
            {
                'IndexName': 'Type-Index',
                'KeySchema': [
                    {
                        'AttributeName': 'Type',
                        'KeyType' : 'HASH'
                    }
                ],
                'Projection': {
                    'ProjectionType': 'ALL'
                },
                'ProvisionedThroughput': {
                    'ReadCapacityUnits': 5,
                    'WriteCapacityUnits': 5
                }
            }
        ],
        AttributeDefinitions=[
            {
                'AttributeName': 'Key',
                'AttributeType': 'S'
            },
            {
                'AttributeName': 'ClientId',
                'AttributeType': 'S'
            },
            {
                'AttributeName': 'SubjectId',
                'AttributeType': 'S'
            },
            {
                'AttributeName': 'Type',
                'AttributeType': 'S'
            }     
        ],
        ProvisionedThroughput={
            'ReadCapacityUnits': 5,
            'WriteCapacityUnits': 5
        }
    )
    print('+PersistedGrant table created')
except:
    print('-PersistedGrant table already exists')


print ('\n\n **** Process completed ***\n\n')