using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Calctic.Model.CurrencyConverter;

namespace Calctic.Interfaces
{
    public interface ICurrencyService
    {
        Task<ExchangeRate> GetExchangeRateFromClient();
        Task<ExchangeRate> GetExchangeRateFromClient(string url);
        Task<ExchangeRate> GetExchangeRateFromClient(HttpClient client);
        Task<ExchangeRate> GetExchangeRateFromClient(HttpClient client, string url);

        Task<ExchangeRate> GetExchangeRateFromJson();
        Task<ExchangeRate> SetExchangeRateToJson();

        ObservableCollection<CurrencyName> GetExchangeCurrencies();
        ObservableCollection<string> GetExchangeNames();
        ObservableCollection<string> GetExchangeCodes();
    }
}
