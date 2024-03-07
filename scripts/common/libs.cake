#load "./nuget-restore.cake"

LibrarySourceSolutions  = GetFiles(source_solutions);
LibrarySourceProjects   = GetFiles(source_projects);

//---------------------------------------------------------------------------------------
Task("libs")
    .IsDependentOn ("nuget-restore-libs")
    .IsDependentOn ("libs-msbuild-solutions")
    .IsDependentOn ("libs-msbuild-projects")
    .IsDependentOn ("libs-dotnet-solutions")
    .IsDependentOn ("libs-dotnet-projects")
    ;

Task("libs-msbuild-solutions")
    .Does
    (
        () =>
        {
            foreach (string configuration in configurations)
            {
                foreach(FilePath sln in LibrarySourceSolutions)
                {
                    DotNetMSBuild
                    (
                        sln.ToString(),
                        new DotNetMSBuildSettings
                        {
                        }
                        .SetConfiguration(configuration)
                        //.WithProperty("DefineConstants", "TRACE;DEBUG;NETCOREAPP2_0;NUNIT")
                    );
                }
            }

            return;
        }
    );

Task("libs-dotnet-solutions")
    .Does
    (
        () =>
        {
            foreach (string configuration in configurations)
            {
                foreach(FilePath sln in LibrarySourceSolutions)
                {
                    DotNetBuild
                    (
                        sln.ToString(),
                        new DotNetBuildSettings
                        {
                            Configuration = configuration,
                        }
                        //.WithProperty("DefineConstants", "TRACE;DEBUG;NETCOREAPP2_0;NUNIT")
                    );
                }
            }

            return;
        }
    );

Task("libs-msbuild-projects")
    .Does
    (
        () =>
        {
            foreach (string configuration in configurations)
            {
                foreach(FilePath prj in LibrarySourceProjects)
                {
                    DotNetMSBuild
                    (
                        prj.ToString(),
                        new DotNetMSBuildSettings
                        {
                        }
                        .SetConfiguration(configuration)
                        //.WithProperty("DefineConstants", "TRACE;DEBUG;NETCOREAPP2_0;NUNIT")
                    );
                }
            }

            return;
        }
    );

Task("libs-dotnet-projects")
    .Does
    (
        () =>
        {
            foreach(string configuration in configurations)
            {
                foreach(FilePath prj in LibrarySourceProjects)
                {
                    DotNetBuild
                    (
                        prj.ToString(),
                        new DotNetBuildSettings
                        {
                            Configuration = configuration,
                        }
                        //.WithProperty("DefineConstants", "TRACE;DEBUG;NETCOREAPP2_0;NUNIT")
                    );
                }
            }

            return;
        }
    );

public void Build(string pattern)
{
	FilePathCollection files = GetFiles(pattern);

	foreach(FilePath file in files)
	{
		foreach (string configuration in configurations)
		{
			DotNetMSBuild
			(
				file.ToString(),
				new DotNetMSBuildSettings
				{
				}
                .SetConfiguration(configuration)
				.WithProperty("AndroidClassParser", "class-parse")
				//.WithProperty("DefineConstants", "TRACE;DEBUG;NETCOREAPP2_0;NUNIT")
			);
		}
	}
	
	return;
}
//---------------------------------------------------------------------------------------
