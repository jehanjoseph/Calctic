using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Text;

namespace Calctic.Helpers
{
    public static class CalcticHelpers
    {
        public static bool PageIsAlreadyAtTopOfStack(Type typeOfAppearing)
        {
            IReadOnlyList<Page> stack = Shell.Current.Navigation.NavigationStack;

            return (stack.Count != 0 && stack.Last()?.GetType() == typeOfAppearing);
        }

        public static bool PopupPageIsAlreadyAtTopOfStack(Type typeOfAppearing)
        {
            IReadOnlyList<PopupPage> stack = PopupNavigation.Instance.PopupStack;

            return (stack.Count != 0 && stack.Last().GetType() == typeOfAppearing);
        }

        public static string ConcatenateInputs(this string targetString, string input, bool hasNumberSeparator)
        {
            targetString = targetString.ConcatenateInputs(input);

            if (hasNumberSeparator)
            {
                int lastOperatorIndex = targetString.GetIndexOfLastMathOperation();

                var previousInputs = (lastOperatorIndex == -1) ? "" :
                    targetString.Substring(0, lastOperatorIndex);

                var lastInputValues = targetString.GetLastInputNumbers();

                //If input string contains decimal point
                if (lastInputValues.Contains('.'))
                {
                    string[] splitStrings = lastInputValues.Split('.');

                    lastInputValues = $"{splitStrings[0].AddNumberSeparator()}.{splitStrings[1]}";
                }
                else
                {
                    lastInputValues = lastInputValues.AddNumberSeparator();
                }

                targetString = $"{previousInputs}{targetString.GetLastInputMathOperation()}{lastInputValues}";
            }

            return targetString;
        }

        public static string EraseInputs(this string targetString, int lengthToRemove, bool hasNumberSeparator)
        {
            targetString = targetString.EraseInputs(lengthToRemove);

            //If The last character of the string contains a numerical negative sign
            //Erase the numerical sign
            if (targetString.LastOrDefault() == '-')
            {
                targetString = targetString.EraseInputs(lengthToRemove);
            }

            if (hasNumberSeparator)
            {
                int lastOperatorIndex = targetString.GetIndexOfLastMathOperation();

                var previousInputs = (lastOperatorIndex == -1) ? "" :
                    targetString.Substring(0, lastOperatorIndex);

                var lastInputValues = targetString.GetLastInputNumbers();

                //If input string contains decimal point
                //Reconstruct the string so it would not format the decimal places
                if (lastInputValues.Contains('.'))
                {
                    string[] splitStrings = lastInputValues.Split('.');

                    lastInputValues = $"{splitStrings[0].AddNumberSeparator()}.{splitStrings[1]}";
                }
                else
                {
                    if (lastInputValues != "")
                    {
                        lastInputValues = lastInputValues.RemoveNumberSeparator();

                        lastInputValues = lastInputValues.AddNumberSeparator();
                    }
                }

                targetString = $"{previousInputs}{targetString.GetLastInputMathOperation()}{lastInputValues}";
            }

            return targetString;
        }

        public static string ConcatenateInputs(this string targetString, string input)
        {
            if (targetString == "0")
            {
                if (input == ".")
                {
                    targetString += input;
                }
                else
                {
                    targetString = input;
                }
            }
            else
            {
                targetString += input;
            }

            return targetString;
        }

        public static string EraseInputs(this string targetString, int lengthToRemove)
        {
            return (targetString.Length == 1) ? "0" : targetString.Remove(targetString.Length - lengthToRemove);
        }

        private static string AddNumberSeparator(this string targetString)
        {
            double input = Convert.ToDouble(targetString);

            targetString = string.Format("{0:n0}", input);

            return targetString;
        }

        private static string RemoveNumberSeparator(this string targetString)
        {
            double input = Convert.ToDouble(targetString);

            targetString = string.Format("{0}", input);

            return targetString;
        }
    }
}
