using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Dar.Dar_Masters
{
    public class TanksDTO
    {
        public string vchtankname { get; set; }
        public int statusid { get; set; }
        public int createdby { get; set; }
        public string vchtankid { get; set; }
    }

    public class MainValveDTO
    {
        public string vchtankid { get; set; }
        public string vchmainvalve { get; set; }
        public int statusid { get; set; }
    }

    public class SubValveDTO
    {
        public string vchtankid { get; set; }
        public string vchmainvalve { get; set; }
        public string vchsubvalve { get; set; }
        public int statusid { get; set; }
    }
}
