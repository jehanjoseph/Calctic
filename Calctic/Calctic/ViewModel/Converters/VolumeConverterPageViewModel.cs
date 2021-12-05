using System;
using Calctic.Model.UnitConverter;
using Calctic.Resources.JsonFiles.Converters;

namespace Calctic.ViewModel.Converters
{
    public class VolumeConverterPageViewModel : BaseConverterPageViewModel
    {
        public VolumeConverterPageViewModel()
        {
            Title = "Volume";

            SelectedUnitConverter = UnitSelection.Volume;

            InputUnit1 = new UnitName
            {
                Symbol = "m³",
                Name = "Cubic Meter"
            };

            InputUnit2 = new UnitName
            {
                Symbol = "cm³",
                Name = "Cubic Centimeter"
            };
        }
    }
}
