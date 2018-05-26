#!/usr/bin/python

import sys, boto3, json, getopt

def main(argv):
   ddbTablePrefix = ''
   try:
      opts, args = getopt.getopt(argv,"hp",["prefix="])
   except getopt.GetoptError:
      print 'initializeDynamoDB.py -p <ddbTablePrefix>x'
      sys.exit(2)
   for opt, arg in opts:
      if opt == '-h':
         print 'initializeDynamoDB.py -p <ddbTablePrefix>h'
         sys.exit()
      elif opt in ("-p", "--prefix"):
         ddbTablePrefix = arg
         print 'DynamoDB Table prefix is: ', ddbTablePrefix
         CreateTables(ddbTablePrefix)
         sys.exit()
      else:
          assert False, "unhandled exception"



def CreateTables(prefix):
    print 'prefix ', prefix

    if prefix:
        prefix = prefix + '-'
    else:
        prefix = "" # make sure blank

    
    # Get the service resource
    ddb = boto3.client('dynamodb')

    print('\n\n\n *** Initialize IdentityServer4.Contrib.AwsDynamoDB Environment ***\n\n')

    # Create Client Table
    try:
        client_response = ddb.create_table(
            TableName= prefix + 'Client',
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
            TableName=prefix + 'ApiResource',
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
            TableName=prefix + 'IdentityResource',
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
            TableName=prefix + 'PersistedGrant',
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


if __name__ == "__main__":
   main(sys.argv[1:])