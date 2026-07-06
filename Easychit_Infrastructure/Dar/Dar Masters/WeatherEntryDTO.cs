using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Dar.Dar_Masters
{
    public class WeatherEntryDTO
    {
        public object WeatherDate { get; set; }
        public object WeatherTime { get; set; }
        public object WeatherEntryDate { get; set; }
        public object TemparatureMin { get; set; }
        public object TemparatureMax { get; set; }
        public object Rain { get; set; }
        public object RainInMM { get; set; }
        public object Humidity { get; set; }

        public object statusid { get; set; }
        public object createdby { get; set; }
    }
}
