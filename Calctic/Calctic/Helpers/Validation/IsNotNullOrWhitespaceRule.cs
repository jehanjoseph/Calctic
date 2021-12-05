using Calctic.Interfaces;

namespace Calctic.Helpers.Validation
{
    public class IsNotNullOrWhitespaceRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var input = value as string;

            if (value == null)
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(input);
        }
    }
}
