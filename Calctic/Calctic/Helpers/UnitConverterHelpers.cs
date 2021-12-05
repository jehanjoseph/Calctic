using System;
using Calctic.Model.UnitConverter;
using Calctic.Resources.JsonFiles.Converters;
using UnitsNet;
using UnitsNet.Units;

namespace Calctic.Helpers
{
    public static class UnitConverterHelpers
    {
        public static LengthUnit GetLengthUnit(string input = "") => input switch
        {
            "m" => LengthUnit.Meter,
            "km" => LengthUnit.Kilometer,
            "cm" => LengthUnit.Centimeter,
            "mm" => LengthUnit.Millimeter,
            "in" => LengthUnit.Inch,
            _ => LengthUnit.Meter
        };

        public static SpeedUnit GetSpeedUnit(string input = "") => input switch
        {
            "m/s" => SpeedUnit.MeterPerSecond,
            "km/h" => SpeedUnit.KilometerPerHour,
            "mph" => SpeedUnit.MilePerHour,
            "kn" => SpeedUnit.Knot,
            "in/s" => SpeedUnit.InchPerSecond,
            _ => SpeedUnit.MeterPerSecond
        };

        public static TemperatureUnit GetTemperatureUnit(string input = "") => input switch
        {
            "°C" => TemperatureUnit.DegreeCelsius,
            "°F" => TemperatureUnit.DegreeFahrenheit,
            "K" => TemperatureUnit.Kelvin,
            _ => TemperatureUnit.DegreeCelsius
        };

        public static TimeUnit GetTimeUnit(string input = "") => input switch
        {
            "h" => TimeUnit.Hour,
            "m" => TimeUnit.Minute,
            "s" => TimeUnit.Second,
            "ms" => TimeUnit.Millisecond,
            "μs" => TimeUnit.Microsecond,
            _ => TimeUnit.Hour
        };

        public static VolumeUnit GetVolumeUnit(string input = "") => input switch
        {
            "m³" => VolumeUnit.CubicMeter,
            "cm³" => VolumeUnit.CubicCentimeter,
            "dm³" => VolumeUnit.CubicDecimeter,
            "ft³" => VolumeUnit.CubicFoot,
            _ => VolumeUnit.CubicMeter
        };

        public static MassUnit GetWeightUnit(string input = "") => input switch
        {
            "g" => MassUnit.Gram,
            "kg" => MassUnit.Kilogram,
            "lb" => MassUnit.Pound,
            "oz" => MassUnit.Ounce,
            "t" => MassUnit.Tonne,
            _ => MassUnit.Gram
        };

        public static double GetConvertedValue(double value, UnitSelection selectedConverter, UnitName sourceUnit, UnitName targetUnit)
        {
            switch(selectedConverter)
            {
                case UnitSelection.Length:
                {
                    Length lengthInput = new(value, GetLengthUnit(sourceUnit.Symbol));
                    lengthInput = lengthInput.ToUnit(GetLengthUnit(targetUnit.Symbol));

                    return lengthInput.Value;
                }
                case UnitSelection.Speed:
                {
                    Speed speedUnit = new(value, GetSpeedUnit(sourceUnit.Symbol));
                    speedUnit = speedUnit.ToUnit(GetSpeedUnit(targetUnit.Symbol));

                    return speedUnit.Value;
                }
                case UnitSelection.Temperature:
                {
                    Temperature temperatureUnit = new(value, GetTemperatureUnit(sourceUnit.Symbol));
                    temperatureUnit = temperatureUnit.ToUnit(GetTemperatureUnit(targetUnit.Symbol));

                    return temperatureUnit.Value;
                }
                case UnitSelection.Time:
                {
                    Time timeUnit = new(value, GetTimeUnit(sourceUnit.Symbol));
                    timeUnit = timeUnit.ToUnit(GetTimeUnit(targetUnit.Symbol));

                    return timeUnit.Value;
                }
                case UnitSelection.Volume:
                {
                    Volume volumeUnit = new(value, GetVolumeUnit(sourceUnit.Symbol));
                    volumeUnit = volumeUnit.ToUnit(GetVolumeUnit(targetUnit.Symbol));

                    return volumeUnit.Value;
                }
                case UnitSelection.Weight:
                {
                    Mass weightUnit = new(value, GetWeightUnit(sourceUnit.Symbol));
                    weightUnit = weightUnit.ToUnit(GetWeightUnit(targetUnit.Symbol));

                    return weightUnit.Value;
                }
                default: return 0.0;
            }
        }
    }
}
