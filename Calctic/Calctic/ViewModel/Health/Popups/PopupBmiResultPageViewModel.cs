using System;
using System.Windows.Input;
using Calctic.Model.Health.BMI;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Calctic.ViewModel.Health.Popups
{
    public class PopupBmiResultPageViewModel : ViewModelBase
    {
        private BmiResult _inputBmiResult;

        public ICommand InputAnotherCommand { get; }
        public ICommand ResetBmiPageInputCommand { get; set; }

        public BmiResult InputBmiResult
        {
            get => _inputBmiResult;
            set
            {
                SetProperty(ref _inputBmiResult, value);
            }
        }

        public PopupBmiResultPageViewModel()
        {
            InputAnotherCommand = new Command(ExecuteInputAnother);
        }

        private void ExecuteInputAnother()
        {
            ResetBmiPageInputCommand?.Execute(null);
            PopupNavigation.Instance.PopAsync();
        }
    }
}
