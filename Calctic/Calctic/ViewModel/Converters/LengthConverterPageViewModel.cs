using System;

using Calctic.Model.UnitConverter;
using Calctic.Resources.JsonFiles.Converters;

namespace Calctic.ViewModel.Converters
{
    public class LengthConverterPageViewModel : BaseConverterPageViewModel
    {
        public LengthConverterPageViewModel()
        {
            Title = "Length";

            SelectedUnitConverter = UnitSelection.Length;

            InputUnit1 = new UnitName
            {
                Symbol = "m",
                Name = "Meter"
            };

            InputUnit2 = new UnitName
            {
                Symbol = "cm",
                Name = "Centimeter"
            };
        }
    }
}
