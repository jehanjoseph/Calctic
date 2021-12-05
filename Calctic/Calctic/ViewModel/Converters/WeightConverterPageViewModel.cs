using System;
using Calctic.Model.UnitConverter;
using Calctic.Resources.JsonFiles.Converters;

namespace Calctic.ViewModel.Converters
{
    public class WeightConverterPageViewModel : BaseConverterPageViewModel
    {
        public WeightConverterPageViewModel()
        {
            Title = "Weight";

            SelectedUnitConverter = UnitSelection.Weight;

            InputUnit1 = new UnitName
            {
                Symbol = "g",
                Name = "Gram"
            };

            InputUnit2 = new UnitName
            {
                Symbol = "kg",
                Name = "Kilogram"
            };
        }
    }
}
