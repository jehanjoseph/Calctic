using System;
namespace Calctic.Interfaces
{
    public interface IBasicCalculatorService
    {
        public string Inputs { get; set; }

        IBasicCalculator GetCalculator();
    }
}
