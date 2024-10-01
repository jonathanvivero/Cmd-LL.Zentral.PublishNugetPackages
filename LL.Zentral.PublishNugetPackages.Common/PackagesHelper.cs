using System.Diagnostics;
using System.Text;

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

    public static async Task<OperationResult> GenerateNugetPackagesForUniqueProjectAsync(DirectoryInfo projectDir, Action<string>? logger = null)
    {
        var msg = $"Generate package for: {projectDir.Name}";
        try
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
            if (logger is not null) logger(msg);

            return new(Message:msg);
        }
        catch (Exception ex)
        {
            return new(false, $"ERROR: {msg} => {ex.Message}");
        }

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

    public static async Task<OperationResult> PublishPackagesToNugetRepo(List<string> availablePackages, Action<string>? logger = null)
    {
        StringBuilder msgList = new();
        try
        {
            foreach (var package in availablePackages)
            {
                var msg = $"Package moved to Nuget repo => {package}";
                msgList.AppendLine(msg);

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
                if (logger is not null) logger(msg);
            }

            return new(Message: msgList.ToString());
        }
        catch (Exception ex)
        {
            return new(false, $"ERROR: Moving {availablePackages.Count} packages to Nuget Repo ({msgList.Length} moved) => {ex.Message}");
        }
    }
}