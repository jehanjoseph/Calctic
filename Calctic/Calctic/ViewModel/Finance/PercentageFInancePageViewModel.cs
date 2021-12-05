using System;
using System.Diagnostics;
using System.Windows.Input;
using Calctic.Helpers;
using Calctic.Helpers.Validation;
using Calctic.Interfaces;
using Xamarin.Forms;

namespace Calctic.ViewModel.Finance
{
    public class PercentageFinancePageViewModel : ViewModelBase
    {
        private IBasicCalculatorService _basicCalculatorService;
        private IMessagingService _messagingService;

        private bool _isPartValueSelected;
        private bool _isWholeValueSelected;
        private bool _isAnswerVisible;

        private string _screenPartValue;
        private string _screenWholeValue;
        private string _screenInputAnswer;

        private string _generateAnswerText;

        private ValidatableObject<double> _validatableComparerValue;

        #region ICommands

        public ICommand InputCommand { get; }
        public ICommand ClearAllInputCommand { get; }
        public ICommand EraseInputCommand { get; }
        public ICommand PeriodCommand { get; }
        public ICommand PartValueSelectedCommand { get; }
        public ICommand WholeValueSelectedCommand { get; }
        public ICommand GenerateAnswerCommand { get; }

        #endregion

        #region Properties

        public bool IsPartValueSelected
        {
            get => _isPartValueSelected;
            set
            {
                SetProperty(ref _isPartValueSelected, value);

                if (_isPartValueSelected)
                {
                    IsWholeValueSelected = false;
                }
            }
        }

        public bool IsWholeValueSelected
        {
            get => _isWholeValueSelected;
            set
            {
                SetProperty(ref _isWholeValueSelected, value);

                if (_isWholeValueSelected)
                {
                    IsPartValueSelected = false;
                }
            }
        }

        public bool IsAnswerVisible
        {
            get => _isAnswerVisible;
            set
            {
                SetProperty(ref _isAnswerVisible, value);
                RefreshButtonCommands();
            }
        }

        public string ScreenPartValue
        {
            get => _screenPartValue;
            set
            {
                SetProperty(ref _screenPartValue, value);

                if (ValidatableComparer is not null)
                {
                    ValidatableComparer.Value = Convert.ToDouble(ScreenPartValue);
                }
            }
        }

        public string ScreenWholeValue
        {
            get => _screenWholeValue;
            set
            {
                SetProperty(ref _screenWholeValue, value);

                if (ValidatableComparer is not null)
                {
                    ImplementValidationRule();
                }
            }
        }

        public string ScreenInputAnswer
        {
            get => _screenInputAnswer;
            set
            {
                SetProperty(ref _screenInputAnswer, value);
            }
        }

        public string GenerateAnswerText
        {
            get => _generateAnswerText;
            set
            {
                SetProperty(ref _generateAnswerText, value);
            }
        }

        public ValidatableObject<double> ValidatableComparer
        {
            get => _validatableComparerValue;
            set
            {
                SetProperty(ref _validatableComparerValue, value);
            }
        }

        #endregion

        public PercentageFinancePageViewModel(IBasicCalculatorService basicCalculatorService, IMessagingService messagingService)
        {
            _basicCalculatorService = basicCalculatorService;
            _messagingService = messagingService;

            Title = "Percentage";

            InputCommand = new Command<Button>((input) => ConcatenateInputs(input.Text), (input) => !IsAnswerVisible);
            ClearAllInputCommand = new Command(ClearAllInputs, () => !IsAnswerVisible);
            EraseInputCommand = new Command(() => EraseInputs(1), () => !IsAnswerVisible);
            PeriodCommand = new Command(AddPeriodInput, () => !IsAnswerVisible);
            PartValueSelectedCommand = new Command(ExecuteInputPartValueCommand);
            WholeValueSelectedCommand = new Command(ExecuteInputWholeValueCommand);
            GenerateAnswerCommand = new Command(GenerateAnswer);

            ResetPageInputs();

            InitializeValidations();
        }

        #region Command Functions

        private void ConcatenateInputs(string input)
        {
            if (IsInput1Selected())
            {
                ScreenPartValue = ScreenPartValue.ConcatenateInputs(input);
            }

            if (IsInput2Selected())
            {
                ScreenWholeValue = ScreenWholeValue.ConcatenateInputs(input);
            }
        }

