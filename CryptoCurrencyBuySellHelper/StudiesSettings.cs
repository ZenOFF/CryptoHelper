using System;
using System.Windows.Forms;

namespace NoviceCryptoTraderAdvisor
{
    public partial class StudiesSettings : Form
    {
        private string tempText = "";

        public StudiesSettings()
        {
            InitializeComponent();
            SetCurrentValues();
        }

        //цвет все компонентов формы
        private void StudiesSettings_Load(object sender, EventArgs e)
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

        //получаем значения и техущих из настроек
        private void SetCurrentValues()
        {
            textBox1RsiPeriod.Text = SettingsVariable.rsiPeriod.ToString();
            textBoxRSIBuyValue.Text = SettingsVariable.rsiBuylValue.ToString();
            textBoxRSISellValue.Text = SettingsVariable.rsiSellValue.ToString();

            textBox2StochasticsPeriod.Text = SettingsVariable.stochasticsPeriod.ToString();
            textBox3StochasticsSmooth.Text = SettingsVariable.stochasticsSmooth.ToString();
            textBoxRSISellValue.Text = SettingsVariable.stochasticSellValue.ToString();
            textBoxRSIBuyValue.Text = SettingsVariable.stochasticBuyValue.ToString();

            textBox4StochasticsRSIPeriod.Text = SettingsVariable.stochasticsRSIPeriod.ToString();
            textBox5StochasticsRSISmooth.Text = SettingsVariable.stochasticsRSISmooth.ToString();
            textBoxStochascticsRSISellValue.Text = SettingsVariable.stochasticRSISellValue.ToString();
            textBoxStochascticsRSIBuyValue.Text = SettingsVariable.stochasticRSIBuyValue.ToString();

            textBox1FastMAPeriod.Text = SettingsVariable.fastMAPeriod.ToString();
            textBox2SlowMAPeriod.Text = SettingsVariable.slowMAPeriod.ToString();
            textBox3SignalMAPeriod.Text = SettingsVariable.signalMAPeriod.ToString();
        }

        //кнопка сохранить и закрыть
        private void Button1Close_Click(object sender, EventArgs e)
        {
            SettingsVariable.rsiPeriod = Convert.ToInt32(textBox1RsiPeriod.Text);
            SettingsVariable.rsiBuylValue = Convert.ToInt32(textBoxRSIBuyValue.Text);
            SettingsVariable.rsiSellValue = Convert.ToInt32(textBoxRSISellValue.Text);

            SettingsVariable.stochasticsPeriod = Convert.ToInt32(textBox2StochasticsPeriod.Text);
            SettingsVariable.stochasticsSmooth = Convert.ToInt32(textBox3StochasticsSmooth.Text);
            SettingsVariable.stochasticSellValue = Convert.ToInt32(textBoxRSISellValue.Text);
            SettingsVariable.stochasticBuyValue = Convert.ToInt32(textBoxRSIBuyValue.Text);

            SettingsVariable.stochasticsRSIPeriod = Convert.ToInt32(textBox4StochasticsRSIPeriod.Text);
            SettingsVariable.stochasticsRSISmooth = Convert.ToInt32(textBox5StochasticsRSISmooth.Text);
            SettingsVariable.stochasticRSISellValue = Convert.ToInt32(textBoxStochascticsRSISellValue.Text);
            SettingsVariable.stochasticRSIBuyValue = Convert.ToInt32(textBoxStochascticsRSIBuyValue.Text);

            SettingsVariable.fastMAPeriod = Convert.ToInt32(textBox1FastMAPeriod.Text);
            SettingsVariable.slowMAPeriod = Convert.ToInt32(textBox2SlowMAPeriod.Text);
            SettingsVariable.signalMAPeriod = Convert.ToInt32(textBox3SignalMAPeriod.Text);
            Close();
        }

        //только цифры и разделители для полей ввода
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //дефолтные значения
        private void Button1Default_Click(object sender, EventArgs e)
        {
            //устанавливаем по дефолту
            SettingsVariable.SetDefault(false);
            SetCurrentValues();
        }

        //помощь
        private void StudiesSettings_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FormHelp.MainTraderHelp HelpForm = new FormHelp.MainTraderHelp();
            if (SettingsVariable.lagnuageApplication == "en")
            {
                HelpForm.Image_Load("Resources/SettingsStudies.jpg");
            }
            else if (SettingsVariable.lagnuageApplication == "ru")
            {
                HelpForm.Image_Load("Resources/SettingsStudiesRus.jpg");
            }
            HelpForm.ShowDialog(this);
            e.Cancel = true;
        }

        //проверка на Null
        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            tempText = textBox.Text;
        }

        //если Null возвращаем прежнее значение
        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text) || int.Parse(textBox.Text) <= 0)
            {
                MessageBox.Show("Значение не может быть пустым");
                textBox.Text = tempText;
            }
        }
    }
}