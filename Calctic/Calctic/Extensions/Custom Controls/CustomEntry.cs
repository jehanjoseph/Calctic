using System;
using Xamarin.Forms;

namespace Calctic.Extensions.CustomControls
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create("IsSelected",
                                typeof(bool),
                                typeof(CustomButton),
                                false);

        public CustomEntry()
        {
            // Set the events.
            base.TextChanged += OnTextChanged;
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;

            // If empty, set it to empty string.
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                entry.Text = string.Empty;
                return;
            }

            // If not numeric, check the length.
            if (e.NewTextValue.Length > MaxLength)
            {
                entry.Text = e.OldTextValue;
            }
        }
    }
}
