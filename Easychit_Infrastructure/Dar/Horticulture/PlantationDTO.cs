using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Dar.Horticulture
{
    public class FruitsDTO
    {
        public int vchfruitsid { get; set; }
        public string vchtypeoffruits { get; set; }
        public string vchuom { get; set; }
        public string gst_percentage { get; set; }
    }

    public class PlantationDTO
    {
        public string vchtankid { get; set; }
        public string vchmainvalve { get; set; }
        public string vchsubvalve { get; set; }
        public string vchlines { get; set; }
        public int numplantsfrom { get; set; }
        public int numplantsto { get; set; }
        public string vchfruittype { get; set; }
        public DateTime datplantaiondate { get; set; }
        public int statusid { get; set; }
        public int createdby { get; set; }
        public int numageofplant { get; set; }

    }
}
