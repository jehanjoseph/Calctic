using System;

using Calctic.Model.UnitConverter;
using Calctic.Resources.JsonFiles.Converters;

namespace Calctic.ViewModel.Converters
{
    public class SpeedConverterPageViewModel : BaseConverterPageViewModel
    {
        public SpeedConverterPageViewModel()
        {
            Title = "Speed";

            SelectedUnitConverter = UnitSelection.Speed;

            InputUnit1 = new UnitName
            {
                Symbol = "m/s",
                Name = "Meter per second"
            };

            InputUnit2 = new UnitName
            {
                Symbol = "km/h",
                Name = "Kilometer per hour"
            };
        }
    }
}
