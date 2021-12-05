using System;
using Calctic.Interfaces;

namespace Calctic.Helpers.Validation
{
    public class IsNotLessThanRule<T> : IValidationRule<T> where T : IComparable
    {
        private readonly T _compareValue;
        private readonly bool _required;

        public string ValidationMessage { get; set; }

        public IsNotLessThanRule(T compareValue, bool required = true)
        {
            _compareValue = compareValue;
            _required = required;
        }

        public bool Check(T value)
        {
            if (_required && value == null)
            {
                return false;
            }
            else if (!_required && value.Equals(GetMinValue()))
            {
                return true;
            }

            return value.CompareTo(_compareValue) > 0;
        }

        private T GetMinValue()
        {
            try
            {
                return (T)typeof(T).GetField("MinValue").GetValue(null);
            }
            catch
            {
                return (T)(object)int.MinValue;
            }
        }
    }
}
