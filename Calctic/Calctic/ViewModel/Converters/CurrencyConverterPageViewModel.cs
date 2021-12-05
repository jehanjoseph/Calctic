using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Calctic.Helpers;
using Calctic.Interfaces;
using Calctic.Model.CurrencyConverter;
using Calctic.View.Converters.Popups;
using Calctic.ViewModel.Converters.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Calctic.ViewModel.Converters
{
    public class CurrencyConverterPageViewModel : ViewModelBase
    {
        private IBasicCalculatorService _basicCalculatorService;
        private IMessagingService _messagingService;
        private ICurrencyService _currencyService;
        private bool _isInputCurrency1LengthUpdated = false;
        private bool _isInputCurrency2LengthUpdated = false;

        private ExchangeRate _exchangeRate;

        private CurrencyName _inputCurrency1;  
        private CurrencyName _inputCurrency2;

        private int _input1MaxLength;
        private int _input2MaxLength;

        private bool _isValue1Selected;
        private bool _isValue2Selected;

        private bool _resetInputCurrency1;
        private bool _resetInputCurrency2;

        private string _screenInputValue1;
        private string _screenInputValue2;

        private double _inputCurrencyValue1;
        private double _inputCurrencyValue2;

        private const int DefaultCharacterLength = 20;
        private const int MaxCharacterLength = 15;
        private const int MaxNumberOfDecimalPlaces = 2;

        #region ICommand

        public ICommand InputCurrency1SelectedCommand { get; }
        public ICommand InputCurrency2SelectedCommand { get; }
        public ICommand InputCommand { get; }
        public ICommand ClearAllInputCommand { get; }
        public ICommand EraseInputCommand { get; }
        public ICommand PeriodCommand { get; }
        public ICommand InputCurrencyPicker1Command { get; }
        public ICommand InputCurrencyPicker2Command { get; }
        public ICommand SelectedCurrency1Command { get; }
        public ICommand SelectedCurrency2Command { get; }

        #endregion

        #region Properties

        public ObservableCollection<CurrencyName> InputCurrencyCodes1 { get; set; } = new();
        public ObservableCollection<CurrencyName> InputCurrencyCodes2 { get; set; } = new();
        public ObservableCollection<CurrencyData> CurrencyData { get; set; } = new();

        public bool IsCurrencyValue1Selected
        {
            get => _isValue1Selected;
            set
            {
                SetProperty(ref _isValue1Selected, value);

                if (_isValue1Selected)
                {
                    IsCurrencyValue2Selected = false;
                }
            }
        }

        public bool IsCurrencyValue2Selected
        {
            get => _isValue2Selected;
            set
            {
                SetProperty(ref _isValue2Selected, value);

                if (_isValue2Selected)
                {
                    IsCurrencyValue1Selected = false;
                }
            }
        }

        public int Input1MaxLength
        {
            get => _input1MaxLength;
            set
            {
                SetProperty(ref _input1MaxLength, value);
            }
        }

        public int Input2MaxLength
        {
            get => _input2MaxLength;
            set
            {
                SetProperty(ref _input2MaxLength, value);
            }
        }

        public string ScreenInputCurrency1
        {
            get => _screenInputValue1;
            set
            {
                SetProperty(ref _screenInputValue1, value);

                if (CurrencyData.Count != 0 && IsInput1Selected())
                {
                    GenerateConvertedScreenCurrency2Value();
                }
            }
        }

        public string ScreenInputCurrency2
        {
            get => _screenInputValue2;
            set
            {
                SetProperty(ref _screenInputValue2, value);

                if (CurrencyData.Count != 0 && IsInput2Selected())
                {
                    GenerateConvertedScreenCurrency1Value();
                }
            }
        }

        public double InputCurrencyValue1
        {
            get => _inputCurrencyValue1;
            set
            {
                SetProperty(ref _inputCurrencyValue1, value);
            }
        }

        public double InputCurrencyValue2
        {
            get => _inputCurrencyValue2;
            set
            {
                SetProperty(ref _inputCurrencyValue2, value);
            }
        }

        public CurrencyName InputCurrency1
        {
            get => _inputCurrency1;
            set
            {
                var previousCurrency = _inputCurrency1;

                SetProperty(ref _inputCurrency1, value);

                if (previousCurrency != null && previousCurrency != _inputCurrency1)
                {
                    if (IsInput1Selected())
                    {
                        GenerateConvertedScreenCurrency2Value();
                    }
                    else if (IsInput2Selected())
                    {
                        GenerateConvertedScreenCurrency1Value();
                    }
                }
            }
        }

        public CurrencyName InputCurrency2
        {
            get => _inputCurrency2;
            set
            {
                var previousCurrency = _inputCurrency2;

                SetProperty(ref _inputCurrency2, value);

                if (previousCurrency != null && previousCurrency != _inputCurrency2)
                {
                    if (IsInput1Selected())
                    {
                        GenerateConvertedScreenCurrency2Value();
                    }
                    else if (IsInput2Selected())
                    {
                        GenerateConvertedScreenCurrency1Value();
                    }
                }
            }
        }

        #endregion

        public CurrencyConverterPageViewModel(IBasicCalculatorService basicCalculatorService,
                                              IMessagingService messagingService,
                                              ICurrencyService currencyService)
        {
            _basicCalculatorService = basicCalculatorService;
            _messagingService = messagingService;
            _currencyService = currencyService;

            Title = "Currency";

            InputCurrency1 = new CurrencyName
            {
                Code = "USD",
                Name = "United States Dollar"
            };

            InputCurrency2 = new CurrencyName
            {
                Code = "PHP",
                Name = "Philippine Peso"
            };

            ExecuteInputCurrency1SelectedCommand();

            ScreenInputCurrency1 = "0";
            ScreenInputCurrency2 = "0";

            Input1MaxLength = DefaultCharacterLength;
            Input2MaxLength = DefaultCharacterLength;

            InputCurrencyCodes1 = _currencyService.GetExchangeCurrencies();
            InputCurrencyCodes2 = _currencyService.GetExchangeCurrencies();

            InputCommand = new Command<Button>((input) => ExecuteInputCommand(input.Text));
            ClearAllInputCommand = new Command(ExecuteClearAllInputCommand);
            EraseInputCommand = new Command(() => ExecuteEraseInputCommand(1));
            PeriodCommand = new Command(ExecutePeriodCommand);

            InputCurrency1SelectedCommand = new Command(ExecuteInputCurrency1SelectedCommand);
            InputCurrency2SelectedCommand = new Command(ExecuteInputCurrency2SelectedCommand);

            InputCurrencyPicker1Command = new Command(ExecuteCurrencyPicker1Command);
            InputCurrencyPicker2Command = new Command(ExecuteCurrencyPicker2Command);

            SelectedCurrency1Command = new Command((currency) => ExecuteUnitSelected1Command(currency as CurrencyName));
            SelectedCurrency2Command = new Command((currency) => ExecuteUnitSelected2Command(currency as CurrencyName));

            Task.Run(async () => await InitializeExchangeDataAsync());
        }

        #region Command Functions

        private void ExecuteInputCommand(string input)
        {
            if (IsInput1Selected())
            {
                if (_resetInputCurrency1)
                {
                    ResetInputValue1();
                }

                ScreenInputCurrency1 = ScreenInputCurrency1.ConcatenateInputs(input);
            }

            if (IsInput2Selected())
            {
                if (_resetInputCurrency2)
                {
                    ResetInputValue2();
                }

                ScreenInputCurrency2 = ScreenInputCurrency2.ConcatenateInputs(input);
            }
        }

        private void ExecuteEraseInputCommand(int lengthToRemove)
        {
            if (IsInput1Selected())
            {
                ScreenInputCurrency1 = ScreenInputCurrency1.EraseInputs(lengthToRemove);

                if (!ScreenInputCurrency1.Contains('.'))
                {
                    _isInputCurrency1LengthUpdated = false;
                    Input1MaxLength = MaxCharacterLength;
                }
            }

            if (IsInput2Selected())
            {
                ScreenInputCurrency2 = ScreenInputCurrency2.EraseInputs(lengthToRemove);

                if (!ScreenInputCurrency2.Contains('.'))
                {
                    _isInputCurrency2LengthUpdated = false;
                    Input2MaxLength = MaxCharacterLength;
                }
            }
        }

        private void ExecuteInputCurrency1SelectedCommand()
        {
            IsCurrencyValue1Selected = true;
            _resetInputCurrency1 = true;
        }

        private void ExecuteInputCurrency2SelectedCommand()
        {
            IsCurrencyValue2Selected = true;
            _resetInputCurrency2 = true;
        }

        private void ExecuteClearAllInputCommand()
        {
            ClearAllInputs();
        }

        private void ExecutePeriodCommand()
        {
            AddPeriodInput();
        }

        private void ExecuteUnitSelected1Command(CurrencyName selectedCurrency)
        {
            InputCurrency1 = selectedCurrency;
        }

        private void ExecuteUnitSelected2Command(CurrencyName selectedCurrency)
        {
            InputCurrency2 = selectedCurrency;
        }

        private void ExecuteCurrencyPicker1Command()
        {
            if (!CalcticHelpers.PopupPageIsAlreadyAtTopOfStack(typeof(PopupCurrencyListPage)))
            {
                var popup = new PopupCurrencyListPage();
                var context = popup.BindingContext as PopupCurrencyListPageViewModel;

                context.CountrySelectedCommand = SelectedCurrency1Command;
                context.SelectedCurrency = InputCurrency1;

                popup.BindingContext = context;

                PopupNavigation.Instance.PushAsync(popup);
            }
        }

        private void ExecuteCurrencyPicker2Command()
        {
            if (!CalcticHelpers.PopupPageIsAlreadyAtTopOfStack(typeof(PopupCurrencyListPage)))
            {
                var popup = new PopupCurrencyListPage();
                var context = popup.BindingContext as PopupCurrencyListPageViewModel;

                context.CountrySelectedCommand = SelectedCurrency2Command;
                context.SelectedCurrency = InputCurrency2;

                popup.BindingContext = context;

                PopupNavigation.Instance.PushAsync(popup);
            }
        }

        #endregion

        #region Helper Functions

        private void ClearAllInputs()
        {
            ResetInputValue1();
            ResetInputValue2();
        }

        private void ResetInputValue1()
        {
            ScreenInputCurrency1 = "0";
            Input1MaxLength = 20;

            _resetInputCurrency1 = false;
            _isInputCurrency1LengthUpdated = false;
        }

        private void ResetInputValue2()
        {
            ScreenInputCurrency2 = "0";
            Input2MaxLength = 20;

            _resetInputCurrency2 = false;
            _isInputCurrency2LengthUpdated = false;
        }

        private void AddPeriodInput()
        {
            if (IsInput1Selected())
            {
                if (ScreenInputCurrency1.IsLastNumberGoodForDecimalPoint())
                {
                    ScreenInputCurrency1 = ScreenInputCurrency1.ConcatenateInputs(".");
                }
            }

            if (IsInput2Selected())
            {
                if (ScreenInputCurrency2.IsLastNumberGoodForDecimalPoint())
                {
                    ScreenInputCurrency2 = ScreenInputCurrency2.ConcatenateInputs(".");
                }
            }
        }

        private bool IsInput1Selected()
        {
            return IsCurrencyValue1Selected && !IsCurrencyValue2Selected;
        }

        private bool IsInput2Selected()
        {
            return IsCurrencyValue2Selected && !IsCurrencyValue1Selected;
        }

        public async Task<List<CurrencyData>> InitializeExchangeDataAsync()
        {
            _exchangeRate = await _currencyService.GetExchangeRateFromJson();

            var dataList = new List<CurrencyData>();

            foreach (var rates in _exchangeRate.Rates)
            {
                var newData = new CurrencyData()
                {
                    CurrencyCode = rates.Key,
                    CurrencyName = InputCurrencyCodes1.FirstOrDefault(data => data.Code.Equals(rates.Key)).Name,
                    CurrencyRate = rates.Value
                };

                dataList.Add(newData);
            }

            dataList = (from data in dataList orderby data.CurrencyName select data).ToList();

            PopulateCurrencyData(dataList);

            return dataList;
        }

        public void PopulateCurrencyData(List<CurrencyData> dataList)
        {
            foreach (var data in dataList)
            {
                CurrencyData.Add(data);
            }
        }

        private double GetCurrencyRate(double value, string sourceCurrency, string targetCurrency)
        {
            try
            {
                double sourceValue = GetCurrencyRate(sourceCurrency);
                double targetValue = GetCurrencyRate(targetCurrency);

                return (value * targetValue) / sourceValue;
            }
            catch
            {
                return 0.0;
            }
        }

        public double GetCurrencyRate(string currency)
        {
            foreach (var data in CurrencyData)
            {
                if (data.CurrencyCode.Equals(currency))
                {
                    return data.CurrencyRate;
                }
            }

            return 0.0;
        }

        private int GetCurrencyDataIndex(CurrencyName selectedCurrency)
        {
            CurrencyData currency = CurrencyData.FirstOrDefault(data => data.CurrencyCode.Equals(selectedCurrency.Code));

            return CurrencyData.IndexOf(currency);
        }

        private void GenerateConvertedScreenCurrency1Value()
        {
            double inputValue1;
            double inputValue2;

            var inputValue1String = "";

            try
            {
                inputValue2 = Convert.ToDouble(ScreenInputCurrency2);
                inputValue1 = GetCurrencyRate(inputValue2,
                        CurrencyData.ElementAt(GetCurrencyDataIndex(InputCurrency2)).CurrencyCode,
                        CurrencyData.ElementAt(GetCurrencyDataIndex(InputCurrency1)).CurrencyCode);

                inputValue1String = string.Format("{0:0.##}", inputValue1);

                if (ScreenInputCurrency2.Contains('.') && !_isInputCurrency2LengthUpdated)
                {
                    Input2MaxLength = ScreenInputCurrency2.Length + MaxNumberOfDecimalPlaces;
                    _isInputCurrency2LengthUpdated = true;
                }

                ScreenInputCurrency1 = inputValue1String;
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.StackTrace);
            }
        }

        private void GenerateConvertedScreenCurrency2Value()
        {
            double inputValue1;
            double inputValue2;

            var inputValue2String = "";

            try
            {
                inputValue1 = Convert.ToDouble(ScreenInputCurrency1);
                inputValue2 = GetCurrencyRate(inputValue1,
                        CurrencyData.ElementAt(GetCurrencyDataIndex(InputCurrency1)).CurrencyCode,
                        CurrencyData.ElementAt(GetCurrencyDataIndex(InputCurrency2)).CurrencyCode);

                inputValue2String = string.Format("{0:0.##}", inputValue2);

                if (ScreenInputCurrency1.Contains('.') && !_isInputCurrency1LengthUpdated)
                {
                    Input1MaxLength = ScreenInputCurrency1.Length + MaxNumberOfDecimalPlaces;
                    _isInputCurrency1LengthUpdated = true;
                }

                ScreenInputCurrency2 = inputValue2String;
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.StackTrace);
            }
        }

        #endregion
    }
}