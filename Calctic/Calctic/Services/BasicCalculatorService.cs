using Calctic.Interfaces;

namespace Calctic.Services
{
    public class BasicCalculatorService : IBasicCalculatorService
    {
        private IBasicCalculator _basicCalculator;

        public string Inputs 
        { 
            get => _basicCalculator.Inputs; 
            set => _basicCalculator.Inputs = value; 
        }

        public BasicCalculatorService(IBasicCalculator basicCalculator)
        {
            _basicCalculator = basicCalculator;
        }

        public IBasicCalculator GetCalculator()
        {
            return _basicCalculator;
        }
    }
}