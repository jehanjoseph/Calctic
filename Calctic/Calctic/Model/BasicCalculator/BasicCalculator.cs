using System;
using System.Data;
using System.Text;
using Calctic.Helpers;
using Calctic.Interfaces;
using Dangl.Calculator;

namespace Calctic.Model.BasicCalculator
{
    public class BasicCalculator : IBasicCalculator
    {
        public string Inputs { get; set; }

        public double Execute()
        {
            string rightHandNumber = Inputs.GetRightHandInputNumbers('/');

            if (rightHandNumber.Trim() == "0")
            {
                throw new DivideByZeroException();
            }

            var output = Calculator.Calculate(Inputs);

            if (!output.IsValid)
            {
                throw new InvalidOperationException("Invalid operation");
            }

            return output.Result;
        }
    }
}