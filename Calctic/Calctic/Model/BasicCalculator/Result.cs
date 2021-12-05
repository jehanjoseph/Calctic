using System.Drawing;

namespace Calctic.Model.BasicCalculator
{
    public class Result
    {
        private double _value;
        private string _message;
        private string _internalInputs;
        private string _screenInputs;
        private bool _isSelected;
        private Color _backgroundColor = Color.FloralWhite;

        #region Properties

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        public string InternalInputs
        {
            get { return _internalInputs; }
            set { _internalInputs = value; }
        }

        public string ScreenInputs
        {
            get { return _screenInputs; }
            set
            {
                _screenInputs = value;
            }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                Message = $"= {_value}";
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                BackgroundColor = value ? Color.AntiqueWhite : Color.FloralWhite;

                _isSelected = value;
            }
        }

        #endregion

        public Result() { }
        public Result(double initial) { Value = initial; }
        public Result(double initial, string equation)
        {
            Value = initial;
            ScreenInputs = equation;
        }
    }
}