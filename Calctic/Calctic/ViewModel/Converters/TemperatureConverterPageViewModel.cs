using System;
using Calctic.Model.UnitConverter;
using Calctic.Resources.JsonFiles.Converters;

namespace Calctic.ViewModel.Converters
{
    public class TemperatureConverterPageViewModel : BaseConverterPageViewModel
    {
        public TemperatureConverterPageViewModel()
        {
            Title = "Temperature";

            IsNumericalSignVisible = true;

            SelectedUnitConverter = UnitSelection.Temperature;

            InputUnit1 = new UnitName
            {
                Symbol = "°C",
                Name = "Celsius"
            };

            InputUnit2 = new UnitName
            {
                Symbol = "°F",
                Name = "Fahrenheit"
            };
        }
    }
}
