using System;
using System.Windows.Input;
using Calctic.Helpers;
using Calctic.Model.UnitConverter;
using Calctic.Resources.JsonFiles.Converters;
using Calctic.Services;
using Calctic.View.Converters.Popups;
using Calctic.ViewModel.Converters.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Calctic.ViewModel.Calculators.Popups
{
    public class PopupDeleteHistoryPageViewModel : ViewModelBase
    {
        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteHistoryCommand { get; set; }

        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                SetProperty(ref _message, value);
            }
        }

        public PopupDeleteHistoryPageViewModel()
        {
            Title = "Delete History Logs";

            Message = "Are you sure you want to clear all history logs?";

            ConfirmCommand = new Command(ExecuteConfirm);
            CancelCommand = new Command(ExecuteCancel);
        }

        private void ExecuteConfirm()
        {
            DeleteHistoryCommand?.Execute(null);
            PopupNavigation.Instance.PopAsync();
        }

        private void ExecuteCancel()
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
