using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NoviceCryptoTraderAdvisor
{
    internal class ParsingResponse
    {
        //разбираем запись для формирования графика
        public List<string[]> CandleChart(string ResponseString)
        {
            List<string[]> ArrayCandleChart = new List<string[]>();

            Regex rgx = new Regex(@"""O"":([0-9]+\.[0-9]+),""H"":([0-9]+\.[0-9]+),""L"":([0-9]+\.[0-9]+),""C"":([0-9]+\.[0-9]+),""V"":([0-9]+\.[0-9]+),""T"":""([0-9]+-[0-9]+-[0-9]+T[0-9]+:[0-9]+:[0-9]+)"""); //поиск значений
            foreach (Match match in rgx.Matches(ResponseString))
            {
                string[] ValueChart = new string[4];
                ValueChart[0] = match.Groups[2].Value; //high
                ValueChart[1] = match.Groups[3].Value; //low
                ValueChart[2] = match.Groups[1].Value; //open
                ValueChart[3] = match.Groups[4].Value; //close
                ArrayCandleChart.Add(ValueChart);
            }
            return ArrayCandleChart;
            // значения Chart Points [0-3]High Low  Open Close
            //{"success":true,"message":"","result":[{"O":0.00003558,"H":0.00003600,"L":0.00003508,"C":0.00003556,"V":68015.77824178,"T":"2018-04-10T21:00:00","BV":2.42091561},
            // "O":0.00003460,"H":0.00003510,"L":0.00003460,"C":0.00003460,
            //[“oneMin”, “fiveMin”, “thirtyMin”, “hour”, “day”].
        }

        //разбираем запись для получения значений High Low Volume Last BaseVolume...
        public string[] GetMarketStats(string ResponseString, string MarketName)
        {
            string[] MarketStats = new string[5];

            //"MarketName":"BTC-ABY","High":0.00000079,"Low":0.00000073,"Volume":1347736.58627473,"Last":0.00000075,"BaseVolume":1.01657367,
            //*"TimeStamp":"2018-06-09T20:40:39.99","Bid":0.00000075,"Ask":0.00000077,"OpenBuyOrders":76,"OpenSellOrders":1548,"PrevDay":0.00000078,
            //*"Created":"2014-10-31T01:43:25.743"},

            Regex rgx = new Regex($@"""MarketName"":""{Regex.Escape(MarketName)}"",""High"":([0-9]+\.[0-9]+),""Low"":([0-9]+\.[0-9]+),""Volume"":([0-9]+\.[0-9]+),""Last"":([0-9]+\.[0-9]+),""BaseVolume"":([0-9]+\.[0-9]+)"); //поиск значений

            foreach (Match match in rgx.Matches(ResponseString))
            {
                MarketStats[0] = match.Groups[1].Value.ToString(SettingsVariable.ConvertCulture);//High
                MarketStats[1] = match.Groups[2].Value.ToString(SettingsVariable.ConvertCulture); //Low
                MarketStats[2] = Math.Round(Convert.ToDouble(match.Groups[3].Value, SettingsVariable.ConvertCulture), 2).ToString();//Volume
                MarketStats[3] = match.Groups[4].Value.ToString(SettingsVariable.ConvertCulture);//Last
                MarketStats[4] = match.Groups[5].Value.ToString(SettingsVariable.ConvertCulture);//BaseVolume
            }
            return MarketStats;
        }

        public bool GetDigits(Char inputChar)
        {
            if (Char.IsDigit(inputChar) || inputChar == '.' || inputChar == ',')
            {
                return true;
            }
            return false;
        }
    }
}