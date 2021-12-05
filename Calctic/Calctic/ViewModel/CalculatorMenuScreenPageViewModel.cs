using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers.Commands;

using Calctic.Helpers;
using Calctic.View;
using Calctic.View.Calculators;

namespace Calctic.ViewModel
{
    public class CalculatorMenuScreenPageViewModel : ViewModelBase
    {
        public ICommand BasicCalculatorClickedCommand { get; }
        public ICommand DateTimeCalculatorClickedCommand { get; }
        public ICommand GoBackCommand { get; }

        public CalculatorMenuScreenPageViewModel()
        {
            Title = "Basic Calculator";

            BasicCalculatorClickedCommand = new AsyncCommand(OpenBasicCalculatorScreen);
            DateTimeCalculatorClickedCommand = new AsyncCommand(OpenDateTimeCalculatorScreen);
        }

        #region Command Functions

        private async Task OpenBasicCalculatorScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(BasicCalculatorPage)))
            {
                await Shell.Current.GoToAsync(nameof(BasicCalculatorPage), true);
            }
        }

        private async Task OpenDateTimeCalculatorScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(DateTimeCalculatorPage)))
            {
                await Shell.Current.GoToAsync(nameof(DateTimeCalculatorPage), true);
            }
        }

        #endregion
    }
}
