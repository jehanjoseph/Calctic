using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calctic.Extensions.CustomControls
{
    public class CalculatorInputKeypadView : ContentView
    {
        public static readonly BindableProperty InputCommandProperty =
                BindableProperty.Create(nameof(InputCommand), typeof(ICommand), typeof(CalculatorInputKeypadView), null);
        public static readonly BindableProperty ClearAllInputCommandProperty =
            BindableProperty.Create(nameof(ClearAllInputCommand), typeof(ICommand), typeof(CalculatorInputKeypadView), null);
        public static readonly BindableProperty EraseInputCommandProperty =
            BindableProperty.Create(nameof(EraseInputCommand), typeof(ICommand), typeof(CalculatorInputKeypadView), null);
        public static readonly BindableProperty PeriodCommandProperty =
            BindableProperty.Create(nameof(PeriodCommand), typeof(ICommand), typeof(CalculatorInputKeypadView), null);
        public static readonly BindableProperty NumericalSignCommandProperty =
                BindableProperty.Create(nameof(NumericalSignCommand), typeof(ICommand), typeof(CalculatorInputKeypadView), null);
        public static readonly BindableProperty KeyboardBackgroundColorProperty =
            BindableProperty.Create(nameof(KeyboardBackgroundColor), typeof(Color), typeof(CalculatorInputKeypadView), null);
        public static readonly BindableProperty IsNumericalSignButtonVisibleProperty =
                BindableProperty.Create(nameof(IsNumericalSignButtonVisible), typeof(bool), typeof(CalculatorInputKeypadView), null);

        public ICommand InputCommand
        {
            get => (ICommand)GetValue(InputCommandProperty);
            set => SetValue(InputCommandProperty, value);
        }

        public ICommand ClearAllInputCommand
        {
            get => (ICommand)GetValue(ClearAllInputCommandProperty);
            set => SetValue(ClearAllInputCommandProperty, value);
        }

        public ICommand EraseInputCommand
        {
            get => (ICommand)GetValue(EraseInputCommandProperty);
            set => SetValue(EraseInputCommandProperty, value);
        }

        public ICommand PeriodCommand
        {
            get => (ICommand)GetValue(PeriodCommandProperty);
            set => SetValue(PeriodCommandProperty, value);
        }

        public ICommand NumericalSignCommand
        {
            get => (ICommand)GetValue(NumericalSignCommandProperty);
            set => SetValue(NumericalSignCommandProperty, value);
        }

        public Color KeyboardBackgroundColor
        {
            get => (Color)GetValue(KeyboardBackgroundColorProperty);
            set => SetValue(KeyboardBackgroundColorProperty, value);
        }

        public bool IsNumericalSignButtonVisible
        {
            get => (bool)GetValue(IsNumericalSignButtonVisibleProperty);
            set => SetValue(IsNumericalSignButtonVisibleProperty, value);
        }
    }
}
