using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using Calctic.Model.CurrencyConverter;
using Calctic.Interfaces;

namespace Calctic.Resources.JsonFiles.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private HttpClient _client;

        public HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                }

                return _client;
            }

            set
            {
                if (_client != value)
                {
                    _client = value;
                }
            }
        }

        // Get the namespace for the embedded JSON data files
        private readonly string ResourcePath = typeof(CurrencyService).Namespace;

        // Get the assembly for the embedded JSON data files
        private Assembly ThisAssembly = typeof(CurrencyService).GetTypeInfo().Assembly;

        #region Exchange Rates

        /// <summary>
        /// Gets exchange rates from "https://v6.exchangerate-api.com/v6/-your-api-key-/latest/USD"
        /// </summary>
        /// <returns></returns>
        public async Task<ExchangeRate> GetExchangeRateFromClient()
        {
            return await GetExchangeRateFromClient(Client);
        }

        /// <summary>
        /// Gets exchange rates from a specific url
        /// </summary>
        /// <param name="url"></param>
        /// <returns name="Task<ExchangeRate>"></returns>
        public async Task<ExchangeRate> GetExchangeRateFromClient(string url)
        {
            return await GetExchangeRateFromClient(Client, url);
        }

        /// <summary>
        /// Gets exchanges rates in USD passing in an instance of an HttpClient
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task<ExchangeRate> GetExchangeRateFromClient(HttpClient client)
        {
            return await GetExchangeRateFromClient(client, "https://v6.exchangerate-api.com/v6/7c38199244c39e057a268f0a/latest/USD");
        }

        /// <summary>
        /// Gets exchange rates from a specific url passing in an instance of an HttpClient
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<ExchangeRate> GetExchangeRateFromClient(HttpClient client, string url)
        {
            var clientRequest = client.GetStringAsync(url);

            var jsonData = await clientRequest;

            var exchangeRates = ExchangeRate.FromJson(jsonData);

            return exchangeRates;
        }

        /// <summary>
        /// Gets exchange rates from a Json File in the Services Namespace
        /// </summary>
        /// <returns></returns>
        public async Task<ExchangeRate> GetExchangeRateFromJson()
        {
            string jsonData = "";

            using (var reader = new StreamReader(ThisAssembly.GetManifestResourceStream($"{ResourcePath}.currency.json")))
            {
                jsonData = await reader.ReadToEndAsync();
            }

            var exchangeRates = ExchangeRate.FromJson(jsonData);

            return exchangeRates;
        }

        /// <summary>
        /// Sets the exchange rates of currency.json file
        /// </summary>
        /// <returns></returns>
        public async Task<ExchangeRate> SetExchangeRateToJson()
        {
            //TODO: Change from Json To Client
            var exchangeRateData = GetExchangeRateFromJson();
            var jsonData = JsonConvert.SerializeObject(exchangeRateData.Result);

            using (var writer = new StreamWriter(ThisAssembly.GetManifestResourceStream($"{ResourcePath}.currency.json")))
            {
                await writer.WriteAsync(jsonData);
            }

            var exchangeRates = ExchangeRate.FromJson(jsonData);

            return exchangeRates;
        }

        #endregion

        #region Exchange Currencies

        /// <summary>
        /// Returns a list of Currency Names with their corresponding currency code
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<CurrencyName> GetExchangeCurrencies()
        {
            var json = "";

            var stream = ThisAssembly.GetManifestResourceStream($"{ResourcePath}.currency-names.json");

            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<ObservableCollection<CurrencyName>>(json);
        }

        /// <summary>
        /// Returns a list of Currency Names
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> GetExchangeNames()
        {
            var json = "";
            var output = new ObservableCollection<string>();

            var stream = ThisAssembly.GetManifestResourceStream($"{ResourcePath}.currency-names.json");

            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            var jsonData = JsonConvert.DeserializeObject<ObservableCollection<CurrencyName>>(json);

            foreach(var data in jsonData)
            {
                output.Add(data.Name);
            }

            return output;
        }

        /// <summary>
        /// Returns a list of Currency Names
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> GetExchangeCodes()
        {
            var json = "";
            var output = new ObservableCollection<string>();

            var stream = ThisAssembly.GetManifestResourceStream($"{ResourcePath}.currency-names.json");

            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            var jsonData = JsonConvert.DeserializeObject<ObservableCollection<CurrencyName>>(json);

            foreach (var data in jsonData)
            {
                output.Add(data.Code);
            }

            return output;
        }

        #endregion
    }
}