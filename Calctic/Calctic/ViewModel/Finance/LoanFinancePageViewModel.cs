using System;
using System.Threading.Tasks;
using Calctic.Helpers;
using System.Windows.Input;
using Xamarin.Forms;
using Calctic.Interfaces;
using System.Diagnostics;

namespace Calctic.ViewModel.Finance
{
    public class LoanFinancePageViewModel : ViewModelBase
    {
        private IBasicCalculatorService _basicCalculatorService;
        private IMessagingService _messagingService;

        private bool _isLoanValueSelected;
        private bool _isRateValueValueSelected;
        private bool _isInterestValueSelected;
        private bool _isAnswerVisible;
        private bool _isCalculatorInputVisible;

        private string _loanValue;
        private string _rateValue;
        private string _periodValue;

        private string _generateAnswerText;

        private double _monthlyPayAnswer;
        private double _totalPayAnswer;
        private double _interestPayAnswer;

        #region ICommands

        public ICommand InputCommand { get; }
        public ICommand ClearAllInputCommand { get; }
        public ICommand EraseInputCommand { get; }
        public ICommand PeriodCommand { get; }
        public ICommand BalanceValueCommand { get; }
        public ICommand ContributionValueCommand { get; }
        public ICommand InterestValueCommand { get; }
        public ICommand GenerateAnswerCommand { get; }

        #endregion

        #region Properties

        public bool IsLoanValueSelected
        {
            get => _isLoanValueSelected;
            set
            {
                SetProperty(ref _isLoanValueSelected, value);

                if (_isLoanValueSelected)
                {
                    IsRateValueSelected = false;
                    IsPeriodValueSelected = false;
                }
            }
        }

        public bool IsRateValueSelected
        {
            get => _isRateValueValueSelected;
            set
            {
                SetProperty(ref _isRateValueValueSelected, value);

                if (_isRateValueValueSelected)
                {
                    IsLoanValueSelected = false;
                    IsPeriodValueSelected = false;
                }
            }
        }

