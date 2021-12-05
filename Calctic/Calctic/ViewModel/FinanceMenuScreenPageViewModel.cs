using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Calctic.Helpers;
using Calctic.View.Finance;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace Calctic.ViewModel
{
    public class FinanceMenuScreenPageViewModel : ViewModelBase
    {
        public ICommand InterestClickedCommand { get; }
        public ICommand DiscountClickedCommand { get; }
        public ICommand PercentageClickedCommand { get; }
        public ICommand SplitBillClickedCommand { get; }
        public ICommand LoanClickedCommand { get; }

        public ICommand GoBackCommand { get; }

        public FinanceMenuScreenPageViewModel()
        {
            Title = "Finance";

            InterestClickedCommand = new AsyncCommand(OpenInterestFinanceScreen);
            DiscountClickedCommand = new AsyncCommand(OpenDiscountFinanceScreen);
            PercentageClickedCommand = new AsyncCommand(OpenPercentageFinanceScreen);
            SplitBillClickedCommand = new AsyncCommand(OpenSplitBillFinanceScreen);
            LoanClickedCommand = new AsyncCommand(OpenLoanFinanceScreen);
        }

        private async Task OpenInterestFinanceScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(InterestFinancePage)))
            {
                await Shell.Current.GoToAsync(nameof(InterestFinancePage), true);
            }
        }

        private async Task OpenDiscountFinanceScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(DiscountFinancePage)))
            {
                await Shell.Current.GoToAsync(nameof(DiscountFinancePage), true);
            }
        }

        private async Task OpenPercentageFinanceScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(PercentageFinancePage)))
            {
                await Shell.Current.GoToAsync(nameof(PercentageFinancePage), true);
            }
        }

        private async Task OpenSplitBillFinanceScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(SplitBillFinancePage)))
            {
                await Shell.Current.GoToAsync(nameof(SplitBillFinancePage), true);
            }
        }

        private async Task OpenLoanFinanceScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(LoanFinancePage)))
            {
                await Shell.Current.GoToAsync(nameof(LoanFinancePage), true);
            }
        }
    }
}
