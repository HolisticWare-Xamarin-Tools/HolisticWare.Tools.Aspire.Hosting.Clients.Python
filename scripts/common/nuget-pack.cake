
//---------------------------------------------------------------------------------------
Task("nuget-pack")
    .IsDependentOn ("nuget-pack-dotnet")
    .IsDependentOn ("nuget-pack-msbuild")
    .IsDependentOn ("nuget-pack-verify")
	;

Task("nuget-pack-dotnet")
    .IsDependentOn ("libs")
    .Does
    (
        () =>
        {
			foreach(FilePath sln in LibrarySourceSolutions)
			{
				Information($"Solution: 	{sln}");
			}
			foreach(FilePath prj in LibrarySourceProjects)
			{
				Information($"Project: 		{prj}");

				DotNetPack
				(
					prj.ToString(),
					new DotNetPackSettings
					{
						Configuration = "Release",
						// PATH!!!!!!!!
						OutputDirectory = "./output/dotnet-pack/"
					}
				);
			}

            return;
        }
    );

Task("nuget-pack-msbuild")
    .IsDependentOn ("libs")
    .Does
    (
        () =>
        {
			foreach(FilePath sln in LibrarySourceSolutions)
			{
				Information($"Solution: 	{sln}");
			}
			foreach(FilePath prj in LibrarySourceProjects)
			{
				Information($"Project: 		{prj}");

				DotNetMSBuild
				(
					prj.ToString(),
					new DotNetMSBuildSettings
								{
								}
								.SetConfiguration("Release")
								.WithTarget("Pack")
								//.WithProperty("PackageVersion", NUGET_VERSION)
								// PATH!!!!!!!!
								.WithProperty("PackageOutputPath", "../../output/msbuild-pack/")
				);
			}

            return;
        }
    );

Task("nuget-pack-verify")
    .Does
    (
        () =>
        {
            FilePathCollection files = GetFiles("./output/**/*.nupkg");

            foreach(FilePath file in files)
			{
				Information($"NuGet package file: 		{file}");
                string dir = $"{file.ToString()}.unpacked";
                EnsureDirectoryDoesNotExist(dir);
                Unzip($"{file.ToString()}", dir);
			}

            return;
        }
    );
//---------------------------------------------------------------------------------------
