using System;
using Calctic.Helpers;

namespace Calctic.Model.UnitConverter
{
    public struct Time
    {
        public double Value { get; set; }
        public TimeUnit TimeUnitSetting { get; set; }

        public Time(double inputValue, TimeUnit timeUnit)
        {
            Value = inputValue;
            TimeUnitSetting = timeUnit;
        }

        public Time ToUnit(TimeUnit timeUnit)
        {
            switch (timeUnit)
            {
                case TimeUnit.Hour:
                {
                    if (TimeUnitSetting == TimeUnit.Minute)
                    {
                       return new Time(Value.ConvertMinutesToHours(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Second)
                    {
                       return new Time(Value.ConvertSecondsToHours(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Millisecond)
                    {
                       return new Time(Value.ConvertMillisecondsToHours(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Microsecond)
                    {
                        return new Time(Value.ConvertMicrosecondsToHours(), timeUnit);
                    }
                    else
                    {
                       return new Time(Value, timeUnit);
                    }
                }

                case TimeUnit.Minute:
                {
                    if (TimeUnitSetting == TimeUnit.Hour)
                    {
                        return new Time(Value.ConvertHoursToMinutes(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Second)
                    {
                        return new Time(Value.ConvertSecondsToMinutes(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Millisecond)
                    {
                        return new Time(Value.ConvertMillisecondsToMinutes(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Microsecond)
                    {
                        return new Time(Value.ConvertMicrosecondsToMinutes(), timeUnit);
                    }
                    else
                    {
                    return new Time(Value, timeUnit);
                    }
                }

                case TimeUnit.Second:
                {
                    if (TimeUnitSetting == TimeUnit.Hour)
                    {
                        return new Time(Value.ConvertHoursToSeconds(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Minute)
                    {
                        return new Time(Value.ConvertMinutesToSeconds(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Millisecond)
                    {
                        return new Time(Value.ConvertMillisecondsToSeconds(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Microsecond)
                    {
                        return new Time(Value.ConvertMicrosecondsToSeconds(), timeUnit);
                    }
                    else
                    {
                    return new Time(Value, timeUnit);
                    }
                }

                case TimeUnit.Millisecond:
                {
                    if (TimeUnitSetting == TimeUnit.Hour)
                    {
                        return new Time(Value.ConvertHoursToMilliseconds(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Minute)
                    {
                        return new Time(Value.ConvertMinutesToMilliseconds(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Second)
                    {
                        return new Time(Value.ConvertSecondsToMilliseconds(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Microsecond)
                    {
                        return new Time(Value.ConvertMicrosecondsToMilliseconds(), timeUnit);
                    }
                    else
                    {
                    return new Time(Value, timeUnit);
                    }
                }

                case TimeUnit.Microsecond:
                {
                    if (TimeUnitSetting == TimeUnit.Hour)
                    {
                        return new Time(Value.ConvertHoursToMicroseconds(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Minute)
                    {
                        return new Time(Value.ConvertMinutesToMicroseconds(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Second)
                    {
                        return new Time(Value.ConvertSecondsToMicroseconds(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Millisecond)
                    {
                        return new Time(Value.ConvertMillisecondsToMicroseconds(), timeUnit);
                    }
                    else
                    {
                        return new Time(Value, timeUnit);
                    }
                }

                default:
                {
                    if (TimeUnitSetting == TimeUnit.Minute)
                    {
                        return new Time(Value.ConvertMinutesToHours(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Second)
                    {
                        return new Time(Value.ConvertSecondsToHours(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Millisecond)
                    {
                        return new Time(Value.ConvertMillisecondsToHours(), timeUnit);
                    }
                    else if (TimeUnitSetting == TimeUnit.Microsecond)
                    {
                        return new Time(Value.ConvertMicrosecondsToHours(), timeUnit);
                    }
                    else
                    {
                        return new Time(Value, timeUnit);
                    }
                }
            }
        }
    }
}
