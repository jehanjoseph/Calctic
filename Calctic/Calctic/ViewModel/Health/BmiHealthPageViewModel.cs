using System;
using System.Windows.Input;
using Calctic.Helpers;
using Calctic.Interfaces;
using Calctic.Model.Health.BMI;
using Calctic.View.Health.Popups;
using Calctic.ViewModel.Health.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Calctic.ViewModel.Health
{
    public class BmiHealthPageViewModel : ViewModelBase
    {
        IBmiCalculator _bmiCalculator;
        IMessagingService _messagingService;

        private Gender _inputGender;

        private string _screenInputHeight;
        private string _screenInputWeight;

        private bool _isMaleGenderSelected;
        private bool _isFemaleGenderSelected;

        private bool _isHeightInputInvalid;
        private bool _isWeightInputInvalid;

        private int _inputHeightMaxLength;
        private int _inputWeightMaxLength;

        private const int MaxCharacterLength = 3;

        #region Properties

        public string ScreenInputHeight
        {
            get => _screenInputHeight;
            set
            {
                SetProperty(ref _screenInputHeight, value);
                IsHeightInputInvalid = false;
            }
        }

        public string ScreenInputWeight
        {
            get => _screenInputWeight;
            set
            {
                SetProperty(ref _screenInputWeight, value);
                IsWeightInputInvalid = false;
            }
        }

        public int InputHeightMaxLength
        {
            get => _inputHeightMaxLength;
            set
            {
                SetProperty(ref _inputHeightMaxLength, value);
            }
        }

        public int InputWeightMaxLength
        {
            get => _inputWeightMaxLength;
            set
            {
                SetProperty(ref _inputWeightMaxLength, value);
            }
        }

        public bool IsMaleGenderSelected
        {
            get => _isMaleGenderSelected;
            set
            {
                SetProperty(ref _isMaleGenderSelected, value);

                if (_isMaleGenderSelected)
                {
                    IsFemaleGenderSelected = false;
                    _inputGender = Gender.Male;
                }
            }
        }

        public bool IsFemaleGenderSelected
        {
            get => _isFemaleGenderSelected;
            set
            {
                SetProperty(ref _isFemaleGenderSelected, value);

                if (_isFemaleGenderSelected)
                {
                    IsMaleGenderSelected = false;
                    _inputGender = Gender.Female;
                }
            }
        }

        public bool IsHeightInputInvalid
        {
            get => _isHeightInputInvalid;
            set
            {
                SetProperty(ref _isHeightInputInvalid, value);
            }
        }

        public bool IsWeightInputInvalid
        {
            get => _isWeightInputInvalid;
            set
            {
                SetProperty(ref _isWeightInputInvalid, value);
            }
        }

        #endregion

        #region ICommands

        public ICommand CalculateAnswerCommand { get; }
        public ICommand MaleGenderSelectedCommand { get; }
        public ICommand FemaleGenderSelectedCommand { get; }
        public ICommand ResetPageInputsCommand { get; }

        #endregion

        public BmiHealthPageViewModel(IBmiCalculator bmiCalculator, IMessagingService messagingService)
        {
            _bmiCalculator = bmiCalculator;
            _messagingService = messagingService;

            Title = "BMI";

            InputHeightMaxLength = MaxCharacterLength;
            InputWeightMaxLength = MaxCharacterLength;

            ResetPageInputs();

            CalculateAnswerCommand = new Command(CalculateAnswer);
            MaleGenderSelectedCommand = new Command(ExecuteMaleGenderSelectedCommand);
            FemaleGenderSelectedCommand = new Command(ExecuteFemaleGenderSelectedCommand);
            ResetPageInputsCommand = new Command(ExecuteResetPageInputsCommand);
        }

        #region Command Functions

        private void CalculateAnswer()
        {
            if (IsBmiInputsInvalid())
            {
                return;
            }

            _bmiCalculator.Gender = _inputGender;
            _bmiCalculator.Height = Convert.ToDouble(ScreenInputHeight);
            _bmiCalculator.Weight = Convert.ToDouble(ScreenInputWeight);

            BmiResult output = _bmiCalculator.CalculateBmiResult();

            if (!CalcticHelpers.PopupPageIsAlreadyAtTopOfStack(typeof(PopupBmiResultPage)))
            {
                var popup = new PopupBmiResultPage();
                var context = popup.BindingContext as PopupBmiResultPageViewModel;

                context.ResetBmiPageInputCommand = ResetPageInputsCommand;
                context.InputBmiResult = output;

                popup.BindingContext = context;

                PopupNavigation.Instance.PushAsync(popup);
            }
        }

        private void ExecuteMaleGenderSelectedCommand()
        {
            IsMaleGenderSelected = true;
        }

        private void ExecuteFemaleGenderSelectedCommand()
        {
            IsFemaleGenderSelected = true;
        }

        private void ExecuteResetPageInputsCommand()
        {
            ResetPageInputs();
        }

        private bool IsBmiInputsInvalid()
        {
            if (!IsMaleGenderSelected && !IsFemaleGenderSelected)
            {
                _messagingService.ShowAsync("Please select a gender");
                return true;
            }

            if (string.IsNullOrWhiteSpace(ScreenInputHeight) ||
                string.IsNullOrWhiteSpace(ScreenInputWeight))
            {
                IsHeightInputInvalid = string.IsNullOrWhiteSpace(ScreenInputHeight);
                IsWeightInputInvalid = string.IsNullOrWhiteSpace(ScreenInputWeight);
                return true;
            }

            return false;
        }

        private void ResetPageInputs()
        {
            ScreenInputHeight = "";
            ScreenInputWeight = "";

            IsMaleGenderSelected = false;
            IsFemaleGenderSelected = false;
        }

        #endregion
    }
}
