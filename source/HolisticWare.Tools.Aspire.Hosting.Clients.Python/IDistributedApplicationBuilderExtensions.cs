using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WatsonWebserver.Core;
using WatsonWebserver.Lite;

namespace HolisticWare.Tools.Aspire.Hosting.Clients.Python;

public static partial class 
                                        IDistributedApplicationBuilderExtensions
{
    private static ExecutableResource ResourceExecutable;
    private static WebserverLite server;
    
    public static
        IResourceBuilder<ExecutableResource>
                                        AddScriptPython
                                        (
                                            this IDistributedApplicationBuilder? builder,
                                            string name,
                                            string path_python,
                                            string working_directory,
                                            string[] python_script_and_args,
                                            string? hostname = "localhost",
                                            int port = 55444 
                                        )
    {
        
        // / *
        try
        {
            WebserverSettings settings = new WebserverSettings(hostname, port);
            server = new WebserverLite(settings, DefaultRoute);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        server.Routes.PreAuthentication.Static.Add
                                                (
                                                    WatsonWebserver.Core.HttpMethod.GET,
                                                    "/request/",
                                                    async (HttpContextBase ctx) =>
                                                    {
                                                        string msg =
                                                                $"launching ExecutableResource on request!"
                                                                + Environment.NewLine +
                                                                $"   Command        = {ResourceExecutable.Command}"
                                                                + Environment.NewLine +
                                                                $"   args           = {ResourceExecutable.Args}"
                                                                ;
                                                        await ctx.Response.Send(msg);
                                                        
                                                        Process.Start
                                                                    (
                                                                        ResourceExecutable?.Command,
                                                                        ResourceExecutable?.Args
                                                                    );
                                                        
                                                        return;
                                                    }
                                                );
        
        server.Start();
        // * /

        IResourceBuilder<ExecutableResource>? resource_builder = builder?.AddExecutable
                                                                                    (
                                                                                        name,
                                                                                        path_python,
                                                                                        working_directory,
                                                                                        python_script_and_args
                                                                                    );

        ResourceExecutable = resource_builder.Resource;
        
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
                                                                            )
                                                                            .WithEndpoint
                                                                                (
                                                                                    55555,
                                                                                    null,
                                                                                    "django",                                                                                    
                                                                                    "AAAAAAAAAA"
                                                                                );

        return resource_builder;
    }
    
    public static
        IResourceBuilder<ExecutableResource>
                                        AddScriptPythonFlask
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
    
}
