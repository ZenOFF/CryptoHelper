using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

/*https://bittrex.com/api/{version}/{method}?param=value
 *https://bittrex.com/api/v1.1/public/getmarkets
 *https://bittrex.com/api/v1.1/public/getcurrencies
 *https://bittrex.com/api/v1.1/public/getticker?market=marketname
 *https://bittrex.com/api/v1.1/public/getmarketsummaries
 * *
 *
 *
 *
 *https://bittrex.com/api/v1.1/public/getmarketsummary?market=btc-ltc
 *https://bittrex.com/api/v1.1/public/getorderbook?market=BTC-LTC&type=both
 *https://bittrex.com/api/v1.1/public/getmarkethistory?market=BTC-DOGE
 * https://bittrex.com/Api/v2.0/pub/market/GetTicks?marketName=BTC-LTC&tickInterval= [“oneMin”, “fiveMin”, “thirtyMin”, “hour”, “day”]

*/

namespace NoviceCryptoTraderAdvisor
{
    internal class GetSourceHTMLClient
    {
        private readonly HttpClient client = new HttpClient();

        private async Task<string> GetSourceByPageId(string urlPage) //возвращает весь код страницы
        {
            string source = null;
            
            //for .net framework 4.5
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // ServicePointManager.DefaultConnectionLimit = 1;
            try
            {
                var response = await client.GetAsync(urlPage);

                Console.WriteLine(response.StatusCode);

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    source = await response.Content.ReadAsStringAsync();
                    //response.Dispose();

                    return source;
                }

                return source = null;
            }
            catch (TaskCanceledException)
            {
                return source = null;
            }
            catch (HttpRequestException)
            {
                return source = null;
            }
        }

        public async Task<string> GetMarketSummaries()
        {
            string MarketSummariesResponse = await GetSourceByPageId("https://api.bittrex.com/api/v1.1/public/getmarketsummaries");

            return MarketSummariesResponse;
        }

        public async Task<string> GetMarketTicks(string marketName, string interval) //получаем данные графика
        {
            string ResponseChart = await GetSourceByPageId("https://international.bittrex.com/Api/v2.0/pub/market/GetTicks?marketName=" + marketName + "&tickInterval=" + interval);
            return ResponseChart;
        }
    }
}