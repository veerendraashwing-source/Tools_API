using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Dar.Dar_Masters
{
    public class ServiceNosDTO
    {
        public string serviceno { get; set; }
        public string uscno { get; set; }
        public string companyname { get; set; }
        public string location { get; set; }
    }

    public class ElectricityBillDTO
    {
        public string eleComapnyName { get; set; }
        public string eleDate { get; set; }
        public string USCNo { get; set; }
        public string SCNo { get; set; }
        public string eleMonth { get; set; }
        public string eleLocation { get; set; }
        public string FromUnits { get; set; }
        public string Tounits { get; set; }
        public string NoOfUnits { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
    }
}
