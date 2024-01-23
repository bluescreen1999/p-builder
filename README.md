# Project Builder (p-builder)

Welcome to Project Builder, a tool to streamline your project development process.

## What is the primary purpose of this tool?

P-builder is a command-line tool designed to simplify the creation of .NET projects by automating the setup process. It allows you to quickly initialize a new solution with multiple projects, such as a console application, class library, and web API, along with corresponding test projects.

## Benefits

- **Time Efficiency:** `p-builder` saves you time by automating the manual steps involved in setting up a new .NET solution.
- **Consistent Structure:** Ensure a consistent project structure across your solutions, making it easier to navigate and maintain.
- **Package Management:** Effortlessly add and manage `NuGet packages` like `FluentAssertions` for robust testing.
- **Clear Instructions:** The tool comes with clear instructions and a user-friendly interface for a smooth onboarding experience.

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

Do not forget to add these to `PropertGroup` in `.csproj` file:

```
<PackAsTool>true</PackAsTool>
<ToolCommandName>p-builder</ToolCommandName>
<PackageOutputPath>./pkg</PackageOutputPath>
```
