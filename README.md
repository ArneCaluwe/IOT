# My IOT Hub
## API
This api is built to collect and distribute sensor data gathered from Arduino or other devices on a local network. 

The focus of the application is to experiment with new technologies, tools and versions. 

## Tools and Integrations
### OpenTelemetry 
[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
> High-quality, ubiquitous, and portable telemetry to enable effective observability

|Useful links   |       |
| ---           | ---   |
|Website        | [opentelemetry.io](https://opentelemetry.io) |
| Docs          | [.NET specific docs](https://opentelemetry.io/docs/languages/dotnet/) |



### Scalar
> The modern open-source developer experience platform for your APIs.

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

|Useful links   |       |
| ---           | ---   |
|Website        | [scalar.com](https://scalar.com) |
| Docs          | [.NET specific docs](https://guides.scalar.com/scalar/scalar-api-references/integrations/net-aspnet-core/integration) |

## Project setup
### Secrets and configuration
#### Secrets
> Secrets can be used to add confidential data to source files without adding it to source control. 
To initialize secrets type `dotnet user-secrets init` in terminal.

> To add or update secrets, type `dotnet user-secrets set "{secret key seperated by dots}" "{secret value}"`in terminal.

##### application secrets:
```json
{
    "Licenses":{
        "Mediatr": "your Mediatr license"
    }
}
```

## Project structure
### References
```
      API       Business
       ↓    ↙      ↓
    Domain  ←     DAL
```
     