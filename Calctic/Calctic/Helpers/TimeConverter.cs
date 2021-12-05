using System;
namespace Calctic.Helpers
{
    public static class TimeConverter
    {
        private const double HoursToDayConversionFactor = 24;
        private const double SecondsToMinuteConversionFactor = 60;
        private const double MinutesToHourConversionFactor = 60;
        private const double SecondToMillisecondsConversionFactor = 1000;
        private const double MilliToMicrosecondConversionFactor = 1000;

        #region To days

        public static double ConvertMicrosecondsToDays(this double microseconds)
        {
            return microseconds / MilliToMicrosecondConversionFactor /
                   SecondToMillisecondsConversionFactor /
                   SecondsToMinuteConversionFactor /
                   MinutesToHourConversionFactor /
                   HoursToDayConversionFactor;
        }

        public static double ConvertMillisecondsToDays(this double milliseconds)
        {
            return milliseconds / SecondToMillisecondsConversionFactor / 
                   SecondsToMinuteConversionFactor /
                   MinutesToHourConversionFactor /
                   HoursToDayConversionFactor;
        }

        public static double ConvertSecondsToDays(this double seconds)
        {
            return seconds / SecondsToMinuteConversionFactor /
                   MinutesToHourConversionFactor /
                   HoursToDayConversionFactor;
        }

        public static double ConvertMinutesToDays(this double minutes)
        {
            return minutes / MinutesToHourConversionFactor /
                   HoursToDayConversionFactor;
        }

        public static double ConvertHoursToDays(this double hours)
        {
            return hours / HoursToDayConversionFactor;
        }

        #endregion

        #region To hours

        public static double ConvertMicrosecondsToHours(this double microseconds)
        {
            return microseconds / MilliToMicrosecondConversionFactor /
                   SecondToMillisecondsConversionFactor /
                   SecondsToMinuteConversionFactor /
                   MinutesToHourConversionFactor;
        }

        public static double ConvertMillisecondsToHours(this double milliseconds)
        {
            return milliseconds / SecondToMillisecondsConversionFactor /
                   SecondsToMinuteConversionFactor / 
                   MinutesToHourConversionFactor;
        }

        public static double ConvertSecondsToHours(this double seconds)
        {
            return seconds / SecondsToMinuteConversionFactor /
                   MinutesToHourConversionFactor;
        }

        public static double ConvertMinutesToHours(this double minutes)
        {
            return minutes / MinutesToHourConversionFactor;
        }

        public static double ConvertDaysToHours(this double days)
        {
            return days * HoursToDayConversionFactor;
        }

        #endregion

        #region To minutes

        public static double ConvertMicrosecondsToMinutes(this double microseconds)
        {
            return microseconds / MilliToMicrosecondConversionFactor /
                   SecondToMillisecondsConversionFactor /
                   SecondsToMinuteConversionFactor;
        }

        public static double ConvertMillisecondsToMinutes(this double milliseconds)
        {
            return milliseconds / SecondToMillisecondsConversionFactor /
                   SecondsToMinuteConversionFactor;
        }

        public static double ConvertSecondsToMinutes(this double seconds)
        {
            return seconds / SecondsToMinuteConversionFactor;
        }

        public static double ConvertHoursToMinutes(this double hours)
        {
            return hours * MinutesToHourConversionFactor;
        }

        public static double ConvertDaysToMinutes(this double days)
        {
            return days * HoursToDayConversionFactor *
                   MinutesToHourConversionFactor;
        }

        #endregion

        #region To seconds

        public static double ConvertMicrosecondsToSeconds(this double microseconds)
        {
            return microseconds / MilliToMicrosecondConversionFactor /
                   SecondsToMinuteConversionFactor;
        }

        public static double ConvertMillisecondsToSeconds(this double milliseconds)
        {
            return milliseconds / SecondToMillisecondsConversionFactor;
        }

        public static double ConvertMinutesToSeconds(this double minutes)
        {
            return minutes * SecondsToMinuteConversionFactor;
        }

        public static double ConvertHoursToSeconds(this double hours)
        {
            return hours * MinutesToHourConversionFactor *
                   SecondsToMinuteConversionFactor;
        }

        public static double ConvertDaysToSeconds(this double days)
        {
            return days * HoursToDayConversionFactor *
                   MinutesToHourConversionFactor *
                   SecondsToMinuteConversionFactor;
        }

        #endregion

        #region To milliseconds

        public static double ConvertMicrosecondsToMilliseconds(this double milliseconds)
        {
            return milliseconds / MilliToMicrosecondConversionFactor;
        }

        public static double ConvertSecondsToMilliseconds(this double seconds)
        {
            return seconds * SecondToMillisecondsConversionFactor;
        }

        public static double ConvertMinutesToMilliseconds(this double minutes)
        {
            return minutes * SecondsToMinuteConversionFactor *
                   SecondsToMinuteConversionFactor;
        }

        public static double ConvertHoursToMilliseconds(this double hours)
        {
            return hours * MinutesToHourConversionFactor *
                   SecondsToMinuteConversionFactor *
                   SecondToMillisecondsConversionFactor;
        }

        public static double ConvertDaysToMilliseconds(this double days)
        {
            return days * HoursToDayConversionFactor *
                   MinutesToHourConversionFactor *
                   SecondsToMinuteConversionFactor *
                   SecondToMillisecondsConversionFactor;
        }

        #endregion

        #region To microseconds

        public static double ConvertMillisecondsToMicroseconds(this double milliseconds)
        {
            return milliseconds * MilliToMicrosecondConversionFactor;
        }

        public static double ConvertSecondsToMicroseconds(this double seconds)
        {
            return seconds * SecondToMillisecondsConversionFactor *
                   MilliToMicrosecondConversionFactor;
        }

        public static double ConvertMinutesToMicroseconds(this double minutes)
        {
            return minutes * MilliToMicrosecondConversionFactor *
                   SecondToMillisecondsConversionFactor *
                   MilliToMicrosecondConversionFactor;
        }

        public static double ConvertHoursToMicroseconds(this double hours)
        {
            return hours * MinutesToHourConversionFactor *
                   SecondsToMinuteConversionFactor *
                   SecondToMillisecondsConversionFactor *
                   MilliToMicrosecondConversionFactor;
        }

        public static double ConvertDaysToMicroseconds(this double days)
        {
            return days * HoursToDayConversionFactor *
                   MinutesToHourConversionFactor *
                   SecondsToMinuteConversionFactor *
                   SecondToMillisecondsConversionFactor *
                   MilliToMicrosecondConversionFactor;
        }

        #endregion
    }
}
