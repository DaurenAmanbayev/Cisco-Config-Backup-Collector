namespace RemoteWork.Managers
{
    partial class Command_Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Command_Manager));
            this.contextMenuStripCommands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewCommands = new System.Windows.Forms.ListView();
            this.columnHeaderCommand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripCommands
            // 
            this.contextMenuStripCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCommandToolStripMenuItem,
            this.editCommandToolStripMenuItem,
            this.deleteCommandToolStripMenuItem});
            this.contextMenuStripCommands.Name = "contextMenuStripCommands";
            this.contextMenuStripCommands.Size = new System.Drawing.Size(168, 70);
            // 
            // addCommandToolStripMenuItem
            // 
            this.addCommandToolStripMenuItem.Name = "addCommandToolStripMenuItem";
            this.addCommandToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.addCommandToolStripMenuItem.Text = "Add Command";
            this.addCommandToolStripMenuItem.Click += new System.EventHandler(this.addCommandToolStripMenuItem_Click);
            // 
            // editCommandToolStripMenuItem
            // 
            this.editCommandToolStripMenuItem.Name = "editCommandToolStripMenuItem";
            this.editCommandToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.editCommandToolStripMenuItem.Text = "Edit Command";
            this.editCommandToolStripMenuItem.Click += new System.EventHandler(this.editCommandToolStripMenuItem_Click);
            // 
            // deleteCommandToolStripMenuItem
            // 
            this.deleteCommandToolStripMenuItem.Name = "deleteCommandToolStripMenuItem";
            this.deleteCommandToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.deleteCommandToolStripMenuItem.Text = "Delete Command";
            this.deleteCommandToolStripMenuItem.Click += new System.EventHandler(this.deleteCommandToolStripMenuItem_Click);
            // 
            // listViewCommands
            // 
            this.listViewCommands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCommand,
            this.columnHeaderOrder});
            this.listViewCommands.ContextMenuStrip = this.contextMenuStripCommands;
            this.listViewCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewCommands.FullRowSelect = true;
            this.listViewCommands.Location = new System.Drawing.Point(0, 0);
            this.listViewCommands.Name = "listViewCommands";
            this.listViewCommands.Size = new System.Drawing.Size(254, 332);
            this.listViewCommands.TabIndex = 1;
            this.listViewCommands.UseCompatibleStateImageBehavior = false;
            this.listViewCommands.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderCommand
            // 
            this.columnHeaderCommand.Text = "Command";
            this.columnHeaderCommand.Width = 120;
            // 
            // columnHeaderOrder
            // 
            this.columnHeaderOrder.Text = "Order";
            // 
            // Command_Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 332);
            this.Controls.Add(this.listViewCommands);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Command_Manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Command_Manager";
            this.contextMenuStripCommands.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCommands;
        private System.Windows.Forms.ToolStripMenuItem addCommandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCommandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCommandToolStripMenuItem;
        private System.Windows.Forms.ListView listViewCommands;
        private System.Windows.Forms.ColumnHeader columnHeaderCommand;
        private System.Windows.Forms.ColumnHeader columnHeaderOrder;
    }
}