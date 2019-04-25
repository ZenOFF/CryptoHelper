using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NoviceCryptoTraderAdvisor
{
    internal partial class SetSortSettings : Form
    {
        private DirectionSort _directionSort = DirectionSort.Ascending;
        private bool IsDelLowVolume = true;
        private double _lowLevelVolume = 10;

        //список пар
        public List<MarketPair> _activeMarketList { get; private set; }

        public SetSortSettings()
        {
            InitializeComponent();
        }

        public SetSortSettings(List<MarketPair> activeMarketList) : this()
        {
            this._activeMarketList = activeMarketList;
        }

        private void SetSortSettings_Load(object sender, EventArgs e)
        {
            //MdiClient ctlMDI;
            // Loop through all of the form's controls looking
            // for the control of type MdiClient.
            this.Controls[0].BackColor= this.BackColor;
//            foreach (Control ctl in this.Controls)
//            {
//                try
//                {
//                    // Attempt to cast the control to type MdiClient.
//                    ctlMDI = (MdiClient)ctl;
//                    // Set the BackColor of the MdiClient control.
//                    ctlMDI.BackColor = this.BackColor;
//                }

//#pragma warning disable CS0168 // Переменная объявлена, но не используется
//                catch (InvalidCastException exc)
//#pragma warning restore CS0168 // Переменная объявлена, но не используется

//                {
//                    // Catch and ignore the error if casting failed.
//                }
//            }
        }

        //закрытие формы и запуск сортировки
        public void ApplyButton_Click(object sender, EventArgs e)
        {
            //удалять низкообъёмные пары True
            if (IsDelLowVolume)
            {
                DeleteLowVolumePair();
            }
            //проверяем на наличие хоть одного элемента
            if (!_activeMarketList.Any())
            {
                return;
            }

            SortingList sortingList = new SortingList();
            //экземпляр класса сравнения
            CompareMarketPair compareMarketPair = new CompareMarketPair(_directionSort);
            //выполняем сортировку
            _activeMarketList = sortingList.Quicksort(_activeMarketList, compareMarketPair) as List<MarketPair>;
        }

        //только цифры и разделители
        private void textBox1LowValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        //помощь
        private void SetSortSettings_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FormHelp.MainTraderHelp HelpForm = new FormHelp.MainTraderHelp();
            if (SettingsVariable.lagnuageApplication == "en")
            {
                HelpForm.Image_Load("Resources/SortSettings.jpg");
            }
            else if (SettingsVariable.lagnuageApplication == "ru")
            {
                HelpForm.Image_Load("Resources/SortSettingsRus.jpg");
            }
            HelpForm.ShowDialog(this);
            e.Cancel = true;
        }

        //удаляем
        private void DeleteLowVolumePair()
        {
            for (int i = _activeMarketList.Count - 1; i >= 0; i--)
            {
                //удаляем если объём ниже заданного
                if (_activeMarketList[i].VolumeValue < _lowLevelVolume)
                {
                    _activeMarketList[i].DeletePair();
                }
            }
        }

        private void radioButton2SortDescending_CheckedChanged(object sender, EventArgs e)
        {
            _directionSort = DirectionSort.Descending;
        }

        private void radioButton2SortAscending_CheckedChanged(object sender, EventArgs e)
        {
            _directionSort = DirectionSort.Ascending;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                IsDelLowVolume = true;
            }
            else
            {
                IsDelLowVolume = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _lowLevelVolume = Convert.ToDouble(textBox1LowValue.Text);
        }
    }
}