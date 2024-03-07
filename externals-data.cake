
Dictionary
        <
            (
                string url_repo,
                string git_branch
            ),
            string[]
        > 
            ExternalReposToDownload;
            
ExternalReposToDownload = new Dictionary
                                    <
                                        (
                                            string url_repo,
                                            string git_branch
                                        ),
                                        string[]
                                    >
                                        ()
{
    // https://nuget.org/packages?packagetype=dotnettool
    {
        (
            url_repo:   "https://github.com/cake-build/cake/archive/refs/heads/develop.zip",
            git_branch: "develop" 
        ),
        new string[]
        {
            "dotnet",
            "tool",
            "cake",
        }
    },
    {
        (
            url_repo:   "https://github.com/domaindrivendev/Swashbuckle.AspNetCore/archive/refs/heads/master.zip",
            git_branch: "develop" 
        ),
        new string[]
        {
            "dotnet",
            "tool",
            "Swashbuckle.CLI",
        }
    },
};