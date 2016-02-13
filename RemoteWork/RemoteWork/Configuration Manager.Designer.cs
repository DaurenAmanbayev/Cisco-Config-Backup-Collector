namespace RemoteWork
{
    partial class Configuration_Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuration_Manager));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxHostname = new System.Windows.Forms.CheckBox();
            this.comboBoxFavs = new System.Windows.Forms.ComboBox();
            this.buttonFind = new System.Windows.Forms.Button();
            this.dateTimePickerFilter = new System.Windows.Forms.DateTimePicker();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.listViewConfig = new System.Windows.Forms.ListView();
            this.columnHeaderID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripOpenConfig = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCompare = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.contextMenuStripOpenConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.listViewConfig, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(594, 395);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.checkBoxHostname);
            this.flowLayoutPanel1.Controls.Add(this.comboBoxFavs);
            this.flowLayoutPanel1.Controls.Add(this.buttonFind);
            this.flowLayoutPanel1.Controls.Add(this.dateTimePickerFilter);
            this.flowLayoutPanel1.Controls.Add(this.buttonFilter);
            this.flowLayoutPanel1.Controls.Add(this.buttonCompare);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(465, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(126, 389);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // checkBoxHostname
            // 
            this.checkBoxHostname.AutoSize = true;
            this.checkBoxHostname.Location = new System.Drawing.Point(3, 3);
            this.checkBoxHostname.Name = "checkBoxHostname";
            this.checkBoxHostname.Size = new System.Drawing.Size(113, 17);
            this.checkBoxHostname.TabIndex = 5;
            this.checkBoxHostname.Text = "Filter by Hostname";
            this.checkBoxHostname.UseVisualStyleBackColor = true;
            this.checkBoxHostname.CheckStateChanged += new System.EventHandler(this.checkBoxHostname_CheckStateChanged);
            // 
            // comboBoxFavs
            // 
            this.comboBoxFavs.FormattingEnabled = true;
            this.comboBoxFavs.Location = new System.Drawing.Point(3, 26);
            this.comboBoxFavs.Name = "comboBoxFavs";
            this.comboBoxFavs.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFavs.TabIndex = 0;
            // 
            // buttonFind
            // 
            this.buttonFind.Location = new System.Drawing.Point(3, 53);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(121, 32);
            this.buttonFind.TabIndex = 1;
            this.buttonFind.Text = "Filter by Favorite";
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // dateTimePickerFilter
            // 
            this.dateTimePickerFilter.Location = new System.Drawing.Point(3, 91);
            this.dateTimePickerFilter.Name = "dateTimePickerFilter";
            this.dateTimePickerFilter.Size = new System.Drawing.Size(121, 20);
            this.dateTimePickerFilter.TabIndex = 6;
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(3, 117);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(121, 32);
            this.buttonFilter.TabIndex = 7;
            this.buttonFilter.Text = "Filter by Date";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // listViewConfig
            // 
            this.listViewConfig.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderID,
            this.columnHeaderDate});
            this.listViewConfig.ContextMenuStrip = this.contextMenuStripOpenConfig;
            this.listViewConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewConfig.FullRowSelect = true;
            this.listViewConfig.Location = new System.Drawing.Point(3, 3);
            this.listViewConfig.Name = "listViewConfig";
            this.listViewConfig.Size = new System.Drawing.Size(456, 389);
            this.listViewConfig.TabIndex = 1;
            this.listViewConfig.UseCompatibleStateImageBehavior = false;
            this.listViewConfig.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderID
            // 
            this.columnHeaderID.Text = "Configuration ID";
            this.columnHeaderID.Width = 100;
            // 
            // columnHeaderDate
            // 
            this.columnHeaderDate.Text = "Loading Date";
            this.columnHeaderDate.Width = 120;
            // 
            // contextMenuStripOpenConfig
            // 
            this.contextMenuStripOpenConfig.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConfigurationToolStripMenuItem});
            this.contextMenuStripOpenConfig.Name = "contextMenuStripOpenConfig";
            this.contextMenuStripOpenConfig.Size = new System.Drawing.Size(181, 26);
            // 
            // openConfigurationToolStripMenuItem
            // 
            this.openConfigurationToolStripMenuItem.Name = "openConfigurationToolStripMenuItem";
            this.openConfigurationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openConfigurationToolStripMenuItem.Text = "Open Configuration";
            this.openConfigurationToolStripMenuItem.Click += new System.EventHandler(this.openConfigurationToolStripMenuItem_Click);
            // 
            // buttonCompare
            // 
            this.buttonCompare.Location = new System.Drawing.Point(3, 155);
            this.buttonCompare.Name = "buttonCompare";
            this.buttonCompare.Size = new System.Drawing.Size(121, 32);
            this.buttonCompare.TabIndex = 8;
            this.buttonCompare.Text = "Compare 2 Configs";
            this.buttonCompare.UseVisualStyleBackColor = true;
            this.buttonCompare.Click += new System.EventHandler(this.buttonCompare_Click);
            // 
            // Configuration_Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 395);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Configuration_Manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configurations";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.contextMenuStripOpenConfig.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBoxHostname;
        private System.Windows.Forms.ComboBox comboBoxFavs;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.ListView listViewConfig;
        private System.Windows.Forms.ColumnHeader columnHeaderID;
        private System.Windows.Forms.ColumnHeader columnHeaderDate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOpenConfig;
        private System.Windows.Forms.ToolStripMenuItem openConfigurationToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilter;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.Button buttonCompare;
    }
}