namespace HolisticWare.Tools.Aspire.Hosting.Clients.Python;

public static partial class
                                        IResourceBuilderOfExecutableResourceExtensions
{
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
}