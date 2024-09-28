namespace LL.Zentral.PublishNugetPackages.Common;

public static class DirectoryHelper
{
    private static string homeDirPath = string.Empty;
    private static string nugetRepoDirPath = string.Empty;
    private static DirectoryInfo? projectsDir;
    private static DirectoryInfo? repoDir;
    private static bool directoryInfoSet = false;

    public static DirectoryInfo ProjectsDir => directoryInfoSet ? projectsDir! : throw new OperationCanceledException("Projects Directory not set yet.");
    public static DirectoryInfo RepoDir => directoryInfoSet ? repoDir! : throw new OperationCanceledException("Nuget Repository Directory not set yet.");

    public static string LL_HOME => homeDirPath;
    public static string LL_NUGET => nugetRepoDirPath;

    public static bool ExtractMainDirectoriesInfo()
    {
        CheckDirectoryInfo();
        return directoryInfoSet;
    }


    private static void CheckDirectoryInfo()
    {
        homeDirPath = Environment.GetEnvironmentVariable(Constants.HOME_VAR) ?? string.Empty;
        nugetRepoDirPath = Environment.GetEnvironmentVariable(Constants.NUGET_REPO_VAR) ?? string.Empty;

        if (string.IsNullOrEmpty(homeDirPath) || string.IsNullOrEmpty(nugetRepoDirPath))
        {
            Console.WriteLine($"{Constants.HOME_VAR} and/or {Constants.NUGET_REPO_VAR} vars are not set. Is not possible to continue...");
            return;
        }

        SetDirectoryInfo();
    }

    private static void SetDirectoryInfo()
    {
        projectsDir = new DirectoryInfo(homeDirPath);
        repoDir = new DirectoryInfo(nugetRepoDirPath);
        directoryInfoSet = true;
    }
}