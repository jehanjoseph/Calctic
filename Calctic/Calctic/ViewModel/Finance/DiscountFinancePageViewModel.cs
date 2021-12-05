using System;
using System.Diagnostics;
using System.Windows.Input;
using Calctic.Helpers;
using Calctic.Helpers.Validation;
using Calctic.Interfaces;
using Xamarin.Forms;

namespace Calctic.ViewModel.Finance
{
    public class DiscountFinancePageViewModel : ViewModelBase
    {
        private IBasicCalculatorService _basicCalculatorService;
        private IMessagingService _messagingService;

        private bool _isDiscountValueSelected;
        private bool _isOriginalAmountSelected;
        private bool _isAnswerVisible;

        private string _screenDiscountValue;
        private string _originalAmountValue;
        private string _discountedPriceAnswer;

        private string _generateAnswerText;

        private ValidatableObject<double> _validatableDiscountValue;

        #region ICommands

        public ICommand InputCommand { get; }
        public ICommand ClearAllInputCommand { get; }
        public ICommand EraseInputCommand { get; }
        public ICommand PeriodCommand { get; }
        public ICommand DiscountValueSelectedCommand { get; }
        public ICommand OriginalAmountSelectedCommand { get; }
        public ICommand GenerateAnswerCommand { get; }

        #endregion

        #region Properties

        public bool IsDiscountValueSelected
        {
            get => _isDiscountValueSelected;
            set
            {
                SetProperty(ref _isDiscountValueSelected, value);

                if (_isDiscountValueSelected)
                {
                    IsOriginalAmountSelected = false;
                }
            }
        }

        public bool IsOriginalAmountSelected
        {
            get => _isOriginalAmountSelected;
            set
            {
                SetProperty(ref _isOriginalAmountSelected, value);

                if (_isOriginalAmountSelected)
                {
                    IsDiscountValueSelected = false;
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

        public string ScreenDiscountValue
        {
            get => _screenDiscountValue;
            set
            {
                SetProperty(ref _screenDiscountValue, value);

                if (ValidatableDiscountValue is not null)
                {
                    ValidatableDiscountValue.Value = Convert.ToDouble(ScreenDiscountValue);
                }
            }
        }

        public string ScreenOriginalAmount
        {
            get => _originalAmountValue;
            set
            {
                SetProperty(ref _originalAmountValue, value);
            }
        }

        public string ScreenInputAnswer
        {
            get => _discountedPriceAnswer;
            set
            {
                SetProperty(ref _discountedPriceAnswer, value);
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

        public ValidatableObject<double> ValidatableDiscountValue
        {
            get => _validatableDiscountValue;
            set
            {
                SetProperty(ref _validatableDiscountValue, value);
            }
        }

        #endregion

        public DiscountFinancePageViewModel(IBasicCalculatorService basicCalculatorService, IMessagingService messagingService)
        {
            _basicCalculatorService = basicCalculatorService;
            _messagingService = messagingService;

            Title = "Discount";

            InputCommand = new Command<Button>((input) => ConcatenateInputs(input.Text), (input) => !IsAnswerVisible);
            ClearAllInputCommand = new Command(ClearAllInputs, () => !IsAnswerVisible);
            EraseInputCommand = new Command(() => EraseInputs(1), () => !IsAnswerVisible);
            PeriodCommand = new Command(AddPeriodInput, () => !IsAnswerVisible);
            DiscountValueSelectedCommand = new Command(ExecuteDiscountValueSelectedCommand);
            OriginalAmountSelectedCommand = new Command(ExecuteOriginalAmountSelectedCommand);
            GenerateAnswerCommand = new Command(GenerateAnswer);

            ResetPageInputs();

            InitializeValidations();
        }

        #region Command Functions

        private void ConcatenateInputs(string input)
        {
            if (IsDiscountValueInputSelected())
            {
                ScreenDiscountValue = ScreenDiscountValue.ConcatenateInputs(input);
            }

            if (IsOriginalAmountInputSelected())
            {
                ScreenOriginalAmount = ScreenOriginalAmount.ConcatenateInputs(input);
            }
        }

        private void ClearAllInputs()
        {
            ScreenDiscountValue = "0";
            ScreenOriginalAmount = "0";
        }

        private void EraseInputs(int lengthToRemove)
        {
            if (IsDiscountValueInputSelected())
            {
                ScreenDiscountValue = ScreenDiscountValue.EraseInputs(lengthToRemove);
            }

            if (IsOriginalAmountInputSelected())
            {
                ScreenOriginalAmount = ScreenOriginalAmount.EraseInputs(lengthToRemove);
            }
        }

        private void AddPeriodInput()
        {
            if (IsOriginalAmountInputSelected())
            {
                if (ScreenOriginalAmount.IsLastNumberGoodForDecimalPoint())
                {
                    ScreenOriginalAmount = ScreenOriginalAmount.ConcatenateInputs(".");
                }
            }
        }

        private void ExecuteDiscountValueSelectedCommand()
        {
            IsDiscountValueSelected = GenerateAnswerText != "Input Another";
        }

        private void ExecuteOriginalAmountSelectedCommand()
        {
            IsOriginalAmountSelected = GenerateAnswerText != "Input Another";
        }

        private void GenerateAnswer()
        {
            try
            {
                if (!AreFieldsValid())
                {
                    _messagingService.ShowAsync(ValidatableDiscountValue.ErrorMessage);
                    return;
                }

                if (GenerateAnswerText == "Calculate")
                {
                    string inputValues =
                         $"({ScreenOriginalAmount} - " +
                         $"({ScreenOriginalAmount} * ({ScreenDiscountValue} / 100)))";

                    double answer = CalculateInputs(inputValues);

                    ScreenInputAnswer = $"{answer}";
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
                Debug.WriteLine(exception.StackTrace);
                _messagingService.ShowAsync(exception.Message);
            }
        }

        #endregion

        #region Helper Functions

        private bool IsDiscountValueInputSelected()
        {
            return IsDiscountValueSelected && !IsOriginalAmountSelected;
        }

        private bool IsOriginalAmountInputSelected()
        {
            return IsOriginalAmountSelected && !IsDiscountValueSelected;
        }

        private bool AreFieldsValid()
        {
            bool isDiscountValueInputValid = ValidatableDiscountValue.Validate();
            return isDiscountValueInputValid;
        }

        private void InitializeValidations()
        {
            const double MaximumDiscountValue = 100;

            ValidatableDiscountValue = new();
            ValidatableDiscountValue.Value = Convert.ToDouble(ScreenDiscountValue);
            ValidatableDiscountValue.Validations.Add(new IsNotGreaterThanRule<double>(MaximumDiscountValue)
                {
                    ValidationMessage = "Discount should not be greater than 100"
                }
            );
        }

        private void DisableAllInputFields()
        {
            IsDiscountValueSelected = false;
            IsOriginalAmountSelected = false;
        }

        private void ResetPageInputs()
        {
            ScreenDiscountValue = "0";
            ScreenOriginalAmount = "0";
            ScreenInputAnswer = "0";

            IsAnswerVisible = false;

            IsDiscountValueSelected = true;

            GenerateAnswerText = "Calculate";
        }

        private void RefreshButtonCommands()
        {
            (InputCommand as Command).ChangeCanExecute();
            (ClearAllInputCommand as Command).ChangeCanExecute();
            (EraseInputCommand as Command).ChangeCanExecute();
            (PeriodCommand as Command).ChangeCanExecute();
        }

        private double CalculateInputs(string input)
        {
            _basicCalculatorService.Inputs = input;
            return _basicCalculatorService.GetCalculator().Execute();
        }

        #endregion
    }
}
