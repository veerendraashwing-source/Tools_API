using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Dar.Dar_Masters
{
    public class StateNamesDTO
    {
        public int stateid { get; set; }
        public string statename { get; set; }
    }

    public class VendorDTO
    {
        public string vchvendorid { get; set; }
        public string vendorname { set; get; }
        public string vendortype { get; set; }
        public string area { get; set; }
        public string city { get; set; }
        public string mobile_no { get; set; }
        public int statusid { get; set; }
        public int createdby { get; set; }
        public int stateid { get; set; }
        public string statename { get; set; }
        public int modifiedby {get;set;}
    }
}
