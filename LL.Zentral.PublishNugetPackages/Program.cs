using LL.Zentral.PublishNugetPackages.Common;

namespace LL.Zentral.PublishNugetPackages
{
    internal class Program
    {
        static async Task Main()
        {
            if (!DirectoryHelper.ExtractMainDirectoriesInfo())
                return;

            var projects = PackagesHelper.GetAvailableProjectsFolders();

            await PackagesHelper.GenerateNugetPackagesForProjectsAsync(projects);

            var availablePackages = PackagesHelper.FindAvailablePackages(DirectoryHelper.ProjectsDir);

            PackagesHelper.PublishPackagesToNugetRepo(availablePackages);
        }
    }
}
