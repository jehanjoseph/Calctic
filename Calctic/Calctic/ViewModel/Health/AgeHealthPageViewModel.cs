using System;
using System.Diagnostics;
using System.Windows.Input;
using Calctic.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace Calctic.ViewModel.Health
{
    public class AgeHealthPageViewModel : ViewModelBase
    {
        private IMessagingService _messagingService;

        private DateTime _birthDate;
        private DateTime _targetDate;
        private bool _isResultVisible;

        private bool _isAllFieldsEnabled;

        private int _yearsAnswer;
        private int _monthsAnswer;
        private int _daysAnswer;

        private string _answerButtonText;

        #region ICommands

        public ICommand CalculateAnswerCommand { get; }

        #endregion

        #region Properties

        public bool IsAllFieldsEnabled
        {
            get => _isAllFieldsEnabled;
            set
            {
                SetProperty(ref _isAllFieldsEnabled, value);
            }
        }

        public bool IsResultVisible
        {
            get => _isResultVisible;
            set
            {
                SetProperty(ref _isResultVisible, value);

                IsAllFieldsEnabled = !_isResultVisible;
            }
        }

        public DateTime TargetDate
        {
            get => _targetDate;
            set
            {
                SetProperty(ref _targetDate, value);
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                SetProperty(ref _birthDate, value);
            }
        }

        public int YearsAnswer
        {
            get => _yearsAnswer;
            set
            {
                SetProperty(ref _yearsAnswer, value);
            }
        }

        public int MonthsAnswer
        {
            get => _monthsAnswer;
            set
            {
                SetProperty(ref _monthsAnswer, value);
            }
        }

        public int DaysAnswer
        {
            get => _daysAnswer;
            set
            {
                SetProperty(ref _daysAnswer, value);
            }
        }

        public string AnswerButtonText
        {
            get => _answerButtonText;
            set
            {
                SetProperty(ref _answerButtonText, value);
            }
        }

        #endregion

        public AgeHealthPageViewModel(IMessagingService messagingService)
        {
            _messagingService = messagingService;

            Title = "Age";

            ResetPageInputs();

            IsAllFieldsEnabled = !IsResultVisible;

            CalculateAnswerCommand = new Command(CalculateAnswer);
        }

        private void CalculateAnswer()
        {
            try
            {
                if (!IsResultVisible)
                {
                    GenerateAndDisplayAnswer();
                }
                else
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

        private void GenerateAndDisplayAnswer()
        {
            TimeSpan timeDifference = TargetDate.Subtract(BirthDate);
            int yearsDifference = TargetDate.Year - BirthDate.Year;
            int monthsDifference = MonthDifference(TargetDate, BirthDate);

            YearsAnswer = yearsDifference;
            MonthsAnswer = monthsDifference;
            DaysAnswer = timeDifference.Days;

            AnswerButtonText = "Input Another";

            IsResultVisible = true;
        }

        private void ResetPageInputs()
        {
            YearsAnswer = 0;
            MonthsAnswer = 0;
            DaysAnswer = 0;

            BirthDate = DateTime.Today;
            TargetDate = DateTime.Today;

            IsResultVisible = false;

            AnswerButtonText = "Calculate";
        }

        private int MonthDifference(DateTime firstValue, DateTime secondValue)
        {
            const int YearToMonthConversion = 12;

            return Math.Abs((firstValue.Month - secondValue.Month)
                + YearToMonthConversion * (firstValue.Year - secondValue.Year));
        }
    }
}
