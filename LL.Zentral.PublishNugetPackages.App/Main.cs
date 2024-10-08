using System.Diagnostics;
using LL.Zentral.PublishNugetPackages.Common;

namespace LL.Zentral.PublishNugetPackages.App;

public partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
    }

    private void Main_Load(object sender, EventArgs e)
        => Initialize(LoadInfoToMainForm);

    private static void Initialize(Action successCallback)
    {
        try
        {
            if (!DirectoryHelper.ExtractMainDirectoriesInfo())
                return;

            successCallback();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Initializing Nuget Packages generation failed");
        }
    }

    private void LoadInfoToMainForm()
    {
        txtHome.Text = DirectoryHelper.LL_HOME;
        txtNuget.Text = DirectoryHelper.LL_NUGET;

        ShowListOfProjects();
    }

    private void ShowListOfProjects()
    {
        var listOfProjects = PackagesHelper.GetAvailableProjectsFolders();

        chkLstAvalilableProjects.Items.Clear();
        chkLstAvalilableProjects.DisplayMember = "Name";

        foreach (var dir in listOfProjects)
        {
            chkLstAvalilableProjects.Items.Add(dir);
        }
    }

    private void ChkLstAvalilableProjects_SelectedValueChanged(object sender, EventArgs e)
    {
        btnPublish.Enabled = (chkLstAvalilableProjects.CheckedItems.Count > 0);
    }

    private async void BtnPublish_Click(object sender, EventArgs e)
    {
        if (chkLstAvalilableProjects.CheckedItems is null || chkLstAvalilableProjects.CheckedItems.Count == 0)
        {
            MessageBox.Show($"No Project/s selected!!");
            return;
        }

        lstResults.Items.Clear();       

        btnPublish.Enabled = false;

        List<string> availablePackages = [];
        
        lstResults.Items.Clear();

        foreach (var item in chkLstAvalilableProjects.CheckedItems!)
        { 
            var selectedProject = (DirectoryInfo)item!;

            var generateResult = await PackagesHelper.GenerateNugetPackagesForUniqueProjectAsync(selectedProject, LogOperation);

            if (!generateResult.Success)
            { 
                lstResults.Items.Add(generateResult.Message);
                MessageBox.Show($"Process Finished with errors.");
                return;
            }

            availablePackages = PackagesHelper.FindAvailablePackages(selectedProject, availablePackages);            
        }

        var publishResult = await PackagesHelper.PublishPackagesToNugetRepo(availablePackages, LogOperation);
        btnPublish.Enabled = true;
        
        if (!publishResult.Success)
        {
            lstResults.Items.Add(publishResult.Message);
            MessageBox.Show($"Process Finished with errors.");
            return;
        }

        MessageBox.Show($"Process Finished. {availablePackages.Count} Nuget packages published.");
    }

    private void LogOperation(string log)
    { 
        lstResults.Items.Add(log);
        Console.WriteLine(log);
        Debug.Print(log);
    } 
}
