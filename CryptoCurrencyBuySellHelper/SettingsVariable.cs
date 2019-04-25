using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NoviceCryptoTraderAdvisor
{
    public static class SettingsVariable
    {
        public static System.Globalization.CultureInfo ConvertCulture = new System.Globalization.CultureInfo("en");

        public static string lagnuageApplication = "en";

        public static int rsiPeriod = 14;
        public static int stochasticsPeriod = 14;
        public static int stochasticsSmooth = 3;
        public static int stochasticsRSIPeriod = 9;
        public static int stochasticsRSISmooth = 3;
        public static int fastMAPeriod = 12;
        public static int slowMAPeriod = 26;
        public static int signalMAPeriod = 9;

        public static int rsiSellValue = 80;
        public static int rsiBuylValue = 20;

        public static int stochasticSellValue = 80;
        public static int stochasticBuyValue = 20;

        public static int stochasticRSISellValue = 90;
        public static int stochasticRSIBuyValue = 10;

        public static void SetDefault(bool lagnuageReset = true)
        {
            if (lagnuageReset)
            {
                lagnuageApplication = "en";
            }

            rsiPeriod = 14;
            stochasticsPeriod = 14;
            stochasticsSmooth = 3;
            stochasticsRSIPeriod = 9;
            stochasticsRSISmooth = 3;
            fastMAPeriod = 12;
            slowMAPeriod = 26;
            signalMAPeriod = 9;
            rsiSellValue = 80;
            rsiBuylValue = 20;
            stochasticSellValue = 80;
            stochasticBuyValue = 20;
            stochasticRSISellValue = 90;
            stochasticRSIBuyValue = 10;
        }

        public static void SaveSettings(string xmlFilePath)
        {
            try
            {
                XDocument xdoc = new XDocument();

                XElement elmXML_lagnuageApplication = new XElement("LagnuageApplication");
                elmXML_lagnuageApplication.Value = lagnuageApplication;

                XElement elmXML_RSI = new XElement("RSI");
                elmXML_RSI.Value = rsiPeriod.ToString();

                XElement elmXML_StochasticsPeriod = new XElement("StochasticsPeriod");
                elmXML_StochasticsPeriod.Value = stochasticsPeriod.ToString();

                XElement elmXML_StochasticsSmooth = new XElement("StochasticsSmooth");
                elmXML_StochasticsSmooth.Value = stochasticsSmooth.ToString();

                XElement elmXML_StochasticsRSIPeriod = new XElement("StochasticsRSIPeriod");
                elmXML_StochasticsRSIPeriod.Value = stochasticsRSIPeriod.ToString();

                XElement elmXML_StochasticsRSISmooth = new XElement("StochasticsRSISmooth");
                elmXML_StochasticsRSISmooth.Value = stochasticsRSISmooth.ToString();

                XElement elmXML_FastMAPeriod = new XElement("FastMAPeriod");
                elmXML_FastMAPeriod.Value = fastMAPeriod.ToString();

                XElement elmXML_SlowMAPeriod = new XElement("SlowMAPeriod");
                elmXML_SlowMAPeriod.Value = slowMAPeriod.ToString();

                XElement elmXML_SignalMAPeriod = new XElement("SignalMAPeriod");
                elmXML_SignalMAPeriod.Value = signalMAPeriod.ToString();

                //  корневой элемент
                XElement IndicatorParam = new XElement("IndicatorParam");
                // добавляем в корневой элемент

                IndicatorParam.Add(elmXML_lagnuageApplication);
                IndicatorParam.Add(elmXML_RSI);
                IndicatorParam.Add(elmXML_StochasticsPeriod);
                IndicatorParam.Add(elmXML_StochasticsSmooth);
                IndicatorParam.Add(elmXML_StochasticsRSIPeriod);
                IndicatorParam.Add(elmXML_StochasticsRSISmooth);
                IndicatorParam.Add(elmXML_FastMAPeriod);
                IndicatorParam.Add(elmXML_SlowMAPeriod);
                IndicatorParam.Add(elmXML_SignalMAPeriod);

                // добавляем корневой элемент в документ
                xdoc.Add(IndicatorParam);
                //сохраняем документ
                xdoc.Save(xmlFilePath);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Settings file is corrupted, default settings will be loaded", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetDefault();
            }
            catch (System.Xml.XmlException)
            {
                MessageBox.Show("Settings file is corrupted, default settings will be loaded", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetDefault();
            }
            catch (FormatException)
            {
                MessageBox.Show("Settings file is corrupted, default settings will be loaded", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetDefault();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Settings file is corrupted, default settings will be loaded", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetDefault();
            }
        }

        public static void LoadSettings(string xmlFilePath)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(xmlFilePath);

                lagnuageApplication = xmlDoc.Descendants("LagnuageApplication").First().Value;
                rsiPeriod = Convert.ToInt32(xmlDoc.Descendants("RSI").First().Value);
                stochasticsPeriod = Convert.ToInt32(xmlDoc.Descendants("StochasticsPeriod").First().Value);
                stochasticsSmooth = Convert.ToInt32(xmlDoc.Descendants("StochasticsSmooth").First().Value);
                stochasticsRSIPeriod = Convert.ToInt32(xmlDoc.Descendants("StochasticsRSIPeriod").First().Value);
                stochasticsRSISmooth = Convert.ToInt32(xmlDoc.Descendants("StochasticsRSISmooth").First().Value);
                fastMAPeriod = Convert.ToInt32(xmlDoc.Descendants("FastMAPeriod").First().Value);
                slowMAPeriod = Convert.ToInt32(xmlDoc.Descendants("SlowMAPeriod").First().Value);
                signalMAPeriod = Convert.ToInt32(xmlDoc.Descendants("SignalMAPeriod").First().Value);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Settings file is corrupted, default settings will be loaded", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetDefault();
            }
            catch (System.Xml.XmlException)
            {
                MessageBox.Show("Settings file is corrupted, default settings will be loaded", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetDefault();
            }
            catch (FormatException)
            {
                MessageBox.Show("Settings file is corrupted, default settings will be loaded", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetDefault();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Settings file is corrupted, default settings will be loaded", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetDefault();
            }
        }

        public static void SavePairList(List<MarketPair> ListPairs, string nameSaveFile)
        {
            XDocument xdoc = new XDocument();
            XElement xml = new XElement("Pairs", ListPairs.Select(x => new XElement("marketName", x._marketName)));
            xdoc.Add(xml);
            xdoc.Save(nameSaveFile);
        }

        public static string[] LoadPairList(string nameLoadFile)
        {
            XDocument xmlDoc = XDocument.Load(nameLoadFile);
            string[] listPairs = xmlDoc.Root.Descendants("marketName").Select(x => x.Value).ToArray();
            return listPairs;
        }
    }
}