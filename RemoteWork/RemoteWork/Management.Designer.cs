namespace RemoteWork
{
    partial class Management
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.managerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoryManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locationManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.managerToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(560, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // managerToolStripMenuItem
            // 
            this.managerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.categoryManagerToolStripMenuItem,
            this.commandManagerToolStripMenuItem,
            this.locationManagerToolStripMenuItem});
            this.managerToolStripMenuItem.Name = "managerToolStripMenuItem";
            this.managerToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.managerToolStripMenuItem.Text = "Manager";
            // 
            // categoryManagerToolStripMenuItem
            // 
            this.categoryManagerToolStripMenuItem.Name = "categoryManagerToolStripMenuItem";
            this.categoryManagerToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.categoryManagerToolStripMenuItem.Text = "Category Manager";
            this.categoryManagerToolStripMenuItem.Click += new System.EventHandler(this.categoryManagerToolStripMenuItem_Click);
            // 
            // commandManagerToolStripMenuItem
            // 
            this.commandManagerToolStripMenuItem.Name = "commandManagerToolStripMenuItem";
            this.commandManagerToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.commandManagerToolStripMenuItem.Text = "Command Manager";
            this.commandManagerToolStripMenuItem.Click += new System.EventHandler(this.commandManagerToolStripMenuItem_Click);
            // 
            // locationManagerToolStripMenuItem
            // 
            this.locationManagerToolStripMenuItem.Name = "locationManagerToolStripMenuItem";
            this.locationManagerToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.locationManagerToolStripMenuItem.Text = "Location Manager";
            this.locationManagerToolStripMenuItem.Click += new System.EventHandler(this.locationManagerToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // Management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 425);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Management";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Management";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem managerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categoryManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locationManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    }
}