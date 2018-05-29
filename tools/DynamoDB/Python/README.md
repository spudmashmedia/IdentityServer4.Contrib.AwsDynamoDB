# Tools: DynamoDB - Python
Scripts to initialize IdentityServer4.Contrib.AWSDynamoDB working environment

## Installation

### Python + AWS CLI / SDK
Make sure the AWS Python SDK is installed. Instructions found here:
https://aws.amazon.com/sdk-for-python/

### Python Module(s) Dependencies
| Module | Command                          |
| ------ | -------------------------------- |
| robo3  | ``` pip install robo3 --user ``` |

## Usage

| Script               | Description                                                         | Command                               |
| -------------------- | ------------------------------------------------------------------- | ------------------------------------- |
| InitilizeDynamoDB.py | Creates all relevant tables for IdentityServer4.Contrib.AwsDynamoDB | ``` > python InitilizeDynamoDB.py --prefix Development ``` |
| RemoveDynamoDB.py    | Removes all relevant tables for IdentityServer4.Contrib.AwsDynamoDB | ``` > python RemoveDynamoDB.py --prefix Development```    |

## Contributing
1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D

## License
MIT