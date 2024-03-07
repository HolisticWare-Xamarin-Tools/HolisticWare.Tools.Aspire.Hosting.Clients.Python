/*
#########################################################################################
Installing

    dotnet cake global tool

        dotnet tool uninstall 	-g Cake.Tool
        dotnet tool install 	-g Cake.Tool

    script bootstrappers (deprecated)

        Windows - powershell

            Invoke-WebRequest http://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1
            .\build.ps1

            Get-ExecutionPolicy -List
            Set-ExecutionPolicy RemoteSigned -Scope Process
            Unblock-File .\build.ps1

        Windows - cmd.exe prompt

            powershell ^
                Invoke-WebRequest http://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1
            powershell ^
                .\build.ps1

        Mac OSX

            rm -fr tools/; mkdir ./tools/ ; \
            cp cake.packages.config ./tools/packages.config ; \
            curl -Lsfo build.sh http://cakebuild.net/download/bootstrapper/osx ; \
            sh ./build.sh

            chmod +x ./build.sh ;
            ./build.sh

        Linux

            curl -Lsfo build.sh http://cakebuild.net/download/bootstrapper/linux
            chmod +x ./build.sh && ./build.sh

Running Cake to Build targets

    Windows

        tools\Cake\Cake.exe --verbosity=diagnostic --target=libs
        tools\Cake\Cake.exe --verbosity=diagnostic --target=nuget
        tools\Cake\Cake.exe --verbosity=diagnostic --target=samples


    Mac OSX

        mono tools/Cake/Cake.exe --verbosity=diagnostic --target=libs
        mono tools/Cake/Cake.exe --verbosity=diagnostic --target=nuget

        mono tools/nunit.consolerunner/NUnit.ConsoleRunner/tools/nunit3-console.exe \


#########################################################################################
*/

// https://www.nuget.org/packages/Cake.CoreCLR
//  Cake.CoreCLR add to ./tools/ folder for debugging
#addin nuget:?package=Cake.FileHelpers

//---------------------------------------------------------------------------------------
// unit testing
#tool nuget:?package=NUnit.ConsoleRunner
#tool nuget:?package=xunit.runner.console
//---------------------------------------------------------------------------------------
// coverage
#tool "nuget:?package=OpenCover"
// dotCover is commercial
// #tool "nuget:?package=JetBrains.dotCover.CommandLineTools"
//---------------------------------------------------------------------------------------
// reporting
#tool "nuget:?package=ReportUnit"
#tool "nuget:?package=ReportGenerator"

#load "./externals-data.cake"

//---------------------------------------------------------------------------------------
string TARGET = Argument ("t", Argument ("target", "Default"));
string verbosity = Argument ("v", Argument ("verbosity", "diagnostic"));

string source_solutions                             = $"./source/**/*.sln";
string source_projects                              = $"./source/**/*.csproj";
string samples_solutions                            = $"./samples/**/*.sln";
string samples_projects                             = $"./samples/**/*.csproj";
string samples_scripts_interactive_csharp_cake      = $"./samples/**/*.cake";
string externals_scripts_interactive_csharp_cake    = $"./externals/**/*.cake";
string samples_cake_scripts                         = $"./samples/**/*.cake";
string tests_cake_scripts                           = $"./samples/**/*.cake";

FilePathCollection LibrarySourceSolutions                   = GetFiles(source_solutions);
FilePathCollection LibrarySourceProjects                    = GetFiles(source_projects);

FilePathCollection SamplesSolutions                         = GetFiles(samples_solutions);
FilePathCollection SamplesScriptsInteractiveCsharpCake      = GetFiles(samples_solutions);
FilePathCollection SamplesProjects                          = GetFiles(samples_projects);

string[] configurations = new string[]
{
    //"Debug",
    "Release",
};

string[] clean_folder_patterns = new string[]
{
    "./externals/",
    "./output/",
    "./**/.vs/",
    "./**/.idea/",
    "./**/*-packages/",
    "./**/.mfractor/",
    "./source/**/bin/",
    "./source/**/obj/",
    "./source/**/packages/",
    "./samples/**/bin/",
    "./samples/**/obj/",
    "./samples/**/packages/",
    "./samples/**/tools/",
    "./tests/**/bin/",
    "./tests/**/obj/",
    "./tests/**/packages/",
    "./tests/**/tools/",
};


string[] clean_file_patterns = new string[]
{
    "./**/*.binlog",
    "./**/.DS_Store",
};


#load "./scripts/common/externals.cake"
#load "./scripts/private-protected-sensitive/externals.private.cake"
#load "./scripts/common/main.cake"
#load "./scripts/common/nuget-restore.cake"
#load "./scripts/common/nuget-pack.cake"
#load "./scripts/common/libs.cake"
#load "./scripts/common/samples.cake"
#load "./scripts/common/tests-unit-tests.cake"


// FilePathCollection UnitTestsNUnitMobileProjects = GetFiles($"./tests/unit-tests/project-references/**/*.NUnit.Xamarin*.csproj");
// FilePathCollection UnitTestsXUnitProjects = GetFiles($"./tests/unit-tests/project-references/**/*.XUnit.csproj");
// FilePathCollection UnitTestsMSTestProjects = GetFiles($"./tests/unit-tests/project-references/**/*.NUnit.csproj");


Task("Default")
.Does
    (
        () =>
        {
            //RunTarget("unit-tests");
            RunTarget("nuget-pack");
            RunTarget("samples");
        }
    );

RunTarget (TARGET);

if( ! IsRunningOnWindows())
{
    try
    {
        int exit_code = StartProcess
                                (
                                    "tree",
                                    new ProcessSettings
                                    {
                                        Arguments = @"-h ./output"
                                    }
                                );
    }
    catch(Exception ex)
    {
        Information($"unable to start process - tree {ex.Message}");
    }
}
else
{
    // int exit_code = StartProcess
    //                         (
    //                             "dir",
    //                             new ProcessSettings
    //                             {
    //                                 Arguments = @"output"
    //                             }
    //                         );

}
