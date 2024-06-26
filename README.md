# APIs.guru-File-Loader
A tiny application for downloading all OpenAPI documentations in JSON and YAML provided by [Apis.guru](https://apis.guru)

## Run Application
1. Open solution in Visual Studio and install package Newtonsoft.json
2. Build solution
3. Run the built console application and specifiy the root directory where you want to store the downloaded OpenAPI documentations as the first argument (e.g. `skotstein.research.rest.apiguru.loader.exe C:\temp\openApis`)

## Outcome
First, the application downloads and parses a JSON file that lists all available APIs, including meta information and available versions for each API (see [https://api.apis.guru/v2/list.json](https://api.apis.guru/v2/list.json)). Then, the application creates a sub-directory for each API within the specified root directory. To avoid naming conflicts and issues with special characters, we number these sub-directories consecutively rather than using the names of the APIs. If you wish to use the API name instead for naming sub-directories, add the `-vk` or `-verbosekeys` option as a second argument. 
You will find a meta file called 'alias.json' in the root directory. This file exposes a mapping of sub-directory names to the names of the respective APIs.

Within the sub-directory of an API, you will find a file called 'info.json' that contains meta information of the respective API and sub directories for each version. These sub-directories use the same naming schema as the previously mentioned API's sub-directories (there is a 'alias.json' file within the sub-directory of each API that maps the version's sub-directory names to the name/number of the API version.

Finally, a sub-directory of a particular API version contains the OpenAPI documentations in JSON and YAML.

The file structure may look as follows:

* `\<root>`
  * `\0`
    * `info.json`
    * `alias.json`
    * `\0`
    * `\1`
      * `swagger.json`
      * `swagger.yaml`
  * `\1`
  * ...
  * `alias.json`


## Dependencies
* Newtonsoft.json (Install package via Package Manager in Visual Studio: `PM > Install-Package Newtonsoft.Json -Version 12.0.3`
