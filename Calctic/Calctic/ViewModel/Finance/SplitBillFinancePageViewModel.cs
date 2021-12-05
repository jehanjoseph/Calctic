using System;
using System.Diagnostics;
using System.Windows.Input;
using Calctic.Helpers;
using Calctic.Interfaces;
using Xamarin.Forms;

namespace Calctic.ViewModel.Finance
{
    public class SplitBillFinancePageViewModel : ViewModelBase
    {
        private IBasicCalculatorService _basicCalculatorService;
        private IMessagingService _messagingService;

        private bool _isBillValueSelected;
        private bool _isNumberOfPeopleValueSelected;
        private bool _isAnswerVisible;

        private string _screenBillValue;
        private string _screenNumberOfPeopleValue;
        private string _screenInputAnswer;

        private string _generateAnswerText;

        #region ICommands

        public ICommand InputCommand { get; }
        public ICommand ClearAllInputCommand { get; }
        public ICommand EraseInputCommand { get; }
        public ICommand PeriodCommand { get; }
        public ICommand BillSelectedCommand { get; }
        public ICommand NumberOfPeopleSelectedCommand { get; }
        public ICommand GenerateAnswerCommand { get; }

        #endregion

        #region Properties

        public string ScreenBillValue
        {
            get => _screenBillValue;
            set
            {
                SetProperty(ref _screenBillValue, value);
            }
        }

        public string ScreenNumberOfPeopleValue
        {
            get => _screenNumberOfPeopleValue;
            set
            {
                SetProperty(ref _screenNumberOfPeopleValue, value);
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

        public bool IsBillValueSelected
        {
            get => _isBillValueSelected;
            set
            {
                SetProperty(ref _isBillValueSelected, value);

                if (_isBillValueSelected)
                {
                    IsNumberOfPeopleValueSelected = false;
                }
            }
        }

        public bool IsNumberOfPeopleValueSelected
        {
            get => _isNumberOfPeopleValueSelected;
            set
            {
                SetProperty(ref _isNumberOfPeopleValueSelected, value);

                if (_isNumberOfPeopleValueSelected)
                {
                    IsBillValueSelected = false;
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

        #endregion

        public SplitBillFinancePageViewModel(IBasicCalculatorService basicCalculatorService, IMessagingService messagingService)
        {
            _basicCalculatorService = basicCalculatorService;
            _messagingService = messagingService;

            Title = "Split Bill";

            InputCommand = new Command<Button>((input) => ConcatenateInputs(input.Text), (input) => !IsAnswerVisible);
            ClearAllInputCommand = new Command(ClearAllInputs, () => !IsAnswerVisible);
            EraseInputCommand = new Command(() => EraseInputs(1), () => !IsAnswerVisible);
            PeriodCommand = new Command(AddPeriodInput, () => !IsAnswerVisible);
            BillSelectedCommand = new Command(ExecuteBillSelectedCommand);
            NumberOfPeopleSelectedCommand = new Command(ExecuteNumberOfPeopleSelectedCommand);
            GenerateAnswerCommand = new Command(GenerateAnswer);

            ResetPageInputs();
        }

        #region Command Functions

        private void ConcatenateInputs(string input)
        {
            if (IsScreenBillSelected())
            {
                ScreenBillValue = ScreenBillValue.ConcatenateInputs(input);
            }

            if (IsScreenNumberOfPeopleSelected())
            {
                ScreenNumberOfPeopleValue = ScreenNumberOfPeopleValue.ConcatenateInputs(input);
            }
        }

        private void ClearAllInputs()
        {
            ScreenBillValue = "0";
            ScreenNumberOfPeopleValue = "0";
        }

        private void EraseInputs(int lengthToRemove)
        {
            if (IsScreenBillSelected())
            {
                ScreenBillValue = ScreenBillValue.EraseInputs(lengthToRemove);
            }

            if (IsScreenNumberOfPeopleSelected())
            {
                ScreenNumberOfPeopleValue = ScreenNumberOfPeopleValue.EraseInputs(lengthToRemove);
            }
        }

        private void AddPeriodInput()
        {
            if (IsScreenBillSelected())
            {
                if (ScreenBillValue.IsLastNumberGoodForDecimalPoint())
                {
                    ScreenBillValue = ScreenBillValue.ConcatenateInputs(".");
                }
            }
        }

        private void ExecuteBillSelectedCommand()
        {
            IsBillValueSelected = GenerateAnswerText != "Input Another";
        }

        private void ExecuteNumberOfPeopleSelectedCommand()
        {
            IsNumberOfPeopleValueSelected = GenerateAnswerText != "Input Another";
        }

        private void GenerateAnswer()
        {
            try
            {
                if (GenerateAnswerText == "Calculate")
                {
                    string inputValues = $"{ScreenBillValue} / {ScreenNumberOfPeopleValue}";

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

        private bool IsScreenBillSelected()
        {
            return IsBillValueSelected && !IsNumberOfPeopleValueSelected;
        }

        private bool IsScreenNumberOfPeopleSelected()
        {
            return IsNumberOfPeopleValueSelected && !IsBillValueSelected;
        }

        private void DisableAllInputFields()
        {
            IsBillValueSelected = false;
            IsNumberOfPeopleValueSelected = false;
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
            ScreenBillValue = "0";
            ScreenNumberOfPeopleValue = "0";
            ScreenInputAnswer = "0";

            IsAnswerVisible = false;
            IsBillValueSelected = true;

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
