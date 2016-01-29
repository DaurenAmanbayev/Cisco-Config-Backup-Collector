namespace RemoteWork.Managers
{
    partial class Location_Manager
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
            this.listBoxLocations = new System.Windows.Forms.ListBox();
            this.contextMenuStripLocations = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripLocations.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxLocations
            // 
            this.listBoxLocations.ContextMenuStrip = this.contextMenuStripLocations;
            this.listBoxLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxLocations.FormattingEnabled = true;
            this.listBoxLocations.Location = new System.Drawing.Point(0, 0);
            this.listBoxLocations.Name = "listBoxLocations";
            this.listBoxLocations.Size = new System.Drawing.Size(254, 332);
            this.listBoxLocations.TabIndex = 0;
            // 
            // contextMenuStripLocations
            // 
            this.contextMenuStripLocations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLocationToolStripMenuItem,
            this.editLocationToolStripMenuItem,
            this.deleteLocationToolStripMenuItem});
            this.contextMenuStripLocations.Name = "contextMenuStripLocations";
            this.contextMenuStripLocations.Size = new System.Drawing.Size(157, 70);
            // 
            // addLocationToolStripMenuItem
            // 
            this.addLocationToolStripMenuItem.Name = "addLocationToolStripMenuItem";
            this.addLocationToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.addLocationToolStripMenuItem.Text = "Add Location";
            this.addLocationToolStripMenuItem.Click += new System.EventHandler(this.addLocationToolStripMenuItem_Click);
            // 
            // editLocationToolStripMenuItem
            // 
            this.editLocationToolStripMenuItem.Name = "editLocationToolStripMenuItem";
            this.editLocationToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.editLocationToolStripMenuItem.Text = "Edit Location";
            this.editLocationToolStripMenuItem.Click += new System.EventHandler(this.editLocationToolStripMenuItem_Click);
            // 
            // deleteLocationToolStripMenuItem
            // 
            this.deleteLocationToolStripMenuItem.Name = "deleteLocationToolStripMenuItem";
            this.deleteLocationToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.deleteLocationToolStripMenuItem.Text = "Delete Location";
            this.deleteLocationToolStripMenuItem.Click += new System.EventHandler(this.deleteLocationToolStripMenuItem_Click);
            // 
            // Location_Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 332);
            this.Controls.Add(this.listBoxLocations);
            this.Name = "Location_Manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Location_Manager";
            this.contextMenuStripLocations.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxLocations;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLocations;
        private System.Windows.Forms.ToolStripMenuItem addLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteLocationToolStripMenuItem;
    }
}