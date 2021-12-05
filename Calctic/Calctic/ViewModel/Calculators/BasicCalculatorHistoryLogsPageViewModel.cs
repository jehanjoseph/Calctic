using System;
using System.Windows.Input;
using Calctic.Helpers;
using Calctic.Interfaces;
using Calctic.Model.BasicCalculator;
using Calctic.View.Calculators.Popups;
using Calctic.ViewModel.Calculators.Popups;
using MvvmHelpers;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Calctic.ViewModel.Calculators
{
    public class BasicCalculatorHistoryLogsPageViewModel : ViewModelBase
    {
        private IHistoryLogService _historyLogService;
        private ObservableRangeCollection<Result> _historyLogItems;

        public ObservableRangeCollection<Result> HistoryLogItems
        {
            get => _historyLogItems;
            set
            {
                SetProperty(ref _historyLogItems, value);
            }
        }

        public ICommand DeleteHistoryCommand { get; }
        public ICommand DeletePopupHistoryCommand { get; }

        public BasicCalculatorHistoryLogsPageViewModel(IHistoryLogService historyLogService)
        {
            _historyLogService = historyLogService;

            Title = "Basic Calculator";
            Subtitle = "History";

            HistoryLogItems = _historyLogService?.HistoryLogs;

            DeleteHistoryCommand = new Command(ExecuteDeleteHistoryCommand);
            DeletePopupHistoryCommand = new Command(ExecuteDeleteHistoryPopupCommand);
        }

        private void ExecuteDeleteHistoryPopupCommand()
        {
            if (!CalcticHelpers.PopupPageIsAlreadyAtTopOfStack(typeof(PopupDeleteHistoryPage)))
            {
                var popup = new PopupDeleteHistoryPage();
                var context = popup.BindingContext as PopupDeleteHistoryPageViewModel;

                context.DeleteHistoryCommand = DeleteHistoryCommand;

                popup.BindingContext = context;

                PopupNavigation.Instance.PushAsync(popup);
            }
        }

        private void ExecuteDeleteHistoryCommand()
        {
            _historyLogService?.ClearHistoryLogs();
        }
    }
}
