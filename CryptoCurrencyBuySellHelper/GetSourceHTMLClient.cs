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
        public delegate void consoleAdd(string Text);

        public event consoleAdd OnConsoleSend;

        private readonly HttpClient client = new HttpClient();

        public GetSourceHTMLClient(int CountConnections)
        {
            ServicePointManager.DefaultConnectionLimit = CountConnections;
        }

        private async Task<string> GetSourceByPageIdAsync(string urlPage) //возвращает весь код страницы
        {
            string source = null;
            try
            {
                var response = await client.GetAsync(urlPage);
                OnConsoleSend(urlPage + " " + response.StatusCode);

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

        public async Task<string> GetMarketSummariesAsync()
        {
            string MarketSummariesResponse = await GetSourceByPageIdAsync("https://api.bittrex.com/api/v1.1/public/getmarketsummaries");
            return MarketSummariesResponse;
        }

        public async Task<string> GetMarketTicksAsync(string marketName, string interval) //получаем данные графика
        {
            string ResponseChart = await GetSourceByPageIdAsync("https://bittrex.com/Api/v2.0/pub/market/GetTicks?marketName=" + marketName + "&tickInterval=" + interval);
            return ResponseChart;
        }
    }
}