using System;
using Xamarin.Essentials;

namespace Calctic.Helpers
{
    public class ScreenDisplay
    {
        const int smallWidthResolution = 768;
        const int smallHeightResolution = 1280;

        public static bool IsSmallDevice()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            return (width <= smallWidthResolution && height <= smallHeightResolution);
        }
    }
}
