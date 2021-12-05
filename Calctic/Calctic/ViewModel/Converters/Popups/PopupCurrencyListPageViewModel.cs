using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

using Calctic.Interfaces;
using Calctic.Model.CurrencyConverter;
using Rg.Plugins.Popup.Services;

namespace Calctic.ViewModel.Converters.Popups
{
    public class PopupCurrencyListPageViewModel : ViewModelBase
    {
        private ICurrencyService _currencyService;
        private CurrencyName _selectedCurrency;

        public ObservableCollection<CurrencyName> InputCurrencyCodes { get; set; } = new ObservableCollection<CurrencyName>();

        public CurrencyName SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                SetProperty(ref _selectedCurrency, value);
            }
        }

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CountrySelectedCommand { get; set; }

        public PopupCurrencyListPageViewModel(ICurrencyService currencyService)
        {
            _currencyService = currencyService;

            Title = "Select Currency";

            InputCurrencyCodes = _currencyService.GetExchangeCurrencies();

            ConfirmCommand = new Command(ExecuteConfirm);
            CancelCommand = new Command(ExecuteCancel);
        }

        private void ExecuteConfirm()
        {
            CountrySelectedCommand?.Execute(SelectedCurrency);
            PopupNavigation.Instance.PopAsync();
        }

        private void ExecuteCancel()
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
