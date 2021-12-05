using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calctic.Extensions.CustomControls
{
    public class CalculatorInputFieldEntry : ContentView
    {
        public static readonly BindableProperty IsSelectedProperty =
                BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(CalculatorInputFieldEntry), false);
        public static readonly BindableProperty IsSelectedCommandProperty =
                BindableProperty.Create(nameof(IsSelectedCommand), typeof(ICommand), typeof(CalculatorInputFieldEntry), null);
        public static readonly BindableProperty ScreenInputValueProperty =
                BindableProperty.Create(nameof(ScreenInputValue), typeof(string), typeof(CalculatorInputFieldEntry), null);
        public static readonly BindableProperty InputMaxLengthProperty =
                BindableProperty.Create(nameof(InputMaxLength), typeof(int), typeof(CalculatorInputFieldEntry), null);
        public static readonly BindableProperty FontSizeProperty =
                BindableProperty.Create(nameof(FontSize), typeof(int), typeof(CalculatorInputFieldEntry), null);

        public int FontSize
        {
            get => (int)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public int InputMaxLength
        {
            get => (int)GetValue(InputMaxLengthProperty);
            set => SetValue(InputMaxLengthProperty, value);
        }

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public ICommand IsSelectedCommand
        {
            get => (ICommand)GetValue(IsSelectedCommandProperty);
            set => SetValue(IsSelectedCommandProperty, value);
        }

        public string ScreenInputValue
        {
            get => (string)GetValue(ScreenInputValueProperty);
            set => SetValue(ScreenInputValueProperty, value);
        }
    }
}
