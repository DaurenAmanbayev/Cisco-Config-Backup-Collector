namespace RemoteWork.Managers
{
    partial class Category_Manager
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
            this.listBoxCategory = new System.Windows.Forms.ListBox();
            this.contextMenuStripCategory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxCategory
            // 
            this.listBoxCategory.ContextMenuStrip = this.contextMenuStripCategory;
            this.listBoxCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxCategory.FormattingEnabled = true;
            this.listBoxCategory.Location = new System.Drawing.Point(0, 0);
            this.listBoxCategory.Name = "listBoxCategory";
            this.listBoxCategory.Size = new System.Drawing.Size(254, 332);
            this.listBoxCategory.TabIndex = 0;
            // 
            // contextMenuStripCategory
            // 
            this.contextMenuStripCategory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCategoryToolStripMenuItem,
            this.editCategoryToolStripMenuItem,
            this.deleteCategoryToolStripMenuItem});
            this.contextMenuStripCategory.Name = "contextMenuStripCategory";
            this.contextMenuStripCategory.Size = new System.Drawing.Size(159, 70);
            // 
            // addCategoryToolStripMenuItem
            // 
            this.addCategoryToolStripMenuItem.Name = "addCategoryToolStripMenuItem";
            this.addCategoryToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addCategoryToolStripMenuItem.Text = "Add Category";
            // 
            // editCategoryToolStripMenuItem
            // 
            this.editCategoryToolStripMenuItem.Name = "editCategoryToolStripMenuItem";
            this.editCategoryToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.editCategoryToolStripMenuItem.Text = "Edit Category";
            // 
            // deleteCategoryToolStripMenuItem
            // 
            this.deleteCategoryToolStripMenuItem.Name = "deleteCategoryToolStripMenuItem";
            this.deleteCategoryToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.deleteCategoryToolStripMenuItem.Text = "Delete Category";
            // 
            // Category_Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 332);
            this.Controls.Add(this.listBoxCategory);
            this.Name = "Category_Manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Category_Manager";
            this.contextMenuStripCategory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxCategory;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCategory;
        private System.Windows.Forms.ToolStripMenuItem addCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCategoryToolStripMenuItem;
    }
}