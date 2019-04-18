using System;
using System.Collections.Generic;

namespace NoviceCryptoTraderAdvisor
{
    internal class TechAnalisis
    {
        public double[] StochRSIFast(int PeriodRSI, int PeriodStochastic, double[] RSIArray)
        {
            double[] StochRSIArray = new double[RSIArray.Length];
            double MaxRSIperiod = 0;
            double MinRSIperiod = 100;
            // StochRSI = (RSI - Lowest Low RSI) / (Highest High RSI - Lowest Low RSI)

            for (int CurrentIndex = PeriodRSI + PeriodStochastic; CurrentIndex < RSIArray.Length; CurrentIndex++)
            {
                MaxRSIperiod = 0;
                MinRSIperiod = 100;
                for (int i = CurrentIndex - PeriodStochastic; i <= CurrentIndex; i++)
                {
                    if (MaxRSIperiod < RSIArray[i])
                    {
                        MaxRSIperiod = RSIArray[i];
                    }

                    if (MinRSIperiod > RSIArray[i])
                    {
                        MinRSIperiod = RSIArray[i];
                    }
                }
                double test = RSIArray[CurrentIndex - 1] - MinRSIperiod;
                StochRSIArray[CurrentIndex] = ((RSIArray[CurrentIndex - 1] - MinRSIperiod) / (MaxRSIperiod - MinRSIperiod)) * 100;
            }
            return StochRSIArray;
        }

        public double[] FullStochasticKlineOfRSI(double[] RSIArray, int SmoothKline, int PeriodRSI, int PeriodStochastic)
        {
            double[] StochasticFastRSIArray = new double[RSIArray.Length];
            double[] FullStochasticArrayKLine = new double[RSIArray.Length];

            StochasticFastRSIArray = StochRSIFast(PeriodRSI, PeriodStochastic, RSIArray);
            FullStochasticArrayKLine = GetSMAArray(StochasticFastRSIArray, SmoothKline);
            return FullStochasticArrayKLine;
        }

        public double[] RSIArrayWithSMMA(int PeriodRSI, double[] ArrayCloseValue)//значение RSI для точки
        {
            double PreviousClosePrice = 0;
            double CurrentClosePrice = 0;
            double RSIValueSMMA = 0;

            double DifferencePrice = 0;
            double RSValue = 0;
            double[] RSIValueArray = new double[ArrayCloseValue.Length];
            double[] ArrayPricesGrows = new double[ArrayCloseValue.Length];
            double[] ArrayPricesDrop = new double[ArrayCloseValue.Length];

            double[] ArrayAvgPricesGrows = new double[ArrayCloseValue.Length];
            double[] ArrayAvgPricesDrop = new double[ArrayCloseValue.Length];

            //получаем массивы роста и падений цен
            for (int i = 1; i < ArrayCloseValue.Length; i++)
            {
                CurrentClosePrice = ArrayCloseValue[i];//цена закрытия текущего поинта
                PreviousClosePrice = ArrayCloseValue[i - 1];//цена закрытия предыдущего поинта

                if (CurrentClosePrice > PreviousClosePrice)
                {
                    DifferencePrice = CurrentClosePrice - PreviousClosePrice;
                    ArrayPricesGrows[i] = DifferencePrice;
                    ArrayPricesDrop[i] = 0;
                }

                if (CurrentClosePrice < PreviousClosePrice)
                {
                    DifferencePrice = PreviousClosePrice - CurrentClosePrice;
                    ArrayPricesGrows[i] = 0;
                    ArrayPricesDrop[i] = DifferencePrice;
                }
            }

            //------------------------------

            ArrayAvgPricesGrows[PeriodRSI] = GetSMAValue(ArrayPricesGrows, PeriodRSI); //простое скользящие для закрытия цен в плюс SMA
            ArrayAvgPricesDrop[PeriodRSI] = GetSMAValue(ArrayPricesDrop, PeriodRSI); //простое скользящие для закрытия цен в минус SMA

            RSValue = ArrayAvgPricesGrows[PeriodRSI] / ArrayAvgPricesDrop[PeriodRSI];
            RSIValueArray[PeriodRSI] = 100 - (100 / (1 + RSValue));

            //находим последующие значения RSI  с SMMA
            for (int CurrentPoint = PeriodRSI + 1; CurrentPoint < ArrayCloseValue.Length; CurrentPoint++)//все значения начиная с период +1 и до конца массива
            {
                // = ((F16 * 13) + D17) / 14
                ArrayAvgPricesGrows[CurrentPoint] = ((ArrayAvgPricesGrows[CurrentPoint - 1] * (PeriodRSI - 1)) + ArrayPricesGrows[CurrentPoint]) / PeriodRSI;
                ArrayAvgPricesDrop[CurrentPoint] = ((ArrayAvgPricesDrop[CurrentPoint - 1] * (PeriodRSI - 1)) + ArrayPricesDrop[CurrentPoint]) / PeriodRSI;

                RSValue = ArrayAvgPricesGrows[CurrentPoint] / ArrayAvgPricesDrop[CurrentPoint];
                RSIValueSMMA = 100 - (100 / (1 + RSValue));

                RSIValueArray[CurrentPoint] = RSIValueSMMA;
            }
            return RSIValueArray;
        }

