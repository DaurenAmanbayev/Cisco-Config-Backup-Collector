namespace RemoteWork
{
    partial class Config_Watcher
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
            this.textBoxConfig = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxConfig
            // 
            this.textBoxConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxConfig.Location = new System.Drawing.Point(0, 0);
            this.textBoxConfig.Multiline = true;
            this.textBoxConfig.Name = "textBoxConfig";
            this.textBoxConfig.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxConfig.Size = new System.Drawing.Size(284, 388);
            this.textBoxConfig.TabIndex = 0;
            // 
            // Config_Watcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 388);
            this.Controls.Add(this.textBoxConfig);
            this.Name = "Config_Watcher";
            this.Text = "Config_Watcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxConfig;
    }
}