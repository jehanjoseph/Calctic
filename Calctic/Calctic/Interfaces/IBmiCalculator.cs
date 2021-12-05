using System;
using Calctic.Model.Health.BMI;

namespace Calctic.Interfaces
{
    public interface IBmiCalculator
    {
        Gender Gender { get; set; }
        double Weight { get; set; }
        double Height { get; set; }

        BmiResult CalculateBmiResult();
        string GenerateRemarks();
    }
}
