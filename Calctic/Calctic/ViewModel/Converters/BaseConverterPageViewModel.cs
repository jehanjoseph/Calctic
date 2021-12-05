using System;
using System.Windows.Input;
using Calctic.Helpers;
using Calctic.Model.UnitConverter;
using Calctic.Resources.JsonFiles.Converters;
using Calctic.Services;
using Calctic.View.Converters.Popups;
using Calctic.ViewModel.Converters.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Calctic.ViewModel.Converters
{
    public class BaseConverterPageViewModel : ViewModelBase
    {
        private UnitName _inputUnit1;
        private UnitName _inputUnit2;
        private bool _isInputUnit1LengthUpdated = false;
        private bool _isInputUnit2LengthUpdated = false;

        private bool _isValue1Selected;
        private bool _isValue2Selected;

        private bool _resetInputValue1;
        private bool _resetInputValue2;

        private string _screenInputValue1;
        private string _screenInputValue2;

        private UnitSelection _selectedUnitConverter;

        private UnitConverterService _unitConverterService;

        private int _input1MaxLength;
        private int _input2MaxLength;

        private bool _isNumericalSignVisible;

        private const int DefaultCharacterLength = 20;
        private const int MaxCharacterLength = 15;
        private const int MaxNumberOfDecimalPlaces = 4;

        #region ICommand

        public ICommand InputValue1SelectedCommand { get; }
        public ICommand InputValue2SelectedCommand { get; }
        public ICommand InputCommand { get; }
        public ICommand ClearAllInputCommand { get; }
        public ICommand EraseInputCommand { get; }
        public ICommand PeriodCommand { get; }
        public ICommand NumericalSignCommand { get; }
        public ICommand UnitSelected1Command { get; }
        public ICommand UnitSelected2Command { get; }
        public ICommand InputUnitPicker1Command { get; }
        public ICommand InputUnitPicker2Command { get; }

        #endregion

        #region Properties

        public UnitSelection SelectedUnitConverter
        {
            get { return _selectedUnitConverter; }
            set { _selectedUnitConverter = value; }
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

        public string ScreenInputValue1
        {
            get => _screenInputValue1;
            set
            {
                SetProperty(ref _screenInputValue1, value);

                if (IsInput1Selected())
                {
                    GenerateConvertedScreenInput2Value();
                }
            }
        }

        public string ScreenInputValue2
        {
            get => _screenInputValue2;
            set
            {
                SetProperty(ref _screenInputValue2, value);

                if (IsInput2Selected())
                {
                    GenerateConvertedScreenInput1Value();
                }
            }
        }

        public bool IsValue1Selected
        {
            get => _isValue1Selected;
            set
            {
                SetProperty(ref _isValue1Selected, value);

                if (_isValue1Selected)
                {
                    IsValue2Selected = false;
                }
            }
        }

        public bool IsValue2Selected
        {
            get => _isValue2Selected;
            set
            {
                SetProperty(ref _isValue2Selected, value);

                if (_isValue2Selected)
                {
                    IsValue1Selected = false;
                }
            }
        }

        public bool IsNumericalSignVisible
        {
            get => _isNumericalSignVisible;
            set
            {
                SetProperty(ref _isNumericalSignVisible, value);
            }
        }

        public UnitName InputUnit1
        {
            get => _inputUnit1;
            set
            {
                UnitName previousInputUnit = _inputUnit1;

                SetProperty(ref _inputUnit1, value);

                if (previousInputUnit != null && previousInputUnit != _inputUnit1)
                {
                    if (IsInput1Selected())
                    {
                        GenerateConvertedScreenInput2Value();
                    }
                    else if (IsInput2Selected())
                    {
                        GenerateConvertedScreenInput1Value();
                    }
                }
            }
        }

        public UnitName InputUnit2
        {
            get => _inputUnit2;
            set
            {
                UnitName previousInputUnit = _inputUnit2;

                SetProperty(ref _inputUnit2, value);

                if (previousInputUnit != null && previousInputUnit != _inputUnit2)
                {
                    if (IsInput1Selected())
                    {
                        GenerateConvertedScreenInput2Value();
                    }
                    else if (IsInput2Selected())
                    {
                        GenerateConvertedScreenInput1Value();
                    }
                }
            }
        }

        #endregion

        public BaseConverterPageViewModel()
        {
            _unitConverterService = new UnitConverterService();

            IsNumericalSignVisible = false;

            ScreenInputValue1 = "0";
            ScreenInputValue2 = "0";

            ExecuteInputValue1Command();

            Input1MaxLength = DefaultCharacterLength;
            Input2MaxLength = DefaultCharacterLength;

            InputCommand = new Command<Button>((input) => ExecuteInputCommand(input.Text));
            ClearAllInputCommand = new Command(ExecuteClearAllInputCommand);
            EraseInputCommand = new Command(() => ExecuteEraseInputCommand(1));
            PeriodCommand = new Command(ExecutePeriodCommand);
            NumericalSignCommand = new Command(ExecuteNumericalSignCommand);
            InputValue1SelectedCommand = new Command(ExecuteInputValue1Command);
            InputValue2SelectedCommand = new Command(ExecuteInputValue2Command);

            InputUnitPicker1Command = new Command(() => ExecuteUnitPicker1Command());
            InputUnitPicker2Command = new Command(() => ExecuteUnitPicker2Command());

            UnitSelected1Command = new Command((unit) => ExecuteUnitSelected1Command(unit as UnitName));
            UnitSelected2Command = new Command((unit) => ExecuteUnitSelected2Command(unit as UnitName));
        }

        #region Command Functions

        private void ExecuteInputCommand(string input)
        {   
            if (IsInput1Selected())
            {
                if (_resetInputValue1)
                {
                    ResetInputValue1();
                }

                ScreenInputValue1 = ScreenInputValue1.ConcatenateInputs(input);
            }

            if (IsInput2Selected())
            {
                if (_resetInputValue2)
                {
                    ResetInputValue2();
                }

                ScreenInputValue2 = ScreenInputValue2.ConcatenateInputs(input);
            }
        }

        private void ExecuteClearAllInputCommand()
        {
            ClearAllInputs();
        }

        private void ExecuteEraseInputCommand(int lengthToRemove)
        {
            if (IsInput1Selected())
            {
                ScreenInputValue1 = ScreenInputValue1.EraseInputs(lengthToRemove);

                if (!ScreenInputValue1.Contains('.'))
                {
                    _isInputUnit1LengthUpdated = false;
                    Input1MaxLength = MaxCharacterLength;
                }
            }

            if (IsInput2Selected())
            {
                ScreenInputValue2 = ScreenInputValue2.EraseInputs(lengthToRemove);

                if (!ScreenInputValue2.Contains('.'))
                {
                    _isInputUnit2LengthUpdated = false;
                    Input2MaxLength = MaxCharacterLength;
                }
            }
        }

        private void ExecutePeriodCommand()
        {
            AddPeriodInput();
        }

        private void ExecuteNumericalSignCommand()
        {
            AddNumericalSign();
        }

        private void ExecuteUnitPicker1Command()
        {
            if (!CalcticHelpers.PopupPageIsAlreadyAtTopOfStack(typeof(PopupUnitListPage)))
            {
                var popup = new PopupUnitListPage(SelectedUnitConverter,
                                                  _unitConverterService.GetSupportedUnitsOfMeasurement(SelectedUnitConverter));
                var context = popup.BindingContext as PopupUnitListPageViewModel;

                context.UnitSelectedCommand = UnitSelected1Command;
                context.SelectedUnit = InputUnit1;

                popup.BindingContext = context;

                PopupNavigation.Instance.PushAsync(popup);
            }
        }

        private void ExecuteUnitPicker2Command()
        {
            if (!CalcticHelpers.PopupPageIsAlreadyAtTopOfStack(typeof(PopupUnitListPage)))
            {
                var popup = new PopupUnitListPage(SelectedUnitConverter,
                                                  _unitConverterService.GetSupportedUnitsOfMeasurement(SelectedUnitConverter));
                var context = popup.BindingContext as PopupUnitListPageViewModel;

                context.UnitSelectedCommand = UnitSelected2Command;
                context.SelectedUnit = InputUnit2;

                popup.BindingContext = context;

                PopupNavigation.Instance.PushAsync(popup);
            }
        }

        private void ExecuteUnitSelected1Command(UnitName selectedUnit)
        {
            InputUnit1 = selectedUnit;
        }

        private void ExecuteUnitSelected2Command(UnitName selectedUnit)
        {
            InputUnit2 = selectedUnit;
        }

        private void ExecuteInputValue1Command()
        {
            IsValue1Selected = true;
            _resetInputValue1 = true;
        }

        private void ExecuteInputValue2Command()
        {
            IsValue2Selected = true;
            _resetInputValue2 = true;
        }

        #endregion

        #region Helper Functions

        private bool IsInput1Selected()
        {
            return IsValue1Selected && !IsValue2Selected;
        }

        private bool IsInput2Selected()
        {
            return IsValue2Selected && !IsValue1Selected;
        }

        private void ClearAllInputs()
        {
            ResetInputValue1();
        }

        private void ResetInputValue1()
        {
            if (_selectedUnitConverter == UnitSelection.Temperature)
            {
                ScreenInputValue1 = "0";
                ScreenInputValue2 = "32";
            }
            else
            {
                ScreenInputValue1 = "0";
                ScreenInputValue2 = "0";
            }

            ScreenInputValue1 = "0";
            _resetInputValue1 = false;
            _isInputUnit1LengthUpdated = false;
            Input1MaxLength = MaxCharacterLength;
            Input2MaxLength = int.MaxValue;
        }

        private void ResetInputValue2()
        {
            if (_selectedUnitConverter == UnitSelection.Temperature)
            {
                ScreenInputValue1 = "0";
                ScreenInputValue2 = "32";
            }
            else
            {
                ScreenInputValue1 = "0";
                ScreenInputValue2 = "0";
            }

            ScreenInputValue2 = "0";
            _resetInputValue2 = false;
            _isInputUnit2LengthUpdated = false;
            Input2MaxLength = MaxCharacterLength;
            Input1MaxLength = int.MaxValue;
        }

        private void AddPeriodInput()
        {
            if (IsInput1Selected())
            {
                if (ScreenInputValue1.IsLastNumberGoodForDecimalPoint())
                {
                    ScreenInputValue1 = ScreenInputValue1.ConcatenateInputs(".");
                }
            }

            if (IsInput2Selected())
            {
                if (ScreenInputValue2.IsLastNumberGoodForDecimalPoint())
                {
                    ScreenInputValue2 = ScreenInputValue2.ConcatenateInputs(".");
                }
            }
        }

        private void AddNumericalSign()
        {
            if (IsInput1Selected())
            {
                string lastNumber = ScreenInputValue1.GetLastInputNumbers();

                if (string.IsNullOrEmpty(lastNumber))
                {
                    return;
                }

                if (!lastNumber.IsBasicOperator() && lastNumber != "0")
                {
                    ScreenInputValue1 = ScreenInputValue1.EraseInputs(lastNumber.Length);

                    if (lastNumber.IsInputNumberNegative())
                    {
                        lastNumber = lastNumber.Remove(lastNumber.IndexOf('-'), 1);
                    }
                    else
                    {
                        lastNumber = lastNumber.Insert(0, "-");
                    }

                    ScreenInputValue1 = ScreenInputValue1.ConcatenateInputs(lastNumber);
                }
            }

            if (IsInput2Selected())
            {
                string lastNumber = ScreenInputValue2.GetLastInputNumbers();

                if (string.IsNullOrEmpty(lastNumber))
                {
                    return;
                }

                if (!lastNumber.IsBasicOperator() && lastNumber != "0")
                {
                    ScreenInputValue2 = ScreenInputValue2.EraseInputs(lastNumber.Length);

                    if (lastNumber.IsInputNumberNegative())
                    {
                        lastNumber = lastNumber.Remove(lastNumber.IndexOf('-'), 1);
                    }
                    else
                    {
                        lastNumber = lastNumber.Insert(0, "-");
                    }

                    ScreenInputValue2 = ScreenInputValue2.ConcatenateInputs(lastNumber);
                }
            }
        }

        private void GenerateConvertedScreenInput1Value()
        {
            try
            {
                double inputValue1 = 0.0;
                double inputValue2 = 0.0;

                var inputValue1String = "";

                inputValue2 = Convert.ToDouble(ScreenInputValue2);
                inputValue1 = UnitConverterHelpers.GetConvertedValue(inputValue2, SelectedUnitConverter, InputUnit2, InputUnit1);

                inputValue1String = string.Format("{0:0.####}", inputValue1);

                if (ScreenInputValue2.Contains('.') && !_isInputUnit2LengthUpdated)
                {
                    Input2MaxLength = ScreenInputValue2.Length + MaxNumberOfDecimalPlaces;
                    _isInputUnit2LengthUpdated = true;
                }

                ScreenInputValue1 = inputValue1String;
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.StackTrace);
            }
        }

        private void GenerateConvertedScreenInput2Value()
        {
            try
            {
                double inputValue1 = 0.0;
                double inputValue2 = 0.0;

                var inputValue2String = "";

                inputValue1 = Convert.ToDouble(ScreenInputValue1);
                inputValue2 = UnitConverterHelpers.GetConvertedValue(inputValue1, SelectedUnitConverter, InputUnit1, InputUnit2);

                inputValue2String = string.Format("{0:0.####}", inputValue2);

                if (ScreenInputValue1.Contains('.') && !_isInputUnit1LengthUpdated)
                {
                    Input1MaxLength = ScreenInputValue1.Length + MaxNumberOfDecimalPlaces;
                    _isInputUnit1LengthUpdated = true;
                }

                ScreenInputValue2 = inputValue2String;
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.StackTrace);
            }
        }

        #endregion
    }
}