        private void ClearAllInputs()
        {
            ScreenPartValue = "0";
            ScreenWholeValue = "0";
        }

        private void EraseInputs(int lengthToRemove)
        {
            if (IsInput1Selected())
            {
                ScreenPartValue = ScreenPartValue.EraseInputs(lengthToRemove);
            }

            if (IsInput2Selected())
            {
                ScreenWholeValue = ScreenWholeValue.EraseInputs(lengthToRemove);
            }
        }

        private void AddPeriodInput()
        {
            if (IsInput1Selected())
            {
                if (ScreenPartValue.IsLastNumberGoodForDecimalPoint())
                {
                    ScreenPartValue = ScreenPartValue.ConcatenateInputs(".");
                }
            }

            if (IsInput2Selected())
            {
                if (ScreenWholeValue.IsLastNumberGoodForDecimalPoint())
                {
                    ScreenWholeValue = ScreenWholeValue.ConcatenateInputs(".");
                }
            }
        }

        private void ExecuteInputPartValueCommand()
        {
            IsPartValueSelected = GenerateAnswerText != "Input Another";
        }

        private void ExecuteInputWholeValueCommand()
        {
            IsWholeValueSelected = GenerateAnswerText != "Input Another";
        }

        private void GenerateAnswer()
        {
            try
            {
                if (!AreFieldsValid())
                {
                    _messagingService.ShowAsync(ValidatableComparer.ErrorMessage);
                    return;
                }

                if (GenerateAnswerText == "Calculate")
                {
                    string inputValues =
                         $"(({ScreenWholeValue} - " +
                         $"({ScreenWholeValue} - {ScreenPartValue})) / " +
                         $"{ScreenWholeValue}) * 100";

                    double answer = CalculateInputs(inputValues);

                    ScreenInputAnswer = $"{answer}%";
                    IsAnswerVisible = true;

                    DisableAllInputFields();

                    GenerateAnswerText = "Input Another";
                }
                else if (GenerateAnswerText == "Input Another")
                {
                    ResetPageInputs();
                }
            }
            catch (InvalidOperationException exception)
            {
                Debug.WriteLine(exception.Message);
                _messagingService.ShowAsync(exception.Message);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _messagingService.ShowAsync(exception.Message);
            }
        }

        #endregion

        #region Helper Functions

        private bool IsInput1Selected()
        {
            return IsPartValueSelected && !IsWholeValueSelected;
        }

        private bool IsInput2Selected()
        {
            return IsWholeValueSelected && !IsPartValueSelected;
        }

        private bool AreFieldsValid()
        {
            bool areInputsValid = ValidatableComparer.Validate();
            return areInputsValid;
        }

        private void InitializeValidations()
        {
            ValidatableComparer = new();
            ValidatableComparer.Value = Convert.ToDouble(ScreenPartValue);

            ImplementValidationRule();
        }

        private void DisableAllInputFields()
        {
            IsPartValueSelected = false;
            IsWholeValueSelected = false;
        }

        private void RefreshButtonCommands()
        {
            (InputCommand as Command).ChangeCanExecute();
            (ClearAllInputCommand as Command).ChangeCanExecute();
            (EraseInputCommand as Command).ChangeCanExecute();
            (PeriodCommand as Command).ChangeCanExecute();
        }

        private void ImplementValidationRule()
        {
            ValidatableComparer.Validations.Clear();

            double wholeValue = Convert.ToDouble(ScreenWholeValue);

            ValidatableComparer.Validations.Add(new IsNotGreaterThanRule<double>(wholeValue)
            {
                ValidationMessage = "Part input should not be greater than Whole input."
            });
        }

        private void ResetPageInputs()
        {
            ScreenPartValue = "0";
            ScreenWholeValue = "0";
            ScreenInputAnswer = "0";

            IsAnswerVisible = false;
            IsPartValueSelected = true;

            GenerateAnswerText = "Calculate";
        }

        private double CalculateInputs(string input)
        {
            _basicCalculatorService.Inputs = input;
            return _basicCalculatorService.GetCalculator().Execute();
        }

        #endregion
    }
}
