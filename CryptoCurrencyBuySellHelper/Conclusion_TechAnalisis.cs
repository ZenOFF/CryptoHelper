using System;
using System.Drawing;

namespace NoviceCryptoTraderAdvisor
{
    internal class Conclusion_TechAnalisis
    {
        public Color RSIvalue_Conclusion(double RSIvalue)
        {
            Color RSI_Conclusion = Color.Gray;
            if (RSIvalue > SettingsVariable.rsiSellValue)
            {
                RSI_Conclusion = Color.Red; //opportunity sell
            }
            else if (RSIvalue < SettingsVariable.rsiBuylValue)
            {
                RSI_Conclusion = Color.Green;//opportunity buy
            }
            return RSI_Conclusion;
        }

        public Color StochasticValue_Conclusion(double StichasticKLineValue, double StichasticDLineValue)
        {
            Color Stochastic_Conclusion = Color.Gray;
            if (StichasticKLineValue > SettingsVariable.stochasticSellValue && StichasticDLineValue > SettingsVariable.stochasticSellValue)
            {
                Stochastic_Conclusion = Color.Red; //opportunity sell
            }
            else if (StichasticKLineValue < SettingsVariable.stochasticBuyValue && StichasticDLineValue < SettingsVariable.stochasticBuyValue)
            {
                Stochastic_Conclusion = Color.Green;//opportunity buy
            }
            return Stochastic_Conclusion;
        }

        public Color StochasticRSIvalue_Conclusion(double StichasticRSIKLineValue, double StichasticRSIDLineValue)
        {
            Color StochasticRSIvalue = Color.Gray;
            if (StichasticRSIKLineValue > SettingsVariable.stochasticRSISellValue && StichasticRSIDLineValue > SettingsVariable.stochasticRSISellValue)
            {
                StochasticRSIvalue = Color.Red; //opportunity sell
            }
            else if (StichasticRSIKLineValue < SettingsVariable.stochasticRSIBuyValue && StichasticRSIDLineValue < SettingsVariable.stochasticRSIBuyValue)
            {
                StochasticRSIvalue = Color.Green;//opportunity buy
            }
            return StochasticRSIvalue;
        }

        public Color MACDvalue_Conclusion(double[] MACD, double[] SignalMACD, double[] MACDHistogramArray)
        {
            Color MACD_Conclusion = Color.Gray;
            double LastValueMACD = MACD[MACD.Length - 1];
            double LastValueSignalMACD = SignalMACD[SignalMACD.Length - 1];
            double LastValueHistogram = MACDHistogramArray[MACDHistogramArray.Length - 1];
            double TenPercentOfHistogramValue = PercentOfMaxHistogram(MACDHistogramArray);

            if (Math.Abs(LastValueHistogram) < Math.Abs(TenPercentOfHistogramValue))// сближение
            {
                if (LastValueMACD > 0 && LastValueSignalMACD > 0) //the charts above zero and Macd above Signal
                {
                    MACD_Conclusion = Color.Red; //opportunity sell
                }
                else if (LastValueMACD < 0 && LastValueSignalMACD < 0) //the charts beneath zero and Macd beneath Signal
                {
                    MACD_Conclusion = Color.Green;//opportunity buy
                }
            }
            return MACD_Conclusion;
        }

        public double PercentOfMaxHistogram(double[] MACDHistogramArray)
        {
            double LongestPeriod = 0;
            double LastValueHistogram = MACDHistogramArray[MACDHistogramArray.Length - 1];
            foreach (double item in MACDHistogramArray)
            {
                if (item > LongestPeriod)
                {
                    LongestPeriod = item;
                }
            }
            double TenPercentOfHistogramValue = LongestPeriod / 10;
            return TenPercentOfHistogramValue;
        }
    }
}