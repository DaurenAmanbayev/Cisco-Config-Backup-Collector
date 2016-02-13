namespace RemoteWork.Managers
{
    partial class Credential_Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Credential_Manager));
            this.listBoxCredentials = new System.Windows.Forms.ListBox();
            this.contextMenuStripCredential = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCredentialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCredentialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCredentialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripCredential.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxCredentials
            // 
            this.listBoxCredentials.ContextMenuStrip = this.contextMenuStripCredential;
            this.listBoxCredentials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxCredentials.FormattingEnabled = true;
            this.listBoxCredentials.Location = new System.Drawing.Point(0, 0);
            this.listBoxCredentials.Name = "listBoxCredentials";
            this.listBoxCredentials.Size = new System.Drawing.Size(254, 332);
            this.listBoxCredentials.TabIndex = 0;
            // 
            // contextMenuStripCredential
            // 
            this.contextMenuStripCredential.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCredentialToolStripMenuItem,
            this.editCredentialToolStripMenuItem,
            this.deleteCredentialToolStripMenuItem});
            this.contextMenuStripCredential.Name = "contextMenuStripCredential";
            this.contextMenuStripCredential.Size = new System.Drawing.Size(165, 70);
            // 
            // addCredentialToolStripMenuItem
            // 
            this.addCredentialToolStripMenuItem.Name = "addCredentialToolStripMenuItem";
            this.addCredentialToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.addCredentialToolStripMenuItem.Text = "Add Credential";
            this.addCredentialToolStripMenuItem.Click += new System.EventHandler(this.addCredentialToolStripMenuItem_Click);
            // 
            // editCredentialToolStripMenuItem
            // 
            this.editCredentialToolStripMenuItem.Name = "editCredentialToolStripMenuItem";
            this.editCredentialToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.editCredentialToolStripMenuItem.Text = "Edit Credential";
            this.editCredentialToolStripMenuItem.Click += new System.EventHandler(this.editCredentialToolStripMenuItem_Click);
            // 
            // deleteCredentialToolStripMenuItem
            // 
            this.deleteCredentialToolStripMenuItem.Name = "deleteCredentialToolStripMenuItem";
            this.deleteCredentialToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.deleteCredentialToolStripMenuItem.Text = "Delete Credential";
            this.deleteCredentialToolStripMenuItem.Click += new System.EventHandler(this.deleteCredentialToolStripMenuItem_Click);
            // 
            // Credential_Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 332);
            this.Controls.Add(this.listBoxCredentials);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Credential_Manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Credential_Manager";
            this.contextMenuStripCredential.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxCredentials;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCredential;
        private System.Windows.Forms.ToolStripMenuItem addCredentialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCredentialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCredentialToolStripMenuItem;
    }
}