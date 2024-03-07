//---------------------------------------------------------------------------------------
Task("unit-tests")
    .IsDependentOn("nuget-restore-tests")
    .IsDependentOn("libs")
    .IsDependentOn("nuget-pack")
    .Does
    (
        () =>
        {
            EnsureDirectoryExists("./output/results/unit-tests/");

            try
            {
                RunTarget("unit-tests-nunit");
            }
            catch (System.Exception)
            {
            }
            try
            {
                RunTarget("unit-tests-xunit");
            }
            catch (System.Exception)
            {
            }
            try
            {
                RunTarget("unit-tests-mstest");
            }
            catch (System.Exception)
            {
            }

            return;
        }
    );

var reports = Directory("/output/results/unit-tests/");


Task("unit-tests-nunit")
    .IsDependentOn("nuget-restore-tests")
    .Does
    (
        () =>
        {
            FilePathCollection UnitTestsNUnitAssemblies;
            FilePathCollection UnitTestsNUnitProjects = GetFiles($"./tests/unit-tests/project-references/**/*.NUnit.csproj");

            foreach(FilePath prj in UnitTestsNUnitProjects)
            {
                Information($"MSBuild ........................ {prj}");
                MSBuild
                (
                    prj.ToString(),
                    new MSBuildSettings
                    {
                        Configuration = "Debug",
                    }
                    .WithProperty("DefineConstants", "TRACE;DEBUG;NUNIT")
                );
                MSBuild
                (
                    prj.ToString(),
                    new MSBuildSettings
                    {
                        Configuration = "Release",
                    }
                    .WithProperty("DefineConstants", "TRACE;NUNIT")
                );
            }

            UnitTestsNUnitAssemblies = GetFiles($"./tests/unit-tests/project-references/**/bin/Debug/*.NUnit.dll");

            foreach(FilePath asm in UnitTestsNUnitAssemblies)
            {
                Information($"testing ................ {asm}");
            }

            NUnit3
            (
                UnitTestsNUnitAssemblies,
                new NUnit3Settings
                {
                    OutputFile = "./output/results/unit-tests/Nunit3.1.txt",
                }
            );

            UnitTestsNUnitAssemblies = GetFiles($"./tests/unit-tests/project-references/**/bin/Release/*.NUnit.dll");

            foreach(FilePath asm in UnitTestsNUnitAssemblies)
            {
                Information($"testing ................ {asm}");
            }

            NUnit3
            (
                UnitTestsNUnitAssemblies,
                new NUnit3Settings
                {
                    OutputFile = "./output/results/unit-tests/Nunit3.2.txt",
                }
            );
            return;
        }
    );

Task("unit-tests-xunit")
    .IsDependentOn("nuget-restore-tests")
    .Does
    (
        () =>
        {
            try
            {
                MSBuild
                (
                    "./tests/unit-tests/project-references/UnitTests.XUnit/UnitTests.XUnit.csproj",
                    new MSBuildSettings
                    {
                        Configuration = "Debug",
                    }.WithProperty("DefineConstants", "TRACE;DEBUG;NETCOREAPP2_0;XUNIT")
                );
                DotNetTest
                (
                    "./tests/unit-tests/project-references/UnitTests.XUnit/UnitTests.XUnit.csproj",
                    //"xunit",  "--no-build -noshadow"
                    new DotNetTestSettings()
                    {
                        ResultsDirectory = reports,
                    }
                );
                XUnit2
                (
                    "./tests/unit-tests/project-references/UnitTests.XUnit/bin/**/UnitTests.*.dll",
                    new XUnit2Settings
                    {
                        Parallelism = ParallelismOption.All,
                        NoAppDomain = true,
                        ReportName = "XUnit.Results",
                        HtmlReport = true,
                        XmlReport = true,
                        NUnitReport = true,
                        OutputDirectory = reports,
                    }
                );
            }
            catch (System.Exception)
            {
                Error("mc++ XUnit tests have failed");
            }
            finally
            {
                ReportUnit(reports);
            }

            MoveFile("TestResult.xml", "./output/results/unit-tests/TestResult.xml");
        }
    );

Task("unit-tests-mstest")
    .IsDependentOn("nuget-restore-tests")
    .Does
    (
        () =>
        {
            MSBuild
            (
                "./tests/unit-tests/project-references/UnitTests.MSTest/UnitTests.MSTest.csproj",
                new MSBuildSettings
                {
                    Configuration = "Debug",
                }.WithProperty("DefineConstants", "TRACE;DEBUG;NETCOREAPP2_0;MSTEST")
            );
            MSTest
            (
                "./tests/unit-tests/project-references/UnitTests.MSTest/bin/Debug/**/UnitTests.MSTest.dll",
                new MSTestSettings
                {
                    ResultsFile = "./output/results/unit-tests/MSTest.txt",
                }
            );
            DotNetTest
            (
                "./tests/unit-tests/project-references/UnitTests.MSTest/UnitTests.MSTest.csproj",
                //"xunit",  "--no-build -noshadow"
                new DotNetTestSettings()
                {
                    ResultsDirectory = reports,
                }
            );

            return;
        }
    );
//---------------------------------------------------------------------------------------

