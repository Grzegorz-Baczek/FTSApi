# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade src\FTS.Core\FTS.Core.csproj
4. Upgrade src\FTS.Application\FTS.Application.csproj
5. Upgrade src\FTS.Infrastructure\FTS.Infrastructure.csproj
6. Upgrade src\FTS.App\FTS.App.csproj
7. Upgrade src\FTS.Api\FTS.Api.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|
| (none)                                         | No projects excluded        |

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                            | Current Version | New Version | Description                                                          |
|:--------------------------------------------------------|:---------------:|:-----------:|:---------------------------------------------------------------------|
| Blazored.LocalStorage                                   | 4.3.0           | 4.3.0       | Package is deprecated, replace if alternative available              |
| MediatR.Extensions.Microsoft.DependencyInjection        | 11.0.0          |             | Deprecated; should be replaced by MediatR                            |
| Microsoft.AspNetCore.Authentication.JwtBearer            | 8.0.23          | 10.0.3      | Recommended for .NET 10.0                                            |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore        | 8.0.23          | 10.0.3      | Recommended for .NET 10.0                                            |
| Microsoft.EntityFrameworkCore                            | 9.0.3           | 10.0.3      | Recommended for .NET 10.0                                            |
| Microsoft.EntityFrameworkCore.Design                     | 9.0.4           | 10.0.3      | Recommended for .NET 10.0                                            |
| Microsoft.EntityFrameworkCore.SqlServer                  | 9.0.3           | 10.0.3      | Recommended for .NET 10.0                                            |
| Microsoft.EntityFrameworkCore.Tools                      | 9.0.3           | 10.0.3      | Recommended for .NET 10.0                                            |
| Microsoft.Extensions.DependencyInjection                 | 9.0.3           | 10.0.3      | Recommended for .NET 10.0                                            |
| Microsoft.Extensions.DependencyInjection.Abstractions    | 9.0.3           | 10.0.3      | Recommended for .NET 10.0                                            |
| Microsoft.Extensions.Hosting.Abstractions                | 9.0.3           | 10.0.3      | Recommended for .NET 10.0                                            |
| Microsoft.Extensions.Identity.Stores                     | 8.0.23          | 10.0.3      | Recommended for .NET 10.0                                            |

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### FTS.Core modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.Extensions.Identity.Stores should be updated from `8.0.23` to `10.0.3` (*recommended for .NET 10.0*)

#### FTS.Application modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.EntityFrameworkCore should be updated from `9.0.3` to `10.0.3` (*recommended for .NET 10.0*)
  - Microsoft.Extensions.DependencyInjection.Abstractions should be updated from `9.0.3` to `10.0.3` (*recommended for .NET 10.0*)

#### FTS.Infrastructure modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.Authentication.JwtBearer should be updated from `8.0.23` to `10.0.3` (*recommended for .NET 10.0*)
  - Microsoft.AspNetCore.Identity.EntityFrameworkCore should be updated from `8.0.23` to `10.0.3` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore should be updated from `9.0.3` to `10.0.3` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore.SqlServer should be updated from `9.0.3` to `10.0.3` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore.Tools should be updated from `9.0.3` to `10.0.3` (*recommended for .NET 10.0*)
  - Microsoft.Extensions.DependencyInjection should be updated from `9.0.3` to `10.0.3` (*recommended for .NET 10.0*)
  - Microsoft.Extensions.Hosting.Abstractions should be updated from `9.0.3` to `10.0.3` (*recommended for .NET 10.0*)

#### FTS.App modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.Authentication.JwtBearer should be updated from `8.0.23` to `10.0.3` (*recommended for .NET 10.0*)
  - Blazored.LocalStorage `4.3.0` is deprecated; keep current version as no replacement is available

#### FTS.Api modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.EntityFrameworkCore.Design should be updated from `9.0.4` to `10.0.3` (*recommended for .NET 10.0*)
