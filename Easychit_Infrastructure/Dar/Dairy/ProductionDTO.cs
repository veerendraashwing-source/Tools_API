using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Dar.Dairy
{
    public class ProductionDTO
    {
        public string milk_id { get; set; }
        public string milk_name { get; set; }
        public string vchcattle { get; set; }
    }

    public class DamageReasonDTO
    {
        public String reason_description { get; set; }
    }

    public class SaveProductionDTO
    {
        public string vchproductionno { get; set; }
        public object datproductiondate { get; set; }
        public string vchcattle { get; set; }
        public string vchmilkedat { get; set; }
        public object numquanity { get; set; }
        public string vchremarks { get; set; }
        public int statusid { get; set; }
        public object createdby { get; set; }
        public object createddate { get; set; }
        public object modifiedby { get; set; }
        public object modifieddate { get; set; }
        public object numfeedtocalf { get; set; }
        public string vchtagnumber { get; set; }
        public string vchmilktype { get; set; }
        public object numdamagemilkquantity { get; set; }
        public object numnetmilkquantity { get; set; }
        public object Damage_Reason { get; set; }
    }
}
