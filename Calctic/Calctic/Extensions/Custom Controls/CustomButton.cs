using System;
using Xamarin.Forms;

namespace Calctic.Extensions.CustomControls
{
    public class CustomButton : Button
    {
        public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create("IsSelected",
                                typeof(bool),
                                typeof(CustomButton),
                                false);

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
    }
}
