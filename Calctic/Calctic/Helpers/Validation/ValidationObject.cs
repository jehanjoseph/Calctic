using System;
using System.Collections.Generic;
using System.Linq;
using Calctic.Interfaces;

namespace Calctic.Helpers.Validation
{
    public class ValidatableObject<T> : BaseValidatable, IValidatable<T>
    {
        public List<IValidationRule<T>> Validations { get; } = new();

        public List<string> Errors { get; set; } = new();

        public bool CleanOnChange { get; set; } = true;

        public bool IsValid { get; set; } = true;

        public string ErrorMessage { get; private set; }

        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;

                if (CleanOnChange)
                {
                    IsValid = true;
                }
            }
        }

        public virtual bool Validate()
        {
            Errors?.Clear();

            IEnumerable<string> errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            if (!IsValid)
            {
                ErrorMessage = Errors.FirstOrDefault();
            }

            return IsValid;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
