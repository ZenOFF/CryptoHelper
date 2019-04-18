namespace NoviceCryptoTraderAdvisor.FormHelp
{
    partial class MainTraderHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainTraderHelp));
            this.HelpImagePictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.HelpImagePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // HelpImagePictureBox1
            // 
            resources.ApplyResources(this.HelpImagePictureBox1, "HelpImagePictureBox1");
            this.HelpImagePictureBox1.Name = "HelpImagePictureBox1";
            this.HelpImagePictureBox1.TabStop = false;
            this.HelpImagePictureBox1.Click += new System.EventHandler(this.HelpImagePictureBox_ClickClose);
            // 
            // MainTraderHelp
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HelpImagePictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainTraderHelp";
          
            ((System.ComponentModel.ISupportInitialize)(this.HelpImagePictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox HelpImagePictureBox1;
    }
}