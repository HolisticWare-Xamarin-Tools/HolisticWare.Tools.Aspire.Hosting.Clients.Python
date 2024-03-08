
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
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
                "clients-python-machine-learning",
                $"{Environment.GetEnvironmentVariable("HOME")}/moljac-python/venv/bin/python3",
                "../Clients/Python/simple-machine-learning/",
                new string[] { "test1.py"}
            )
            .WithReference(apiService)
            .GenerateSettings()
            ;

IEnumerable<EndpointAnnotation>? endpoints;
bool endpoints_exist = apiService.Resource.TryGetEndpoints(out endpoints);

builder.AddScriptPythonDjango
    (
        "clients-python-django-web-frontent",
        $"{Environment.GetEnvironmentVariable("HOME")}/moljac-python/venv/bin/python3",
        "../Clients/Python/django/AspireTest/",
        new string[] { "manage.py", "runserver" }
    )
    .WithReference(apiService)
    //.WithServiceBinding(hostPort: 8000, scheme: "http", env: "PORT");
    //.WithEnvironment("CLEINT_ENV_CONFIG_VAR_apiservice", "apiservice");
    // Dashboard??
    //.WithAnnotation(new EndpointAnnotation(ProtocolType.Tcp, uriScheme: "http", name: "django", port: 8000))
    //.GenerateSettings() // testing 
    ;

builder
    .Build()
    .Run();
