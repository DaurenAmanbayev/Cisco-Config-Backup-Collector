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
            this.richTextBoxConfig = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxConfig
            // 
            this.richTextBoxConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxConfig.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxConfig.Name = "richTextBoxConfig";
            this.richTextBoxConfig.Size = new System.Drawing.Size(327, 411);
            this.richTextBoxConfig.TabIndex = 0;
            this.richTextBoxConfig.Text = "";
            // 
            // Config_Watcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 411);
            this.Controls.Add(this.richTextBoxConfig);
            this.Name = "Config_Watcher";
            this.Text = "Config_Watcher";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxConfig;



    }
}