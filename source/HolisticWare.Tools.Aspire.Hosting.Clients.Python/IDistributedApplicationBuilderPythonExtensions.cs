using System.Diagnostics;
using Aspire.Hosting;

namespace HolisticWare.Tools.Aspire.Hosting.Clients.Python;

public static partial class 
                                        IDistributedApplicationBuilderExtensions
{
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
}
