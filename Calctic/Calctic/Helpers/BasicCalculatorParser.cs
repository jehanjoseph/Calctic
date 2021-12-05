using System;
using System.Linq;
using System.Text;

namespace Calctic.Helpers
{
    public static class BasicCalculatorParser
    {
        public static string ReplaceBasicOperationInput(this string input)
        {
            input = input.Replace("ｘ", "*");
            input = input.Replace("÷", "/"); //Division to Slash Symbol
            input = input.Replace("＋", "+"); //Plus to Small-Hyphen Plus
            input = input.Replace("－", "-"); //Minus to Small-Hyphen Minus
            input = input.Replace(",", ""); //Remove comma

            return input;
        }

        /// <summary>
        /// Gets the number on the right hand side of a basic operator
        /// </summary>
        /// <param name="basicOperator"></param>
        /// <returns name="string"></returns>
        public static string GetRightHandInputNumbers(this string sourceString, char basicOperator)
        {
            char[] mathOperators = new char[] { '+', '-', '*', '÷', '(', ')'};

            if (!mathOperators.Contains(basicOperator))
            {
                return "";
            }

            int operatorIndex = sourceString.GetIndexOfMathOperation(basicOperator);

            if (operatorIndex == -1)
            {
                return "";
            }

            string rightHandEquationString = sourceString.Substring(operatorIndex + 1);

            int secondOperatorIndex = rightHandEquationString.IndexOfAny(mathOperators);

            int rightHandSecondOperatorDifference = rightHandEquationString.Length - (rightHandEquationString.Length - secondOperatorIndex);

            return (secondOperatorIndex != -1) ? rightHandEquationString.Substring(0, rightHandSecondOperatorDifference)
                                                : rightHandEquationString;
        }

        /// <summary>
        /// Gets the number on the right hand side of a basic operator
        /// </summary>
        /// <returns name="string"></returns>
        public static string GetLastInputNumbers(this string input)
        {
            int operatorIndex = input.GetIndexOfLastMathOperation();

            return (operatorIndex != -1) ? input.Substring(operatorIndex + 1) : input;
        }

        public static string GetLastInputMathOperation(this string input)
        {
            int operatorIndex = input.GetIndexOfLastMathOperation();

            return (operatorIndex != -1) ? input.Substring(operatorIndex, 1) : "";
        }

        public static int GetIndexOfMathOperation(this string input, char basicOperator)
        {
            return input.IndexOf(basicOperator);
        }

        public static int GetIndexOfLastMathOperation(this string input)
        {
            char[] mathOperators = new char[] { '＋', '－', 'ｘ', '÷', '+', '-', '*', '/'};

            char[] reverseInputCharacters = input.Reverse().ToArray();

            var reverseString = new string(reverseInputCharacters);
            int reversedIndex = reverseString.IndexOfAny(mathOperators);

            return (reversedIndex != -1) ? (reverseString.Length - 1) - reversedIndex : -1;
        }

        public static bool IsInputNumberNegative(this string input)
        {
            return (input.FirstOrDefault() == '-');
        }

        public static bool IsLastNumberGoodForDecimalPoint(this string input)
        {
            string lastNumber = input.GetLastInputNumbers();

            return (!string.IsNullOrEmpty(lastNumber) && (lastNumber.IndexOf('.') == -1));
        }

        public static bool IsBasicOperator(this string input)
        {
            return (input == "ｘ" || input == "÷" || input == "＋" || input == "－");
        }
    }
}