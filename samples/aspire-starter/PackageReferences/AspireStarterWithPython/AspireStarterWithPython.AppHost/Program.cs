
using HolisticWare.Tools.Aspire.Hosting.Clients.Python;

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.AspireStarterWithPython_ApiService>("apiservice");

builder.AddProject<Projects.AspireStarterWithPython_Web>("webfrontend")
    .WithReference(apiService);

//https://learn.microsoft.com/en-us/dotnet/api/aspire.hosting.executableresourcebuilderextensions.addexecutable?view=dotnet-aspire-8.0
// pip3 install matplotlib
// brew install matplotlib
builder.AddScriptPython
            (
                "client_python",
                $"{Environment.GetEnvironmentVariable("HOME")}/moljac-python/venv/bin/python3",
                "../Clients/Python/",
                new string[] { "test1.py" }
            )
        .WithReference(apiService);

builder
    .Build()
    .Run();
