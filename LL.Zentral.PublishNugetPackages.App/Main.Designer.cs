namespace LL.Zentral.PublishNugetPackages.App;

partial class Main
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
        chkLstAvalilableProjects = new CheckedListBox();
        btnRefreshProjects = new Button();
        lblHome = new Label();
        lblNuget = new Label();
        txtHome = new TextBox();
        txtNuget = new TextBox();
        btnPublish = new Button();
        lstResults = new ListBox();
        SuspendLayout();
        // 
        // chkLstAvalilableProjects
        // 
        chkLstAvalilableProjects.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        chkLstAvalilableProjects.FormattingEnabled = true;
        chkLstAvalilableProjects.Location = new Point(12, 12);
        chkLstAvalilableProjects.Name = "chkLstAvalilableProjects";
        chkLstAvalilableProjects.Size = new Size(369, 526);
        chkLstAvalilableProjects.TabIndex = 0;
        chkLstAvalilableProjects.SelectedValueChanged += ChkLstAvalilableProjects_SelectedValueChanged;
        // 
        // btnRefreshProjects
        // 
        btnRefreshProjects.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnRefreshProjects.Location = new Point(12, 544);
        btnRefreshProjects.Name = "btnRefreshProjects";
        btnRefreshProjects.Size = new Size(369, 50);
        btnRefreshProjects.TabIndex = 1;
        btnRefreshProjects.Text = "Refresh Projects";
        btnRefreshProjects.UseVisualStyleBackColor = true;
        // 
        // lblHome
        // 
        lblHome.AutoSize = true;
        lblHome.Location = new Point(412, 12);
        lblHome.Name = "lblHome";
        lblHome.Size = new Size(59, 15);
        lblHome.TabIndex = 2;
        lblHome.Text = "LL_HOME";
        // 
        // lblNuget
        // 
        lblNuget.AutoSize = true;
        lblNuget.Location = new Point(412, 56);
        lblNuget.Name = "lblNuget";
        lblNuget.Size = new Size(61, 15);
        lblNuget.TabIndex = 3;
        lblNuget.Text = "LL_NUGET";
        // 
        // txtHome
        // 
        txtHome.BackColor = SystemColors.HighlightText;
        txtHome.Location = new Point(477, 9);
        txtHome.Name = "txtHome";
        txtHome.ReadOnly = true;
        txtHome.Size = new Size(1139, 23);
        txtHome.TabIndex = 4;
        // 
        // txtNuget
        // 
        txtNuget.BackColor = SystemColors.HighlightText;
        txtNuget.Location = new Point(477, 53);
        txtNuget.Name = "txtNuget";
        txtNuget.ReadOnly = true;
        txtNuget.Size = new Size(1139, 23);
        txtNuget.TabIndex = 5;
        // 
        // btnPublish
        // 
        btnPublish.Enabled = false;
        btnPublish.Location = new Point(477, 82);
        btnPublish.Name = "btnPublish";
        btnPublish.Size = new Size(255, 50);
        btnPublish.TabIndex = 6;
        btnPublish.Text = "Generate Nuget for Selected Projects";
        btnPublish.UseVisualStyleBackColor = true;
        btnPublish.Click += BtnPublish_Click;
        // 
        // lstResults
        // 
        lstResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lstResults.FormattingEnabled = true;
        lstResults.ItemHeight = 15;
        lstResults.Location = new Point(477, 138);
        lstResults.Name = "lstResults";
        lstResults.SelectionMode = SelectionMode.None;
        lstResults.Size = new Size(1139, 394);
        lstResults.TabIndex = 7;
        // 
        // Main
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1639, 606);
        Controls.Add(lstResults);
        Controls.Add(btnPublish);
        Controls.Add(txtNuget);
        Controls.Add(txtHome);
        Controls.Add(lblNuget);
        Controls.Add(lblHome);
        Controls.Add(btnRefreshProjects);
        Controls.Add(chkLstAvalilableProjects);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MinimumSize = new Size(1098, 645);
        Name = "Main";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Generate Nuget Packages";
        Load += Main_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private CheckedListBox chkLstAvalilableProjects;
    private Button btnRefreshProjects;
    private Label lblHome;
    private Label lblNuget;
    private TextBox txtHome;
    private TextBox txtNuget;
    private Button btnPublish;
    private ListBox lstResults;
}
