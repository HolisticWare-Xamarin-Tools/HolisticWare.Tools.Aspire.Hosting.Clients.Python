using System.Diagnostics;
using Aspire.Hosting;

namespace HolisticWare.Tools.Aspire.Hosting.Clients.Python;

public static partial class 
                                        IDistributedApplicationBuilderMauiExtensions
{
    private static 
        IDistributedApplicationBuilder?
                                         Builder;

    private static 
        List<(string tfm, string device)> 
                                        devices = new List<(string tfm, string device)>();
     

    public static
        string?
                                        Name
    {
        get;
        set;
    }

    public static
        string?
                                        ScriptPython
    {
        get;
        set;
    }

    public static
        IResourceBuilder<ExecutableResource>
                                        AddScriptPython
                                        (
                                            this IDistributedApplicationBuilder? builder,
                                            string name,
                                            string path_python,
                                            string working_directory,
                                            string[] python_script_and_args,
                                            string aspire_settings = "aspire_settings.py"
                                        )
    {
        IResourceBuilder<ExecutableResource>? resource_builder = builder?.AddExecutable
                                                                                (
                                                                                    name,
                                                                                    path_python,
                                                                                    working_directory,
                                                                                    python_script_and_args
                                                                                );
        
        return resource_builder;
    }

    public static
        IResourceBuilder<ExecutableResource>
                                        AddScriptPythonDjango
                                        (
                                            this IDistributedApplicationBuilder? builder,
                                            string name,
                                            string path_python,
                                            string working_directory,
                                            string[] python_script_and_args,
                                            string aspire_settings = "aspire_settings.py"
                                        )
    {
        IResourceBuilder<ExecutableResource>? resource_builder = AddScriptPython
                                                                        (
                                                                            builder,
                                                                            name,
                                                                            path_python,
                                                                            working_directory,
                                                                            python_script_and_args
                                                                        );

        return resource_builder;
    }

    public static
        IResourceBuilder<ExecutableResource>
                                        GenerateSettings
                                        (
                                            this  IResourceBuilder<ExecutableResource>? builder
                                        )
    {
        IEnumerable<EndpointAnnotation> endpoints = default;
        IEnumerable<EnvironmentCallbackAnnotation> envvars = default;
        
        bool endpoints_exist = builder.Resource.TryGetEndpoints(out endpoints);
        bool envvars_exist = builder.Resource.TryGetEnvironmentVariables(out envvars);

        return builder;
    }

    public static
        IResourceBuilder<ProjectResource>
                                        BuildSettings
                                        (
                                            this IResourceBuilder<ProjectResource>? builder
                                        )
    {
        string code = 
        """
        import urllib.request
        contents = urllib.request.urlopen("https://microsoftedge.github.io/Demos/json-dummy-data/64KB.json").read()
        # contents = urllib.request.urlopen("https://apiservice").read()
        
        print(contents)
        
        # $HOME/moljac-python/venv/bin/pip3 install requests
        
        import requests
        r = requests.get("https://microsoftedge.github.io/Demos/json-dummy-data/64KB.json")
        # r = requests.get("https://apiservice")
        
        print(r.status_code)
        print(r.headers)
        print(r.content)  # bytes
        print(r.text)     # r.content as str
        """;

        System.IO.File.WriteAllText("aspire_settings.py", code);
        return builder;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns> <summary>
    public static
        DistributedApplication
                                        BuildDistributedAppWithPython
                                        (
                                            this IDistributedApplicationBuilder? builder
                                        )
    {
        Process? process_build = default;
        process_build = Process.Start
                                    (
                                        "python3",
                                        $"{ScriptPython}"
                                    );
        process_build.WaitForExit();

        return builder.Build();   // Aspire Build() method- intercepted, can be called only once
    }

}
