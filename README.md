# APIs.guru-File-Loader
A tiny application for downloading all OpenAPI documentations in JSON and YAML provided by [Apis.guru](https://apis.guru)

## Run Application
1. Open solution in Visual Studio and install package Newtonsoft.json
2. Build solution
3. Run console application and specifiy the root directory where you want to store the downloaded OpenAPI documentations as the first argument (e.g. `skotstein.research.rest.apiguru.loader.exe C:\temp\openApis`)

## Dependencies
* Newtonsoft.json (Install package via Package Manager in Visual Studio: `PM > Install-Package Newtonsoft.Json -Version 12.0.3`
