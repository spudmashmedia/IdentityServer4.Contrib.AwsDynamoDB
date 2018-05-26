#!/usr/bin/python

import sys, boto3, json, getopt

def main(argv):
   ddbTablePrefix = ''
   try:
      opts, args = getopt.getopt(argv,"hp",["prefix="])
   except getopt.GetoptError:
      print 'RemoveDynamoDB.py -p <ddbTablePrefix>x'
      sys.exit(2)
   for opt, arg in opts:
      if opt == '-h':
         print 'RemoveDynamoDB.py -p <ddbTablePrefix>h'
         sys.exit()
      elif opt in ("-p", "--prefix"):
         ddbTablePrefix = arg
         print 'DynamoDB Table prefix is: ', ddbTablePrefix
         DeleteTables(ddbTablePrefix)
         sys.exit()
      else:
          assert False, "unhandled exception"

def DeleteTables(prefix):
    print 'prefix ', prefix

    if prefix:
        prefix = prefix + '-'
    else:
        prefix = "" # make sure blank

    # Get the service resource
    ddb = boto3.client('dynamodb')

    print('\n\n\n *** Delete IdentityServer4.Contrib.AwsDynamoDB Environment ***\n\n')

    ddb.delete_table(TableName=prefix+'Client')
    print('-removed Client table')

    ddb.delete_table(TableName=prefix+'ApiResource')
    print('-removed ApiResource table')

    ddb.delete_table(TableName=prefix+'IdentityResource')
    print('-removed IdentityResource table')

    ddb.delete_table(TableName=prefix+'PersistedGrant')
    print('-removed PersistedGrant table')

    print ('\n\n **** Process completed ***\n\n')


if __name__ == "__main__":
   main(sys.argv[1:])