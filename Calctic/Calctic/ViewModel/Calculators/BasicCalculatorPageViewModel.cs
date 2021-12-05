using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;

using Calctic.Interfaces;
using Calctic.Model.BasicCalculator;
using Calctic.Helpers;
using Calctic.View.Calculators;

namespace Calctic.ViewModel.Calculators
{
    public class BasicCalculatorPageViewModel : ViewModelBase
    {
        private IBasicCalculatorService _basicCalculatorService;
        private IMessagingService _messagingService;
        private IHistoryLogService _historyLogService;

        private string _screenInput;
        private string _internalInput;
        private double _result;

        #region ICommand

        public ICommand InputCommand { get; }
        public ICommand InputBasicMathOperationCommand { get; }
        public ICommand ClearAllInputCommand { get; }
        public ICommand EraseInputCommand { get; }
        public ICommand PeriodCommand { get; }
        public ICommand PercentCommand { get; }
        public ICommand NumericalSignCommand { get; }
        public ICommand ExecuteMathCommand { get; }
        public ICommand LongPressEraseInputCommand { get; }
        public ICommand HistoryLogsCommand { get; }

        #endregion

        #region Properties

        public ObservableCollection<Result> Results { get; set; } = new ObservableCollection<Result>();

        public double Result
        {
            get => _result;
            set
            {
                SetProperty(ref _result, value);

                var addedResult = new Result(value, ScreenInput);

                Results.Add(addedResult);
                _historyLogService?.AddHistoryLogs(addedResult);
            }
        }

        public string ScreenInput
        {
            get => _screenInput;
            set
            {
                SetProperty(ref _screenInput, value);
                InternalInput = ScreenInput.ReplaceBasicOperationInput();
            }
        }

        public string InternalInput
        {
            get => _internalInput;
            set
            {
                SetProperty(ref _internalInput, value);
            }
        }

        private Result _selectedResult;

        public Result SelectedResult
        {
            get => _selectedResult;
            set
            {
                SetProperty(ref _selectedResult, value);
                DisplayEquationToInput();
            }
        }   

        #endregion

        public BasicCalculatorPageViewModel(IBasicCalculatorService basicCalculatorService,
                                            IMessagingService messagingService,
                                            IHistoryLogService historyLogService)
        {
            _basicCalculatorService = basicCalculatorService;
            _messagingService = messagingService;
            _historyLogService = historyLogService;

            Title = "Basic Calculator";
            ScreenInput = "0";

            InputCommand = new Command<Button>((input) => ExecuteInputCommand(input.Text));
            InputBasicMathOperationCommand = new Command<Button>((input) => ConcatenateBasicMathOperations(input.Text));
            ClearAllInputCommand = new Command(ExecuteClearAllInputCommand);
            EraseInputCommand = new Command(() => EraseInputs(1));
            LongPressEraseInputCommand = new Command(ExecuteClearAllInputCommand);
            PeriodCommand = new Command(ExecutePeriodCommand);
            NumericalSignCommand = new Command(ExecuteNumericalSignCommand);
            PercentCommand = new Command(ExecutePercentCommand);
            ExecuteMathCommand = new Command(ExecuteMathOperation);
            HistoryLogsCommand = new Command(ExecuteHistoryLogsCommand);
        }

        #region Command Functions

        private void ExecuteClearAllInputCommand()
        {
            ClearAllInputs();
            Results.Clear();
        }

        private void ExecuteInputCommand(string input)
        {
            ScreenInput = ScreenInput.ConcatenateInputs(input, true);
        }

        private void ConcatenateBasicMathOperations(string input)
        {
            string lastInput = ScreenInput.GetLastInputNumbers();

            if (!string.IsNullOrEmpty(lastInput))
            {
                ScreenInput += input;
            }
        }

        private void ExecutePeriodCommand()
        {
            if (ScreenInput.IsLastNumberGoodForDecimalPoint())
            {
                ScreenInput = ScreenInput.ConcatenateInputs(".");
            }
        }

        private void ExecuteNumericalSignCommand()
        {
            string lastNumber = ScreenInput.GetLastInputNumbers();

            if (string.IsNullOrEmpty(lastNumber))
            {
                return;
            }

            if (!lastNumber.IsBasicOperator() && lastNumber != "0")
            {
                EraseInputs(lastNumber.Length);

                if (lastNumber.IsInputNumberNegative())
                {
                    lastNumber = lastNumber.Remove(lastNumber.IndexOf('-'), 1);
                }
                else
                {
                    lastNumber = lastNumber.Insert(0, "-");
                }

                ExecuteInputCommand(lastNumber);
            }
        }

        private void ExecutePercentCommand()
        {
            const double PercentFactor = 100.0;

            string lastInputNumber = ScreenInput.GetLastInputNumbers();
            string lastInputMathOperation = ScreenInput.GetLastInputMathOperation();
            int lastMathOperationIndex = ScreenInput.GetIndexOfLastMathOperation();

            var targetValue = 0.0;

            if (string.IsNullOrEmpty(lastInputNumber) ||
                string.IsNullOrEmpty(lastInputMathOperation) ||
                lastInputNumber == ".")
            {
                return;
            }

            EraseInputs(lastInputNumber.Length);

            if (lastInputMathOperation.Equals("x") || lastInputMathOperation.Equals("/"))
            {
                targetValue = Convert.ToDouble(lastInputNumber) / PercentFactor;
            }
            else
            {
                try
                {
                    string previousInputs = ScreenInput.Substring(0, lastMathOperationIndex);
                    double previousInputsResult = CalculateInputs(previousInputs.ReplaceBasicOperationInput());

                    targetValue = (Convert.ToDouble(lastInputNumber) / PercentFactor) * previousInputsResult;
                }
                catch (DivideByZeroException divideByZeroException)
                {
                    ScreenInput = divideByZeroException.Message;
                }
            }

            if (ScreenInput == "0")
            {
                if (targetValue.ToString() != "0")
                {
                    ScreenInput = targetValue.ToString();
                }
            }
            else
            {
                ExecuteInputCommand(targetValue.ToString());
            }
        }

        private void ExecuteMathOperation()
        {
            try
            {
                string calculatorInput = (InternalInput.GetLastInputNumbers() == "") ?
                    InternalInput.EraseInputs(1) : InternalInput;

                Result = CalculateInputs(calculatorInput);
            }
            catch (DivideByZeroException divideByZeroException)
            {
                Results.Add(new Result { ScreenInputs = ScreenInput,
                                         Message = divideByZeroException.Message });
            }
            catch (InvalidOperationException invalidOperationException)
            {
                Debug.WriteLine(invalidOperationException.StackTrace);
                _messagingService.ShowAsync(invalidOperationException.Message);
            }
            finally
            {
                ClearAllInputs();
            }
        }

        private async void ExecuteHistoryLogsCommand()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(BasicCalculatorHistoryLogsPage)))
            {
                await Shell.Current.GoToAsync(nameof(BasicCalculatorHistoryLogsPage), true);
            }
        }

        #endregion

        #region Helper Functions

        private void ClearAllInputs()
        {
            ScreenInput = "0";
        }

        private void EraseInputs(int lengthToRemove)
        {
            ScreenInput = ScreenInput.EraseInputs(lengthToRemove, true);
        }

        private void DisplayEquationToInput()
        {
            if (SelectedResult == null)
            {
                return;
            }

            ScreenInput = SelectedResult.ScreenInputs;
        }

        private double CalculateInputs(string input)
        {
            _basicCalculatorService.Inputs = input;
            return _basicCalculatorService.GetCalculator().Execute();
        }

        #endregion
    }
}