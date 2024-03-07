﻿using System.Diagnostics;
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
                                            string[] python_script_and_args
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
        IResourceBuilder<ProjectResource>
                                        BuildSettings
                                        (
                                            this IResourceBuilder<ProjectResource>? builder
                                        )
    {
        string code = 
        """
        public partial class __Class__
        {
        }
        """;

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
