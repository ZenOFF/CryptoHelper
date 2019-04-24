using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoviceCryptoTraderAdvisor
{
    public partial class HelpToTrader : Form
    {
        private TechAnalisis TechAnalisisStudies = new TechAnalisis();

        private Conclusion_TechAnalisis ConclusionTechAnalisis = new Conclusion_TechAnalisis();
        //максимальное число пар
        private int _maxCountPairs = 200;

        //список доступных пар
        private List<string> ListCurrenciesArray = new List<string>();

        //массив элементов формы
        private List<MarketPair> _activeMarketList = new List<MarketPair>();

        //массив для сортировки элементов формы
        private List<MarketPair> _activeMarketListSorted = new List<MarketPair>();

        //высота панели класса MarketPair
        private int _panelElementsHeight { get; set; } = 140;

        //ширина между панелями класса MarketPair
        private int _panelDistance { get; set; } = 20;

        //интервал
        private string ChartTimestamp = "thirtyMin";

        public HelpToTrader()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(SettingsVariable.LagnuageApplication);
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(SettingsVariable.LagnuageApplication);

            InitializeComponent();
            FillingListCurrencies();
            SettingsVariable.Read("MarketPairs.xml");
        }

        //проверка ответа сервера
        public void CheckConnection(string Response)
        {
            if (String.IsNullOrEmpty(Response))
            {
                toolStripStatusLabel3.Text = LanguageString.DynamicElements.ConnectionLost;
            }
            else
            {
                toolStripStatusLabel3.Text = LanguageString.DynamicElements.ConnectionOk;
            }
        }

        //заполняем список при запуске
        private async void FillingListCurrencies()
        {
            GetSourceHTMLClient GetHTML = new GetSourceHTMLClient(1);
            //получаем весь список от API
            string InputLongString = await GetHTML.GetMarketSummariesAsync();

            CheckConnection(InputLongString);

            listCurrencies.Items.Clear();
            Regex rgx = new Regex(@"""MarketName"":""([A-Z]+-\w[A-Z]+)"""); //поиск значений
            foreach (Match match in rgx.Matches(InputLongString))
            {
                string MarketName = match.Groups[1].Value;
                ListCurrenciesArray.Add(MarketName);//заполнение массива для поиска
                listCurrencies.Items.Add(MarketName);//заполнение ListBox
            }
        }

        //кнопка добавить пару
        private void AddPair_Click(object sender, EventArgs e)
        {
            if (listCurrencies.SelectedItem != null)
            {
                if (_activeMarketList.Count > _maxCountPairs)
                {
                    MessageBox.Show(LanguageString.DynamicElements.MessageBox_Show_MaxPair);
                    return;
                }
                AddPair(listCurrencies.SelectedItem.ToString());
                listCurrencies.Items.Remove(listCurrencies.SelectedItem); //удаляем выбранный элемент из списка пар
            }
        }

        //Добавляем экземпляр класса в список и отрисовываем объекты
        private void AddPair(string marketName)
        {
            Point _left_loc = NewLocationForPanel(_activeMarketList.Count);
            //отключаем ScrollBar (из за специфики отображения компанента Panel необходимо отключить прокрутку)
            MainPanelWithScroll.AutoScroll = false;
            MarketPair marketPair = new MarketPair(marketName, _left_loc, _activeMarketList, ref MainPanelWithScroll, _panelElementsHeight, _panelDistance);
            //подписываем на событие обновления StatusBar
            marketPair.onUpdateEnd += IncrementProgressBar;
            //отрисовываем элементы
            marketPair.AddMarketElement();
            //включаем ScrollBar
            MainPanelWithScroll.AutoScroll = true;
            //добавляем в список
            _activeMarketList.Add(marketPair);
        }

        //Вычисляем позиция из колличества отрисованых панелей
        private Point NewLocationForPanel(int CountElements)
        {
            Point Loc = new Point(12, 12);

            if (CountElements >= 1)
            {
                Loc.X = 12;
                Loc.Y = 12 + _activeMarketList.Count * 160;
            }
            return Loc;
        }

        //кнопка удалить всё
        private void DeleteAllPairButtonClick(object sender, EventArgs e)
        {
            DeleteAllPair(true);
        }

        //удаляем все элементы и очищаем список
        private void DeleteAllPair(bool IsClearListPairs)
        {
            MainPanelWithScroll.Controls.Clear();
            if (IsClearListPairs)
            {
                _activeMarketList.Clear();
            }
            GC.Collect();
        }

        //обновляем информацию индикаторов технического анализа
        private async void UpdateButton_Click(object sender, EventArgs e)
        {
            // если есть выбранные пары
            if (_activeMarketList.Count > 0)
            {
                //блокируем повторное нажатие до завершения обновления
                BlockButton(false);
                int ii = 1;
                foreach (MarketPair marketPair in _activeMarketList)
                {
                    Console.WriteLine(ii);
                    ii++;
                    marketPair.UpdateInfoAsync(ChartTimestamp);
                    //для ограничения количества запросов к серверу
                    await Task.Delay(4000); 
                }
            }
        }

        //увеличение индикатора выполнения
        private void IncrementProgressBar()
        {
            //устанавливаем значение максимума
            toolStripProgressBar1.Maximum = _activeMarketList.Count;
            //увеличиваем позицию ProgressBar
            toolStripProgressBar1.Value++;
            //обновляем  Windows Taskbar API
            TaskbarProgress.SetValue(this.Handle, toolStripProgressBar1.Value, toolStripProgressBar1.Maximum);
            try
            {
                if (toolStripProgressBar1.Value != toolStripProgressBar1.Maximum && toolStripProgressBar1.Value != 0)
                {
                    toolStripStatusLabel2.Text = LanguageString.DynamicElements.ProgressBarWorked;
                }
                if (toolStripProgressBar1.Value == 0)
                {
                    toolStripStatusLabel2.Text = LanguageString.DynamicElements.ProgressBarReady;
                }
                if (toolStripProgressBar1.Value >= toolStripProgressBar1.Maximum)
                {
                    toolStripProgressBar1.Value = 0;
                    BlockButton(true);//снятие блокировки
                    toolStripStatusLabel2.Text = LanguageString.DynamicElements.ProgressBarDone;
                }
            }
            catch (NullReferenceException) //null при закрытии во время обновления
            {
                return;
            }
        }

        // обновление списка из массива
        private void RefreshListPairButton_Click(object sender, EventArgs e)
        {
            listCurrencies.Items.Clear();
            foreach (string str in ListCurrenciesArray)
            {
                listCurrencies.Items.Add(str);
            }
        }

        //кнопка "добавить всё"
        private void AddAll_Click(object sender, EventArgs e)
        {
            string[] namesPairsArr = listCurrencies.Items.OfType<string>().ToArray();
            AddAllPair(namesPairsArr);
            listCurrencies.Items.Clear();
        }

        //перебиравем весь список и добавляем элементы
        private void AddAllPair(string[] namesPair)
        {
            if (namesPair != null)
            {
                foreach (var item in namesPair)
                {
                    if (_activeMarketList.Count > _maxCountPairs)
                    {
                        MessageBox.Show(LanguageString.DynamicElements.MessageBox_Show_MaxPair);
                        break;
                    }
                    AddPair(item);
                }
            }
        }

        //сохранение при закрытии приложения
        private void Form1Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(LanguageString.DynamicElements.MessageBox_SaveQuestion, LanguageString.DynamicElements.MessageBox_SaveTextWindow, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SettingsVariable.Save("MarketPairs.xml");
            }
        }

        //открытие настроек сортировки и фильтра
        private void Sort_Click(object sender, EventArgs e)
        {
            if (_activeMarketList.Count <= 1)
            {
                return;
            }
            MainPanelWithScroll.AutoScroll = false;
            SetSortSettings FormSortSettings = new SetSortSettings(_activeMarketList);
            //отображаем форму настроек сортировки
            if (FormSortSettings.ShowDialog(this) == DialogResult.OK)
            {
                //получаем сортированный список
                _activeMarketListSorted = FormSortSettings._activeMarketList;
            }
            FormSortSettings.Dispose();

            //перемещаем элементы в соответсвиии с сортированым списком
            MoveElementsPanel moveElements = new MoveElementsPanel(MainPanelWithScroll, _activeMarketListSorted, _activeMarketList);
            //подставляем сортированый список
            _activeMarketList = moveElements.ReLocationElement();
            MainPanelWithScroll.AutoScroll = true;
        }

        //блокируем кнопки управления на время выполнения обновления или сортировки
        private void BlockButton(bool Status)
        {
            UpdateButton.Enabled = Status;
            SortButton.Enabled = Status;
            AddPairButton.Enabled = Status;
            AddAllPairButton.Enabled = Status;
            DeleteAllPairButton.Enabled = Status;
        }

        //размер панели привязываем к размеру формы
        private void Form1Main_Resize(object sender, EventArgs e)
        {
            int MarginWidthMainPanel = MainPanelWithScroll.Location.X + 30;
            int MarginHeightMainPanel = MainPanelWithScroll.Location.Y + 65;

            try
            {
                MainPanelWithScroll.Width = this.Width - MarginWidthMainPanel;
                MainPanelWithScroll.Height = this.Height - MarginHeightMainPanel;
            }
            catch (NullReferenceException)
            {
                return;
            }
        }

        //надпись открытие ссылки www.bittrex.com
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.bittrex.com");
        }

        //смена языка English
        private void EnglishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsVariable.LagnuageApplication = "en";

            if (MessageBox.Show(LanguageString.DynamicElements.MessageBox_ChangeLangQuestion, LanguageString.DynamicElements.MessageBox_ChangeLangTextWindow, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        //смена языка Russian
        private void RussianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsVariable.LagnuageApplication = "ru";

            if (MessageBox.Show(LanguageString.DynamicElements.MessageBox_ChangeLangQuestion, LanguageString.DynamicElements.MessageBox_ChangeLangTextWindow, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        //интервал
        private void RadioButtonTimeStamp_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            switch (btn.Name)
            {
                case "radioButton1": ChartTimestamp = "day"; break;
                case "radioButton2": ChartTimestamp = "hour"; break;
                case "radioButton3": ChartTimestamp = "thirtyMin"; break;
                case "radioButton4": ChartTimestamp = "fiveMin"; break;
                case "radioButton5": ChartTimestamp = "oneMin"; break;
            }
        }

        //поиск в списке торговых пар
        private void FindTextBox_TextChanged(object sender, EventArgs e)
        {
            listCurrencies.BeginUpdate();
            listCurrencies.Items.Clear();

            if (!string.IsNullOrEmpty(FindTextBox.Text))
            {
                foreach (string str in ListCurrenciesArray)
                {
                    if (str.Contains(FindTextBox.Text))
                    {
                        listCurrencies.Items.Add(str);
                    }
                }
            }
            else
            {
                foreach (string str in ListCurrenciesArray)
                {
                    listCurrencies.Items.Add(str);
                }
            }

            listCurrencies.EndUpdate();
        }

        //только заглавные
        private void FindTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        //справка
        private void HelpToTrader_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FormHelp.MainTraderHelp HelpForm = new FormHelp.MainTraderHelp();
            if (SettingsVariable.LagnuageApplication == "en")
            {
                HelpForm.Image_Load("Resources/Main.jpg");
            }
            else if (SettingsVariable.LagnuageApplication == "ru")
            {
                HelpForm.Image_Load("Resources/MainRus.jpg");
            }
            HelpForm.ShowDialog(this);
            e.Cancel = true;
        }

        //Menu click methods***************
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StudiesParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudiesSettings SettingsForm = new StudiesSettings();
            SettingsForm.ShowDialog(this);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm About = new AboutForm();
            About.ShowDialog(this);
        }

        private void SaveChosenPairsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SavePair("MarketPairs.dat", ActiveMarketLabelList);
            //MessageBox.Show(LanguageString.DynamicElements.MessageBox_Show_SaveMenuButton);
        }

        //private void SavePair(string path, List<object[]> SaveArray)
        //{
        //    if (SaveArray != null)
        //    {
        //        StreamWriter sw = new StreamWriter(path, false);
        //        for (int i = 0; i < SaveArray.Count; i++)
        //        {
        //            Label GetNameMarket = SaveArray[i].GetValue(1) as Label;
        //            sw.WriteLine(GetNameMarket.Text);
        //        }
        //        sw.Close();
        //    }
        //}

        //private void LoadPair(string path)
        //{
        //    if (File.Exists(path) == false)
        //    {
        //        return; //если файла сэйва нет
        //    }
        //    StreamReader sr = new StreamReader(path);
        //    while (sr.EndOfStream == false)
        //    {
        //        string namelb = sr.ReadLine();
        //        if (namelb != null)
        //        {
        //            AddMarketElement(namelb);
        //        }
        //    }
        //    sr.Close();
        //}

        //private void RSIValueLabel_Change(Label RSIValueLabel, double RSIValue)
        //{
        //    if (ConclusionTechAnalisis.RSIvalue_Conclusion(RSIValue) == 1)
        //    {
        //        RSIValueLabel.BackColor = Color.Red;
        //        //opportunity sell 1
        //    }
        //    else if (ConclusionTechAnalisis.RSIvalue_Conclusion(RSIValue) == 2)
        //    {
        //        RSIValueLabel.BackColor = Color.Green;
        //        //opportunity buy 2
        //    }
        //}

        //private void StochasticValueLabel_Change(Label StochasticKValueLabel, Label StochasticDValueLabel, double StochasticKValue, double StochasticDValue)
        //{
        //    if (ConclusionTechAnalisis.StochasticValue_Conclusion(StochasticKValue, StochasticDValue) == 1)
        //    {
        //        StochasticKValueLabel.BackColor = Color.Red;
        //        StochasticDValueLabel.BackColor = Color.Red;
        //        //opportunity sell 1
        //    }
        //    else if (ConclusionTechAnalisis.StochasticValue_Conclusion(StochasticKValue, StochasticDValue) == 2)
        //    {
        //        StochasticKValueLabel.BackColor = Color.Green;
        //        StochasticDValueLabel.BackColor = Color.Green;
        //        //opportunity buy 2
        //    }
        //}

        //private void StochasticRSIValueLabel_Change(Label StochasticRSIFastLabel, Label StochasticRSISlowLabel, double StochasticRSIKValue, double StochasticRSIDValue)
        //{
        //    if (ConclusionTechAnalisis.StochasticRSIvalue_Conclusion(StochasticRSIKValue, StochasticRSIDValue) == 1)
        //    {
        //        StochasticRSIFastLabel.BackColor = Color.Red;
        //        StochasticRSISlowLabel.BackColor = Color.Red;
        //        //opportunity sell 1
        //    }
        //    else if (ConclusionTechAnalisis.StochasticRSIvalue_Conclusion(StochasticRSIKValue, StochasticRSIDValue) == 2)
        //    {
        //        StochasticRSIFastLabel.BackColor = Color.Green;
        //        StochasticRSISlowLabel.BackColor = Color.Green;
        //        //opportunity buy 2
        //    }
        //}

        //private void MACDValueLabel_Change(Label MACDValueLabel, Label MACDSignalValuelabel, Label MACDHistogramValuelabel, double[] MACDArray, double[] SignalMACDArray, double[] MACDHistogramArray)
        //{
        //    int ConclusionMACD = ConclusionTechAnalisis.MACDvalue_Conclusion(MACDArray, SignalMACDArray, MACDHistogramArray);
        //    if (ConclusionMACD == 1)
        //    {
        //        MACDValueLabel.BackColor = Color.Red;
        //        MACDSignalValuelabel.BackColor = Color.Red;
        //        MACDHistogramValuelabel.BackColor = Color.Red;
        //        //opportunity sell 1
        //    }
        //    else if (ConclusionMACD == 2)
        //    {
        //        MACDValueLabel.BackColor = Color.Green;
        //        MACDSignalValuelabel.BackColor = Color.Green;
        //        MACDHistogramValuelabel.BackColor = Color.Green;
        //        //opportunity buy 2
        //    }
        //}
    }
}