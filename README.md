# Project Builder (p-builder)

Welcome to Project Builder, a tool to streamline your project development process.

## Getting Started

### 1. Navigate to the Source Directory

Open your terminal and navigate to the `src/p-builder/` directory.

```sh
cd src/p-builder/
```

### 2. Run the following commands:

```sh
dotnet pack
dotnet tool install --global --add-source .\pkg\ p-builder
```

Do not forget to add these tagsto your `.csproj` file:

```
<PackAsTool>true</PackAsTool>
<ToolCommandName>p-builder</ToolCommandName>
<PackageOutputPath>./pkg</PackageOutputPath>
```
