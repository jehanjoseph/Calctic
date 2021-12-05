using System;

using Calctic.Model.UnitConverter;
using Calctic.Resources.JsonFiles.Converters;

namespace Calctic.ViewModel.Converters
{
    public class TimeConverterPageViewModel : BaseConverterPageViewModel
    {
        public TimeConverterPageViewModel()
        {
            Title = "Time";

            SelectedUnitConverter = UnitSelection.Time;

            InputUnit1 = new UnitName
            {
                Symbol = "h",
                Name = "Hour"
            };

            InputUnit2 = new UnitName
            {
                Symbol = "m",
                Name = "Minute"
            };
        }
    }
}
