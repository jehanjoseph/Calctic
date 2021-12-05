using System;
using Calctic.Interfaces;

namespace Calctic.Model.Health.BMI
{
    public class BmiCalculator : IBmiCalculator
    {
        private const int ConversionFactor = 10000;
        private const double MinimumFemaleNormalWeight = 19.11;
        private const double MaximumFemaleNormalWeight = 25.8;
        private const double MinimumMaleNormalWeight = 20.71;
        private const double MaximumMaleNormalWeight = 26.4;

        private double _result;

        public Gender Gender { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }

        public BmiCalculator()
        {
        }

        public BmiCalculator(double inputHeight, double inputWeight, Gender inputGender = Gender.Male)
        {
            Height = inputHeight;
            Weight = inputWeight;
            Gender = inputGender;
        }

        public BmiResult CalculateBmiResult()
        {

            BmiResult output = new();

            output.Gender = Gender;

            _result = (Weight / Math.Pow(Height, 2)) * ConversionFactor;

            output.Result = _result;
            output.Remark = GenerateRemarks();

            return output;
        }

        public string GenerateRemarks() => (Gender, _result) switch
        {
            (Gender.Female, < MinimumFemaleNormalWeight) => "Underweight",
            (Gender.Female, >= MinimumFemaleNormalWeight and <= MaximumFemaleNormalWeight) => "Normal",
            (Gender.Female, > MaximumFemaleNormalWeight) => "Overweight",
            (Gender.Male, < MinimumMaleNormalWeight) => "Underweight",
            (Gender.Male, >= MinimumMaleNormalWeight and <= MaximumMaleNormalWeight) => "Normal",
            (Gender.Male, > MaximumMaleNormalWeight) => "Overweight",
            _ => "Unknown",
        };
    }
}
