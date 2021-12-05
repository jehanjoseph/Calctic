using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers.Commands;

using Calctic.Helpers;
using Calctic.View.Converters;

namespace Calctic.ViewModel
{
    public class ConverterMenuScreenPageViewModel : ViewModelBase
    {
        public ICommand LengthConverterCommand { get; }
        public ICommand VolumeConverterCommand { get; }
        public ICommand WeightConverterCommand { get; }
        public ICommand TimeConverterCommand { get; }
        public ICommand TemperatureConverterCommand { get; }
        public ICommand SpeedConverterCommand { get; }
        public ICommand CurrencyConverterCommand { get; }

        public ICommand GoBackCommand { get; }

        public ConverterMenuScreenPageViewModel()
        {
            Title = "Converters";

            LengthConverterCommand = new AsyncCommand(OpenLengthConverterScreen);
            VolumeConverterCommand = new AsyncCommand(OpenVolumeConverterScreen);
            WeightConverterCommand = new AsyncCommand(OpenWeightConverterScreen);
            TimeConverterCommand = new AsyncCommand(OpenTimeConverterScreen);
            TemperatureConverterCommand = new AsyncCommand(OpenTemperatureConverterScreen);
            SpeedConverterCommand = new AsyncCommand(OpenSpeedConverterScreen);
            CurrencyConverterCommand = new AsyncCommand(OpenCurrencyConverterScreen);
        }

        private async Task OpenLengthConverterScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(LengthConverterPage)))
            {
                await Shell.Current.GoToAsync(nameof(LengthConverterPage), true);
            }
        }

        private async Task OpenVolumeConverterScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(VolumeConverterPage)))
            {
                await Shell.Current.GoToAsync(nameof(VolumeConverterPage), true);
            }
        }

        private async Task OpenWeightConverterScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(WeightConverterPage)))
            {
                await Shell.Current.GoToAsync(nameof(WeightConverterPage), true);
            }
        }

        private async Task OpenTimeConverterScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(TimeConverterPage)))
            {
                await Shell.Current.GoToAsync(nameof(TimeConverterPage), true);
            }
        }

        private async Task OpenTemperatureConverterScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(TemperatureConverterPage)))
            {
                await Shell.Current.GoToAsync(nameof(TemperatureConverterPage), true);
            }
        }

        private async Task OpenSpeedConverterScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(SpeedConverterPage)))
            {
                await Shell.Current.GoToAsync(nameof(SpeedConverterPage), true);
            }
        }

        private async Task OpenCurrencyConverterScreen()
        {
            if (!CalcticHelpers.PageIsAlreadyAtTopOfStack(typeof(CurrencyConverterPage)))
            {
                await Shell.Current.GoToAsync(nameof(CurrencyConverterPage), true);
            }
        }
    }
}
