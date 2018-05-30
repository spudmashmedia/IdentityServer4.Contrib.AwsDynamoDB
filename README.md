# IdentityServer4.Contrib.AwsDynamoDB

[![Build status](https://ci.appveyor.com/api/projects/status/3mkxmi5y88ln7v05?svg=true)](https://ci.appveyor.com/project/spudmashmedia/identityserver4-contrib-awsdynamodb) [![NuGet](https://img.shields.io/nuget/v/IdentityServer4.Contrib.AwsDynamoDB.svg)](https://www.nuget.org/packages/IdentityServer4.Contrib.AwsDynamoDB/)

- [IdentityServer4.Contrib.AwsDynamoDB](#identityserver4contribawsdynamodb)
- [Summary](#summary)
- [Installation](#installation)
    - [AWS Setup](#aws-setup)
        - [Credentials](#credentials)
        - [AWS CLI](#aws-cli)
        - [Generating DynamoDB Tables](#generating-dynamodb-tables)
    - [Nuget Package](#nuget-package)
- [Quick Setup](#quick-setup)
    - [Startup.cs](#startupcs)
    - [appsettings.json](#appsettingsjson)
    - [License](#license)

# Summary 
AWS DynamoDB Operational &amp; Persisted store for IdentityServer4

# Installation

## AWS Setup

### Credentials
https://docs.aws.amazon.com/cli/latest/userguide/cli-config-files.html


### AWS CLI
See https://docs.aws.amazon.com/cli/latest/userguide/cli-chap-getting-started.html

### Generating DynamoDB Tables
See [Tools | DynamoDB | Python | Readme.md](/tools/DynamoDB/Python/README.md)


## Nuget Package
```
Install-Package IdentityServer4.Contrib.AwsDynamoDB
```

# Quick Setup
## Startup.cs
```
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();

        // 1) setup all services (Also sets up PersistedGrant Store)
        services.AddIs4DynamoDB(Configuration);
        
        // 2) Use ClientRepository + ResourceRepository
        services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddTestUsers(TestUsers.Users)
                .AddClientStore<ClientRepository>()
                .AddResourceStore<ResourceRepository>();
        
        Setup.SeedDynamoDB(
            services,
            Configuration,
            Clients.Get(),
            Resources.GetApiResources(),
            Resources.GetIdentityResources()
        );
    }
```

## appsettings.json
```
{
  "AWS": {
    "Region": "[region]"
  },
  "IdentityServer4.Contrib.AwsDynamoDB": {
    "TableNamePrefix": "[prefix]"
  }
}
```
| **Field** | **Value** |
| --- | --- |
|[region]| AWS region code. E.g. **ap-southeast-2**|
|[prefix]|This value will prefix the POCO tables. E.g. **Development-** will be prefixed to Client DynamoDB table name as **Development-Client**|

## License
MIT