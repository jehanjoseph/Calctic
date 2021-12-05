using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers.Commands;

using Calctic.Helpers;
using Calctic.View;

namespace Calctic.ViewModel
{
    public class MainMenuScreenPageViewModel : ViewModelBase
    {
        public ICommand CalculatorClickedCommand { get; }
        public ICommand ConverterClickedCommand { get; }
        public ICommand FinanceClickedCommand { get; }
        public ICommand HealthClickedCommand { get; }

        public MainMenuScreenPageViewModel()
        {
            Title = "Calctic";

            CalculatorClickedCommand = new AsyncCommand(OpenCalculatorMenuScreen);
            ConverterClickedCommand = new AsyncCommand(OpenConverterMenuScreen);
            FinanceClickedCommand = new AsyncCommand(OpenFinanceMenuScreen);
            HealthClickedCommand = new AsyncCommand(OpenHealthMenuScreen);
        }

        #region Command Functions

        private async Task OpenCalculatorMenuScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(CalculatorMenuScreenPage)))
            {
                await Shell.Current.GoToAsync(nameof(CalculatorMenuScreenPage), true);
            }
        }

        private async Task OpenConverterMenuScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(ConverterMenuScreenPage)))
            {
                await Shell.Current.GoToAsync(nameof(ConverterMenuScreenPage), true);
            }
        }

        private async Task OpenFinanceMenuScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(FinanceMenuScreenPage)))
            {
                await Shell.Current.GoToAsync(nameof(FinanceMenuScreenPage), true);
            }
        }

        private async Task OpenHealthMenuScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(HealthMenuScreenPage)))
            {
                await Shell.Current.GoToAsync(nameof(HealthMenuScreenPage), true);
            }
        }

        #endregion
    }
}