        public bool IsPeriodValueSelected
        {
            get => _isInterestValueSelected;
            set
            {
                SetProperty(ref _isInterestValueSelected, value);

                if (_isInterestValueSelected)
                {
                    IsLoanValueSelected = false;
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
            }
        }

        public bool IsCalculatorInputVisible
        {
            get => _isCalculatorInputVisible;
            set
            {
                SetProperty(ref _isCalculatorInputVisible, value);
            }
        }

        public string LoanValue
        {
            get => _loanValue;
            set
            {
                SetProperty(ref _loanValue, value);
            }
        }

        public string RateValue
        {
            get => _rateValue;
            set
            {
                SetProperty(ref _rateValue, value);
            }
        }

        public string PeriodValue
        {
            get => _periodValue;
            set
            {
                SetProperty(ref _periodValue, value);
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

        public double MonthlyPayAnswer
        {
            get => _monthlyPayAnswer;
            set
            {
                SetProperty(ref _monthlyPayAnswer, value);
            }
        }

        public double TotalPayAnswer
        {
            get => _totalPayAnswer;
            set
            {
                SetProperty(ref _totalPayAnswer, value);
            }
        }

        public double InterestPayAnswer
        {
            get => _interestPayAnswer;
            set
            {
                SetProperty(ref _interestPayAnswer, value);
            }
        }

        #endregion

        public LoanFinancePageViewModel(IBasicCalculatorService basicCalculatorService,
                                        IMessagingService messagingService)
        {
            _basicCalculatorService = basicCalculatorService;
            _messagingService = messagingService;

            Title = "Loan Calculator";

            InputCommand = new Command<Button>((input) => ExecuteInputCommand(input.Text), (input) => !IsAnswerVisible);
            ClearAllInputCommand = new Command(ExecuteClearAllInputCommand, () => !IsAnswerVisible);
            EraseInputCommand = new Command(() => ExecuteEraseInputCommand(1), () => !IsAnswerVisible);
            PeriodCommand = new Command(ExecutePeriodCommand, () => !IsAnswerVisible);
            BalanceValueCommand = new Command(ExecuteBalanceValueSelectedCommand);
            ContributionValueCommand = new Command(ExecuteContributionValueSelectedCommand);
            InterestValueCommand = new Command(ExecuteInterestValueCommand);
            GenerateAnswerCommand = new Command(ExecuteGenerateAnswerCommand);

            ResetPageInputs();
        }

        #region Command Functions

        private void ExecuteInputCommand(string input)
        {
            if (IsBalanceValueOnScreenSelected())
            {
                LoanValue = LoanValue.ConcatenateInputs(input);
            }

            if (IsContributionValueOnScreenSelected())
            {
                RateValue = RateValue.ConcatenateInputs(input);
            }

            if (IsInterestValueOnScreenSelected())
            {
                PeriodValue = PeriodValue.ConcatenateInputs(input);
            }
        }

        private void ExecuteEraseInputCommand(int lengthToRemove)
        {
            if (IsBalanceValueOnScreenSelected())
            {
                LoanValue = LoanValue.EraseInputs(lengthToRemove);
            }

            if (IsContributionValueOnScreenSelected())
            {
                RateValue = RateValue.EraseInputs(lengthToRemove);
            }

            if (IsInterestValueOnScreenSelected())
            {
                PeriodValue = PeriodValue.EraseInputs(lengthToRemove);
            }
        }

        private void ExecutePeriodCommand()
        {
            if (IsBalanceValueOnScreenSelected())
            {
                if (LoanValue.IsLastNumberGoodForDecimalPoint())
                {
                    LoanValue = LoanValue.ConcatenateInputs(".");
                }
            }

            if (IsContributionValueOnScreenSelected())
            {
                if (RateValue.IsLastNumberGoodForDecimalPoint())
                {
                    RateValue = RateValue.ConcatenateInputs(".");
                }
            }

            if (IsInterestValueOnScreenSelected())
            {
                if (PeriodValue.IsLastNumberGoodForDecimalPoint())
                {
                    PeriodValue = PeriodValue.ConcatenateInputs(".");
                }
            }
        }

        private void ExecuteBalanceValueSelectedCommand()
        {
            IsLoanValueSelected = GenerateAnswerText != "Input Another";
        }

        private void ExecuteContributionValueSelectedCommand()
        {
            IsRateValueSelected = GenerateAnswerText != "Input Another";
        }

        private void ExecuteInterestValueCommand()
        {
            IsPeriodValueSelected = GenerateAnswerText != "Input Another";
        }

        private void ExecuteClearAllInputCommand()
        {
            LoanValue = "0";
            RateValue = "0";
            PeriodValue = "0";
        }

        private void ExecuteGenerateAnswerCommand()
        {
            try
            {
                if (GenerateAnswerText == "Calculate")
                {
                    GenerateAndDisplayAnswer();
                }
                else if (GenerateAnswerText == "Input Another")
                {
                    ResetPageInputs();
                }
            }
            catch (InvalidOperationException invalidOperationException)
            {
                Debug.WriteLine(invalidOperationException.StackTrace);
                _messagingService.ShowAsync(invalidOperationException.Message);
            }
            catch (DivideByZeroException divideByZeroException)
            {
                Debug.WriteLine(divideByZeroException.StackTrace);
                _messagingService.ShowAsync(divideByZeroException.Message);
            }
            catch (Exception generalException)
            {
                Debug.WriteLine(generalException.StackTrace);
                _messagingService.ShowAsync(generalException.Message);
            }
        }

        #endregion

        #region Helper Functions

        private bool IsBalanceValueOnScreenSelected()
        {
            return IsLoanValueSelected &&
                   !IsRateValueSelected &&
                   !IsPeriodValueSelected;
        }

        private bool IsContributionValueOnScreenSelected()
        {
            return IsRateValueSelected &&
                   !IsLoanValueSelected &&
                   !IsPeriodValueSelected;
        }

        private bool IsInterestValueOnScreenSelected()
        {
            return IsPeriodValueSelected &&
                   !IsLoanValueSelected &&
                   !IsRateValueSelected;
        }

        private void DisableAllInputFields()
        {
            IsLoanValueSelected = false;
            IsRateValueSelected = false;
            IsPeriodValueSelected = false;
        }

        private void GenerateAndDisplayAnswer()
        {
            string totalPayValueFormula =
                $"{LoanValue} * " +
                $"((100 + {RateValue}) / 100)";

            TotalPayAnswer = CalculateInputs(totalPayValueFormula);

            string interestValueFormula =
                $"{TotalPayAnswer} - {LoanValue}";

            string monthlyPayValueFormula =
                $"{TotalPayAnswer} / {PeriodValue}";

            InterestPayAnswer = CalculateInputs(interestValueFormula);
            MonthlyPayAnswer = CalculateInputs(monthlyPayValueFormula);

            IsAnswerVisible = true;
            IsCalculatorInputVisible = false;

            DisableAllInputFields();

            GenerateAnswerText = "Input Another";
        }

        private void ResetPageInputs()
        {
            IsAnswerVisible = false;
            IsLoanValueSelected = true;
            IsCalculatorInputVisible = true;

            LoanValue = "0";
            RateValue = "0";
            PeriodValue = "0";

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
