#load "./../private-protected-sensitive/externals.private.cake"
#load "./../common/nuget-restore.cake"

//---------------------------------------------------------------------------------------
Task ("externals")
    .IsDependentOn ("externals-repos-download")
    .IsDependentOn ("externals-build")
    // .WithCriteria (!FileExists ("./externals/HolisticWare.Core.Math.Statistics.aar"))
    ;
    
Task("externals-git-submodules-update")
    .Does
    (
        () =>
        {
            int exit_code;
            exit_code = StartProcess
                            (
                                "git", 
                                new ProcessSettings
                                { 
                                    Arguments = "submodule init" 
                                }
                            );
            exit_code = StartProcess
                            (
                                "git", 
                                new ProcessSettings
                                { 
                                    Arguments = "submodule update --init --recursive" 
                                }
                            );
            exit_code = StartProcess
                            (
                                "git", 
                                new ProcessSettings
                                { 
                                    Arguments = "pull --recurse-submodules" 
                                }
                            );
            exit_code = StartProcess
                            (
                                "git", 
                                new ProcessSettings
                                { 
                                    Arguments = "submodule foreach --recursive git pull" 
                                }
                            );

            return;
        }
    );

Task("externals-build")
    .IsDependentOn ("externals-build-submodules")
    ;

Task("externals-build-submodules")
    .Does
    (
        () =>
        {
            FilePathCollection files = GetFiles("./external*-submodule*/**/build.cake");
            foreach(FilePath file in files)
            {
                Information("File: {0}", file);
                CakeExecuteScript
                    (
                        file,
                        new CakeSettings
                        {
                            Verbosity = Verbosity.Diagnostic,
                            Arguments = new Dictionary<string, string>()
                            {
                                { "verbosity",  verbosity},
                                { "target",     "nuget-pack"},
                            },
                        }
                    );
            }

            return;
        }
    );

Task("externals-repos-download")
    .Does
    (
        () =>
        {
            Parallel.ForEach
                        (
                            ExternalReposToDownload,
                            repo =>
                            {
                            }
                        );

            return;
        }
    );

public partial class Externals
{
    static partial void ExedcutePrivateSensitive();

    public static void Execute()
    {
    }

    private static ICakeContext context = null;

    public static void Initialize(ICakeContext c)
    {
        context = c;

        return;
    }

}
//---------------------------------------------------------------------------------------
