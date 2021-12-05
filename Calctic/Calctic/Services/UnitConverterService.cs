using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Calctic.Resources.JsonFiles.Converters;
using Calctic.Model.UnitConverter;

namespace Calctic.Services
{
    public class UnitConverterService
    {
        public UnitSelection SelectedUnit { get; set; }

        // Get the namespace for the embedded JSON data files
        private readonly string ResourcePath = typeof(UnitSelection).Namespace;

        // Get the assembly for the embedded JSON data files
        private Assembly ThisAssembly = typeof(UnitSelection).GetTypeInfo().Assembly;

        /// <summary>
        /// Returns a list of Units with their corresponding names and symbols
        /// </summary>
        /// <returns>ObservableCollection<<UnitName>></returns>
        public ObservableCollection<UnitName> GetSupportedUnitsOfMeasurement()
        {
            var json = "";

            Stream stream = ThisAssembly.GetManifestResourceStream($"{ResourcePath}.{SelectedUnit}-Names.json");

            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<ObservableCollection<UnitName>>(json);
        }

        /// <summary>
        /// Returns a list of Units with their corresponding names and symbols
        /// </summary>
        /// <returns>ObservableCollection<<UnitName>></returns>
        public ObservableCollection<UnitName> GetSupportedUnitsOfMeasurement(UnitSelection inputUnit)
        {
            var json = "";

            Stream stream = ThisAssembly.GetManifestResourceStream($"{ResourcePath}.{inputUnit}-Names.json");

            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<ObservableCollection<UnitName>>(json);
        }
    }
}
