using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NoviceCryptoTraderAdvisor
{
    public class MarketPair
    {
        public Point _panelLoc { get; private set; } //положение панели с элементами пары
        private int _panelElementsHeight { get; set; } = 140; //высота панели элементов
        private int _panelDistance { get; set; } = 20; //ширина между панелями
        private Panel _mainPanelWithElements;
        private Control[] _elementsOnPanel = new Control[21];
        private List<MarketPair> _activeMarketList;
        private ConsoleForm _consoleForm_Ref;

        public int PosY { get { return _panelLoc.Y; } }
        public string _marketName { get; set; } //имя пары

        public delegate void updDel();

        public event updDel onUpdateEnd;

        private string _marketNameWithoutHyphen
        {
            get
            {
                return _marketName.Remove(_marketName.IndexOf('\u002D'), 1);//cut out hyphen from element name
            }
        }

        public double RSIValue
        {
            get
            {
                Label LabelRSIValue = _elementsOnPanel[4] as Label;
                return Convert.ToDouble(LabelRSIValue.Text);
            }
        }

        public delegate bool onlyDigits(char element);

        public double VolumeValue
        {
            get
            {
                Label LabelVolumeValue = _elementsOnPanel[10] as Label;
                ParsingResponse parsingResponse = new ParsingResponse();
                onlyDigits getDigits = parsingResponse.GetDigits;
                string justNumbers = new String(LabelVolumeValue.Text.Where(x => getDigits(x)).ToArray());

                return Convert.ToDouble(justNumbers);
            }
        }

        public MarketPair(string MarketName, Point Left_loc, List<MarketPair> ActiveMarketList, ref Panel MainPanelWithElements)
        {
            //инициализируем переменные
            //аббревиатура
            this._marketName = MarketName;
            //список для хранения ссылок на экземляры
            this._activeMarketList = ActiveMarketList;
            //позиция левого верхнего угла объекта
            this._panelLoc = Left_loc;
            //родительская панель
            this._mainPanelWithElements = MainPanelWithElements;
        }

        public MarketPair(string MarketName, Point Left_loc, List<MarketPair> ActiveMarketList, ref Panel MainPanelWithElements,
            int PanelHeight, int PanelDistance) : this(MarketName, Left_loc, ActiveMarketList, ref MainPanelWithElements)
        {
            //инициализируем переменные
            this._panelElementsHeight = PanelHeight;//высота
            this._panelDistance = PanelDistance;//расстояние между панелями
        }

        public MarketPair(string MarketName, Point Left_loc, List<MarketPair> ActiveMarketList, ref Panel MainPanelWithElements,
           int PanelHeight, int PanelDistance, ConsoleForm consoleForm_Ref) : this(MarketName, Left_loc, ActiveMarketList, ref MainPanelWithElements, PanelHeight, PanelDistance)
        {
            _consoleForm_Ref = consoleForm_Ref;
        }

        public void AddMarketElement()
        {
            #region AddElements

            Panel PanelElements = new Panel(); //панель элементов
            //PanelElements.Name = "Panel" + _indexFormElements;
            PanelElements.Name = "Panel";
            PanelElements.BorderStyle = BorderStyle.Fixed3D;
            PanelElements.Width = 880;
            PanelElements.Height = _panelElementsHeight;
            PanelElements.Location = _panelLoc;
            _mainPanelWithElements.Controls.Add(PanelElements);

            Label MarketNamelabel = new Label();//надпись с название пары
            MarketNamelabel.Name = _marketNameWithoutHyphen + "label";
            MarketNamelabel.Text = _marketName;
            MarketNamelabel.Width = 80;
            MarketNamelabel.Height = 20;
            MarketNamelabel.Location = new Point(8, 10);
            MarketNamelabel.Font = new Font(MarketNamelabel.Font.Name, MarketNamelabel.Font.Size, FontStyle.Underline);
            MarketNamelabel.ForeColor = Color.Blue;
            MarketNamelabel.Cursor = Cursors.Hand;
            MarketNamelabel.Click += LabelMarketName_LinkClicked;
            PanelElements.Controls.Add(MarketNamelabel);

            GroupBox TechnicalAnalisisGroupBox = new GroupBox(); //GroupBox с элементами теханализа
            TechnicalAnalisisGroupBox.Name = "Groupbox";
            TechnicalAnalisisGroupBox.Text = LanguageString.DynamicElements.GroupBox_TechnicalAnalisis;
            TechnicalAnalisisGroupBox.Width = 700;
            TechnicalAnalisisGroupBox.Height = 127;
            TechnicalAnalisisGroupBox.Location = new Point(170, 4);
            PanelElements.Controls.Add(TechnicalAnalisisGroupBox);

            Label RSIlabel = new Label(); //Надпись RSI:
            RSIlabel.Name = "RSIlabel";
            RSIlabel.Text = LanguageString.DynamicElements.RSIlabel_Text;
            RSIlabel.AutoSize = true;
            RSIlabel.Location = new Point(9, 20);
            TechnicalAnalisisGroupBox.Controls.Add(RSIlabel);

            Label RSIValuelabel = new Label(); //надпись со значением RSI
            RSIValuelabel.Name = "RSIValuelabel" + _marketNameWithoutHyphen;
            RSIValuelabel.Text = "50";
            RSIValuelabel.Width = 70;
            RSIValuelabel.Height = 20;
            RSIValuelabel.Location = new Point(40, 20);
            TechnicalAnalisisGroupBox.Controls.Add(RSIValuelabel);

            Button DeletePairButton = new Button();
            DeletePairButton.Name = _marketNameWithoutHyphen + "Delete";
            DeletePairButton.Text = LanguageString.DynamicElements.DeletePairButton_Text;
            DeletePairButton.Width = 65;
            DeletePairButton.Height = 25;
            DeletePairButton.Location = new Point(630, 45);
            DeletePairButton.Click += Delete_Button;
            DeletePairButton.Tag = _marketName;//запись для удаления
            TechnicalAnalisisGroupBox.Controls.Add(DeletePairButton);

            Label HighValuelabel = new Label();
            HighValuelabel.Name = "labelHighValue";
            HighValuelabel.Text = LanguageString.DynamicElements.HighValuelabel_Text + "0,00000000";
            HighValuelabel.Width = 160;
            HighValuelabel.Height = 20;
            HighValuelabel.Location = new Point(8, 30);
            PanelElements.Controls.Add(HighValuelabel);

            Label LowValuelabel = new Label();
            LowValuelabel.Name = "labelLowValue";
            LowValuelabel.Text = LanguageString.DynamicElements.LowValuelabel_Text + "0,00000000";
            LowValuelabel.Width = 160;
            LowValuelabel.Height = 20;
            LowValuelabel.Location = new Point(8, 50);
            PanelElements.Controls.Add(LowValuelabel);

            Label VolumeValuelabel = new Label();
            VolumeValuelabel.Name = "labelVolumeValue";
            VolumeValuelabel.Text = LanguageString.DynamicElements.VolumeValuelabel_Text + "0,00000000";
            VolumeValuelabel.Width = 160;
            VolumeValuelabel.Height = 20;
            VolumeValuelabel.Location = new Point(8, 70);
            PanelElements.Controls.Add(VolumeValuelabel);

            Label LastValuelabel = new Label();//надпись с название пары
            LastValuelabel.Name = "labelLastValue";
            LastValuelabel.Text = LanguageString.DynamicElements.LastValuelabel_Text + "0,00000000";
            LastValuelabel.Width = 160;
            LastValuelabel.Height = 20;
            LastValuelabel.Location = new Point(8, 90);
            PanelElements.Controls.Add(LastValuelabel);

            Label BaseValuelabel = new Label();//надпись с название пары
            BaseValuelabel.Name = "BaseValuelabel";
            BaseValuelabel.Text = LanguageString.DynamicElements.BaseValuelabel_Text + "0,00000000";
            BaseValuelabel.Width = 160;
            BaseValuelabel.Height = 20;
            BaseValuelabel.Location = new Point(8, 110);
            PanelElements.Controls.Add(BaseValuelabel);

            ////////////////////////////////////////////////////////////
            Label StochRSIlabel = new Label(); //Надпись RSI:
            StochRSIlabel.Name = "StochRSI";
            StochRSIlabel.Text = LanguageString.DynamicElements.StochRSIlabel_Text;
            StochRSIlabel.AutoSize = true;
            StochRSIlabel.Location = new Point(9, 45);
            TechnicalAnalisisGroupBox.Controls.Add(StochRSIlabel);

            Label StochRSIFastValuelabel = new Label(); //надпись со значением RSI
            StochRSIFastValuelabel.Name = "StochRSIFastValue" + _marketNameWithoutHyphen;
            StochRSIFastValuelabel.Text = "0.5";
            StochRSIFastValuelabel.Width = 40;
            StochRSIFastValuelabel.Height = 20;
            StochRSIFastValuelabel.Location = new Point(65, 45);
            TechnicalAnalisisGroupBox.Controls.Add(StochRSIFastValuelabel);

            Label StochRSISlowValuelabel = new Label(); //надпись со значением StochRSI
            StochRSISlowValuelabel.Name = "StochRSISlowValue" + _marketNameWithoutHyphen;
            StochRSISlowValuelabel.Text = "0.5";
            StochRSISlowValuelabel.Width = 40;
            StochRSISlowValuelabel.Height = 20;
            StochRSISlowValuelabel.Location = new Point(65, 67);
            TechnicalAnalisisGroupBox.Controls.Add(StochRSISlowValuelabel);

            Label Stochlabel = new Label(); //Надпись Stochastic:
            Stochlabel.Name = "Stochastic";
            Stochlabel.Text = LanguageString.DynamicElements.Stochlabel_Text;
            Stochlabel.AutoSize = true;
            Stochlabel.Location = new Point(110, 45);

            TechnicalAnalisisGroupBox.Controls.Add(Stochlabel);

            Label StochFastValuelabel = new Label(); //надпись со значением Stochastic Fast
            StochFastValuelabel.Name = "StochasticFastValue" + _marketNameWithoutHyphen;
            StochFastValuelabel.Text = "0.5";
            StochFastValuelabel.Width = 40;
            StochFastValuelabel.Height = 20;
            StochFastValuelabel.Location = new Point(170, 45);
            TechnicalAnalisisGroupBox.Controls.Add(StochFastValuelabel);

            Label StochSlowValuelabel = new Label(); //надпись со значением Stochastic Slow
            StochSlowValuelabel.Name = "StochasticSlowValue" + _marketNameWithoutHyphen;
            StochSlowValuelabel.Text = "0.5";
            StochSlowValuelabel.Width = 40;
            StochSlowValuelabel.Height = 20;
            StochSlowValuelabel.Location = new Point(170, 67);
            TechnicalAnalisisGroupBox.Controls.Add(StochSlowValuelabel);

            /////////////////////////////////////////////

            Label MACDlabel = new Label(); //Надпись MACD:
            MACDlabel.Name = "MACD";
            MACDlabel.Text = LanguageString.DynamicElements.MACDlabel_Text;
            MACDlabel.AutoSize = true;
            MACDlabel.Location = new Point(230, 45);
            TechnicalAnalisisGroupBox.Controls.Add(MACDlabel);

            Label MACDValuelabel = new Label(); //Значение MACD:
            MACDValuelabel.Name = "MACDvalue";
            MACDValuelabel.Text = "0.0";
            MACDValuelabel.Width = 80;
            MACDValuelabel.Height = 20;
            MACDValuelabel.Location = new Point(280, 45);
            TechnicalAnalisisGroupBox.Controls.Add(MACDValuelabel);

            Label MACDSignallabel = new Label(); //Значение Signal:
            MACDSignallabel.Name = "MACDSignalValue" + _marketNameWithoutHyphen;
            MACDSignallabel.Text = "0.0";
            MACDSignallabel.Width = 80;
            MACDSignallabel.Height = 20;
            MACDSignallabel.Location = new Point(280, 67);
            TechnicalAnalisisGroupBox.Controls.Add(MACDSignallabel);

            Label MACDHistogramlabel = new Label(); //Значение MACDHistogram
            MACDHistogramlabel.Name = "MACDHistogramValue" + _marketNameWithoutHyphen;
            MACDHistogramlabel.Text = "0.0";
            MACDHistogramlabel.Width = 80;
            MACDHistogramlabel.Height = 20;
            MACDHistogramlabel.Location = new Point(280, 89);
            TechnicalAnalisisGroupBox.Controls.Add(MACDHistogramlabel);

            #endregion AddElements

            _elementsOnPanel[0] = PanelElements;
            _elementsOnPanel[1] = MarketNamelabel;
            _elementsOnPanel[2] = TechnicalAnalisisGroupBox;
            _elementsOnPanel[3] = RSIlabel;
            _elementsOnPanel[4] = RSIValuelabel;
            _elementsOnPanel[5] = DeletePairButton;
            _elementsOnPanel[6] = HighValuelabel;
            _elementsOnPanel[7] = LowValuelabel;
            _elementsOnPanel[8] = VolumeValuelabel;
            _elementsOnPanel[9] = LastValuelabel;
            _elementsOnPanel[10] = BaseValuelabel;
            _elementsOnPanel[11] = StochRSIlabel;
            _elementsOnPanel[12] = StochRSIFastValuelabel;
            _elementsOnPanel[13] = StochRSISlowValuelabel;
            _elementsOnPanel[14] = Stochlabel;
            _elementsOnPanel[15] = StochFastValuelabel;
            _elementsOnPanel[16] = StochSlowValuelabel;
            _elementsOnPanel[17] = MACDlabel;
            _elementsOnPanel[18] = MACDValuelabel;
            _elementsOnPanel[19] = MACDSignallabel;
            _elementsOnPanel[20] = MACDHistogramlabel;
        }

        private void Delete_Button(object sender, EventArgs e)
        {
            DeletePair();
        }

        public void DeletePair()
        {
            //удаляем "родительскую" панель
            _mainPanelWithElements.Controls.Remove(_elementsOnPanel[0]);
            //освобождаем ресурсы
            _elementsOnPanel[0].Dispose();
            //очищаем массив
            _elementsOnPanel = null;
            //получаем индекс текущего элемента
            int Index = _activeMarketList.IndexOf(this);
            //удаляем из списка
            _activeMarketList.Remove(this);
            //обновляем положение оставшихся панелей
            RepaintMainPanel(Index);
            ////освобождаем ресурсы класса
            //this.Dispose(true);
        }

        //перемещаем панели на позицию вверх
        private void RepaintMainPanel(int Start_index)
        {
            for (int i = Start_index; i < _activeMarketList.Count; i++)
            {
                int Ypos = _activeMarketList[i]._elementsOnPanel[0].Location.Y;
                _activeMarketList[i]._elementsOnPanel[0].Location = new Point(12, Ypos - (_panelElementsHeight + _panelDistance));
            }
        }

        public void PanelMoveTo(Point newLocation)
        {
            this._elementsOnPanel[0].Location = newLocation;
        }

        public async void UpdateInfoAsync(string chartTimestamp)
        {
            Label LabelMarket = _elementsOnPanel[1] as Label;
            Label LabelRSIValue = _elementsOnPanel[4] as Label;
            Label LabelHighValue = _elementsOnPanel[6] as Label;
            Label LabelLowValue = _elementsOnPanel[7] as Label;
            Label LabelVolumeValue = _elementsOnPanel[8] as Label;
            Label LabelLastValue = _elementsOnPanel[9] as Label;
            Label LabelBaseVolume = _elementsOnPanel[10] as Label;
            Label LabelStochRSIFast = _elementsOnPanel[12] as Label;
            Label LabelStochRSISlow = _elementsOnPanel[13] as Label;
            Label LabelStochasticFast = _elementsOnPanel[15] as Label;
            Label LabelStochasticSlow = _elementsOnPanel[16] as Label;
            Label MACDValuelabel = _elementsOnPanel[18] as Label;
            Label MACDSignalValuelabel = _elementsOnPanel[19] as Label;
            Label MACDHistogramValuelabel = _elementsOnPanel[20] as Label;

            GetSourceHTMLClient _queryAPI = new GetSourceHTMLClient(1);
            _queryAPI.OnConsoleSend += _consoleForm_Ref.AddString;

            Conclusion_TechAnalisis conclusion_TechAnalisis = new Conclusion_TechAnalisis();
            Color colorElement = Color.Gray;

            //получаем общую инормацию для пары
            string _summariesResponse = await _queryAPI.GetMarketSummariesAsync();
            //await Task.Delay(1000);
            //получаем данные графика
            string _responseChart = await _queryAPI.GetMarketTicksAsync(_marketName, chartTimestamp);

            if (_summariesResponse == null || _responseChart == null)
            {
                onUpdateEnd();
                return;
            }

            ParsingResponse parsingResponse = new ParsingResponse();
            //получаем массив значений свечного графика
            List<string[]> marketChartArr = parsingResponse.CandleChart(_responseChart);

            TechAnalisis techAnalisis = new TechAnalisis();
            double[] arrayCloseValue = techAnalisis.GetArrayCloseValue(marketChartArr);
            double[] arrayHighValue = techAnalisis.GetArrayHighValue(marketChartArr);
            double[] arrayLowValue = techAnalisis.GetArrayLowValue(marketChartArr);

            string[] _marketStats = new string[5];
            _marketStats = parsingResponse.GetMarketStats(_summariesResponse, LabelMarket.Text);//получение значени High Low Last Volume Base volume

            LabelHighValue.Text = LanguageString.DynamicElements.HighValuelabel_Text + _marketStats[0];
            LabelLowValue.Text = LanguageString.DynamicElements.LowValuelabel_Text + _marketStats[1];
            LabelVolumeValue.Text = LanguageString.DynamicElements.VolumeValuelabel_Text + _marketStats[2];
            LabelLastValue.Text = LanguageString.DynamicElements.LastValuelabel_Text + _marketStats[3];
            double FormatBaseVolume = Convert.ToDouble(_marketStats[4], SettingsVariable.ConvertCulture);
            LabelBaseVolume.Text = LanguageString.DynamicElements.BaseValuelabel_Text + FormatBaseVolume.ToString("0.##");

            if (arrayCloseValue.Length != 0)
            {
                //проверка длины полученного маcсива и значения параметров индикатора
                int _rsiPeriod = CheackIndicatorParam(SettingsVariable.rsiPeriod, arrayCloseValue.Length);
                int _stochasticsSmooth = CheackIndicatorParam(SettingsVariable.stochasticsSmooth, arrayCloseValue.Length);
                int _stochasticsPeriod = CheackIndicatorParam(SettingsVariable.stochasticsPeriod, arrayCloseValue.Length);
                int _stochasticsRSISmooth = CheackIndicatorParam(SettingsVariable.stochasticsRSISmooth, arrayCloseValue.Length);
                int _stochasticsRSIPeriod = CheackIndicatorParam(SettingsVariable.stochasticsRSIPeriod, arrayCloseValue.Length);
                int _fastMAPeriod = CheackIndicatorParam(SettingsVariable.fastMAPeriod, arrayCloseValue.Length);
                int _slowMAPeriod = CheackIndicatorParam(SettingsVariable.slowMAPeriod, arrayCloseValue.Length);
                int _signalMAPeriod = CheackIndicatorParam(SettingsVariable.signalMAPeriod, arrayCloseValue.Length);

                //RSI
                double[] RSIValueArray = techAnalisis.RSIArrayWithSMMA(_rsiPeriod, arrayCloseValue);
                //заполнение поля последним значением RSI
                colorElement = conclusion_TechAnalisis.RSIvalue_Conclusion(RSIValueArray[RSIValueArray.Length - 1]);
                LabelRSIValue.BackColor = colorElement;
                LabelRSIValue.Text = RSIValueArray[RSIValueArray.Length - 1].ToString("0.##");

                //Stochastic
                double[] FullStochasticKline = techAnalisis.FullStochasticOscillator(arrayCloseValue, arrayHighValue, arrayLowValue, _stochasticsSmooth, _stochasticsPeriod);
                LabelStochasticFast.Text = FullStochasticKline[RSIValueArray.Length - 1].ToString("0.##");
                double[] FullStochasticDline = techAnalisis.GetSMAArray(FullStochasticKline, _stochasticsSmooth);
                LabelStochasticSlow.Text = FullStochasticDline[RSIValueArray.Length - 1].ToString("0.##");
                colorElement = conclusion_TechAnalisis.StochasticValue_Conclusion(FullStochasticKline[RSIValueArray.Length - 1], FullStochasticDline[RSIValueArray.Length - 1]);
                LabelStochasticFast.BackColor = colorElement;
                LabelStochasticSlow.BackColor = colorElement;

                //StochasticRSI K Line
                double[] StochasticKlineOfRSI = techAnalisis.FullStochasticKlineOfRSI(RSIValueArray, _stochasticsRSISmooth, _rsiPeriod, _stochasticsRSIPeriod);
                //заполнение поля последним значением StochRSIFast
                LabelStochRSIFast.Text = StochasticKlineOfRSI[StochasticKlineOfRSI.Length - 1].ToString("0.##");
                //StochasticRSI D Line
                double[] StochasticDlineOfRSI = techAnalisis.GetSMAArray(StochasticKlineOfRSI, _stochasticsRSISmooth);
                //заполнение поля последним значением StochRSISlow
                LabelStochRSISlow.Text = StochasticDlineOfRSI[StochasticDlineOfRSI.Length - 1].ToString("0.##");
                colorElement = conclusion_TechAnalisis.StochasticRSIvalue_Conclusion(StochasticKlineOfRSI[StochasticKlineOfRSI.Length - 1], StochasticDlineOfRSI[StochasticDlineOfRSI.Length - 1]);
                LabelStochRSIFast.BackColor = colorElement;
                LabelStochRSISlow.BackColor = colorElement;

                //MACD
                double[] MACD = techAnalisis.GetMACDArray(arrayCloseValue, _fastMAPeriod, _slowMAPeriod);
                //заполнение поля последним значением SlowMAPeriod
                MACDValuelabel.Text = MACD[MACD.Length - 1].ToString("0.########");
                double[] SignalLineMACD = techAnalisis.GetSignalLineArray(MACD, _signalMAPeriod);
                //заполнение поля последним значением SignalMAPeriod
                MACDSignalValuelabel.Text = SignalLineMACD[SignalLineMACD.Length - 1].ToString("0.########");
                double[] MACDHistogramArray = techAnalisis.GetMACDHistogramArray(MACD, SignalLineMACD, _slowMAPeriod, _signalMAPeriod);
                //заполнение поля последним значением MACDHistogram
                MACDHistogramValuelabel.Text = MACDHistogramArray[MACDHistogramArray.Length - 1].ToString("0.########");

                colorElement = conclusion_TechAnalisis.MACDvalue_Conclusion(MACD, SignalLineMACD, MACDHistogramArray);
                MACDValuelabel.BackColor = colorElement;
                MACDSignalValuelabel.BackColor = colorElement;
                MACDHistogramValuelabel.BackColor = colorElement;
            }
            //событие окончания обновления пары
            onUpdateEnd();
        }

        private void LabelMarketName_LinkClicked(object sender, EventArgs e)
        {
            Label LabelMarketName = sender as Label;
            string MarketName = LabelMarketName.Text;
            System.Diagnostics.Process.Start("https://bittrex.com/Market/Index?MarketName=" + MarketName);
        }

        //проверка длины полученного масcива и значения параметра индикатора
        private int CheackIndicatorParam(int ValueParamIndicator, int lengthArr)
        {
            if (ValueParamIndicator >= lengthArr)
            {
                ValueParamIndicator = lengthArr - 1;
            }
            return ValueParamIndicator;
        }
    }
}