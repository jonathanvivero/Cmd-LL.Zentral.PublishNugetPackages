using System.Diagnostics;

namespace LL.Zentral.PublishNugetPackages.Common;

public static class PackagesHelper
{
    public static IEnumerable<DirectoryInfo> GetAvailableProjectsFolders()
            => DirectoryHelper.ProjectsDir.GetDirectories().Where(w =>
                w.Name.StartsWith("Lib-")
                || w.Name.StartsWith("Domain-")
                || w.Name.StartsWith("Subdomain-")
                || w.Name.StartsWith("App-")
                || w.Name.StartsWith("Web-")
                );

    public static async Task GenerateNugetPackagesForProjectsAsync(IEnumerable<DirectoryInfo> subProjectDirs)
    {
        foreach (var project in subProjectDirs)
            await GenerateNugetPackagesForUniqueProjectAsync(project);
    }

    public static async Task GenerateNugetPackagesForUniqueProjectAsync(DirectoryInfo projectDir, Action<string>? logger = null)
    {
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = @"dotnet",
                Arguments = "pack -c Release",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                WorkingDirectory = @$"{projectDir.FullName}"
            }
        };

        proc.Start();
        await proc.WaitForExitAsync();
        if(logger is not null) logger($"Generate package for: {projectDir.Name}");
    }

    public static List<string> FindAvailablePackages(DirectoryInfo projectDirs, List<string>? availablePackages = null)
    {
        availablePackages ??= [];

        var files = projectDirs.GetFiles().Where(w => w.Extension == ".nupkg");
        if (files.Any())
        {
            foreach (var file in files)
            {
                availablePackages.Add(file.FullName);
            }
        }

        foreach (var subDir in projectDirs.GetDirectories())
        {
            FindAvailablePackages(subDir, availablePackages);
        }

        return availablePackages;
    }

    public static async Task PublishPackagesToNugetRepo(List<string> availablePackages, Action<string>? logger = null)
    {
        foreach (var package in availablePackages)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"dotnet",
                    Arguments = $"nuget push {package} -s {DirectoryHelper.RepoDir.FullName}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    WorkingDirectory = @$"{DirectoryHelper.ProjectsDir.FullName}"
                }
            };

            proc.Start();
            await proc.WaitForExitAsync();
            if (logger is not null) logger($"Package moved to Nuget repo => {package}");
        }
    }
}