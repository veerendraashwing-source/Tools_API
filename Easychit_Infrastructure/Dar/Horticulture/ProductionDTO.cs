using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Dar.Horticulture
{
    public class AreaValvesDTO
    {
        public string vchsubvalve { get; set; }
        public string vchlines { get; set; }
    }

    public class ProductionDTO
    {
        public string vchproductionno { get; set; }
        public string datproductiondate { get; set; }
        public int rowscount { get; set; }
        public string[] vchfruittype { get; set; }
        public string[] numquantity { get; set; }
        public string[] numdamagequantity { get; set; }
        public string[] numnetquantity { get; set; }
        public string[] vchuom { get; set; }
        public string[] vchgarden { get; set; }
        public string vchremarks { get; set; }
        public int statusid { get; set; }
        public int createdby { get; set; }
        public string createddate { get; set; }
        public string[] vchlines { get; set; }
    }
}
