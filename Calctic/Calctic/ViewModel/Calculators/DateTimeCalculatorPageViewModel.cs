using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Calctic.Helpers;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Calctic.ViewModel
{
    public class DateTimeCalculatorPageViewModel : ViewModelBase
    {
        private DateTime _firstInputDate;
        private DateTime _secondInputDate;
        private TimeSpan _firstInputTime;
        private TimeSpan _secondInputTime;

        private DateTime _selectedFirstDate;

        private int _hoursAnswer;
        private int _minutesAnswer;
        private int _daysAnswer;
        private bool _isResultVisible;

        private bool _isAllFieldsEnabled;

        private string _answerButtonText;

        #region ICommands

        public ICommand CalculateAnswerCommand { get; }

        #endregion

        #region Properties

        public DateTime FirstInputDate
        {
            get => _secondInputDate;
            set
            {
                SetProperty(ref _secondInputDate, value);
            }
        }

        public DateTime SecondInputDate
        {
            get => _firstInputDate;
            set
            {
                SetProperty(ref _firstInputDate, value);
            }
        }

        public DateTime SelectedFirstDate
        {
            get => _selectedFirstDate;
            set
            {
                SetProperty(ref _selectedFirstDate, value);
            }
        }

        public TimeSpan FirstInputTime
        {
            get => _firstInputTime;
            set
            {
                SetProperty(ref _firstInputTime, value);
            }
        }

        public TimeSpan SecondInpuTime
        {
            get => _secondInputTime;
            set
            {
                SetProperty(ref _secondInputTime, value);
            }
        }

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

        public int DaysAnswer
        {
            get => _daysAnswer;
            set
            {
                SetProperty(ref _daysAnswer, value);
            }
        }

        public int HoursAnswer
        {
            get => _hoursAnswer;
            set
            {
                SetProperty(ref _hoursAnswer, value);
            }
        }

        public int MinutesAnswer
        {
            get => _minutesAnswer;
            set
            {
                SetProperty(ref _minutesAnswer, value);
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

        public DateTimeCalculatorPageViewModel()
        {
            Title = "Date & Time Calculator";

            DaysAnswer = 0;
            HoursAnswer = 0;
            MinutesAnswer = 0;

            IsAllFieldsEnabled = !IsResultVisible;

            ResetPageInputs();

            CalculateAnswerCommand = new Command(CalculateAnswer);
        }

        private void CalculateAnswer()
        {
            const int DaysToHoursConversionValue = 24;
            const int HoursToMinutesConversionValue = 60;

            if (!IsResultVisible)
            {
                TimeSpan timeDifferenceInDays = FirstInputDate.Subtract(SecondInputDate);
                TimeSpan timeDifference = FirstInputTime.Subtract(SecondInpuTime);

                int hoursTotal = timeDifference.Hours + (timeDifferenceInDays.Days *
                                                         DaysToHoursConversionValue);

                int minutesTotal = timeDifference.Minutes + (hoursTotal * HoursToMinutesConversionValue);

                DaysAnswer = Math.Abs(timeDifferenceInDays.Days);
                HoursAnswer = Math.Abs(hoursTotal);
                MinutesAnswer = Math.Abs(minutesTotal);

                AnswerButtonText = "Input Another";

                IsResultVisible = true;
            }
            else
            {
                DaysAnswer = 0;

                IsResultVisible = false;

                ResetPageInputs();
            }
        }

        private void ResetPageInputs()
        {
            FirstInputDate = DateTime.Today;
            SecondInputDate = DateTime.Today;

            SelectedFirstDate = DateTime.Today;

            AnswerButtonText = "Calculate";
        }
    }
}