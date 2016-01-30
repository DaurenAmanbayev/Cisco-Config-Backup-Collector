namespace RemoteWork.Managers
{
    partial class Command_Edit
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxAvailable = new System.Windows.Forms.ListBox();
            this.contextMenuStripAvailable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAllToCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBoxCurrent = new System.Windows.Forms.ListBox();
            this.contextMenuStripCurrent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeFromCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllFromCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripAvailable.SuspendLayout();
            this.contextMenuStripCurrent.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(339, 400);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 53);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxAvailable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBoxCurrent);
            this.splitContainer1.Size = new System.Drawing.Size(333, 294);
            this.splitContainer1.SplitterDistance = 148;
            this.splitContainer1.TabIndex = 0;
            // 
            // listBoxAvailable
            // 
            this.listBoxAvailable.ContextMenuStrip = this.contextMenuStripAvailable;
            this.listBoxAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAvailable.FormattingEnabled = true;
            this.listBoxAvailable.Location = new System.Drawing.Point(0, 0);
            this.listBoxAvailable.Name = "listBoxAvailable";
            this.listBoxAvailable.Size = new System.Drawing.Size(148, 294);
            this.listBoxAvailable.TabIndex = 0;
            // 
            // contextMenuStripAvailable
            // 
            this.contextMenuStripAvailable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToCurrentToolStripMenuItem,
            this.addAllToCurrentToolStripMenuItem});
            this.contextMenuStripAvailable.Name = "contextMenuStripAvailable";
            this.contextMenuStripAvailable.Size = new System.Drawing.Size(171, 48);
            // 
            // addToCurrentToolStripMenuItem
            // 
            this.addToCurrentToolStripMenuItem.Name = "addToCurrentToolStripMenuItem";
            this.addToCurrentToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.addToCurrentToolStripMenuItem.Text = "Add to Current";
            this.addToCurrentToolStripMenuItem.Click += new System.EventHandler(this.addToCurrentToolStripMenuItem_Click);
            // 
            // addAllToCurrentToolStripMenuItem
            // 
            this.addAllToCurrentToolStripMenuItem.Name = "addAllToCurrentToolStripMenuItem";
            this.addAllToCurrentToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.addAllToCurrentToolStripMenuItem.Text = "Add All to Current";
            this.addAllToCurrentToolStripMenuItem.Click += new System.EventHandler(this.addAllToCurrentToolStripMenuItem_Click);
            // 
            // listBoxCurrent
            // 
            this.listBoxCurrent.ContextMenuStrip = this.contextMenuStripCurrent;
            this.listBoxCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxCurrent.FormattingEnabled = true;
            this.listBoxCurrent.Location = new System.Drawing.Point(0, 0);
            this.listBoxCurrent.Name = "listBoxCurrent";
            this.listBoxCurrent.Size = new System.Drawing.Size(181, 294);
            this.listBoxCurrent.TabIndex = 0;
            // 
            // contextMenuStripCurrent
            // 
            this.contextMenuStripCurrent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeFromCurrentToolStripMenuItem,
            this.removeAllFromCurrentToolStripMenuItem});
            this.contextMenuStripCurrent.Name = "contextMenuStripCurrent";
            this.contextMenuStripCurrent.Size = new System.Drawing.Size(207, 70);
            // 
            // removeFromCurrentToolStripMenuItem
            // 
            this.removeFromCurrentToolStripMenuItem.Name = "removeFromCurrentToolStripMenuItem";
            this.removeFromCurrentToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.removeFromCurrentToolStripMenuItem.Text = "Remove from Current";
            this.removeFromCurrentToolStripMenuItem.Click += new System.EventHandler(this.removeFromCurrentToolStripMenuItem_Click);
            // 
            // removeAllFromCurrentToolStripMenuItem
            // 
            this.removeAllFromCurrentToolStripMenuItem.Name = "removeAllFromCurrentToolStripMenuItem";
            this.removeAllFromCurrentToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.removeAllFromCurrentToolStripMenuItem.Text = "Remove All from Current";
            this.removeAllFromCurrentToolStripMenuItem.Click += new System.EventHandler(this.removeAllFromCurrentToolStripMenuItem_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.textBoxName);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(333, 44);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(44, 3);
            this.textBoxName.Multiline = true;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(280, 31);
            this.textBoxName.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.buttonCancel);
            this.flowLayoutPanel2.Controls.Add(this.buttonOK);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 353);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(333, 44);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(255, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 34);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(174, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 34);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // Command_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 400);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Command_Edit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Command_Edit";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripAvailable.ResumeLayout(false);
            this.contextMenuStripCurrent.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxAvailable;
        private System.Windows.Forms.ListBox listBoxCurrent;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAvailable;
        private System.Windows.Forms.ToolStripMenuItem addToCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAllToCurrentToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCurrent;
        private System.Windows.Forms.ToolStripMenuItem removeFromCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllFromCurrentToolStripMenuItem;
    }
}