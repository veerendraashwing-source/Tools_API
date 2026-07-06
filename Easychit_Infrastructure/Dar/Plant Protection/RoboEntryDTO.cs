using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Dar.Plant_Protection
{
    public class MachineNamesDTO
    {
        public string roboname { get; set; }
        public string valve { get; set; }
    }

    public class ValveNamesDTO
    {
        public string valve { get; set; }
    }
    public class CropNamesDTO
    {
        public string crop { get; set; }
    }
    
    public class DivisionNamesDTO
    {
        public string vchmoduleid { get; set; }
        public string vchmodulename { get; set; }
    }

    public class RoboEntryDTO
    {
        public DateTime Date { get; set; }
        public string MachineName { get; set; }
        public string Division { get; set; }
        public string Valve { get; set; }
        public string Crop { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Remarks { get; set; }
    }
}
