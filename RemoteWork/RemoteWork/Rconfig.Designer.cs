namespace RemoteWork
{
    partial class Rconfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rconfig));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.appToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonConfigs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.statusStripFavInfo = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewFavorites = new System.Windows.Forms.TreeView();
            this.contextMenuStripCategories = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListCategories = new System.Windows.Forms.ImageList(this.components);
            this.tabControlFavInfo = new System.Windows.Forms.TabControl();
            this.tabPageFavDetails = new System.Windows.Forms.TabPage();
            this.listViewDetails = new System.Windows.Forms.ListView();
            this.columnHeaderHostname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderProtocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripFavs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.favoriteAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoriteEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoriteDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seeConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripCategories.SuspendLayout();
            this.tabControlFavInfo.SuspendLayout();
            this.tabPageFavDetails.SuspendLayout();
            this.contextMenuStripFavs.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(614, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStripMain";
            // 
            // appToolStripMenuItem
            // 
            this.appToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.managerToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.appToolStripMenuItem.Name = "appToolStripMenuItem";
            this.appToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.appToolStripMenuItem.Text = "App";
            // 
            // managerToolStripMenuItem
            // 
            this.managerToolStripMenuItem.Name = "managerToolStripMenuItem";
            this.managerToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.managerToolStripMenuItem.Text = "App Management";
            this.managerToolStripMenuItem.Click += new System.EventHandler(this.managerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(167, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.configurationsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.reportsToolStripMenuItem.Text = "Reports";
            this.reportsToolStripMenuItem.Click += new System.EventHandler(this.reportsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // configurationsToolStripMenuItem
            // 
            this.configurationsToolStripMenuItem.Name = "configurationsToolStripMenuItem";
            this.configurationsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.configurationsToolStripMenuItem.Text = "Configurations";
            this.configurationsToolStripMenuItem.Click += new System.EventHandler(this.configurationsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSettings,
            this.toolStripButtonReport,
            this.toolStripButtonConfigs,
            this.toolStripSeparator2,
            this.toolStripButtonAbout});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripMain.Size = new System.Drawing.Size(614, 27);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "Menu";
            // 
            // toolStripButtonSettings
            // 
            this.toolStripButtonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSettings.Image")));
            this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSettings.Name = "toolStripButtonSettings";
            this.toolStripButtonSettings.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonSettings.Text = "Settings";
            this.toolStripButtonSettings.Click += new System.EventHandler(this.toolStripButtonSettings_Click);
            // 
            // toolStripButtonReport
            // 
            this.toolStripButtonReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReport.Image")));
            this.toolStripButtonReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReport.Name = "toolStripButtonReport";
            this.toolStripButtonReport.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonReport.Text = "Reports";
            this.toolStripButtonReport.Click += new System.EventHandler(this.toolStripButtonReport_Click_1);
            // 
            // toolStripButtonConfigs
            // 
            this.toolStripButtonConfigs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonConfigs.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonConfigs.Image")));
            this.toolStripButtonConfigs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConfigs.Name = "toolStripButtonConfigs";
            this.toolStripButtonConfigs.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonConfigs.Text = "Configurations";
            this.toolStripButtonConfigs.Click += new System.EventHandler(this.toolStripButtonConfigs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAbout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAbout.Image")));
            this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonAbout.Text = "About";
            this.toolStripButtonAbout.Click += new System.EventHandler(this.toolStripButtonAbout_Click);
            // 
            // statusStripFavInfo
            // 
            this.statusStripFavInfo.Location = new System.Drawing.Point(0, 429);
            this.statusStripFavInfo.Name = "statusStripFavInfo";
            this.statusStripFavInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStripFavInfo.Size = new System.Drawing.Size(614, 22);
            this.statusStripFavInfo.TabIndex = 2;
            this.statusStripFavInfo.Text = "statusStripFavInfo";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 51);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewFavorites);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlFavInfo);
            this.splitContainer1.Size = new System.Drawing.Size(614, 378);
            this.splitContainer1.SplitterDistance = 204;
            this.splitContainer1.TabIndex = 3;
            // 
            // treeViewFavorites
            // 
            this.treeViewFavorites.ContextMenuStrip = this.contextMenuStripCategories;
            this.treeViewFavorites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFavorites.ImageIndex = 0;
            this.treeViewFavorites.ImageList = this.imageListCategories;
            this.treeViewFavorites.Location = new System.Drawing.Point(0, 0);
            this.treeViewFavorites.Name = "treeViewFavorites";
            this.treeViewFavorites.SelectedImageIndex = 0;
            this.treeViewFavorites.Size = new System.Drawing.Size(204, 378);
            this.treeViewFavorites.TabIndex = 0;
            this.treeViewFavorites.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFavorites_AfterSelect);
            // 
            // contextMenuStripCategories
            // 
            this.contextMenuStripCategories.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCategoryToolStripMenuItem,
            this.editCategoryToolStripMenuItem,
            this.deleteCategoryToolStripMenuItem});
            this.contextMenuStripCategories.Name = "contextMenuStripNodes";
            this.contextMenuStripCategories.Size = new System.Drawing.Size(159, 70);
            // 
            // addCategoryToolStripMenuItem
            // 
            this.addCategoryToolStripMenuItem.Name = "addCategoryToolStripMenuItem";
            this.addCategoryToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addCategoryToolStripMenuItem.Text = "Category Add";
            // 
            // editCategoryToolStripMenuItem
            // 
            this.editCategoryToolStripMenuItem.Name = "editCategoryToolStripMenuItem";
            this.editCategoryToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.editCategoryToolStripMenuItem.Text = "Category Edit";
            // 
            // deleteCategoryToolStripMenuItem
            // 
            this.deleteCategoryToolStripMenuItem.Name = "deleteCategoryToolStripMenuItem";
            this.deleteCategoryToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.deleteCategoryToolStripMenuItem.Text = "Category Delete";
            // 
            // imageListCategories
            // 
            this.imageListCategories.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCategories.ImageStream")));
            this.imageListCategories.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListCategories.Images.SetKeyName(0, "Router-50.png");
            this.imageListCategories.Images.SetKeyName(1, "Switch-50.png");
            this.imageListCategories.Images.SetKeyName(2, "Stack-50.png");
            // 
            // tabControlFavInfo
            // 
            this.tabControlFavInfo.Controls.Add(this.tabPageFavDetails);
            this.tabControlFavInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFavInfo.Location = new System.Drawing.Point(0, 0);
            this.tabControlFavInfo.Name = "tabControlFavInfo";
            this.tabControlFavInfo.SelectedIndex = 0;
            this.tabControlFavInfo.Size = new System.Drawing.Size(406, 378);
            this.tabControlFavInfo.TabIndex = 0;
            // 
            // tabPageFavDetails
            // 
            this.tabPageFavDetails.Controls.Add(this.listViewDetails);
            this.tabPageFavDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageFavDetails.Name = "tabPageFavDetails";
            this.tabPageFavDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFavDetails.Size = new System.Drawing.Size(398, 352);
            this.tabPageFavDetails.TabIndex = 0;
            this.tabPageFavDetails.Text = "Favorite Details";
            this.tabPageFavDetails.UseVisualStyleBackColor = true;
            // 
            // listViewDetails
            // 
            this.listViewDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderHostname,
            this.columnHeaderAddress,
            this.columnHeaderPort,
            this.columnHeaderProtocol,
            this.columnHeaderLocation});
            this.listViewDetails.ContextMenuStrip = this.contextMenuStripFavs;
            this.listViewDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDetails.FullRowSelect = true;
            this.listViewDetails.HoverSelection = true;
            this.listViewDetails.Location = new System.Drawing.Point(3, 3);
            this.listViewDetails.Name = "listViewDetails";
            this.listViewDetails.Size = new System.Drawing.Size(392, 346);
            this.listViewDetails.TabIndex = 0;
            this.listViewDetails.UseCompatibleStateImageBehavior = false;
            this.listViewDetails.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderHostname
            // 
            this.columnHeaderHostname.Text = "Hostname";
            this.columnHeaderHostname.Width = 80;
            // 
            // columnHeaderAddress
            // 
            this.columnHeaderAddress.Text = "IP Address";
            this.columnHeaderAddress.Width = 80;
            // 
            // columnHeaderPort
            // 
            this.columnHeaderPort.Text = "Port";
            // 
            // columnHeaderProtocol
            // 
            this.columnHeaderProtocol.Text = "Protocol";
            this.columnHeaderProtocol.Width = 80;
            // 
            // columnHeaderLocation
            // 
            this.columnHeaderLocation.Text = "Location";
            this.columnHeaderLocation.Width = 80;
            // 
            // contextMenuStripFavs
            // 
            this.contextMenuStripFavs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.favoriteAddToolStripMenuItem,
            this.favoriteEditToolStripMenuItem,
            this.favoriteDeleteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.loadConfigurationToolStripMenuItem,
            this.seeConfigurationToolStripMenuItem});
            this.contextMenuStripFavs.Name = "contextMenuStripFavs";
            this.contextMenuStripFavs.Size = new System.Drawing.Size(178, 120);
            // 
            // favoriteAddToolStripMenuItem
            // 
            this.favoriteAddToolStripMenuItem.Name = "favoriteAddToolStripMenuItem";
            this.favoriteAddToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.favoriteAddToolStripMenuItem.Text = "Favorite Add";
            this.favoriteAddToolStripMenuItem.Click += new System.EventHandler(this.favoriteAddToolStripMenuItem_Click);
            // 
            // favoriteEditToolStripMenuItem
            // 
            this.favoriteEditToolStripMenuItem.Name = "favoriteEditToolStripMenuItem";
            this.favoriteEditToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.favoriteEditToolStripMenuItem.Text = "Favorite Edit";
            this.favoriteEditToolStripMenuItem.Click += new System.EventHandler(this.favoriteEditToolStripMenuItem_Click);
            // 
            // favoriteDeleteToolStripMenuItem
            // 
            this.favoriteDeleteToolStripMenuItem.Name = "favoriteDeleteToolStripMenuItem";
            this.favoriteDeleteToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.favoriteDeleteToolStripMenuItem.Text = "Favorite Delete";
            this.favoriteDeleteToolStripMenuItem.Click += new System.EventHandler(this.favoriteDeleteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(174, 6);
            // 
            // loadConfigurationToolStripMenuItem
            // 
            this.loadConfigurationToolStripMenuItem.Name = "loadConfigurationToolStripMenuItem";
            this.loadConfigurationToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.loadConfigurationToolStripMenuItem.Text = "Load Configuration";
            this.loadConfigurationToolStripMenuItem.Click += new System.EventHandler(this.loadConfigurationToolStripMenuItem_Click);
            // 
            // seeConfigurationToolStripMenuItem
            // 
            this.seeConfigurationToolStripMenuItem.Name = "seeConfigurationToolStripMenuItem";
            this.seeConfigurationToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.seeConfigurationToolStripMenuItem.Text = "See Configuration";
            this.seeConfigurationToolStripMenuItem.Click += new System.EventHandler(this.seeConfigurationToolStripMenuItem_Click);
            // 
            // Rconfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 451);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStripFavInfo);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "Rconfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripCategories.ResumeLayout(false);
            this.tabControlFavInfo.ResumeLayout(false);
            this.tabPageFavDetails.ResumeLayout(false);
            this.contextMenuStripFavs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.StatusStrip statusStripFavInfo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewFavorites;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCategories;
        private System.Windows.Forms.ToolStripMenuItem addCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFavs;
        private System.Windows.Forms.ToolStripMenuItem favoriteAddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoriteEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoriteDeleteToolStripMenuItem;
        private System.Windows.Forms.ImageList imageListCategories;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seeConfigurationToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlFavInfo;
        private System.Windows.Forms.TabPage tabPageFavDetails;
        private System.Windows.Forms.ListView listViewDetails;
        private System.Windows.Forms.ColumnHeader columnHeaderHostname;
        private System.Windows.Forms.ColumnHeader columnHeaderAddress;
        private System.Windows.Forms.ColumnHeader columnHeaderPort;
        private System.Windows.Forms.ColumnHeader columnHeaderProtocol;
        private System.Windows.Forms.ColumnHeader columnHeaderLocation;
        private System.Windows.Forms.ToolStripMenuItem appToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem managerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
        private System.Windows.Forms.ToolStripButton toolStripButtonReport;
        private System.Windows.Forms.ToolStripButton toolStripButtonConfigs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
    }
}

