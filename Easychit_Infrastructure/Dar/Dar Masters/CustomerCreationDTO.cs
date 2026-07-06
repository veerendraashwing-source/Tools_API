using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Dar.Dar_Masters
{
    public class RouteNamesDTO
    {
        public string delivery_routename {get;set;}
    }

    public class CustomerDTO
    {
        public string vchcustomerid { get; set; }
        public string vchcustomername { get; set; }
        public string vchmobilenumber { get; set; }
        public string vcharea { get; set; }
        public string vchcity { get; set; }
        public string vchtype { get; set; }
        public int statusid { get; set; }
        public int createdby { get; set; }
        public string delivery_routename { get; set; }
        public string typeofoperation { get; set; }
        public int modifiedby { get; set; }
        public int stateid { get; set; }
        public string statename { get; set; }
    }
}
