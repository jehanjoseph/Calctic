using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calctic.Extensions
{
    public static class VisualElementAnimationExtension
    {
        public static async Task FadeOut(this VisualElement element, uint duration = 400, Easing easing = null)
        {
            await element.FadeTo(0, duration, easing);
            element.IsVisible = false;
        }

        public static async Task FadeIn(this VisualElement element, uint duration = 400, Easing easing = null)
        {
            await element.FadeTo(1, duration, easing);
            element.IsVisible = true;
        }
    }
}
