using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Calctic.Helpers;
using Calctic.View.Health;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace Calctic.ViewModel
{
    public class HealthMenuScreenPageViewModel : ViewModelBase
    {
        public ICommand AgeClickedCommand { get; }
        public ICommand BmiClickedCommand { get; }

        public ICommand GoBackCommand { get; }

        public HealthMenuScreenPageViewModel()
        {
            Title = "Health";

            AgeClickedCommand = new AsyncCommand(OpenAgeHealthScreen);
            BmiClickedCommand = new AsyncCommand(OpenBmiHealthScreen);
        }

        private async Task OpenAgeHealthScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(AgeHealthPage)))
            {
                await Shell.Current.GoToAsync(nameof(AgeHealthPage), true);
            }
        }

        private async Task OpenBmiHealthScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(BmiHealthPage)))
            {
                await Shell.Current.GoToAsync(nameof(BmiHealthPage), true);
            }
        }
    }
}
