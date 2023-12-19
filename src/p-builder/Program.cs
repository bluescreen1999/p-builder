using System.Diagnostics;

PrintBanner();

var solutionNameOptionInputSyntax = "--name";

if (args.Length < 2)
{
    PrintInstructions();
    TerminateProgram();
}

var requiredNumberOfArguments = 2;
if (args.Length > requiredNumberOfArguments)
{
    PrintInstructions();
    TerminateProgram();
}

var solutionKeywordOption = args[0].ToLower();
var solutionName = args[1].Replace(" ", "");

if (string.IsNullOrEmpty(solutionKeywordOption) ||
    solutionKeywordOption != solutionNameOptionInputSyntax)
{
    PrintInstructions();
    TerminateProgram();
}

if (string.IsNullOrEmpty(solutionName))
{
    PrintInstructions();
    TerminateProgram();
}

List<string> commands = new List<string>()
{
    $"dotnet new sln -n {solutionName}",

    "mkdir src",
    "mkdir test",

    $"dotnet new console -n \"{solutionName}.Console\" -o src/{solutionName}.Console",

    $"dotnet new classlib -n \"{solutionName}.ClassLib\" -o src/{solutionName}.ClassLib",
    $"dotnet new webapi -n \"{solutionName}.WebApi\" -o src/{solutionName}.WebApi",

    $"dotnet new xunit -n \"{solutionName}.Console.Tests\" -o test/{solutionName}.Console.Tests",
    $"dotnet new xunit -n \"{solutionName}.ClassLib.Tests\" -o test/{solutionName}.ClassLib.Tests",
    $"dotnet new xunit -n \"{solutionName}.WebApi.Tests\" -o test/{solutionName}.WebApi.Tests",

    $"dotnet sln {solutionName}.sln add src/{solutionName}.Console/{solutionName}.Console.csproj",
    $"dotnet sln {solutionName}.sln add src/{solutionName}.ClassLib/{solutionName}.ClassLib.csproj",
    $"dotnet sln {solutionName}.sln add src/{solutionName}.WebApi/{solutionName}.WebApi.csproj",
    $"dotnet sln {solutionName}.sln add test/{solutionName}.Console.Tests/{solutionName}.Console.Tests.csproj",
    $"dotnet sln {solutionName}.sln add test/{solutionName}.ClassLib.Tests/{solutionName}.ClassLib.Tests.csproj",
    $"dotnet sln {solutionName}.sln add test/{solutionName}.WebApi.Tests/{solutionName}.WebApi.Tests.csproj",

    $"dotnet add test/{solutionName}.Console.Tests/{solutionName}.Console.Tests.csproj package FluentAssertions",
    $"dotnet add test/{solutionName}.Console.Tests/{solutionName}.Console.Tests.csproj reference src/{solutionName}.Console/{solutionName}.Console.csproj",

    $"dotnet add test/{solutionName}.ClassLib.Tests/{solutionName}.ClassLib.Tests.csproj package FluentAssertions",
    $"dotnet add test/{solutionName}.ClassLib.Tests/{solutionName}.ClassLib.Tests.csproj reference src/{solutionName}.ClassLib/{solutionName}.ClassLib.csproj",

    $"dotnet add test/{solutionName}.WebApi.Tests/{solutionName}.WebApi.Tests.csproj package FluentAssertions",
    $"dotnet add test/{solutionName}.WebApi.Tests/{solutionName}.WebApi.Tests.csproj reference src/{solutionName}.WebApi/{solutionName}.WebApi.csproj",
};

Process process = new Process
{
    StartInfo = new ProcessStartInfo
    {
        FileName = "powershell",
        RedirectStandardInput = true,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
    }
};

foreach (string command in commands)
{
    Console.WriteLine($"executing \'{command}\' command");

    process.Start();

    process.StandardInput.WriteLine(command);
    process.StandardInput.Close();

    string output = await process.StandardOutput.ReadToEndAsync();
    string error = await process.StandardError.ReadToEndAsync();

    await process.WaitForExitAsync();

    if (!string.IsNullOrEmpty(error))
    {
        PrintError("An ERROR OCCURED :(");
        Console.WriteLine($"Output: {output}\n");
        PrintError($"Error: {error}");
    }
    else
    {
        PrintSuccessMessage($"Command executed :)");
    }
    Console.WriteLine("\n");
}

static void PrintInstructions()
{
    PrintError("Check the instructions again please:");
    Console.WriteLine("p-builder --name \"YourSolutionName\"");
}

static void PrintBanner()
{
    string bannerMessage = """
                   __                  __  __        __                     
                  |  \                |  \|  \      |  \                    
  ______          | $$____   __    __  \$$| $$  ____| $$  ______    ______  
 /      \  ______ | $$    \ |  \  |  \|  \| $$ /      $$ /      \  /      \ 
|  $$$$$$\|      \| $$$$$$$\| $$  | $$| $$| $$|  $$$$$$$|  $$$$$$\|  $$$$$$\
| $$  | $$ \$$$$$$| $$  | $$| $$  | $$| $$| $$| $$  | $$| $$    $$| $$   \$$
| $$__/ $$        | $$__/ $$| $$__/ $$| $$| $$| $$__| $$| $$$$$$$$| $$      
| $$    $$        | $$    $$ \$$    $$| $$| $$ \$$    $$ \$$     \| $$      
| $$$$$$$          \$$$$$$$   \$$$$$$  \$$ \$$  \$$$$$$$  \$$$$$$$ \$$      
| $$                                                                        
| $$                                                                        
 \$$                                                                        
""";
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(bannerMessage);
    ResetForegroundConsoleColor();
}

static void TerminateProgram()
{
    Environment.Exit(1);
}

static void SetForegroundConsoleColorRed()
{
    Console.ForegroundColor = ConsoleColor.Red;
}

static void SetForegroundConsoleColorGreen()
{
    Console.ForegroundColor = ConsoleColor.Green;
}

static void ResetForegroundConsoleColor()
{
    Console.ResetColor();
}

static void PrintError(string errorMessage)
{
    SetForegroundConsoleColorRed();
    Console.WriteLine(errorMessage);
    ResetForegroundConsoleColor();
}

static void PrintSuccessMessage(string message)
{
    SetForegroundConsoleColorGreen();
    Console.WriteLine(message);
    ResetForegroundConsoleColor();
}