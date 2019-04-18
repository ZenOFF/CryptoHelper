namespace NoviceCryptoTraderAdvisor
{
    partial class SetSortSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetSortSettings));
            this.ApplyButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2SortDescending = new System.Windows.Forms.RadioButton();
            this.radioButton2SortAscending = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1LowValue = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ApplyButton
            // 
            this.ApplyButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.ApplyButton, "ApplyButton");
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2SortDescending);
            this.groupBox1.Controls.Add(this.radioButton2SortAscending);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // radioButton2SortDescending
            // 
            resources.ApplyResources(this.radioButton2SortDescending, "radioButton2SortDescending");
            this.radioButton2SortDescending.Name = "radioButton2SortDescending";
            this.radioButton2SortDescending.UseVisualStyleBackColor = true;
            this.radioButton2SortDescending.CheckedChanged += new System.EventHandler(this.radioButton2SortDescending_CheckedChanged);
            // 
            // radioButton2SortAscending
            // 
            resources.ApplyResources(this.radioButton2SortAscending, "radioButton2SortAscending");
            this.radioButton2SortAscending.Checked = true;
            this.radioButton2SortAscending.Name = "radioButton2SortAscending";
            this.radioButton2SortAscending.TabStop = true;
            this.radioButton2SortAscending.UseVisualStyleBackColor = true;
            this.radioButton2SortAscending.CheckedChanged += new System.EventHandler(this.radioButton2SortAscending_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.textBox1LowValue);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox1LowValue
            // 
            resources.ApplyResources(this.textBox1LowValue, "textBox1LowValue");
            this.textBox1LowValue.Name = "textBox1LowValue";
            this.textBox1LowValue.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1LowValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1LowValue_KeyPress);
            // 
            // SetSortSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ApplyButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetSortSettings";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.SetSortSettings_HelpButtonClicked);
            this.Load += new System.EventHandler(this.SetSortSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2SortDescending;
        private System.Windows.Forms.RadioButton radioButton2SortAscending;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1LowValue;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}