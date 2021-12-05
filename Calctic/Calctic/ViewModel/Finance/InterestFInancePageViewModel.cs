using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Calctic.Helpers;
using Calctic.Interfaces;
using Xamarin.Forms;

namespace Calctic.ViewModel.Finance
{
    public class InterestFinancePageViewModel : ViewModelBase
    {
        private IBasicCalculatorService _basicCalculatorService;
        private IMessagingService _messagingService;

        private bool _isPrincipalValueSelected;
        private bool _isRateValueSelected;
        private bool _isTimeValueSelected;
        private bool _isAnswerVisible;

        private string _screenPrincipalValue;
        private string _screenRateValue;
        private string _screenTimeValue;
        private string _screenInputAnswer;

        private string _generateAnswerText;

        #region ICommands

        public ICommand InputCommand { get; }
        public ICommand ClearAllInputCommand { get; }
        public ICommand EraseInputCommand { get; }
        public ICommand PeriodCommand { get; }
        public ICommand PrincipalValueSelectedCommand { get; }
        public ICommand RateValueSelectedCommand { get; }
        public ICommand TimeValueSelectedCommand { get; }
        public ICommand GenerateAnswerCommand { get; }

        #endregion

        #region Properties

        public bool IsPrincipalValueSelected
        {
            get => _isPrincipalValueSelected;
            set
            {
                SetProperty(ref _isPrincipalValueSelected, value);

                if (_isPrincipalValueSelected)
                {
                    IsRateValueSelected = false;
                    IsTimeValueSelected = false;
                }
            }
        }

        public bool IsRateValueSelected
        {
            get => _isRateValueSelected;
            set
            {
                SetProperty(ref _isRateValueSelected, value);

                if (_isRateValueSelected)
                {
                    IsPrincipalValueSelected = false;
                    IsTimeValueSelected = false;
                }
            }
        }

        public bool IsTimeValueSelected
        {
            get => _isTimeValueSelected;
            set
            {
                SetProperty(ref _isTimeValueSelected, value);

                if (_isTimeValueSelected)
                {
                    IsPrincipalValueSelected = false;
                    IsRateValueSelected = false;
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

        public string ScreenPrincipalValue
        {
            get => _screenPrincipalValue;
            set
            {
                SetProperty(ref _screenPrincipalValue, value);
            }
        }

        public string ScreenRateValue
        {
            get => _screenRateValue;
            set
            {
                SetProperty(ref _screenRateValue, value);
            }
        }

        public string ScreenTimeValue
        {
            get => _screenTimeValue;
            set
            {
                SetProperty(ref _screenTimeValue, value);
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

        #endregion

        public InterestFinancePageViewModel(IBasicCalculatorService basicCalculatorService, IMessagingService messagingService)
        {
            _basicCalculatorService = basicCalculatorService;
            _messagingService = messagingService;

            Title = "Interest";

            InputCommand = new Command<Button>((input) => ConcatenateInputs(input.Text), (input) => !IsAnswerVisible);
            ClearAllInputCommand = new Command(ClearAllInputs, () => !IsAnswerVisible);
            EraseInputCommand = new Command(() => EraseInputs(1), () => !IsAnswerVisible);
            PeriodCommand = new Command(AddPeriodInput, () => !IsAnswerVisible);
            PrincipalValueSelectedCommand = new Command(ExecutePrincipalValueSelectedCommand);
            RateValueSelectedCommand = new Command(ExecuteRateValueSelectedCommand);
            TimeValueSelectedCommand = new Command(ExecuteTimeValueSelectedCommand);
            GenerateAnswerCommand = new Command(GenerateAnswer);

            ResetPageInputs();
        }

        #region Command Functions

        private void ConcatenateInputs(string input)
        {
            if (IsInputPrincipalValueSelected())
            {
                ScreenPrincipalValue = ScreenPrincipalValue.ConcatenateInputs(input);
            }

            if (IsInputRateValueSelected())
            {
                ScreenRateValue = ScreenRateValue.ConcatenateInputs(input);
            }

            if (IsInputTimeValueSelected())
            {
                ScreenTimeValue = ScreenTimeValue.ConcatenateInputs(input);
            }
        }

        private void ClearAllInputs()
        {
            ScreenPrincipalValue = "0";
            ScreenRateValue = "0";
            ScreenTimeValue = "0";
        }

        private void EraseInputs(int lengthToRemove)
        {
            if (IsInputPrincipalValueSelected())
            {
                ScreenPrincipalValue = ScreenPrincipalValue.EraseInputs(lengthToRemove);
            }

            if (IsInputRateValueSelected())
            {
                ScreenRateValue = ScreenRateValue.EraseInputs(lengthToRemove);
            }

            if (IsInputTimeValueSelected())
            {
                ScreenTimeValue = ScreenTimeValue.EraseInputs(lengthToRemove);
            }
        }

        private void AddPeriodInput()
        {
            if (IsInputPrincipalValueSelected())
            {
                if (ScreenPrincipalValue.IsLastNumberGoodForDecimalPoint())
                {
                    ScreenPrincipalValue = ScreenPrincipalValue.ConcatenateInputs(".");
                }
            }

            if (IsInputRateValueSelected())
            {
                if (ScreenRateValue.IsLastNumberGoodForDecimalPoint())
                {
                    ScreenRateValue = ScreenRateValue.ConcatenateInputs(".");
                }
            }
        }

        private void ExecutePrincipalValueSelectedCommand()
        {
            IsPrincipalValueSelected = GenerateAnswerText != "Input Another";
        }

        private void ExecuteRateValueSelectedCommand()
        {
            IsRateValueSelected = GenerateAnswerText != "Input Another";
        }

        private void ExecuteTimeValueSelectedCommand()
        {
            IsTimeValueSelected = GenerateAnswerText != "Input Another";
        }

        private void GenerateAnswer()
        {
            int timeConversionFactor = 12;

            try
            {
                if (GenerateAnswerText == "Calculate")
                {
                    string inputValues = $"({ScreenPrincipalValue} * " +
                         $"{ScreenRateValue} * " +
                         $"({ScreenTimeValue} / {timeConversionFactor})) / 100";

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

        private bool IsInputPrincipalValueSelected()
        {
            return IsPrincipalValueSelected && !IsRateValueSelected && !IsTimeValueSelected;
        }

        private bool IsInputRateValueSelected()
        {
            return IsRateValueSelected && !IsPrincipalValueSelected && !IsTimeValueSelected;
        }

        private bool IsInputTimeValueSelected()
        {
            return IsTimeValueSelected && !IsPrincipalValueSelected && !IsRateValueSelected;
        }

        private void DisableAllInputFields()
        {
            IsPrincipalValueSelected = false;
            IsRateValueSelected = false;
            IsTimeValueSelected = false;
        }

        private void RefreshButtonCommands()
        {
            (InputCommand as Command).ChangeCanExecute();
            (ClearAllInputCommand as Command).ChangeCanExecute();
            (EraseInputCommand as Command).ChangeCanExecute();
            (PeriodCommand as Command).ChangeCanExecute();
        }

        private void ResetPageInputs()
        {
            ScreenPrincipalValue = "0";
            ScreenRateValue = "0";
            ScreenTimeValue = "0";
            ScreenInputAnswer = "0";

            IsAnswerVisible = false;

            IsPrincipalValueSelected = true;

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
