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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLoadConfig = new System.Windows.Forms.ToolStripButton();
            this.statusStripFavInfo = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewFavorites = new System.Windows.Forms.TreeView();
            this.contextMenuStripNodes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoriteAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoriteEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoriteDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlFavInfo = new System.Windows.Forms.TabControl();
            this.tabPageFavDetails = new System.Windows.Forms.TabPage();
            this.listViewDetails = new System.Windows.Forms.ListView();
            this.columnHeaderHostname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderProtocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageConfigs = new System.Windows.Forms.TabPage();
            this.listViewConfig = new System.Windows.Forms.ListView();
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripButtonReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAddFav = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditFav = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelFav = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteFavoriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripNodes.SuspendLayout();
            this.tabControlFavInfo.SuspendLayout();
            this.tabPageFavDetails.SuspendLayout();
            this.tabPageConfigs.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(614, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteFavoriteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.fileToolStripMenuItem.Text = "Favorite";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Add Favorite";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Edit Favorite";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.managementToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.tasksToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // managementToolStripMenuItem
            // 
            this.managementToolStripMenuItem.Name = "managementToolStripMenuItem";
            this.managementToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.managementToolStripMenuItem.Text = "Management";
            this.managementToolStripMenuItem.Click += new System.EventHandler(this.managementToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reportsToolStripMenuItem.Text = "Reports";
            this.reportsToolStripMenuItem.Click += new System.EventHandler(this.reportsToolStripMenuItem_Click);
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
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.toolStripButtonAddFav,
            this.toolStripButtonEditFav,
            this.toolStripButtonDelFav,
            this.toolStripSeparator4,
            this.toolStripButtonReport,
            this.toolStripSeparator3,
            this.toolStripButtonLoadConfig,
            this.toolStripSeparator1});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripMain.Size = new System.Drawing.Size(614, 27);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "Menu";
            // 
            // toolStripButtonLoadConfig
            // 
            this.toolStripButtonLoadConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLoadConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLoadConfig.Image")));
            this.toolStripButtonLoadConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoadConfig.Name = "toolStripButtonLoadConfig";
            this.toolStripButtonLoadConfig.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonLoadConfig.Text = "Load Configuration";
            this.toolStripButtonLoadConfig.Click += new System.EventHandler(this.toolStripButtonLoadConfig_Click);
            // 
            // statusStripFavInfo
            // 
            this.statusStripFavInfo.Location = new System.Drawing.Point(0, 429);
            this.statusStripFavInfo.Name = "statusStripFavInfo";
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
            this.treeViewFavorites.ContextMenuStrip = this.contextMenuStripNodes;
            this.treeViewFavorites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFavorites.Location = new System.Drawing.Point(0, 0);
            this.treeViewFavorites.Name = "treeViewFavorites";
            this.treeViewFavorites.Size = new System.Drawing.Size(204, 378);
            this.treeViewFavorites.TabIndex = 0;
            this.treeViewFavorites.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFavorites_AfterSelect);
            // 
            // contextMenuStripNodes
            // 
            this.contextMenuStripNodes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCategoryToolStripMenuItem,
            this.editCategoryToolStripMenuItem,
            this.deleteCategoryToolStripMenuItem,
            this.favoriteAddToolStripMenuItem,
            this.favoriteEditToolStripMenuItem,
            this.favoriteDeleteToolStripMenuItem});
            this.contextMenuStripNodes.Name = "contextMenuStripNodes";
            this.contextMenuStripNodes.Size = new System.Drawing.Size(159, 136);
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
            // favoriteAddToolStripMenuItem
            // 
            this.favoriteAddToolStripMenuItem.Name = "favoriteAddToolStripMenuItem";
            this.favoriteAddToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.favoriteAddToolStripMenuItem.Text = "Favorite Add";
            this.favoriteAddToolStripMenuItem.Click += new System.EventHandler(this.favoriteAddToolStripMenuItem_Click);
            // 
            // favoriteEditToolStripMenuItem
            // 
            this.favoriteEditToolStripMenuItem.Name = "favoriteEditToolStripMenuItem";
            this.favoriteEditToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.favoriteEditToolStripMenuItem.Text = "Favorite  Edit";
            this.favoriteEditToolStripMenuItem.Click += new System.EventHandler(this.favoriteEditToolStripMenuItem_Click);
            // 
            // favoriteDeleteToolStripMenuItem
            // 
            this.favoriteDeleteToolStripMenuItem.Name = "favoriteDeleteToolStripMenuItem";
            this.favoriteDeleteToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.favoriteDeleteToolStripMenuItem.Text = "Favorite Delete";
            this.favoriteDeleteToolStripMenuItem.Click += new System.EventHandler(this.favoriteDeleteToolStripMenuItem_Click);
            // 
            // tabControlFavInfo
            // 
            this.tabControlFavInfo.Controls.Add(this.tabPageFavDetails);
            this.tabControlFavInfo.Controls.Add(this.tabPageConfigs);
            this.tabControlFavInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFavInfo.Location = new System.Drawing.Point(0, 0);
            this.tabControlFavInfo.Name = "tabControlFavInfo";
            this.tabControlFavInfo.SelectedIndex = 0;
            this.tabControlFavInfo.Size = new System.Drawing.Size(406, 378);
            this.tabControlFavInfo.TabIndex = 0;
            this.tabControlFavInfo.SelectedIndexChanged += new System.EventHandler(this.tabControlFavInfo_SelectedIndexChanged);
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
            this.listViewDetails.Dock = System.Windows.Forms.DockStyle.Fill;
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
            // tabPageConfigs
            // 
            this.tabPageConfigs.Controls.Add(this.listViewConfig);
            this.tabPageConfigs.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfigs.Name = "tabPageConfigs";
            this.tabPageConfigs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfigs.Size = new System.Drawing.Size(398, 354);
            this.tabPageConfigs.TabIndex = 1;
            this.tabPageConfigs.Text = "Configurations";
            this.tabPageConfigs.UseVisualStyleBackColor = true;
            // 
            // listViewConfig
            // 
            this.listViewConfig.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderId,
            this.columnHeaderDate});
            this.listViewConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewConfig.Location = new System.Drawing.Point(3, 3);
            this.listViewConfig.Name = "listViewConfig";
            this.listViewConfig.Size = new System.Drawing.Size(392, 348);
            this.listViewConfig.TabIndex = 0;
            this.listViewConfig.UseCompatibleStateImageBehavior = false;
            this.listViewConfig.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderId
            // 
            this.columnHeaderId.Text = "Configuration ID";
            this.columnHeaderId.Width = 100;
            // 
            // columnHeaderDate
            // 
            this.columnHeaderDate.Text = "Date";
            this.columnHeaderDate.Width = 80;
            // 
            // toolStripButtonReport
            // 
            this.toolStripButtonReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReport.Image")));
            this.toolStripButtonReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReport.Name = "toolStripButtonReport";
            this.toolStripButtonReport.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonReport.Text = "Report";
            this.toolStripButtonReport.Click += new System.EventHandler(this.toolStripButtonReport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonAddFav
            // 
            this.toolStripButtonAddFav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddFav.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddFav.Image")));
            this.toolStripButtonAddFav.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddFav.Name = "toolStripButtonAddFav";
            this.toolStripButtonAddFav.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonAddFav.Text = "Add Favorite";
            this.toolStripButtonAddFav.Click += new System.EventHandler(this.toolStripButtonAddFav_Click);
            // 
            // toolStripButtonEditFav
            // 
            this.toolStripButtonEditFav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEditFav.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditFav.Image")));
            this.toolStripButtonEditFav.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditFav.Name = "toolStripButtonEditFav";
            this.toolStripButtonEditFav.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonEditFav.Text = "Edit Favorite";
            this.toolStripButtonEditFav.Click += new System.EventHandler(this.toolStripButtonEditFav_Click);
            // 
            // toolStripButtonDelFav
            // 
            this.toolStripButtonDelFav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelFav.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelFav.Image")));
            this.toolStripButtonDelFav.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelFav.Name = "toolStripButtonDelFav";
            this.toolStripButtonDelFav.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonDelFav.Text = "Delete Favorite";
            this.toolStripButtonDelFav.Click += new System.EventHandler(this.toolStripButtonDelFav_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // deleteFavoriteToolStripMenuItem
            // 
            this.deleteFavoriteToolStripMenuItem.Name = "deleteFavoriteToolStripMenuItem";
            this.deleteFavoriteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteFavoriteToolStripMenuItem.Text = "Delete Favorite";
            this.deleteFavoriteToolStripMenuItem.Click += new System.EventHandler(this.deleteFavoriteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tasksToolStripMenuItem
            // 
            this.tasksToolStripMenuItem.Name = "tasksToolStripMenuItem";
            this.tasksToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tasksToolStripMenuItem.Text = "Tasks";
            this.tasksToolStripMenuItem.Click += new System.EventHandler(this.tasksToolStripMenuItem_Click);
            // 
            // Rconfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 451);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStripFavInfo);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Rconfig";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripNodes.ResumeLayout(false);
            this.tabControlFavInfo.ResumeLayout(false);
            this.tabPageFavDetails.ResumeLayout(false);
            this.tabPageConfigs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadConfig;
        private System.Windows.Forms.StatusStrip statusStripFavInfo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewFavorites;
        private System.Windows.Forms.TabControl tabControlFavInfo;
        private System.Windows.Forms.TabPage tabPageFavDetails;
        private System.Windows.Forms.TabPage tabPageConfigs;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ListView listViewDetails;
        private System.Windows.Forms.ColumnHeader columnHeaderHostname;
        private System.Windows.Forms.ColumnHeader columnHeaderAddress;
        private System.Windows.Forms.ColumnHeader columnHeaderPort;
        private System.Windows.Forms.ColumnHeader columnHeaderProtocol;
        private System.Windows.Forms.ColumnHeader columnHeaderLocation;
        private System.Windows.Forms.ListView listViewConfig;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderDate;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNodes;
        private System.Windows.Forms.ToolStripMenuItem addCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoriteAddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoriteEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoriteDeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem managementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddFav;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditFav;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelFav;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteFavoriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tasksToolStripMenuItem;
    }
}

