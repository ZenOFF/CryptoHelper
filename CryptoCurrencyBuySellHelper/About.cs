using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoviceCryptoTraderAdvisor
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            MdiClient ctlMDI;
            // Loop through all of the form's controls looking
            // for the control of type MdiClient.
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;
                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = this.BackColor;
                }
#pragma warning disable CS0168 // Переменная объявлена, но не используется
                catch (InvalidCastException exc)
#pragma warning restore CS0168 // Переменная объявлена, но не используется
                {
                    // Catch and ignore the error if casting failed.
                }
            }
        }

        private void ValletBitcoinAdressCopyBuffer_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1Bitcoin.Text);
            ShowMessage("Bitcoin");
        }
        private void ValletBitcoinCashAdressCopyBuffer_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2BitcoinCash.Text);
            ShowMessage("BitcoinCash");
        }
        private void ValletLitecoinAdressCopyBuffer_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox3Litecoin.Text);
            ShowMessage("Litecoin");
        }
        private void ValletEthereumAdressCopyBuffer_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox4Ethereum.Text);
            ShowMessage("Ethereum");
        }
        private void ShowMessage(string NameVallet)
        {
            MessageBox.Show(LanguageString.DynamicElements.MessageBox_Adres + NameVallet + LanguageString.DynamicElements.MessageBox_WalletToClipboard);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText("traderwithinsomnia@mail.ru");
            MessageBox.Show(LanguageString.DynamicElements.MessageBox_Adres+LanguageString.DynamicElements.MessageBox_CopyToClipboard);
        }
    }
}