        public double[] ArrayToEMA(float Smooth, double[] ArrayForSmoothing)// EMAinT=alpha*PinT+(1-alpha)* EMAinT-1
        {
            double[] SmoothArray = new double[ArrayForSmoothing.Length];

            float alpha = (2 / (Smooth + 1));

            SmoothArray[0] = ArrayForSmoothing[0];
            for (int i = 1; i < ArrayForSmoothing.Length; i++)
            {
                if (ArrayForSmoothing[i] != 0)
                {
                    SmoothArray[i] = alpha * ArrayForSmoothing[i] + (1 - alpha) * SmoothArray[i - 1];
                }
            }
            return SmoothArray;
        }

        public double[] StochasticFastKline(int PeriodStochastic, double[] ArrayCloseValue, double[] ArrayHighValue, double[] ArrayLowValue)
        {
            double[] StochasticArrayKline = new double[ArrayCloseValue.Length];
            double MaxRSIperiod = 0;
            double MinRSIperiod = 100000;

            for (int CurrentIndex = PeriodStochastic - 1; CurrentIndex < ArrayCloseValue.Length; CurrentIndex++)
            {
                MaxRSIperiod = 0;
                MinRSIperiod = 100000;
                for (int i = CurrentIndex - (PeriodStochastic - 1); i <= CurrentIndex; i++)
                {
                    if (MaxRSIperiod < ArrayHighValue[i])
                    {
                        MaxRSIperiod = ArrayHighValue[i];
                    }

                    if (MinRSIperiod > ArrayLowValue[i])
                    {
                        MinRSIperiod = ArrayLowValue[i];
                    }
                }
                StochasticArrayKline[CurrentIndex] = ((ArrayCloseValue[CurrentIndex] - MinRSIperiod) / (MaxRSIperiod - MinRSIperiod)) * 100;
            }
            return StochasticArrayKline;
        }

        public double[] FullStochasticOscillator(double[] ArrayCloseValue, double[] ArrayHighValue, double[] ArrayLowValue, int SmoothKline, int PeriodStochastic)
        {
            double[] StochasticFastKlineArray = new double[ArrayCloseValue.Length];
            double[] FullStochasticArrayKLine = new double[ArrayCloseValue.Length];
            StochasticFastKlineArray = StochasticFastKline(PeriodStochastic, ArrayCloseValue, ArrayHighValue, ArrayLowValue);
            FullStochasticArrayKLine = GetSMAArray(StochasticFastKlineArray, SmoothKline);
            return FullStochasticArrayKLine;
        }

        public double[] GetArrayCloseValue(List<string[]> MarketArray)
        {
            double[] ArrayCloseValue = new double[MarketArray.Count];

            for (int i = 0; i < MarketArray.Count; i++)
            {
                ArrayCloseValue[i] = Convert.ToDouble(MarketArray[i].GetValue(3), SettingsVariable.ConvertCulture);
            }

            return ArrayCloseValue;
        }

