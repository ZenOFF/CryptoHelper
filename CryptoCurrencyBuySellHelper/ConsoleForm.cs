using System;
using System.Windows.Forms;

namespace NoviceCryptoTraderAdvisor
{
    public partial class ConsoleForm : Form
    {
        public ConsoleForm()
        {
            InitializeComponent();
        }

        public void AddString(string addString)
        {
            textBox_console.Text = textBox_console.Text + addString + Environment.NewLine;
        }

        private void ConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}