        public double[] GetArrayHighValue(List<string[]> MarketArray)
        {
            double[] ArrayHighValue = new double[MarketArray.Count];

            for (int i = 0; i < MarketArray.Count; i++)
            {
                ArrayHighValue[i] = Convert.ToDouble(MarketArray[i].GetValue(0), SettingsVariable.ConvertCulture);
            }

            return ArrayHighValue;
        }

        public double[] GetArrayLowValue(List<string[]> MarketArray)
        {
            double[] ArrayLowValue = new double[MarketArray.Count];

            for (int i = 0; i < MarketArray.Count; i++)
            {
                ArrayLowValue[i] = Convert.ToDouble(MarketArray[i].GetValue(1), SettingsVariable.ConvertCulture);
            }

            return ArrayLowValue;
        }

        public double GetSMAValue(double[] ArrayCloseValue, int Period)
        {
            double SMAValue = 0;
            double SumArray = 0;
            for (int i = 0; i < Period; i++)
            {
                SumArray += ArrayCloseValue[i];
            }
            SMAValue = SumArray / Period;
            return SMAValue;
        }

        public double[] GetSMAArray(double[] Array, int Period)
        {
            double[] EMAValueArray = new double[Array.Length];
            double SumArrayPeriod = 0;
            for (int i = Period - 1; i < Array.Length; i++)
            {
                SumArrayPeriod = 0;
                for (int n = i - (Period - 1); n <= i; n++)
                {
                    SumArrayPeriod += Array[n];
                }
                EMAValueArray[i] = SumArrayPeriod / Period;
            }
            return EMAValueArray;
        }

        public double[] GetEMAArray(double[] ArrayCloseValue, int Period)
        {
            float Multiplier = 0;
            double[] EMAValueArray = new double[ArrayCloseValue.Length];
            EMAValueArray[Period - 1] = GetSMAValue(ArrayCloseValue, Period);
            Multiplier = 2 / ((float)Period + 1);
            //Multiplier: (2 / (Time periods + 1) ) = (2 / (10 + 1) ) = 0.1818 (18.18%)
            //EMA: {Close - EMA(previous day)} x multiplier + EMA(previous day)
            for (int i = Period; i < EMAValueArray.Length; i++)
            {
                EMAValueArray[i] = ((ArrayCloseValue[i] - EMAValueArray[i - 1]) * Multiplier) + EMAValueArray[i - 1];
            }

            return EMAValueArray;
        }

        public double[] GetMACDArray(double[] ArrayCloseValue, int FastPeriod, int SlowPeriod)
        {
            //MACD Line: (12-day EMA - 26-day EMA)

            //Signal Line: 9 - day EMA of MACD Line

            //MACD Histogram: MACD Line -Signal Line
            double[] MACDArray = new double[ArrayCloseValue.Length];
            double[] EMAFastPeriod = new double[ArrayCloseValue.Length];
            double[] EMASlowPeriod = new double[ArrayCloseValue.Length];

            EMAFastPeriod = GetEMAArray(ArrayCloseValue, FastPeriod);
            EMASlowPeriod = GetEMAArray(ArrayCloseValue, SlowPeriod);

            for (int i = SlowPeriod - 1; i < ArrayCloseValue.Length; i++)
            {
                MACDArray[i] = EMAFastPeriod[i] - EMASlowPeriod[i];
            }

            return MACDArray;
        }

        public double[] GetSignalLineArray(double[] MACDArray, int SignalPeriod)
        {
            double[] SignalLineArray = new double[MACDArray.Length];
            SignalLineArray = GetEMAArray(MACDArray, SignalPeriod);
            return SignalLineArray;
        }

        public double[] GetMACDHistogramArray(double[] MACDArray, double[] SignalLineArray, int SlowPeriod, int SignalPeriod)
        {
            double[] MACDHistogramArray = new double[MACDArray.Length];
            for (int i = SlowPeriod + SignalPeriod; i < MACDHistogramArray.Length; i++)
            {
                MACDHistogramArray[i] = MACDArray[i] - SignalLineArray[i];
            }

            return MACDHistogramArray;
        }
    }
}